using System;
using Android.Widget;
using Java.Lang;

namespace lucid
{
    public class ViewHolder: Java.Lang.Object
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
    }
}