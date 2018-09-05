
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
    [Activity(Label = "PortfolioSummaryActivity",ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class PortfolioSummaryActivity : Activity
    {
        #region vars
        private ImageButton backButton;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.portfolio_summary_layout);
            // Create your application here
            setUpVariables();
        }

        private void setUpVariables() {
            var toolbar = FindViewById<Toolbar>(Resource.Id.ps_toolbar);
            toolbar.SetBackgroundColor(MainActivity.toolbarColor);
            backButton = FindViewById<ImageButton>(Resource.Id.ps_back_btn);
            backButton.SetBackgroundColor(MainActivity.toolbarColor);
            backButton.Click += BackButton_Click;
        }

        void BackButton_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}
