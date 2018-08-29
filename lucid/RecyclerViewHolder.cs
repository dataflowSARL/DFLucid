using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace lucid
{
	public class RecyclerViewHolder: RecyclerView.ViewHolder
    {
        public TextView tit_nom_even { get; set; }
        public TextView tit_nom_odd { get; set; }
        public TextView isin_even { get; set; }
        public TextView isin_odd { get; set; }
        public TextView sumqty_even { get; set; }
        public TextView sumqty_odd { get; set; }
        public TextView pos_bal_sys_tot_usd_details_even { get; set; }
        public TextView pos_bal_sys_tot_usd_details_odd { get; set; }
        public TextView weight_even { get; set; }
        public TextView weight_odd { get; set; }
        public TextView asset_description_even { get; set; }
        public TextView asset_description_odd { get; set; }
        public TextView balance_even { get; set; }
        public TextView balance_odd { get; set; }
        public TextView weight_percentage_even { get; set; }
        public TextView weight_percentage_odd { get; set; }
        public ImageButton details_btn_even { get; set; }
        public ImageButton details_btn_odd { get; set; }

        public RecyclerViewHolder(View itemview, Action<int> listener) : base(itemview) {
            tit_nom_even = itemview.FindViewById<TextView>(Resource.Id.tit_nom_even);
            tit_nom_odd = itemview.FindViewById<TextView>(Resource.Id.tit_nom_odd);
            isin_even = itemview.FindViewById<TextView>(Resource.Id.isin_even);
            isin_odd = itemview.FindViewById<TextView>(Resource.Id.isin_odd);
            sumqty_even = itemview.FindViewById<TextView>(Resource.Id.sum_qty_even);
            sumqty_odd = itemview.FindViewById<TextView>(Resource.Id.sum_qty_odd);
            pos_bal_sys_tot_usd_details_even = itemview.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_details_even);
            pos_bal_sys_tot_usd_details_odd = itemview.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_details_odd);
            weight_even = itemview.FindViewById<TextView>(Resource.Id.weight_percentage_tv_details_even);
            weight_odd = itemview.FindViewById<TextView>(Resource.Id.weight_percentage_tv_details_odd);
            asset_description_even = itemview.FindViewById<TextView>(Resource.Id.asset_description_even);
            asset_description_odd = itemview.FindViewById<TextView>(Resource.Id.asset_description_odd);
            balance_even = itemview.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_even);
            balance_odd = itemview.FindViewById<TextView>(Resource.Id.pos_bal_sys_tot_usd_odd);
            weight_percentage_even = itemview.FindViewById<TextView>(Resource.Id.weight_percentage_tv_even);
            weight_percentage_odd = itemview.FindViewById<TextView>(Resource.Id.weight_percentage_tv_odd);
            details_btn_even = itemview.FindViewById<ImageButton>(Resource.Id.details_button_even);
            details_btn_odd = itemview.FindViewById<ImageButton>(Resource.Id.details_button_odd);

            itemview.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }
}