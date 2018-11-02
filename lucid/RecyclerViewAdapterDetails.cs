using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MarketFlowLibrary;

namespace lucid
{
	public class RecyclerViewAdapterDetails: RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick;
        public List<Position> mItemsPosition;
        public MKFUser mUser;
        public Context mContext;
        public string mAsset_code;
        public string mTitle;
        public RecyclerViewAdapterDetails(List<Position> items, Context context, MKFUser user, String asset_code, String title)
        {
            mUser = user;
            mItemsPosition = items;
            mContext = context;
            mAsset_code = asset_code;
            mTitle = title;
        }

        public override int ItemCount => mItemsPosition.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            if (position % 2 == 1) {
                recyclerViewHolder.tit_nom_odd.Text = mItemsPosition[position].SecurityName;
                recyclerViewHolder.isin_odd.Text = mItemsPosition[position].ISIN;
                recyclerViewHolder.sumqty_odd.Text = mItemsPosition[position].Quantity.ToString("#,##0.0000");
                recyclerViewHolder.pos_bal_sys_tot_usd_details_odd.Text = mItemsPosition[position].BalanceSystem.ToString("#,##0.00");
                recyclerViewHolder.weight_odd.Text = mItemsPosition[position].Weight.ToString("#0.00") + "%";
                recyclerViewHolder.all_details_btn_odd.Click += delegate {
                    Intent all_details = new Intent(mContext, typeof(AllDetailsActivity));
                    all_details.PutExtra("tit_cod", mItemsPosition[position].SecurityCode);
                    all_details.PutExtra("webclicode", mUser.WebCliCode);
                    all_details.PutExtra("clicode", mUser.CliCode);
                    all_details.PutExtra("assetcode", mAsset_code);
                    all_details.PutExtra("description", mTitle);
                    mContext.StartActivity(all_details);
                };
            } else {
                recyclerViewHolder.tit_nom_even.Text = mItemsPosition[position].SecurityName;
                recyclerViewHolder.isin_even.Text = mItemsPosition[position].ISIN;
                recyclerViewHolder.sumqty_even.Text = mItemsPosition[position].Quantity.ToString("#,##0.0000");
                recyclerViewHolder.pos_bal_sys_tot_usd_details_even.Text = mItemsPosition[position].BalanceSystem.ToString("#,##0.00");
                recyclerViewHolder.weight_even.Text = mItemsPosition[position].Weight.ToString("#0.00") + "%";
                recyclerViewHolder.all_details_btn_even.Click += delegate {
                    Intent all_details = new Intent(mContext, typeof(AllDetailsActivity));
                    all_details.PutExtra("tit_cod", mItemsPosition[position].SecurityCode);
                    all_details.PutExtra("webclicode", mUser.WebCliCode);
                    all_details.PutExtra("clicode", mUser.CliCode);
                    all_details.PutExtra("assetcode", mAsset_code);
                    all_details.PutExtra("description", mTitle);
                    mContext.StartActivity(all_details);
                };
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layoutResource = 0;
            switch (viewType)
            {
                case 0:
                    layoutResource = Resource.Layout.recyclerview_card_aad_even_layout;
                    break;
                case 1:
                    layoutResource = Resource.Layout.recyclerview_card_aad_odd_layout;
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