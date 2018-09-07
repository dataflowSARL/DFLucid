using System;
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
        public API_Response<AccountSummary> mResponse;
        public MKFUser mUser;
        public Context mContext;
        public RecyclerViewAdapterAccountSummary(API_Response<AccountSummary> response , Context context , MKFUser user)
        {
            mUser = user;
            mResponse = response;
            mContext = context;
        }

        public override int ItemCount => mResponse.Content.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder recyclerViewHolder = holder as RecyclerViewHolder;
            if(position % 2 == 1) {
                recyclerViewHolder.account_type_odd.Text = mResponse.Content[position].AccountToDisplay;
                recyclerViewHolder.account_type_odd.Text = mResponse.Content[position].AccountType;
                recyclerViewHolder.currency_account_summary_odd.Text = mResponse.Content[position].CurrencySymbol;
                recyclerViewHolder.amount_system_odd.Text = mResponse.Content[position].AmountSystem.ToString("#,##0.00");
                recyclerViewHolder.as_btn_odd.Click += delegate {
                    Toast.MakeText(mContext, "button clicked", ToastLength.Short).Show();

                };
            } else {
                recyclerViewHolder.account_type_even.Text = mResponse.Content[position].AccountToDisplay;
                recyclerViewHolder.account_type_even.Text = mResponse.Content[position].AccountType;
                recyclerViewHolder.currency_account_summary_even.Text = mResponse.Content[position].CurrencySymbol;
                recyclerViewHolder.amount_system_even.Text = mResponse.Content[position].AmountSystem.ToString("#,##0.00");
                recyclerViewHolder.as_btn_even.Click += delegate {
                    Toast.MakeText(mContext, "button clicked", ToastLength.Short).Show();

                };
            }

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            int layoutResource = 0;
            switch(viewType) {
                case 0:
                    layoutResource = Resource.Layout.recyclerview_account_summary_even_layout;
                    break;
                case 1:
                    layoutResource = Resource.Layout.recyclerview_account_summary_odd_layout;
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

        private void OnClick(int obj) {
            if(ItemClick != null) {
                ItemClick(this, obj);
            }
        }
    }
}
