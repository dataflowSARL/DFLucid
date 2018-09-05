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

namespace lucid
{
    [Activity(Theme = "@style/Theme.DesignDemo")]
    public class HomeActivity : AppCompatActivity
    {

        #region variables
        private NavigationView navigationView;
        private DrawerLayout drawerLayout;
        private LinearLayout linearLayout;
        //private MKFUser user;
        private TextView username;
        #endregion

        protected override void OnCreate(Bundle bundle)  
        {  
            base.OnCreate(bundle);
            // Set our view from the "home" layout resource  
            SetContentView(Resource.Layout.Home);
            linearLayout = FindViewById<LinearLayout>(Resource.Id.home_linear_layout);
            drawerLayout = FindViewById < DrawerLayout > (Resource.Id.drawer_layout);  
            // Create ActionBarDrawerToggle button and add it to the toolbar  
            var toolbar = FindViewById < V7Toolbar > (Resource.Id.toolbar);  
            SetSupportActionBar(toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById < NavigationView > (Resource.Id.nav_view);
            View headerView = navigationView.GetHeaderView(0);
            username = headerView.FindViewById<TextView>(Resource.Id.header_username);
            username.Text = MainActivity.user.Username ?? "Username Not Found";
            setupDrawerContent(navigationView); //Calling Function
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
                        Intent logout = new Intent(this , typeof(MainActivity));
                        StartActivity(logout);
                        break;
                    case "About Us":
                        Intent aboutUs = new Intent(this, typeof(AboutUsActivity));
                        StartActivity(aboutUs);
                        break;
                    default:
                        Snackbar.Make(linearLayout, "You are not connected", Snackbar.LengthShort).Show();
                        break;
                }
            };  
        }  

        public override bool OnCreateOptionsMenu(IMenu menu)
        {  
            navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation
            return true;
        }
    }
}