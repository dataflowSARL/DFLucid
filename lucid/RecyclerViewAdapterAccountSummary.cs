using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MKFLibrary;
using MKFLibrary.API;

namespace lucid
{
	public class RecyclerViewAdapterAccountSummary: RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public List<AccountSummary> mList;
        public MKFUser mUser;
        public Context mContext;
        public RecyclerViewAdapterAccountSummary(List<AccountSummary> list , Context context , MKFUser user)
        {
            mUser = user;
            mList = list;
            mContext = context;
        }

        public override int ItemCount => mList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            recyclerViewHolder.account_to_display.Text = mList[position].AccountToDisplay;
            recyclerViewHolder.account_type.Text = mList[position].AccountType;
            recyclerViewHolder.currency_account_summary.Text = mList[position].CurrencySymbol;
            if(mList[position].AmountSystem > 0) {
                recyclerViewHolder.amount_system.SetTextColor(Android.Graphics.Color.ParseColor("#7bb89c"));
                recyclerViewHolder.currency_account_summary.SetTextColor(Android.Graphics.Color.ParseColor("#7bb89c"));
            } else if(mList[position].AmountSystem < 0) {
                recyclerViewHolder.amount_system.SetTextColor(Android.Graphics.Color.Red);
                recyclerViewHolder.currency_account_summary.SetTextColor(Android.Graphics.Color.Red);
            } else {
                recyclerViewHolder.amount_system.SetTextColor(Android.Graphics.Color.Blue);
                recyclerViewHolder.currency_account_summary.SetTextColor(Android.Graphics.Color.Blue);
            }
            recyclerViewHolder.amount_system.Text = mList[position].AmountSystem.ToString("#,##0.00");
            recyclerViewHolder.as_btn.Click += delegate {
                Toast.MakeText(mContext, "button clicked", ToastLength.Short).Show();

            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layoutResource = Resource.Layout.recyclerview_account_summary_layout;
            View row = LayoutInflater.From(parent.Context).Inflate(layoutResource, parent, false);
            RecyclerViewHolder recyclerViewHolder = new RecyclerViewHolder(row, OnClick);
            return recyclerViewHolder;
        }

        private void OnClick(int obj) {
            if(ItemClick != null) {
                ItemClick(this, obj);
            }
        }
    }
}
