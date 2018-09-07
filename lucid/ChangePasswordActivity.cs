
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

        private Timer timer;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.change_password_layout);
            // Create your application here
            setUpVariables();
        }

        private void setUpVariables() {
            var toolbar = FindViewById<Toolbar>(Resource.Id.cp_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            screenWidth = Resources.DisplayMetrics.WidthPixels;
            GradientDrawable gd = new GradientDrawable();
            gd.SetCornerRadius(10);
            gd.SetStroke(3, MainActivity.TOOLBAR_COLOR);
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
            confirm_button.SetTextColor(MainActivity.TOOLBAR_COLOR);
            old_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            new_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_button.Click += Confirm_Button_Click;
            webclicode = MainActivity.user.WebCliCode;
            clicode = MainActivity.user.CliCode;
            password = MainActivity.user.Password;
            timer = new Timer(HomeActivity.INTERVAL);
            HomeActivity.COUNTDOWN = HomeActivity.INITIAL_VALUE;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        void Confirm_Button_Click(object sender, EventArgs e)
        {
            LoginResult loginResult = new LoginResult();
            if(new_password.Text == string.Empty || old_password.Text == string.Empty || confirm_password.Text == string.Empty) {
                error_message.Text = "Fields cannot be empty";
                error_message.Visibility = ViewStates.Visible;
            } else if (!old_password.Text.Equals(MainActivity.user.Password)) {
                error_message.Text = "Wrong old password";
                error_message.Visibility = ViewStates.Visible;
            } else if (!new_password.Text.Equals(confirm_password.Text)) {
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
                        this.RunOnUiThread(() => updatePasswordSuccess(loginResult));
                    }
                    catch (Exception exception)
                    {
                        this.RunOnUiThread(() => updatePasswordFail());
                    }
                });
            }
        }

        private void updatePasswordSuccess(LoginResult loginResult) {
            progressBar.Visibility = ViewStates.Invisible;
            error_message.Visibility = ViewStates.Invisible;
            Window.ClearFlags(WindowManagerFlags.NotTouchable);
            if (loginResult.Success == true) {
                old_password.Text = string.Empty;
                new_password.Text = string.Empty;
                confirm_password.Text = string.Empty;
                Snackbar.Make(linearLayout, "Password Updated", Snackbar.LengthLong).Show();
                Intent home = new Intent(this, typeof(HomeActivity));
                StartActivity(home);
            } else {
                Snackbar.Make(linearLayout,loginResult.WebMessage, Snackbar.LengthLong).Show();
            }
        }

        private void updatePasswordFail() {
            progressBar.Visibility = ViewStates.Invisible;
            error_message.Visibility = ViewStates.Invisible;
            Window.ClearFlags(WindowManagerFlags.NotTouchable);
            Snackbar.Make(linearLayout , "You are not connected", Snackbar.LengthLong).Show();
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
