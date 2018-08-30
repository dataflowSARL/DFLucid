using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MarketFlowLibrary;
using MKFLibrary;

namespace lucid
{
    public class RecyclerViewAdapterAssetAllocation: RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick;
        public List<AssetAllocation> mItems;
        public MKFUser mUser;
        public Context mContext;
        public RecyclerViewAdapterAssetAllocation(List<AssetAllocation> items, Context context, MKFUser user)
        {
            mUser = user;
            mItems = items;
            mContext = context;
        }

        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            if (position % 2 == 1) {
				recyclerViewHolder.asset_description_odd.Text = mItems[position].AssetDescription;
                recyclerViewHolder.balance_odd.Text = mItems[position].Balance.ToString("#,##0.00");
                recyclerViewHolder.weight_percentage_odd.Text = mItems[position].Weight.ToString("#0.00") + "%";
                if (Convert.ToInt16(mItems[position].Code) <= 0) {
                    recyclerViewHolder.details_btn_odd.Visibility = ViewStates.Invisible;
                }
                recyclerViewHolder.details_btn_odd.Click += delegate {
                    if (Convert.ToInt16(mItems[position].Code) > 0)
                    {
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].Code);
                        details.PutExtra("webclicode", mUser.WebCliCode);
                        details.PutExtra("clicode", mUser.CliCode);
                        mContext.StartActivity(details);
                    }
                };
            } else {
                recyclerViewHolder.asset_description_even.Text = mItems[position].AssetDescription;
                recyclerViewHolder.balance_even.Text = mItems[position].Balance.ToString("#,##0.00"); ;
                recyclerViewHolder.weight_percentage_even.Text = mItems[position].Weight.ToString("#0.00") + "%";
                if (Convert.ToInt16(mItems[position].Code) <= 0)
                {
                    recyclerViewHolder.details_btn_even.Visibility = ViewStates.Invisible;
                }
                recyclerViewHolder.details_btn_even.Click += delegate {
                    if (Convert.ToInt16(mItems[position].Code) > 0)
                    {
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].Code);
                        details.PutExtra("webclicode", mUser.WebCliCode);
                        details.PutExtra("clicode", mUser.CliCode);
                        mContext.StartActivity(details);
                    }
                };
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layoutResource = 0;
            switch (viewType)
            {
                case 0:
                    layoutResource = Resource.Layout.recyclerview_card_aa_even_layout;
                    break;
                case 1:
                    layoutResource = Resource.Layout.recyclerview_card_aa_odd_layout;
                    break;
            }
            View row = LayoutInflater.From(parent.Context).Inflate(layoutResource,parent, false);
            RecyclerViewHolder recyclerViewHolder = new RecyclerViewHolder(row, OnClick);
            return recyclerViewHolder;
        }

        public override int GetItemViewType(int position)
        {
            return position % 2;
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null){
                ItemClick(this, obj);
            }
        }
    }
}