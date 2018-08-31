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
        public TextView security { get; set; }
        public TextView isin_all_details { get; set; }
        public TextView qty { get; set; }
        public TextView maturity_date { get; set; }
        public TextView currency { get; set; }
        public TextView market_price { get; set; }
        public TextView average_price { get; set; }
        public TextView unrealised_pl { get; set; }
        public TextView unrealised_pl_usd { get; set; }
        public TextView gain_loss { get; set; }
        public TextView total_value { get; set; }
        public TextView total_value_usd { get; set; }
        public TextView weight_all_details { get; set; }
        public TextView accued_interest { get; set; }
        public ImageButton details_btn_even { get; set; }
        public ImageButton details_btn_odd { get; set; }
        public ImageButton all_details_btn_even { get; set; }
        public ImageButton all_details_btn_odd { get; set; }

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
            security = itemview.FindViewById<TextView>(Resource.Id.security_tv);
            isin_all_details = itemview.FindViewById<TextView>(Resource.Id.isin_tv);
            qty = itemview.FindViewById<TextView>(Resource.Id.qty_tv);
            maturity_date = itemview.FindViewById<TextView>(Resource.Id.maturity_date_tv);
            currency = itemview.FindViewById<TextView>(Resource.Id.currency_tv);
            market_price = itemview.FindViewById<TextView>(Resource.Id.market_price_tv);
            average_price = itemview.FindViewById<TextView>(Resource.Id.average_price_tv);
            unrealised_pl = itemview.FindViewById<TextView>(Resource.Id.unrealised_pl_tv);
            unrealised_pl_usd = itemview.FindViewById<TextView>(Resource.Id.unrealised_pl_usd_tv);
            gain_loss = itemview.FindViewById<TextView>(Resource.Id.gain_loss_tv);
            total_value = itemview.FindViewById<TextView>(Resource.Id.total_value_tv);
            total_value_usd = itemview.FindViewById<TextView>(Resource.Id.total_value_usd_tv);
            weight_all_details = itemview.FindViewById<TextView>(Resource.Id.weight_tv);
            accued_interest = itemview.FindViewById<TextView>(Resource.Id.accued_interest_tv);
            details_btn_even = itemview.FindViewById<ImageButton>(Resource.Id.details_button_even);
            details_btn_odd = itemview.FindViewById<ImageButton>(Resource.Id.details_button_odd);
            all_details_btn_even = itemview.FindViewById<ImageButton>(Resource.Id.all_details_button_even);
            all_details_btn_odd = itemview.FindViewById<ImageButton>(Resource.Id.all_details_button_odd);

            itemview.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }
}