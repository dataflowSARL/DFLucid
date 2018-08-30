
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MarketFlowLibrary;
using MKFLibrary;

namespace lucid
{
    [Activity(Label = "AllDetailsActivity", ParentActivity = typeof(AssetAllocationDetailsActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".AssetAllocationDetailsActivity")]
    public class AllDetailsActivity : Activity
    {

        #region variables
        private ImageButton back_btn;
        private TextView security;
        private TextView isin;
        private TextView qty;
        private TextView maturity_date;
        private TextView currency;
        private TextView market_price;
        private TextView average_price;
        private TextView unrealised_pl;
        private TextView unrealised_pl_usd;
        private TextView gain_loss;
        private TextView total_value;
        private TextView total_value_usd;
        private TextView weight;
        private TextView accued_interest;

        private MKFUser user;
        private List<Position> mItems;
        private String tit_cod;
        private String asset_cod;

        private ProgressDialog progressDialog;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.all_details_layout);
            setUpVariables();
        }

        async private void setUpVariables()
        {
            progressDialog = new ProgressDialog(this);
            user = new MKFUser();
            user.WebCliCode = Intent.GetStringExtra("webclicode") ?? string.Empty;
            user.CliCode = Intent.GetStringExtra("clicode") ?? string.Empty;
            tit_cod = Intent.GetStringExtra("tit_cod") ?? string.Empty;
            asset_cod = Intent.GetStringExtra("assetcode") ?? string.Empty;
            progressDialog.SetMessage("Please wait...");
            progressDialog.Show();
            List<Position> userAccountPositions = await MarketFlowService.GetPosition(user);
            progressDialog.Dismiss();
            back_btn = FindViewById<ImageButton>(Resource.Id.ad_back_btn);
            back_btn.Click += Back_Btn_Click;
            security = FindViewById<TextView>(Resource.Id.security_tv);
            security.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { tit_nom = u.tit_nom }).ToString();
            isin = FindViewById<TextView>(Resource.Id.isin_tv);
            isin.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { ISIN = u.ISIN }).ToString();
            qty = FindViewById<TextView>(Resource.Id.qty_tv);
            qty.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { sumQty = u.sumQty }).ToString();
            maturity_date = FindViewById<TextView>(Resource.Id.maturity_date_tv);
            maturity_date.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { tit_dat_mat = u.tit_dat_mat }).ToString();
            currency = FindViewById<TextView>(Resource.Id.currency_tv);
            currency.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { devSymb = u.devSymb }).ToString();
            market_price = FindViewById<TextView>(Resource.Id.market_price_tv);
            market_price.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { TitCrs = u.TitCrs }).ToString();
            average_price = FindViewById<TextView>(Resource.Id.average_price_tv);
            average_price.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { CrsMoyen = u.CrsMoyen }).ToString();
            unrealised_pl = FindViewById<TextView>(Resource.Id.unrealised_pl_tv);
            unrealised_pl.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { UnrealizedPnl = u.UnrealizedPnl }).ToString();
            unrealised_pl_usd = FindViewById<TextView>(Resource.Id.unrealised_pl_usd_tv);
            unrealised_pl_usd.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { UnrealizedPnlUSD = u.UnrealizedPnlUSD }).ToString();
            gain_loss = FindViewById<TextView>(Resource.Id.gain_loss_tv);
            gain_loss.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { GainLoss = u.GainLoss }).ToString();
            total_value = FindViewById<TextView>(Resource.Id.total_value_tv);
            total_value.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { PosBalDevTitTot = u.PosBalDevTitTot }).ToString();
            total_value_usd = FindViewById<TextView>(Resource.Id.total_value_usd_tv);
            total_value_usd.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { PosBalSysTot = u.PosBalSysTot }).ToString();
            weight = FindViewById<TextView>(Resource.Id.weight_tv);
            weight.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { Weight = u.Weight }).ToString();
            accued_interest = FindViewById<TextView>(Resource.Id.accued_interest_tv);
            accued_interest.Text = userAccountPositions.Where(u => u.Tit_Cod == tit_cod).Where(u => u.Asset_Cod == asset_cod).Select(u => new Position { IntVal = u.IntVal }).ToString();
        }

        void Back_Btn_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

    }
}