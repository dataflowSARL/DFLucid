using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MarketFlowLibrary;

namespace lucid
{
    public class RecyclerViewDOTAdapter : RecyclerView.Adapter
    {

        public event EventHandler<int> ItemClick;
        public Context mContext;
        public MKFUser mUser;
        public List<Operations> mItems;

        public RecyclerViewDOTAdapter(List<Operations> items, Context context , MKFUser user)
        {
            mContext = context;
            mItems = items;
            mUser = user;
        }

        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.transaction_date_dot.Text = mItems[position].TransactionDate.HasValue ? mItems[position].TransactionDate.Value.ToString("dd/MM/yyyy") : "";
            recyclerViewHolder.buy_sell_dot.Text = mItems[position].BuySell;
            recyclerViewHolder.quantity_dot.Text = mItems[position].Quantity.ToString("#,##0.00");
            recyclerViewHolder.security_name_dot.Text = mItems[position].SecurityName;
            recyclerViewHolder.open_price_dot.Text = mItems[position].OpenPrice.ToString("#,##0.00");
            recyclerViewHolder.net_amount_dot.SetTextColor(Android.Graphics.Color.Blue);
            recyclerViewHolder.net_amount_dot.Text = mItems[position].NetAmount.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int resourceLayout = Resource.Layout.recyclerview_card_dot_layout;
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
