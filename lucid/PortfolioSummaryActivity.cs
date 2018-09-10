
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
    [Activity(Label = "PortfolioSummaryActivity",ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class PortfolioSummaryActivity : Activity
    {
        #region vars
        private ImageButton backButton;
        private LinearLayout linearLayout;

        private Timer ps_timer;
        private int COUNTDOWN = 5 * 60, INTERVAL = 1000, INITIAL = 5 * 60;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.portfolio_summary_layout);
            // Create your application here
            SetUpVariables();
        }

        private void SetUpVariables() {
            linearLayout = FindViewById<LinearLayout>(Resource.Id.ps_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.ps_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            backButton = FindViewById<ImageButton>(Resource.Id.ps_back_btn);
            backButton.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            backButton.Click += BackButton_Click;
            Task.Run(() =>
            {
                ps_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                ps_timer.Elapsed += Timer_Elapsed;
                ps_timer.Start();
            });
        }

        void BackButton_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            Task.Run(() => ps_timer.Stop());
        }

        protected override void OnStop()
        {
            base.OnStop();
            Task.Run(() => ps_timer.Stop());
        }

        protected override void OnPause()
        {
            base.OnPause();
            Task.Run(() => ps_timer.Stop());
        }

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                ps_timer.Stop();
                ps_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                ps_timer.Elapsed += Timer_Elapsed;
                ps_timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                ps_timer.Stop();
                ps_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                ps_timer.Elapsed += Timer_Elapsed;
                ps_timer.Start();
            });
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                ps_timer.Stop();
                ps_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                ps_timer.Elapsed += Timer_Elapsed;
                ps_timer.Start();
            });
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            COUNTDOWN--;
            if (COUNTDOWN == 0)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        ps_timer.Stop();
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

        public void LogoutSuccessful()
        {
            if (!IsFinishing)
            {
                ShowAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
            }
        }

        public void LogoutFailed()
        {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }

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
