
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
using MarketFlowLibrary;
using MKFLibrary;

namespace lucid
{
    [Activity(Label = "AssetAllocationDetailsActivity")]
    public class AssetAllocationDetailsActivity : Activity
    {
        //TODO: replace listview with recyclerview
        #region vars
        private ImageButton back_btn;
        private ListView listView;
        private List<Position> mItemsPosition = new List<Position>();
        private MKFUser user;
        private string assetCode;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.asset_allocation_details_layout);
            // Create your application here
            setUpVariables();
        }

        async private void setUpVariables() {
            back_btn = FindViewById<ImageButton>(Resource.Id.aad_back_btn);
            back_btn.Click += Back_Btn_Click;
            listView = FindViewById<ListView>(Resource.Id.asset_allocation_details_list_view);
            user = new MKFUser();
            user.WebCliCode = Intent.GetStringExtra("webclicode") ?? string.Empty;
            user.CliCode = Intent.GetStringExtra("clicode") ?? string.Empty;
            assetCode = Intent.GetStringExtra("assetcode") ?? string.Empty;
            List<Position> userAccountPositions = await MarketFlowService.GetPosition(user);
            mItemsPosition = userAccountPositions.Where(u => u.Asset_Cod == assetCode).Select(u => new Position() { Tit_Cod = u.Tit_Cod, ISIN = u.ISIN, tit_nom = u.tit_nom, sumQty = u.sumQty, PosBalSysTot = u.PosBalSysTot, Weight = u.Weight }).ToList<Position>();
            MyListViewDetailsAdapter listViewDetailsAdapter = new MyListViewDetailsAdapter(this, mItemsPosition, user);
            listView.Adapter = listViewDetailsAdapter;
        }

        void Back_Btn_Click(object sender, EventArgs e)
        {
            Intent assetAllocation = new Intent(this, typeof(AssetAllocationActivity));
            Bundle bndlanimation = ActivityOptions.MakeCustomAnimation(this, Resource.Drawable.animation, Resource.Drawable.animation2).ToBundle();
            StartActivity(assetAllocation, bndlanimation);
        }

    }
}