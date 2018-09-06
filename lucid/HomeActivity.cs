using System;
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
using MKFLibrary;
using MarketFlow;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using System.Timers;
using Android.App;
using AlertDialog = Android.Support.V7.App.AlertDialog;

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

        public static int COUNTDOWN;
        public static int INITIAL_VALUE = 5 * 60;
        public static int INTERVAL = 1000;
        public static string DIALOG_TITLE = "TIME OUT";
        public static string DIALOG_MESSAGE = "You've been logged out due to inactivity";
        private Timer timer;

        #endregion

        protected override void OnCreate(Bundle bundle)  
        {  
            base.OnCreate(bundle);
            // Set our view from the "home" layout resource  
            SetContentView(Resource.Layout.Home);
            setUpVariables();
        }

        public void setUpVariables() {
            linearLayout = FindViewById<LinearLayout>(Resource.Id.home_linear_layout);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            // Create ActionBarDrawerToggle button and add it to the toolbar  
            var toolbar = FindViewById<V7Toolbar>(Resource.Id.toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            SetSupportActionBar(toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            View headerView = navigationView.GetHeaderView(0);
            headerView.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            username = headerView.FindViewById<TextView>(Resource.Id.header_username);
            username.Text = MainActivity.user.Username ?? "Username Not Found";
            setupDrawerContent(navigationView); //Calling Function
            timer = new Timer(INTERVAL);
            COUNTDOWN = INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void setupDrawerContent(NavigationView navigationView)  
        {
            navigationView.NavigationItemSelected += (sender, e) =>  
            {  
                e.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();
                switch (e.MenuItem.ToString())
                {
                    case "Portfolio Summary":
                        Intent portfolioSummary = new Intent(this, typeof(PortfolioSummaryActivity));
                        StartActivity(portfolioSummary);
                        break;
                    case "Account Summary":
                        Intent accountSummary = new Intent(this, typeof(AccountSummaryActivity));
                        StartActivity(accountSummary);
                        break;
                    case "Asset Allocation":
                        Intent assetAllocation = new Intent(this, typeof(AssetAllocationActivity));
                        StartActivity(assetAllocation);
                        break;
                    case "Details of Transaction":
                        Intent detailsOfTransaction = new Intent(this, typeof(DetailsOfTransactionActivity));
                        StartActivity(detailsOfTransaction);
                        break;
                    case "Profit/Loss":
                        Intent profitLoss = new Intent(this, typeof(ProfitLossActivity));
                        StartActivity(profitLoss);
                        break;
                    case "Change Password":
                        Intent changePassword = new Intent(this, typeof(ChangePasswordActivity));
                        StartActivity(changePassword);
                        break;
                    case "Logout":
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

                        break;
                    case "About Us":
                        Intent aboutUs = new Intent(this, typeof(AboutUsActivity));
                        StartActivity(aboutUs);
                        break;
                    default:
                        Snackbar.Make(linearLayout, "You are not connected", Snackbar.LengthLong).Show();
                        break;
                }
            };  
        }  

        public override bool OnCreateOptionsMenu(IMenu menu)
        {  
            navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation
            return true;
        }

        protected override void OnStart()
        {
            base.OnStart();
            timer = new Timer(INTERVAL);
            COUNTDOWN = INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        protected override void OnResume()
        {
            base.OnResume();
            timer = new Timer(INTERVAL);
            COUNTDOWN = INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            timer = new Timer(INTERVAL);
            COUNTDOWN = INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Write(COUNTDOWN.ToString());
            COUNTDOWN--;
            if (COUNTDOWN == 0)
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

        public void logoutSuccessful() {
            timer.Stop();
            showAlertDialog(DIALOG_TITLE, DIALOG_MESSAGE);
        }

        public void logoutFailed() {
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