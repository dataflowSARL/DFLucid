
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MarketFlow;
using MKFLibrary;

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
        //private MKFUser user;
        private string webclicode;
        private string clicode;
        private string password;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.change_password_layout);
            // Create your application here
            setUpVariables();
        }

        private void setUpVariables() {
            screenWidth = Resources.DisplayMetrics.WidthPixels;
            linearLayout = FindViewById<LinearLayout>(Resource.Id.change_password_layout);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_password);
            progressBar.Visibility = ViewStates.Invisible;
            back_button = FindViewById<ImageButton>(Resource.Id.cp_back_btn);
            back_button.Click += Back_Button_Click;
            old_password = FindViewById<EditText>(Resource.Id.old_password);
            new_password = FindViewById<EditText>(Resource.Id.new_password);
            error_message = FindViewById<TextView>(Resource.Id.update_error);
            error_message.Visibility = ViewStates.Invisible;
            confirm_password = FindViewById<EditText>(Resource.Id.confirm_password);
            confirm_button = FindViewById<Button>(Resource.Id.confirm_button);
            old_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            new_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_button.Click += Confirm_Button_Click;
            webclicode = MainActivity.user.WebCliCode;
            clicode = MainActivity.user.CliCode;
            password = MainActivity.user.Password;
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

            if(loginResult.Success == true) {
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
            Snackbar.Make(linearLayout , "You are not connected", Snackbar.LengthLong).Show();
        }


        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}
