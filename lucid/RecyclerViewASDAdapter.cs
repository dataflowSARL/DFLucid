using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MKFLibrary;
using static Android.Support.V7.Widget.RecyclerView;

namespace lucid
{
    public class RecyclerViewASDAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public Context mContext;
        public MKFUser mUser;
        public List<TRNS> mItems;

        public RecyclerViewASDAdapter(List<TRNS> items,MKFUser user , Context context)
        {
            mContext = context;
            mUser = user;
            mItems = items;
        }

        public override int ItemCount => mItems.Count;

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.transaction_date.Text = mItems[position].TransactionDate.ToString();
            recyclerViewHolder.due_date.Text = mItems[position].DueDate.ToString();
            recyclerViewHolder.transaction_description.Text = mItems[position].TrnsDesc;
            recyclerViewHolder.dbcr.Text = mItems[position].DBCR;
            if(recyclerViewHolder.dbcr.Equals("D")) {
                recyclerViewHolder.dbcr_amount.Text = mItems[position].DbAmount.ToString("#,##0.00");
                recyclerViewHolder.dbcr_amount.SetTextColor(Android.Graphics.Color.Red);
            } else {
                recyclerViewHolder.dbcr_amount.Text = mItems[position].CrAmount.ToString("#,##0.00");
                recyclerViewHolder.dbcr_amount.SetTextColor(Android.Graphics.Color.ParseColor("#7bb89c"));
            }
            //recyclerViewHolder.balance.Text = mItems[position;
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int resourceLayout = Resource.Layout.recyclerview_card_asd_layout;
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
