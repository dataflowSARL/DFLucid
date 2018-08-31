using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MKFLibrary;

namespace lucid
{
    public class AllDetailsRecyclerViewAdapter: RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<Position> mItems;
        public MKFUser mUser;
        public Context mContext;

        public AllDetailsRecyclerViewAdapter(List<Position> items, Context context, MKFUser user)
        {
            mItems = items;
            mContext = context;
            mUser = user;
        }

        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.security.Text = mItems[position].tit_nom;
            recyclerViewHolder.isin_all_details.Text = mItems[position].ISIN;
            recyclerViewHolder.qty.Text = mItems[position].sumQty.ToString("#,##0.0000");
            recyclerViewHolder.maturity_date.Text = mItems[position].tit_dat_mat.ToString();
            recyclerViewHolder.currency.Text = mItems[position].devSymb;
            recyclerViewHolder.market_price.Text = mItems[position].TitCrs.ToString("#,##0.00");
            recyclerViewHolder.average_price.Text = mItems[position].CrsMoyen.ToString("#,##0.00");
            recyclerViewHolder.unrealised_pl.Text = mItems[position].UnrealizedPnl.ToString("#,##0.00");
            recyclerViewHolder.unrealised_pl_usd.Text = mItems[position].UnrealizedPnlUSD.ToString("#,##0.00");
            recyclerViewHolder.gain_loss.Text = mItems[position].GainLoss.ToString("#,##0.00");
            recyclerViewHolder.total_value.Text = mItems[position].PosBalDevTitTot.ToString("#,##0.00");
            recyclerViewHolder.total_value_usd.Text = mItems[position].PosBalSysTot.ToString("#,##0.00");
            recyclerViewHolder.weight_all_details.Text = mItems[position].Weight.ToString("#0.00") + "%";
            recyclerViewHolder.accued_interest.Text = mItems[position].IntVal.ToString("#,##0.00");

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layoutResource = Resource.Layout.recyclerview_card_all_details_layout;
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
            if (ItemClick != null)
            {
                ItemClick(this, obj);
            }
        }
    }
}
