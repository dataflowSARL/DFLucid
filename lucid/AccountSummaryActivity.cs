﻿
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
using Toolbar = Android.Widget.Toolbar;

namespace lucid
{
    [Activity(Label = "AccountSummaryActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class AccountSummaryActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_summary_layout);

            // Create your application here
            setUpVariables();

        }

        private void setUpVariables() {
            var toolbar = FindViewById<Toolbar>(Resource.Id.as_toolbar);
            toolbar.SetBackgroundColor(MainActivity.toolbarColor);
            back_button = FindViewById<ImageButton>(Resource.Id.as_back_btn);
            back_button.SetBackgroundColor(MainActivity.toolbarColor);
            back_button.Click += Back_Button_Click;
        }

        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}
