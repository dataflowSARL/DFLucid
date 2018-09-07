﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MarketFlow;
using MarketFlowLibrary;
using MKFLibrary;
using MKFLibrary.API;
using Toolbar = Android.Widget.Toolbar;

namespace lucid
{
    [Activity(Label = "AccountSummaryActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class AccountSummaryActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        private LinearLayout linearLayout;
        private Button filter_button;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewAdapterAccountSummary mRecyclerViewAdapter;
        private List<AccountSummary> accountSummaries = new List<AccountSummary>();
        private API_Response<AccountSummary> mResponse = new API_Response<AccountSummary>();
        private ProgressBar progressBar;
        private SwipeRefreshLayout swipeRefreshLayout;
        private int state = 0;
        private string show_all = "Show All Balance";
        private string show_non_zero = "Show Non-Zero";

        private Timer timer;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_summary_layout);

            // Create your application here
            setUpVariables();

        }

        private void setUpVariables() {
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_account_summary);
            progressBar.Visibility = ViewStates.Visible;
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview_as);
            mLayoutManager = new LinearLayoutManager(this);
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_to_refresh_account_summary);
            swipeRefreshLayout.SetColorSchemeResources(Resource.Color.blue,
                                              Resource.Color.purple,
                                              Resource.Color.red,
                                              Resource.Color.green);
            swipeRefreshLayout.Refresh += delegate {
                Task.Run(async () =>
                {
                    try
                    {
                        mResponse = await MKFApp.Current.GetAccountSummary();
                        this.RunOnUiThread(() => DisplayRefresher());
                    }
                    catch (Exception e)
                    {
                        this.RunOnUiThread(() => DismissRefresher());
                    }
                });
            };
            linearLayout = FindViewById<LinearLayout>(Resource.Id.as_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.as_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            filter_button = FindViewById<Button>(Resource.Id.filter_button);
            filter_button.Visibility = ViewStates.Invisible;
            filter_button.Click += Filter_Button_Click;
            filter_button.SetTextColor(MainActivity.TOOLBAR_COLOR);
            back_button = FindViewById<ImageButton>(Resource.Id.as_back_btn);
            back_button.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button.Click += Back_Button_Click;
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            Task.Run(async () =>
            {
                try {
                    mResponse = await MKFApp.Current.GetAccountSummary();
                    this.RunOnUiThread(() => Display());
                } catch(Exception e) {
                    this.RunOnUiThread(() => Dismiss());
                }
            });
        }

        void Filter_Button_Click(object sender, EventArgs e)
        {
            if(state == 0) {
                state = 1;
                filter_button.Text = show_non_zero;
                accountSummaries = mResponse.Content;
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mRecyclerViewAdapter = new RecyclerViewAdapterAccountSummary(accountSummaries, this, MainActivity.user);
                mRecyclerViewAdapter.ItemClick += MRecyclerViewAdapter_ItemClick;
                mRecyclerView.SetAdapter(mRecyclerViewAdapter);
            } else {
                state = 0;
                filter_button.Text = show_all;
                accountSummaries = mResponse.Content.Where(u => u.AmountSystem != 0).ToList<AccountSummary>();
                mRecyclerView.SetLayoutManager(mLayoutManager);
                mRecyclerViewAdapter = new RecyclerViewAdapterAccountSummary(accountSummaries, this, MainActivity.user);
                mRecyclerViewAdapter.ItemClick += MRecyclerViewAdapter_ItemClick;
                mRecyclerView.SetAdapter(mRecyclerViewAdapter);
            }
        }


        void MRecyclerViewAdapter_ItemClick(object sender, int e)
        {
            Intent details = new Intent(this, typeof(AccountSummaryDetailsActivity));
            details.PutExtra("account", accountSummaries[e].Account);
            StartActivity(details);
        }

        private void Display() {
            progressBar.Visibility = ViewStates.Gone;
            filter_button.Visibility = ViewStates.Visible;
            state = 0;
            filter_button.Text = show_all;
            if (mResponse.Success == true) {
                mRecyclerView.SetLayoutManager(mLayoutManager);
                accountSummaries = mResponse.Content.Where(u => u.AmountSystem != 0).ToList<AccountSummary>(); 
                mRecyclerViewAdapter = new RecyclerViewAdapterAccountSummary(accountSummaries, this, MainActivity.user);
                mRecyclerViewAdapter.ItemClick += MRecyclerViewAdapter_ItemClick;
                mRecyclerView.SetAdapter(mRecyclerViewAdapter);
            } else {
                Snackbar.Make(linearLayout, mResponse.Message ?? "An Error Occured", Snackbar.LengthLong).Show();
            }
        }

        private void DisplayRefresher()
        {
            swipeRefreshLayout.Refreshing = false;
            filter_button.Visibility = ViewStates.Visible;
            filter_button.Text = show_all;
            state = 0;
            if (mResponse.Success == true)
            {
                mRecyclerView.SetLayoutManager(mLayoutManager);
                accountSummaries = mResponse.Content.Where(u => u.AmountSystem != 0).ToList<AccountSummary>();
                mRecyclerViewAdapter = new RecyclerViewAdapterAccountSummary(accountSummaries, this, MainActivity.user);
                mRecyclerViewAdapter.ItemClick += MRecyclerViewAdapter_ItemClick;
                mRecyclerView.SetAdapter(mRecyclerViewAdapter);
            }
            else
            {
                Snackbar.Make(linearLayout, mResponse.Message ?? "An Error Occured", Snackbar.LengthLong).Show();
            }
        }

        private void Dismiss() {
            progressBar.Visibility = ViewStates.Gone;
            Snackbar.Make(linearLayout, mResponse.Message, Snackbar.LengthLong).Show();
        }

        private void DismissRefresher()
        {
            swipeRefreshLayout.Refreshing = false;
            Snackbar.Make(linearLayout, mResponse.Message, Snackbar.LengthLong).Show();
        }


        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            timer.Stop();
        }

        protected override void OnStart()
        {
            base.OnStart();
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        protected override void OnResume()
        {
            base.OnResume();
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            HomeActivity.COUNTDOWN--;
            if (HomeActivity.COUNTDOWN == 0)
            {
                timer.Stop();
                Task.Run(async () =>
                {
                    try
                    {
                        LoginResult loginResult = await MKFApp.Current.Logout();
                        this.RunOnUiThread(() => logoutSuccessful());
                    }
                    catch (Exception exception)
                    {
                        this.RunOnUiThread(() => logoutFailed());
                    }
                });
            }
        }

        public void logoutSuccessful()
        {
            timer.Stop();
            showAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
        }

        public void logoutFailed()
        {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }

        private void showAlertDialog(String title, String message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetPositiveButton("OK", (sender, e) =>
            {
                Intent logout = new Intent(this, typeof(MainActivity));
                StartActivity(logout);
            });
            builder.Create().Show();
        }

    }
}
