using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using MKFLibrary;

namespace lucid
{
	public class MyListViewDetailsAdapter : BaseAdapter<Position>
    {
        public List<Position> mItemsPosition;
        private Context mContext;
        private MKFUser mUser;

        public MyListViewDetailsAdapter(Context context, List<Position> items, MKFUser user) {
            mItemsPosition = items;
            mContext = context;
            mUser = user;
        }

        public override Position this[int position] => mItemsPosition[position];

        public override int Count => mItemsPosition.Count;

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
                    layoutResource = Resource.Layout.asset_allocation_details_listview_even_row;
                    break;
                case 1:
                    layoutResource = Resource.Layout.asset_allocation_details_listview_odd_row;
                    break;
            }
            View row = convertView;
            if(row == null) {
                row = LayoutInflater.From(mContext).Inflate(layoutResource, parent, false);
                viewHolder = new ViewHolder();
                viewHolder.tit_nom_odd = row.FindViewById<TextView>(Resource.Id.tit_nom_odd);
                viewHolder.tit_nom_even = row.FindViewById<TextView>(Resource.Id.tit_nom_even);
                viewHolder.isin_odd = row.FindViewById<TextView>(Resource.Id.isin_odd);
                viewHolder.isin_even = row.FindViewById<TextView>(Resource.Id.isin_even);
                viewHolder.sumqty_odd = row.FindViewById<TextView>(Resource.Id.sum_qty_odd);
                viewHolder.sumqty_even = row.FindViewById<TextView>(Resource.Id.sum_qty_even);
                viewHolder.pos_bal_sys_tot_usd_details_odd = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_details_odd);
                viewHolder.pos_bal_sys_tot_usd_details_even = row.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_details_even);
                viewHolder.weight_odd = row.FindViewById<TextView>(Resource.Id.weight_percentage_tv_details_odd);
                viewHolder.weight_even = row.FindViewById<TextView>(Resource.Id.weight_percentage_tv_details_even);
                row.Tag = viewHolder;
            } else {
                viewHolder = row.Tag as ViewHolder;
            }

            if (position % 2 == 1)
            {
                viewHolder.tit_nom_odd.Text = mItemsPosition[position].tit_nom;
                viewHolder.isin_odd.Text = mItemsPosition[position].ISIN;
                viewHolder.sumqty_odd.Text = mItemsPosition[position].sumQty.ToString("#,##0.0000");
                viewHolder.pos_bal_sys_tot_usd_details_odd.Text = mItemsPosition[position].PosBalSysTot.ToString("#,##0.00");
                viewHolder.weight_odd.Text = mItemsPosition[position].Weight.ToString("#0.00") + "%";
            }
            else
            {
                viewHolder.tit_nom_even.Text = mItemsPosition[position].tit_nom;
                viewHolder.isin_even.Text = mItemsPosition[position].ISIN;
                viewHolder.sumqty_even.Text = mItemsPosition[position].sumQty.ToString("#,##0.0000");
                viewHolder.pos_bal_sys_tot_usd_details_even.Text = mItemsPosition[position].PosBalSysTot.ToString("#,##0.00");
                viewHolder.weight_even.Text = mItemsPosition[position].Weight.ToString("#0.00") + "%";
            }
            return row;
        }
    }
}