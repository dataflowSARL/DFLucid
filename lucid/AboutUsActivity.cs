
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
    [Activity(Label = "AboutUsActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class AboutUsActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        private LinearLayout linearLayout;
        private TextView aboutus;

        private Timer au_timer;
        private int COUNTDOWN = 5 * 60, INTERVAL = 1000, INITIAL = 5 * 60;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.about_us_layout);
            // Create your application here
            SetUpVariables();
        }

        //set up the activity's views
        private void SetUpVariables(){
            linearLayout = FindViewById<LinearLayout>(Resource.Id.au_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.au_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button = FindViewById<ImageButton>(Resource.Id.au_back_btn);
            back_button.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button.Click += Back_Button_Click;
            aboutus = FindViewById<TextView>(Resource.Id.aboutus_tv);
            aboutus.Text = "This is an Android Application Developed by Dataflow S.A.R.L. for Lucid Investment Bank.";
            Task.Run(() =>
            {
                au_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                au_timer.Elapsed += Timer_Elapsed;
                au_timer.Start();
            });
        }

        //back button clicked
        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            Task.Run(() => au_timer.Stop());
        }

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Task.Run(() =>
        //    {
        //        au_timer.Stop();
        //        au_timer = new Timer(INTERVAL);
        //        COUNTDOWN = INITIAL;
        //        au_timer.Elapsed += Timer_Elapsed;
        //        au_timer.Start();
        //    });
        //}

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Task.Run(() => au_timer.Stop());
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Task.Run(() => au_timer.Stop());
        //}

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                au_timer.Stop();
                au_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                au_timer.Elapsed += Timer_Elapsed;
                au_timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                au_timer.Stop();
                au_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                au_timer.Elapsed += Timer_Elapsed;
                au_timer.Start();
            });
        }

        // detects user interaction
        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                au_timer.Stop();
                au_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                au_timer.Elapsed += Timer_Elapsed;
                au_timer.Start();
            });
        }

        // timer ticks
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            COUNTDOWN--;
            if (COUNTDOWN == 0)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        au_timer.Stop();
                        LoginResult loginResult = await MKFApp.Current.Logout();
                        this.RunOnUiThread(() => LogoutSuccessful());
                    }
                    catch (Exception exception)
                    {
                        this.RunOnUiThread(() => LogoutFailed());
                    }
                });
            }
        }

        // logged out because of inactivity
        public void LogoutSuccessful()
        {
            if (!IsFinishing)
            {
                ShowAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
            }
        }

        // logout failed
        public void LogoutFailed()
        {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }

        // set up alert dialog
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
