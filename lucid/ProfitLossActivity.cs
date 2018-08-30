
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
    [Activity(Label = "ProfitLossActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class ProfitLossActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profit_loss_layout);
            // Create your application here
            setUpVariables();
            back_button.Click += Back_Button_Click;
        }

        private void setUpVariables() {
            back_button = FindViewById<ImageButton>(Resource.Id.pl_back_btn);
        }

        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}
