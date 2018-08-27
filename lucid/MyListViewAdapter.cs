using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using MarketFlowLibrary;

namespace lucid
{
    public class MyListViewAdapter : BaseAdapter<AssetAllocation>
    {

        public List<AssetAllocation> mItems;
        private Context mContext;

        public MyListViewAdapter(Context context , List<AssetAllocation> items)
        {
            mItems = items;
            mContext = context;
        }

        public override AssetAllocation this[int position] => mItems[position];

        public override int Count => mItems.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int GetItemViewType(int position)
        {
            return position % 2;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            int layoutResource = 0;
            int viewType = GetItemViewType(position);
            switch(viewType) {
                case 0:
                    layoutResource = Resource.Layout.asset_allocation_listview_even_row;
                    break;
                case 1:
                    layoutResource = Resource.Layout.asset_allocation_listview_odd_row;
                    break;
            }
            View row = convertView;
            if (row == null) {
                row = LayoutInflater.From(mContext).Inflate(layoutResource, null, false);
            }

            if(position % 2 == 1) {
                TextView asset_description_odd_tv = row.FindViewById<TextView>(Resource.Id.asset_description_odd);
                asset_description_odd_tv.Text = mItems[position].AssetDescription;

                TextView code_odd_tv = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_odd);
                code_odd_tv.Text = mItems[position].Balance.ToString("#,##0.00");

                TextView weight_percentage_odd_tv = row.FindViewById<TextView>(Resource.Id.weight_percentage_tv_odd);
                weight_percentage_odd_tv.Text = mItems[position].Weight.ToString("#0.00") + "%";

                ImageButton details_btn_odd = row.FindViewById<ImageButton>(Resource.Id.details_button_odd);
                if (Convert.ToInt32(mItems[position].Code) <= 0)
                {
                    details_btn_odd.Visibility = ViewStates.Invisible;
                }
                details_btn_odd.Click += delegate {
                    if (Convert.ToInt32(mItems[position].Code) > 0)
                    {
                        //Toast.MakeText(mContext, mItems[position].Code, ToastLength.Short).Show();
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].Code);
                        mContext.StartActivity(details);
                    }
                };
            } else {
                TextView asset_description_even_tv = row.FindViewById<TextView>(Resource.Id.asset_description_even);
                asset_description_even_tv.Text = mItems[position].AssetDescription;

                TextView code_even_tv = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_even);
                code_even_tv.Text = mItems[position].Balance.ToString("#,##0.00");

                TextView weight_percentage_even_tv = row.FindViewById<TextView>(Resource.Id.weight_percentage_tv_even);
                weight_percentage_even_tv.Text = mItems[position].Weight.ToString("#0.00") + "%";

                ImageButton details_btn_even = row.FindViewById<ImageButton>(Resource.Id.details_button_even);
                if (Convert.ToInt32(mItems[position].Code) <= 0)
                {
                    details_btn_even.Visibility = ViewStates.Invisible;
                }
                details_btn_even.Click += delegate {
                    if(Convert.ToInt32(mItems[position].Code) > 0) {
                        //Toast.MakeText(mContext, mItems[position].Code, ToastLength.Short).Show();
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].Code);
                        mContext.StartActivity(details);
                    }
                };
            }
            return row;
        }
    }
}
