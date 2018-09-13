using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MKFLibrary;

namespace lucid
{
    public class RecyclerViewPLAdapter: RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<ClosedOperations> mItems = new List<ClosedOperations>();
        public Context mContext;
        public MKFUser mUser;

        public RecyclerViewPLAdapter(List<ClosedOperations> items, Context context, MKFUser user)
        {
            mItems = items;
            mContext = context;
            mUser = user;
        }

        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.quantity_closed_pl.SetTextColor(Android.Graphics.Color.Blue);
            recyclerViewHolder.estimated_pl.SetTextColor(Android.Graphics.Color.Blue);
            recyclerViewHolder.close_price_pl.SetTextColor(Android.Graphics.Color.Red);
            recyclerViewHolder.open_price_pl.SetTextColor(Android.Graphics.Color.ParseColor("#7bb89c"));
            recyclerViewHolder.closed_date_pl.Text = mItems[position].ClosedDate.HasValue ? mItems[position].ClosedDate.Value.ToString("dd/MM/yyyy") : "";
            recyclerViewHolder.opened_date_pl.Text = mItems[position].OpenedDate.HasValue ? mItems[position].OpenedDate.Value.ToString("dd/MM/yyyy") : "";
            recyclerViewHolder.close_bs_pl.Text = mItems[position].CloseBS;
            recyclerViewHolder.open_bs_pl.Text = mItems[position].OpenBS;
            recyclerViewHolder.security_name_pl.Text = mItems[position].SecurityName;
            recyclerViewHolder.quantity_closed_pl.Text = mItems[position].QuantityClosed.ToString("#,##0.00");
            recyclerViewHolder.close_price_pl.Text = mItems[position].ClosePrice.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
            recyclerViewHolder.open_price_pl.Text = mItems[position].OpenPrice.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
            recyclerViewHolder.estimated_pl.Text = mItems[position].EstimatedProfitLoss.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int resourceLayout = Resource.Layout.recyclerview_card_pl_layout;
            View row = LayoutInflater.From(parent.Context).Inflate(resourceLayout, parent, false);
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
