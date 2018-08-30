
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MarketFlowLibrary;
using MKFLibrary;

namespace lucid
{
    [Activity(Label = "AssetAllocationDetailsActivity", ParentActivity = typeof(AssetAllocationActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".AssetAllocationActivity")]
    public class AssetAllocationDetailsActivity : Activity
    {

        //TODO: implement onclick event for recyclerview (Step 3)
        #region vars
        private ImageButton back_btn;
        private List<Position> mItemsPosition = new List<Position>();
        private MKFUser user;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewAdapterDetails mRecyclerViewAdapter;
        private string assetCode;
        private ProgressDialog progressDialog;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.asset_allocation_details_layout);
            // Create your application here
            setUpVariables();
        }

        async private void setUpVariables() {
            progressDialog = new ProgressDialog(this);
            back_btn = FindViewById<ImageButton>(Resource.Id.aad_back_btn);
            back_btn.Click += Back_Btn_Click;
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview_aa_details);
            user = new MKFUser();
            user.WebCliCode = Intent.GetStringExtra("webclicode") ?? string.Empty;
            user.CliCode = Intent.GetStringExtra("clicode") ?? string.Empty;
            assetCode = Intent.GetStringExtra("assetcode") ?? string.Empty;
            progressDialog.SetMessage("Please wait...");
            progressDialog.Show();
            List<Position> userAccountPositions = await MarketFlowService.GetPosition(user);
            progressDialog.Dismiss();
            mItemsPosition = userAccountPositions.Where(u => u.Asset_Cod == assetCode).Select(u => new Position() { Tit_Cod = u.Tit_Cod, ISIN = u.ISIN, tit_nom = u.tit_nom, sumQty = u.sumQty, PosBalSysTot = u.PosBalSysTot, Weight = u.Weight }).ToList<Position>();
            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mRecyclerViewAdapter = new RecyclerViewAdapterDetails(mItemsPosition, this , user, assetCode);
            mRecyclerView.SetAdapter(mRecyclerViewAdapter);

        }


        void Back_Btn_Click(object sender, EventArgs e)
        {
            //Intent assetAllocation = new Intent(this, typeof(AssetAllocationActivity));
            ////Bundle bndlanimation = ActivityOptions.MakeCustomAnimation(this, Resource.Drawable.animation, Resource.Drawable.animation2).ToBundle();
            ////StartActivity(assetAllocation, bndlanimation);
            //StartActivity(assetAllocation);
            //Finish();
            base.OnBackPressed();
        }

    }
}