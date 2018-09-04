
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace lucid
{
    [Activity(Label = "ChangePasswordActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class ChangePasswordActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        private TextView old_password;
        private TextView new_password;
        private TextView confirm_password;
        private Button confirm_button;
        private ProgressBar progressBar;
        int screenWidth;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.change_password_layout);
            // Create your application here
            setUpVariables();
            back_button.Click += Back_Button_Click;
        }

        private void setUpVariables() {
            screenWidth = Resources.DisplayMetrics.WidthPixels;
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_password);
            progressBar.Visibility = ViewStates.Invisible;
            back_button = FindViewById<ImageButton>(Resource.Id.cp_back_btn);
            old_password = FindViewById<TextView>(Resource.Id.old_password);
            new_password = FindViewById<TextView>(Resource.Id.new_password);
            confirm_password = FindViewById<TextView>(Resource.Id.confirm_password);
            confirm_button = FindViewById<Button>(Resource.Id.confirm_button);
            old_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            new_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, 125);
            confirm_button.Click += Confirm_Button_Click;
        }

        void Confirm_Button_Click(object sender, EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
        }


        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}
