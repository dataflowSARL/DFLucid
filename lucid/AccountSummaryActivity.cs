
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
using Android.Views;
using Android.Widget;
using MarketFlow;
using MKFLibrary;
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
            linearLayout = FindViewById<LinearLayout>(Resource.Id.as_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.as_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button = FindViewById<ImageButton>(Resource.Id.as_back_btn);
            back_button.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button.Click += Back_Button_Click;
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
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
