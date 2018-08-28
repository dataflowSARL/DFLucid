using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using MarketFlowLibrary;
using MKFLibrary;

namespace lucid
{
    public class MyListViewAdapter : BaseAdapter<AssetAllocation>
    {

        public List<AssetAllocation> mItems;
        private Context mContext;
        private MKFUser mUser;

        public MyListViewAdapter(Context context , List<AssetAllocation> items , MKFUser user)
        {
            mItems = items;
            mContext = context;
            mUser = user;
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
            ViewHolder viewHolder;
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
                viewHolder = new ViewHolder();
                viewHolder.asset_description_odd = row.FindViewById<TextView>(Resource.Id.asset_description_odd);
                viewHolder.asset_description_even = row.FindViewById<TextView>(Resource.Id.asset_description_even);
                viewHolder.code_odd = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_odd);
                viewHolder.code_even = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_even);
                viewHolder.weight_percentage_odd = row.FindViewById<TextView>(Resource.Id.weight_percentage_tv_odd);
                viewHolder.weight_percentage_even = row.FindViewById<TextView>(Resource.Id.weight_percentage_tv_even);
                viewHolder.details_btn_odd = row.FindViewById<ImageButton>(Resource.Id.details_button_odd);
                viewHolder.details_btn_even = row.FindViewById<ImageButton>(Resource.Id.details_button_even);
                row.Tag = viewHolder;
            } else {
                viewHolder = row.Tag as ViewHolder;
            }

            if(position % 2 == 1) {
                viewHolder.asset_description_odd.Text = mItems[position].AssetDescription;
                viewHolder.code_odd.Text = mItems[position].Balance.ToString("#,##0.00");
                viewHolder.weight_percentage_odd.Text = mItems[position].Weight.ToString("#0.00") + "%";
                if (Convert.ToInt32(mItems[position].Code) <= 0)
                {
                    viewHolder.details_btn_odd.Visibility = ViewStates.Invisible;
                }
                viewHolder.details_btn_odd.Click += delegate {
                    if (Convert.ToInt32(mItems[position].Code) > 0)
                    {
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].Code);
                        details.PutExtra("webclicode", mUser.WebCliCode);
                        details.PutExtra("clicode", mUser.CliCode);
                        mContext.StartActivity(details);
                    }
                };
            } else {
                viewHolder.asset_description_even.Text = mItems[position].AssetDescription;
                viewHolder.code_even.Text = mItems[position].Balance.ToString("#,##0.00");
                viewHolder.weight_percentage_even.Text = mItems[position].Weight.ToString("#0.00") + "%";
                viewHolder.details_btn_even = row.FindViewById<ImageButton>(Resource.Id.details_button_even);
                if (Convert.ToInt32(mItems[position].Code) <= 0)
                {
                    viewHolder.details_btn_even.Visibility = ViewStates.Invisible;
                }
                viewHolder.details_btn_even.Click += delegate {
                    if(Convert.ToInt32(mItems[position].Code) > 0) {
                        Intent details = new Intent(mContext, typeof(AssetAllocationDetailsActivity));
                        details.PutExtra("assetcode", mItems[position].Code);
                        details.PutExtra("webclicode", mUser.WebCliCode);
                        details.PutExtra("clicode", mUser.CliCode);
                        mContext.StartActivity(details);
                    }
                };
            }
            return row;
        }
    }
}
