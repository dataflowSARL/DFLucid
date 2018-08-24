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
                code_odd_tv.Text = mItems[position].Balance.ToString() + ", " + mItems[position].Weight.ToString() + "%";
            } else {
                TextView asset_description_even_tv = row.FindViewById<TextView>(Resource.Id.asset_description_even);
                asset_description_even_tv.Text = mItems[position].AssetDescription;

                TextView code_even_tv = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_even);
                code_even_tv.Text = mItems[position].Balance.ToString() + ", " + mItems[position].Weight.ToString() + "%";
            }

            return row;
        }
    }
}
