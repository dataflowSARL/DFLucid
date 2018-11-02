using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MarketFlowLibrary;

namespace lucid
{
	public class RecyclerViewPSAdapter: RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<RiskSummary> mItems;
        public MKFUser mUser;
        public Context mContext;
        public RecyclerViewPSAdapter(List<RiskSummary> items , Context context , MKFUser user)
        {
            mUser = user;
            mContext = context;
            mItems = items;
        }

        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.ovl_ps.SetTextColor(Android.Graphics.Color.Red);
            double total_assets = mItems[position].LongPosition + mItems[position].ShortPosition;
            recyclerViewHolder.total_assets_ps.Text = total_assets.ToString("#,##0.00");
            recyclerViewHolder.net_cash_amount_balances_ps.Text = mItems[position].NetBalance.ToString("#,##0.00");
            recyclerViewHolder.initial_margin_ps.Text = mItems[position].Margin.ToString("#,##0.00");
            recyclerViewHolder.maintenance_margin_ps.Text = mItems[position].MarginMC.ToString("#,##0.00");
            recyclerViewHolder.net_asset_value_ps.Text = mItems[position].NetAssetValue.ToString("#,##0.00");
            recyclerViewHolder.ovl_ps.Text = mItems[position].OVL ?? "";
            recyclerViewHolder.total_assets_btn.Click += delegate {
                Intent assetAllocation = new Intent(mContext, typeof(AssetAllocationActivity));
                mContext.StartActivity(assetAllocation);
            };
            recyclerViewHolder.net_cash_balance_btn.Click += delegate {
                Intent accountSummary = new Intent(mContext, typeof(AccountSummaryActivity));
                mContext.StartActivity(accountSummary);
            };
            recyclerViewHolder.total_assets_layout.Click += delegate {
                Intent assetAllocation = new Intent(mContext, typeof(AssetAllocationActivity));
                mContext.StartActivity(assetAllocation);
            };
            recyclerViewHolder.net_cash_balance_layout.Click += delegate {
                Intent accountSummary = new Intent(mContext, typeof(AccountSummaryActivity));
                mContext.StartActivity(accountSummary);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layoutResource = Resource.Layout.recyclerview_card_ps_layout;
            View row = LayoutInflater.From(parent.Context).Inflate(layoutResource, parent, false);
            RecyclerViewHolder recyclerViewHolder = new RecyclerViewHolder(row, OnClick);
            return recyclerViewHolder;
        }

        private void OnClick(int obj)
        {
            if (ItemClick != null)
            {
                ItemClick(this, obj);
            }
        }
    }
}
