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
        public TextView account_to_display { get; set; }
        public TextView account_type { get; set; }
        public TextView currency_account_summary { get; set; }
        public TextView amount_system { get; set; }
        public TextView transaction_date { get; set; }
        public TextView due_date { get; set; }
        public TextView transaction_description { get; set; }
        public TextView dbcr_amount { get; set; }
        public TextView balance { get; set; }
        public TextView transaction_date_dot { get; set; }
        public TextView buy_sell_dot { get; set; }
        public TextView quantity_dot { get; set; }
        public TextView security_name_dot { get; set; }
        public TextView open_price_dot { get; set; }
        public TextView net_amount_dot { get; set; }
        public TextView closed_date_pl { get; set; }
        public TextView opened_date_pl { get; set; }
        public TextView close_bs_pl { get; set; }
        public TextView open_bs_pl { get; set; }
        public TextView security_name_pl { get; set; }
        public TextView quantity_closed_pl { get; set; }
        public TextView close_price_pl { get; set; }
        public TextView open_price_pl { get; set; }
        public TextView estimated_pl { get; set; }
        public ImageButton as_btn { get; set; }
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
            account_to_display = itemview.FindViewById<TextView>(Resource.Id.account_to_display);
            account_type = itemview.FindViewById<TextView>(Resource.Id.account_type);
            currency_account_summary = itemview.FindViewById<TextView>(Resource.Id.currency_symbol);
            amount_system = itemview.FindViewById<TextView>(Resource.Id.amount_system);
            transaction_date = itemview.FindViewById<TextView>(Resource.Id.transaction_date);
            due_date = itemview.FindViewById<TextView>(Resource.Id.due_date);
            transaction_description = itemview.FindViewById<TextView>(Resource.Id.transaction_description);
            dbcr_amount = itemview.FindViewById<TextView>(Resource.Id.dbcr_amount);
            balance = itemview.FindViewById<TextView>(Resource.Id.balance);
            transaction_date_dot = itemview.FindViewById<TextView>(Resource.Id.transaction_date_dot);
            buy_sell_dot = itemview.FindViewById<TextView>(Resource.Id.buy_sell_dot);
            quantity_dot = itemview.FindViewById<TextView>(Resource.Id.quantity_dot);
            security_name_dot = itemview.FindViewById<TextView>(Resource.Id.security_name_dot);
            open_price_dot = itemview.FindViewById<TextView>(Resource.Id.open_price_dot);
            net_amount_dot = itemview.FindViewById<TextView>(Resource.Id.net_amount_dot);
            closed_date_pl = itemview.FindViewById<TextView>(Resource.Id.closed_date);
            opened_date_pl = itemview.FindViewById<TextView>(Resource.Id.opened_date);
            close_bs_pl = itemview.FindViewById<TextView>(Resource.Id.close_bs);
            open_bs_pl = itemview.FindViewById<TextView>(Resource.Id.open_bs);
            security_name_pl = itemview.FindViewById<TextView>(Resource.Id.security_name_pl);
            quantity_closed_pl = itemview.FindViewById<TextView>(Resource.Id.quantity_closed_pl);
            close_price_pl = itemview.FindViewById<TextView>(Resource.Id.close_price_pl);
            open_price_pl = itemview.FindViewById<TextView>(Resource.Id.open_price_pl);
            estimated_pl = itemview.FindViewById<TextView>(Resource.Id.estimated_profit_loss);
            as_btn = itemview.FindViewById<ImageButton>(Resource.Id.as_button);
            details_btn_even = itemview.FindViewById<ImageButton>(Resource.Id.details_button_even);
            details_btn_odd = itemview.FindViewById<ImageButton>(Resource.Id.details_button_odd);
            all_details_btn_even = itemview.FindViewById<ImageButton>(Resource.Id.all_details_button_even);
            all_details_btn_odd = itemview.FindViewById<ImageButton>(Resource.Id.all_details_button_odd);

            itemview.Click += (sender, e) => listener(base.AdapterPosition);
        }
    }
}