
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
    [Activity(Label = "AssetAllocationDetailsActivity")]
    public class AssetAllocationDetailsActivity : Activity
    {
        #region vars
        private ImageButton back_btn;
        #endregion
        //TODO: create listview layout
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.asset_allocation_details_layout);
            // Create your application here
            setUpVariables();
        }

        private void setUpVariables() {
            back_btn = FindViewById<ImageButton>(Resource.Id.aad_back_btn);
            back_btn.Click += Back_Btn_Click;
        }

        void Back_Btn_Click(object sender, EventArgs e)
        {
            Intent assetAllocation = new Intent(this, typeof(AssetAllocationActivity));
            StartActivity(assetAllocation);
        }

    }
}
