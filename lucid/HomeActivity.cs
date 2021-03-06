﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Support.Compat;
using MarketFlowLibrary;
//using MarketFlow;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using System.Timers;
//using Android.App;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using static Java.Text.Normalizer;
using Android.Support.V7.Widget;
//using MarketFlowLibrary.API;


namespace lucid
{
    [Activity(Theme = "@style/Theme.DesignDemo")]
    public class HomeActivity : AppCompatActivity
    {

        #region variables
        private NavigationView navigationView;
        private DrawerLayout drawerLayout;
        private LinearLayout linearLayout;
        private TextView username;
        //private GraphView graphView;

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewPSAdapter mRecyclerViewAdapter;
        private List<RiskSummary> items = new List<RiskSummary>();
        private ProgressBar progressBar;
        private SwipeRefreshLayout swipeRefreshLayout;

        private int COUNTDOWN = 5 * 60;
        private int INITIAL = 5 * 60;
        private int INTERVAL = 1000;
        public static string DIALOG_TITLE = "TIME OUT";
        public static string DIALOG_MESSAGE = "You've been logged out due to inactivity";
        private Timer timer;

        #endregion

        protected override void OnCreate(Bundle bundle)  
        {  
            base.OnCreate(bundle);
            // Set our view from the "home" layout resource  
            SetContentView(Resource.Layout.Home);
            SetUpVariables();
        }

        // Set up the Activity's Views
        public void SetUpVariables() {
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_portfolio_summary);
            progressBar.Visibility = ViewStates.Visible;
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview_ps);
            mLayoutManager = new LinearLayoutManager(this);
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_to_refresh_portfolio_summary);
            swipeRefreshLayout.SetColorSchemeResources(Resource.Color.blue,
                                              Resource.Color.purple,
                                              Resource.Color.red,
                                              Resource.Color.green);
            swipeRefreshLayout.Refresh += delegate {
                Task.Run(async () =>
                {
                    try
                    {
                        items = await MKFApp.Current.GetRiskSummary();
                        this.RunOnUiThread(() => DisplayRefresher());
                    }
                    catch (Exception e)
                    {
                        this.RunOnUiThread(() => DismissRefresher());
                    }
                });
            };
            Task.Run(async () =>
            {
                try
                {
                    items = await MKFApp.Current.GetRiskSummary();
                    this.RunOnUiThread(() => Display());
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    this.RunOnUiThread(() => Dismiss());
                }
            });
            linearLayout = FindViewById<LinearLayout>(lucid.Resource.Id.home_linear_layout);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            // Create ActionBarDrawerToggle button and add it to the toolbar  
            V7Toolbar toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            SetSupportActionBar(toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            View headerView = navigationView.GetHeaderView(0);
            headerView.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            username = headerView.FindViewById<TextView>(Resource.Id.header_username);
            username.Text = Intent.GetStringExtra("username") ?? "Username not Found";
            SetupDrawerContent(navigationView); //Calling Function
            Task.Run(() =>
            {
                timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            });
        }
        private void Display()
        {
            progressBar.Visibility = ViewStates.Gone;
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerViewAdapter = new RecyclerViewPSAdapter(items, this, MainActivity.user);
            mRecyclerView.SetAdapter(mRecyclerViewAdapter);
        }

        private void DisplayRefresher()
        {
            swipeRefreshLayout.Refreshing = false;
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerViewAdapter = new RecyclerViewPSAdapter(items, this, MainActivity.user);
            mRecyclerView.SetAdapter(mRecyclerViewAdapter);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Task.Run(async () =>
            {
                try
                {
                    LoginResult loginResult = await MKFApp.Current.Logout();
                    this.RunOnUiThread(() => LogoutSuccessful());
                }
                catch (Exception exception)
                {
                    this.RunOnUiThread(() => LogoutFailed());
                }
            });
        }

        // data could not be retrieved
        private void Dismiss()
        {
            progressBar.Visibility = ViewStates.Gone;
            Snackbar.Make(linearLayout, "An Error Occured", Snackbar.LengthLong).Show();
        }

        private void DismissRefresher()
        {
            swipeRefreshLayout.Refreshing = false;
            Snackbar.Make(linearLayout, "An Error Occured", Snackbar.LengthLong).Show();
        }

        //Handles the NavigationView
        public void SetupDrawerContent(NavigationView navigationView)  
        {
            navigationView.NavigationItemSelected += (sender, e) =>  
            {  
                e.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();
                switch (e.MenuItem.ToString())
                {
                    case "Portfolio Summary":
                        timer.Stop();
                        break;
                    case "Account Summary":
                        timer.Stop();
                        Intent accountSummary = new Intent(this, typeof(AccountSummaryActivity));
                        StartActivity(accountSummary);
                        break;
                    case "Asset Allocation":
                        timer.Stop();
                        Intent assetAllocation = new Intent(this, typeof(AssetAllocationActivity));
                        StartActivity(assetAllocation);
                        break;
                    case "Details of Transaction":
                        timer.Stop();
                        Intent detailsOfTransaction = new Intent(this, typeof(DetailsOfTransactionActivity));
                        StartActivity(detailsOfTransaction);
                        break;
                    case "Profit/Loss":
                        timer.Stop();
                        Intent profitLoss = new Intent(this, typeof(ProfitLossActivity));
                        StartActivity(profitLoss);
                        break;
                    case "Change Password":
                        timer.Stop();
                        Intent changePassword = new Intent(this, typeof(ChangePasswordActivity));
                        StartActivity(changePassword);
                        break;
                    case "Logout":
                        Task.Run(async () =>
                        {
                            try
                            {
                                LoginResult loginResult = await MKFApp.Current.Logout();
                                this.RunOnUiThread(() => LogoutSuccessful());
                            }
                            catch (Exception exception)
                            {
                                this.RunOnUiThread(() => LogoutFailed());
                            }
                        });

                        break;
                    case "Contact Us":
                        timer.Stop();
                        Intent aboutUs = new Intent(this, typeof(AboutUsActivity));
                        StartActivity(aboutUs);
                        break;
                    default:
                        Snackbar.Make(linearLayout, "An Error Occured", Snackbar.LengthLong).Show();
                        break;
                }
            };  
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {  
            navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation
            return true;
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Task.Run(() => timer.Stop());
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Task.Run(() =>
        //    {
        //        timer.Stop();
        //        timer = new Timer(INTERVAL);
        //        COUNTDOWN = INITIAL;
        //        timer.Elapsed += Timer_Elapsed;
        //        timer.Start();
        //    });
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Task.Run(() => timer.Stop());
        //}

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                timer.Stop();
                timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                timer.Stop();
                timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            });
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                timer.Stop();
                timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            });
        }

        // Called every second when timer ticks.
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Write(COUNTDOWN.ToString());
            COUNTDOWN--;
            if (COUNTDOWN == 0)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        timer.Stop();
                        LoginResult loginResult = await MKFApp.Current.Logout();
                        this.RunOnUiThread(() => LogoutSuccessfulDialog());
                    }
                    catch (Exception exception)
                    {
                        this.RunOnUiThread(() => LogoutFailed());
                    }
                });
            }
        }

        // shows dialog when logged out due to inactivity after 5 minutes
        public void LogoutSuccessfulDialog() {
            if (!IsFinishing)
            {
                ShowAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
            }
        }

        // returns to login page
        public void LogoutSuccessful() {
            Intent logout = new Intent(this, typeof(MainActivity));
            logout.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            StartActivity(logout);
        }

        // called when logout failed.
        public void LogoutFailed() {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }

        // Sets up the alert dialog
        private void ShowAlertDialog(String title, String message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetPositiveButton("OK", (sender, e) =>
            {
                Intent logout = new Intent(this, typeof(MainActivity));
                logout.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                StartActivity(logout);
            });
            builder.Create().Show();
        }
    }
}