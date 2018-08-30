
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
            backButton.Click += BackButton_Click;
        }

        private void setUpVariables() {
            backButton = FindViewById<ImageButton>(Resource.Id.ps_back_btn);
        }

        void BackButton_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}
