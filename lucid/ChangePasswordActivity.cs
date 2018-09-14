
using System;
using System.Collections.Generic;
using Android.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MarketFlow;
using MKFLibrary;
using Toolbar = Android.Widget.Toolbar;
using System.Timers;

namespace lucid
{
    [Activity(Label = "ChangePasswordActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class ChangePasswordActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        private EditText old_password;
        private EditText new_password;
        private EditText confirm_password;
        private Button confirm_button;
        private ProgressBar progressBar;
        private TextView error_message;
        private LinearLayout linearLayout;
        int screenWidth;
        private string webclicode;
        private string clicode;
        private string password;

        private GradientDrawable gd = new GradientDrawable(), gd_submit = new GradientDrawable();

        private Timer cp_timer;
        private int COUNTDOWN = 5 * 60, INTERVAL = 1000, INITIAL = 5 * 60;

        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.change_password_layout);
            // Create your application here
            SetUpVariables();
        }

        //setup acitivity's views
        private void SetUpVariables() {
            var toolbar = FindViewById<Toolbar>(Resource.Id.cp_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            screenWidth = Resources.DisplayMetrics.WidthPixels;
            gd_submit.SetCornerRadius(10);
            gd_submit.SetStroke(3, MainActivity.TEXT_COLOR);
            gd_submit.SetColor(MainActivity.TEXT_COLOR);
            gd.SetCornerRadius(10);
            gd.SetStroke(3, MainActivity.TEXT_COLOR);
            linearLayout = FindViewById<LinearLayout>(Resource.Id.change_password_layout);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_password);
            progressBar.Visibility = ViewStates.Invisible;
            back_button = FindViewById<ImageButton>(Resource.Id.cp_back_btn);
            back_button.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button.Click += Back_Button_Click;
            old_password = FindViewById<EditText>(Resource.Id.old_password);
            old_password.SetTextColor(MainActivity.TOOLBAR_COLOR);
            old_password.SetHighlightColor(Color.LightGray);
            old_password.Background = gd;
            new_password = FindViewById<EditText>(Resource.Id.new_password);
            new_password.SetTextColor(MainActivity.TOOLBAR_COLOR);
            new_password.SetHighlightColor(Color.LightGray);
            new_password.Background = gd;
            error_message = FindViewById<TextView>(Resource.Id.update_error);
            error_message.Visibility = ViewStates.Invisible;
            confirm_password = FindViewById<EditText>(Resource.Id.confirm_password);
            confirm_password.SetTextColor(MainActivity.TOOLBAR_COLOR);
            confirm_password.SetHighlightColor(Color.LightGray);
            confirm_password.Background = gd;
            confirm_button = FindViewById<Button>(Resource.Id.confirm_button);
            old_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            new_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_button.Background = gd_submit;
            confirm_button.Click += Confirm_Button_Click;
            webclicode = MainActivity.user.WebCliCode;
            clicode = MainActivity.user.CliCode;
            password = MainActivity.user.Password;
            Task.Run(() =>
            {
                cp_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                cp_timer.Elapsed += Timer_Elapsed;
                cp_timer.Start();
            });
        }

        //confirm button to change password click
        void Confirm_Button_Click(object sender, EventArgs e)
        {
            LoginResult loginResult = new LoginResult();
            if(new_password.Text == string.Empty || old_password.Text == string.Empty || confirm_password.Text == string.Empty) {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Color.Red);
                new_password.Background = gd;
                old_password.Background = gd;
                confirm_password.Background = gd;
                error_message.Text = "Fields cannot be empty";
                error_message.Visibility = ViewStates.Visible;
            } else if (!old_password.Text.Equals(MainActivity.user.Password)) {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Color.Red);
                new_password.Background = gd;
                old_password.Background = gd;
                confirm_password.Background = gd;
                error_message.Text = "Wrong old password";
                error_message.Visibility = ViewStates.Visible;
            } else if (!new_password.Text.Equals(confirm_password.Text)) {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Color.Red);
                new_password.Background = gd;
                old_password.Background = gd;
                confirm_password.Background = gd;
                error_message.Text = "Passwords don't match";
                error_message.Visibility = ViewStates.Visible;
            }
            else if (new_password.Text.Equals(confirm_password.Text) && old_password.Text.Equals(MainActivity.user.Password))
            {
                progressBar.Visibility = ViewStates.Visible;
                Window.SetFlags(WindowManagerFlags.NotTouchable, WindowManagerFlags.NotTouchable);
                Task.Run(async () =>
                {
                    try
                    {
                        loginResult = await MKFApp.Current.UpdatePassword(new_password.Text, old_password.Text, MainActivity.user.WebCliCode, MainActivity.user.CliCode, MainActivity.user.Username);
                        this.RunOnUiThread(() => UpdatePasswordSuccess(loginResult));
                    }
                    catch (Exception exception)
                    {
                        this.RunOnUiThread(() => UpdatePasswordFail());
                    }
                });
            }
        }

        //password updated successfully
        private void UpdatePasswordSuccess(LoginResult loginResult) {
            progressBar.Visibility = ViewStates.Invisible;
            error_message.Visibility = ViewStates.Invisible;
            Window.ClearFlags(WindowManagerFlags.NotTouchable);
            if (loginResult.Success == true) {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, MainActivity.TEXT_COLOR);
                new_password.Background = gd;
                old_password.Background = gd;
                confirm_password.Background = gd;
                old_password.Text = string.Empty;
                new_password.Text = string.Empty;
                confirm_password.Text = string.Empty;
                Snackbar.Make(linearLayout, "Password Updated", Snackbar.LengthLong).Show();
                Intent home = new Intent(this, typeof(HomeActivity));
                StartActivity(home);
            } else {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Color.Red);
                new_password.Background = gd;
                old_password.Background = gd;
                confirm_password.Background = gd;
                Snackbar.Make(linearLayout,loginResult.WebMessage, Snackbar.LengthLong).Show();
            }
        }

        //password failed to update
        private void UpdatePasswordFail() {
            progressBar.Visibility = ViewStates.Invisible;
            error_message.Visibility = ViewStates.Invisible;
            Window.ClearFlags(WindowManagerFlags.NotTouchable);
            Snackbar.Make(linearLayout , "You are not connected", Snackbar.LengthLong).Show();
        }

        //returns to parent activity
        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            Task.Run(() => cp_timer.Stop());
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Task.Run(() => cp_timer.Stop());
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Task.Run(() =>
        //    {
        //        cp_timer.Stop();
        //        cp_timer = new Timer(INTERVAL);
        //        COUNTDOWN = INITIAL;
        //        cp_timer.Elapsed += Timer_Elapsed;
        //        cp_timer.Start();
        //    });
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Task.Run(() => cp_timer.Stop());
        //}

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                cp_timer.Stop();
                cp_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                cp_timer.Elapsed += Timer_Elapsed;
                cp_timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                cp_timer.Stop();
                cp_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                cp_timer.Elapsed += Timer_Elapsed;
                cp_timer.Start();
            });
        }

        //detects user interaction
        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                cp_timer.Stop();
                cp_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                cp_timer.Elapsed += Timer_Elapsed;
                cp_timer.Start();
            });
        }

        //timer ticks
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            COUNTDOWN--;
            if (COUNTDOWN == 0)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        cp_timer.Stop();
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

        //logout -> inactivity
        public void LogoutSuccessful()
        {
            if (!IsFinishing)
            {
                ShowAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
            }
        }

        //logout failed
        public void LogoutFailed()
        {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }

        //dialog shows up due to inactivity
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
