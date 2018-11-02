using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MarketFlowLibrary;
using static Android.Support.V7.Widget.RecyclerView;

namespace lucid
{
    public class RecyclerViewASDAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public Context mContext;
        public MKFUser mUser;
        public List<TRNS> mItems;
        private decimal balance;

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
            recyclerViewHolder.transaction_date.Text = mItems[position].TransactionDate.HasValue ? mItems[position].TransactionDate.Value.ToString("dd/MM/yyyy") : "";
            recyclerViewHolder.due_date.Text = mItems[position].DueDate.HasValue ? mItems[position].DueDate.Value.ToString("dd/MM/yyyy") : "";
            recyclerViewHolder.transaction_description.Text = mItems[position].TrnsDesc;
            recyclerViewHolder.balance.SetTextColor(Android.Graphics.Color.Blue);
            if(mItems[position].DBCR.Equals("D")) {
                recyclerViewHolder.dbcr_amount.Text = "-" + mItems[position].DbAmount.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
                recyclerViewHolder.dbcr_amount.SetTextColor(Android.Graphics.Color.Red);
                balance = balance - mItems[position].DbAmount;
                recyclerViewHolder.balance.Text = balance.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
            } else if(mItems[position].DBCR.Equals("C")) {
                recyclerViewHolder.dbcr_amount.Text = mItems[position].CrAmount.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
                recyclerViewHolder.dbcr_amount.SetTextColor(Android.Graphics.Color.ParseColor("#7bb89c"));
                balance = balance + mItems[position].CrAmount;
                recyclerViewHolder.balance.Text = balance.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
            } else if(mItems[position].DBCR.Equals("P"))
            {
                recyclerViewHolder.dbcr_amount.Text = string.Empty;
                if(mItems[position].CrAmount == 0 && mItems[position].DbAmount == 0) {
                    balance = mItems[position].DbAmount;
                    recyclerViewHolder.balance.Text = balance.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
                } else if (mItems[position].CrAmount == 0)
                {
                    balance = mItems[position].DbAmount;
                    recyclerViewHolder.balance.Text = balance.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
                } else if(mItems[position].DbAmount == 0) {
                    balance = mItems[position].CrAmount;
                    recyclerViewHolder.balance.Text = balance.ToString("#,##0.00") + " " + mItems[position].CurrencySymbol;
                }
            }
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
