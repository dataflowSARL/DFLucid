
using System;
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
using MKFLibrary;
using Toolbar = Android.Widget.Toolbar;

namespace lucid
{
    [Activity(Label = "AccountSummaryDetailsActivity", ParentActivity = typeof(AccountSummaryActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "AccountSummaryActivity")]
    public class AccountSummaryDetailsActivity : Activity
    {
        #region variables
        private Timer timer;
        private LinearLayout linearLayout;
        private ImageButton back_btn;
        private RecyclerView recyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewASDAdapter recyclerViewASDAdapter;
        private List<TRNS> items = new List<TRNS>();
        private ProgressBar progressBar;
        private SwipeRefreshLayout swipeRefreshLayout;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_summary_details_layout);
            // Create your application here
            setUpVariables();
        }

        private void setUpVariables()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.asd_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_btn = FindViewById<ImageButton>(Resource.Id.asd_back_btn);
            back_btn.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_btn.Click += Back_Btn_Click;
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            linearLayout = FindViewById<LinearLayout>(Resource.Id.account_summary_detail_linear_layout);
        }

        void Back_Btn_Click(object sender, EventArgs e)
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
