using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MarketFlowLibrary;
using MKFLibrary;

namespace lucid
{
	public class RecyclerViewSecurityAdapter: RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
		public List<PortfolioSummary> mItems = new List<PortfolioSummary>();
		public MKFUser mUser;
		public Context mContext;
        public RecyclerViewSecurityAdapter(List<PortfolioSummary> items, Context context , MKFUser user)
        {
			mUser = user;
			mItems = items;
			mContext = context;
        }
        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            if (position % 2 == 1)
            {
                recyclerViewHolder.desc_security_odd.Text = mItems[position].SecuritySubTypeDesc;
                recyclerViewHolder.balance_system_security_odd.Text = mItems[position].BalanceSystem.ToString("#,##0.00");
                recyclerViewHolder.weight_security_odd.Text = mItems[position].WeightPerc.ToString("#0.00") + "%";
                if (Convert.ToInt16(mItems[position].SecuritySubTypeCode) <= 0)
                {
                    recyclerViewHolder.details_security_btn_odd.Visibility = ViewStates.Invisible;
                    recyclerViewHolder.desc_security_odd.SetTextColor(Android.Graphics.Color.Blue);
                    recyclerViewHolder.balance_system_security_odd.SetTextColor(Android.Graphics.Color.Blue);
                    recyclerViewHolder.weight_security_odd.SetTextColor(Android.Graphics.Color.Blue);
                }
                recyclerViewHolder.details_security_btn_odd.Click += delegate
                {
                    if (Convert.ToInt16(mItems[position].SecuritySubTypeCode) > 0)
                    {
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].SecuritySubTypeCode);
                        details.PutExtra("webclicode", mUser.WebCliCode);
                        details.PutExtra("clicode", mUser.CliCode);
                        details.PutExtra("description", mItems[position].SecuritySubTypeDesc);
                        mContext.StartActivity(details);
                    }
                };
            }
            else
            {
                recyclerViewHolder.desc_security_even.Text = mItems[position].SecuritySubTypeDesc;
                recyclerViewHolder.balance_system_security_even.Text = mItems[position].BalanceSystem.ToString("#,##0.00");
                recyclerViewHolder.weight_security_even.Text = mItems[position].WeightPerc.ToString("#0.00") + "%";
                if (Convert.ToInt16(mItems[position].SecuritySubTypeCode) <= 0)
                {
                    recyclerViewHolder.details_security_btn_even.Visibility = ViewStates.Invisible;
                    recyclerViewHolder.desc_security_even.SetTextColor(Android.Graphics.Color.Blue);
                    recyclerViewHolder.balance_system_security_even.SetTextColor(Android.Graphics.Color.Blue);
                    recyclerViewHolder.weight_security_even.SetTextColor(Android.Graphics.Color.Blue);
                }
                recyclerViewHolder.details_security_btn_even.Click += delegate
                {
                    if (Convert.ToInt16(mItems[position].SecuritySubTypeCode) > 0)
                    {
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].SecuritySubTypeCode);
                        details.PutExtra("webclicode", mUser.WebCliCode);
                        details.PutExtra("clicode", mUser.CliCode);
                        details.PutExtra("description", mItems[position].SecuritySubTypeDesc);
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
                    layoutResource = Resource.Layout.recyclerview_card_security_layout;
                    break;
                case 1:
                    layoutResource = Resource.Layout.recyclerview_card_security_odd_layout;
                    break;
            }
            View row = LayoutInflater.From(parent.Context).Inflate(layoutResource, parent, false);
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
