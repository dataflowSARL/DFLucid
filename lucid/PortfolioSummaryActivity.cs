
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
    [Activity(Label = "PortfolioSummaryActivity")]
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
            Intent home = new Intent(this, typeof(HomeActivity));
            Bundle bndlanimation = ActivityOptions.MakeCustomAnimation(this, Resource.Drawable.animation, Resource.Drawable.animation2).ToBundle();
            StartActivity(home, bndlanimation);
        }

    }
}
