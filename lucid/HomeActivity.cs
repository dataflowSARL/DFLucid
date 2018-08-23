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

namespace lucid
{
    [Activity(Theme = "@style/Theme.DesignDemo")]
    public class HomeActivity : AppCompatActivity
    {

        #region variables
        NavigationView navigationView;
        DrawerLayout drawerLayout;
        #endregion


        protected override void OnCreate(Bundle bundle)  
        {  
            base.OnCreate(bundle);  
            // Set our view from the "home" layout resource  
            SetContentView(Resource.Layout.Home);  
            drawerLayout = FindViewById < DrawerLayout > (Resource.Id.drawer_layout);  
            // Create ActionBarDrawerToggle button and add it to the toolbar  
            var toolbar = FindViewById < V7Toolbar > (Resource.Id.toolbar);  
            SetSupportActionBar(toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById < NavigationView > (Resource.Id.nav_view);
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
                        Toast.MakeText(this, e.MenuItem.ToString(), ToastLength.Short).Show();
                        break;
                    case "Account Summary":
                        Toast.MakeText(this, e.MenuItem.ToString(), ToastLength.Short).Show();
                        break;
                    case "Asset Allocation":
                        Intent assetAllocation = new Intent(this, typeof(AssetAllocationActivity));
                        StartActivity(assetAllocation);
                        break;
                    case "Details of Transaction":
                        Toast.MakeText(this, e.MenuItem.ToString(), ToastLength.Short).Show();
                        break;
                    case "Profit/Loss":
                        Toast.MakeText(this, e.MenuItem.ToString(), ToastLength.Short).Show();
                        break;
                    case "Change Password":
                        Toast.MakeText(this, e.MenuItem.ToString(), ToastLength.Short).Show();
                        break;
                    case "Logout":
                        Intent logout = new Intent(this , typeof(MainActivity));
                        StartActivity(logout);
                        break;
                    case "About Us":
                        Toast.MakeText(this, e.MenuItem.ToString(), ToastLength.Short).Show();
                        break;
                    default:
                        Toast.MakeText(this, "Error", ToastLength.Short).Show();
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