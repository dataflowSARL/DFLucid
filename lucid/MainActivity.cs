﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
//using MarketFlow;
using MarketFlowLibrary;

using Toolbar = Android.Widget.Toolbar;

namespace lucid
{
    [Activity(Label = "lucid", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        #region variables
        private EditText username;
        private EditText password;
        private ImageView logo;
        private Button login;
        private TextView error;
        private ProgressBar progressBar;
        private LinearLayout linearLayout;

        private GradientDrawable gd = new GradientDrawable(), gd_submit = new GradientDrawable();

        public static Color TOOLBAR_COLOR = Color.ParseColor("#3e94a6");
        public static Color TEXT_COLOR = Color.ParseColor("#47555e");

        public static MKFUser user;

        int screenWidth;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            setUpVariables();

           
        }

        // set up the activity's views
        private void setUpVariables()
        {
            linearLayout = FindViewById<LinearLayout>(Resource.Id.main_activity_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbarMain);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            screenWidth = Resources.DisplayMetrics.WidthPixels;
            gd_submit.SetCornerRadius(10);
            gd_submit.SetStroke(3, TEXT_COLOR);
            gd_submit.SetColor(TEXT_COLOR);
            gd.SetCornerRadius(10);
            gd.SetStroke(3, TEXT_COLOR);
            username = FindViewById<EditText>(Resource.Id.username);
            username.Background = gd;
            username.SetTextColor(TOOLBAR_COLOR);
            username.SetHighlightColor(Color.LightGray);
            password = FindViewById<EditText>(Resource.Id.password);
            password.Background = gd;
            password.SetTextColor(TOOLBAR_COLOR);
            password.SetHighlightColor(Color.LightGray);
            logo = FindViewById<ImageView>(Resource.Id.logo);
            login = FindViewById<Button>(Resource.Id.login);
            login.Background = gd_submit;
            error = FindViewById<TextView>(Resource.Id.error_message);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBarLogin);
            progressBar.Visibility = ViewStates.Invisible;
            error.Visibility = ViewStates.Invisible;
            login.Click += Login_Click;
            username.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, ViewGroup.LayoutParams.WrapContent);
            password.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, ViewGroup.LayoutParams.WrapContent);
            login.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 2, ViewGroup.LayoutParams.WrapContent);
            IWindowManager windowManager = Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var rotation = windowManager.DefaultDisplay.Rotation;
            if(rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270) {
                logo.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 6, screenWidth / 6);
            } else {
                logo.LayoutParameters = new LinearLayout.LayoutParams(screenWidth / 4, screenWidth / 4);
            }

        }

        // login button clicked
        void Login_Click(object sender, System.EventArgs e)
        {
            progressBar.Visibility = ViewStates.Visible;
            Window.SetFlags(WindowManagerFlags.NotTouchable, WindowManagerFlags.NotTouchable);
            LoginResult loginResult = new LoginResult();
            if (username.Text == string.Empty || password.Text == string.Empty) {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Color.Red);
                username.Background = gd;
                password.Background = gd;
                progressBar.Visibility = ViewStates.Invisible;
                Window.ClearFlags(WindowManagerFlags.NotTouchable);
                error.Text = "Both fields cannot be empty";
                error.Visibility = ViewStates.Visible;
            } else {
                user = new MKFUser();
                user.Username = username.Text;
                user.Password = password.Text;
                Task.Run(async () =>
                {
                    try {

                        //Snackbar.Make(linearLayout, MKFApp.Current.GetUrl(), Snackbar.LengthLong).Show();
                                
                        loginResult = await MKFApp.Current.Login(user.Username, user.Password);
                        this.RunOnUiThread(() => SuccessLoginResultMethod(loginResult));

                    } 
                    catch (Exception exception){
                        this.RunOnUiThread(() =>
                                          // FailLoginResultMethod()
                                           Snackbar.Make(linearLayout, exception.InnerException + " "+ exception.StackTrace, Snackbar.LengthLong).Show()
                                          );
                    }
                });

                //user.WebCliCode = "1203255717565F";
                //user.CliCode = user.WebCliCode;

                //string dsValue = "[{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"...\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":0.0,\"devSymb\":\"\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":0.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"0\",\"Asset_Desc\":\"...\",\"AssetGrp\":1,\"Weight\":0.0,\"CostValueUSD\":0.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Addima Consulting\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":29997.0,\"devSymb\":\"LBP\",\"TitCrs\":25623.22875,\"CrsMoyen\":15774.9524222,\"IntVal\":0.0,\"PosBalDevTitTot\":768619992.81,\"PosBalSysTot\":509864.00849751238,\"titnb\":0.0,\"UnrealizedPnl\":295418745.00501657,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"0\",\"Asset_Desc\":\"...\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":313898.00849667226,\"GainLoss\":62.42983220548772,\"UnrealizedPnlUSD\":195966.00000332773,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Total :\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":29997.0,\"devSymb\":\"USD\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":509864.00849751238,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"0\",\"Asset_Desc\":\"...\",\"AssetGrp\":-1,\"Weight\":0.0,\"CostValueUSD\":313898.00849667226,\"GainLoss\":62.42983220548772,\"UnrealizedPnlUSD\":195966.00000332773,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Equities\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":0.0,\"devSymb\":\"\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":0.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":1,\"Weight\":0.0,\"CostValueUSD\":0.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"ADDIMA CONSULTING LTD.\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":1.0,\"devSymb\":\"USD\",\"TitCrs\":1.0,\"CrsMoyen\":1.0,\"IntVal\":0.0,\"PosBalDevTitTot\":1.0,\"PosBalSysTot\":1.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Public Health Investment LTD\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":1900.0,\"devSymb\":\"USD\",\"TitCrs\":1000.0,\"CrsMoyen\":1000.0,\"IntVal\":0.0,\"PosBalDevTitTot\":1900000.0,\"PosBalSysTot\":1900000.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1900000.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"KABABJI HOLDING SAL\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":1.0,\"devSymb\":\"USD\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":0.0,\"PosBalDevTitTot\":100.0,\"PosBalSysTot\":100.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":100.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"KABABJI INTERNATIONAL COMPANY UK\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":1110.0,\"devSymb\":\"GBP\",\"TitCrs\":1000.0,\"CrsMoyen\":1000.0,\"IntVal\":0.0,\"PosBalDevTitTot\":1110000.0,\"PosBalSysTot\":1473747.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1473747.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"KABABJI INVESTMENT COMPANY\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":2000.0,\"devSymb\":\"USD\",\"TitCrs\":1392.75,\"CrsMoyen\":1150.9718,\"IntVal\":0.0,\"PosBalDevTitTot\":2785500.0,\"PosBalSysTot\":2785500.0,\"titnb\":0.0,\"UnrealizedPnl\":483556.39999999997,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":2301943.6,\"GainLoss\":21.006439949267207,\"UnrealizedPnlUSD\":483556.39999999997,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Total :\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":5012.0,\"devSymb\":\"USD\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":6159348.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"1\",\"Asset_Desc\":\"Equities\",\"AssetGrp\":-1,\"Weight\":0.0,\"CostValueUSD\":5675791.6,\"GainLoss\":8.5196292266967788,\"UnrealizedPnlUSD\":483556.39999999997,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Fixed Income \",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":0.0,\"devSymb\":\"\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":0.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":1,\"Weight\":0.0,\"CostValueUSD\":0.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"DEBBANEH AGRI 7% 2022\",\"tit_dat_emi\":\"2015-11-10T00:00:00\",\"tit_dat_mat\":\"2022-07-18T00:00:00\",\"sumQty\":1000000.0,\"devSymb\":\"USD\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":12663.287699999999,\"PosBalDevTitTot\":1012663.29,\"PosBalSysTot\":1012663.29,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1000000.0,\"GainLoss\":1.2663290000000105,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LB0000011686\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Resource Group Holding 12% 30/03/2019\",\"tit_dat_emi\":\"2014-03-30T00:00:00\",\"tit_dat_mat\":\"2019-03-30T00:00:00\",\"sumQty\":950000.0,\"devSymb\":\"USD\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":25916.0,\"PosBalDevTitTot\":975916.0,\"PosBalSysTot\":975916.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":950000.0,\"GainLoss\":2.7279999999999971,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"5001\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"TFH SIF NOTES COUPON 7% MAT 31/12/2026\",\"tit_dat_emi\":\"2017-02-07T00:00:00\",\"tit_dat_mat\":\"2026-12-31T00:00:00\",\"sumQty\":1000000.0,\"devSymb\":\"USD\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":6777.53425,\"PosBalDevTitTot\":1006777.53,\"PosBalSysTot\":1006777.53,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1000000.0,\"GainLoss\":0.67775299999999206,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LB0000011751\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T-BILLS 6.74% MAT 13/04/2023\",\"tit_dat_emi\":\"2018-04-19T00:00:00\",\"tit_dat_mat\":\"2023-04-13T00:00:00\",\"sumQty\":4805160000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":57922507.5267732,\"PosBalDevTitTot\":4863082507.53,\"PosBalSysTot\":3225925.3781293533,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":3187502.4875621893,\"GainLoss\":1.205423077067147,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180862601\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T-BILLS 7.08% MAT 170425\",\"tit_dat_emi\":\"2018-04-26T00:00:00\",\"tit_dat_mat\":\"2025-04-17T00:00:00\",\"sumQty\":846780000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":9649970.0143176,\"PosBalDevTitTot\":856429970.01,\"PosBalSysTot\":568112.74959203985,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":561711.4427860697,\"GainLoss\":1.1396076914901077,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180863849\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T-BILLS 7.08% MAT 20/03/2025\",\"tit_dat_emi\":\"2018-03-29T00:00:00\",\"tit_dat_mat\":\"2025-03-20T00:00:00\",\"sumQty\":705420000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":11611918.620000001,\"PosBalDevTitTot\":717031918.62,\"PosBalSysTot\":475643.06376119406,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":467940.29850746272,\"GainLoss\":1.6461000000000059,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180859847\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T.BILLS 6.74% MAT 160323\",\"tit_dat_emi\":\"2018-03-22T00:00:00\",\"tit_dat_mat\":\"2023-03-16T00:00:00\",\"sumQty\":1535760000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":25917367.6293408,\"PosBalDevTitTot\":1561677367.63,\"PosBalSysTot\":1035938.5523250415,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1018746.2686567166,\"GainLoss\":1.6875923080429178,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180858609\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Lebanon T.Bills 6.74% Maturity 190123\",\"tit_dat_emi\":\"2018-01-25T00:00:00\",\"tit_dat_mat\":\"2023-01-19T00:00:00\",\"sumQty\":1016400000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":26954224.336116,\"PosBalDevTitTot\":1043354224.34,\"PosBalSysTot\":692108.93820232176,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":674228.85572139313,\"GainLoss\":2.651930769382127,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180580606\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T.BILLS 6.74% MATURITY 271022\",\"tit_dat_emi\":\"2017-11-02T00:00:00\",\"tit_dat_mat\":\"2022-10-27T00:00:00\",\"sumQty\":1924000000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":18553872.00888,\"PosBalDevTitTot\":1942553872.01,\"PosBalSysTot\":1288592.9499237149,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1276285.2404643451,\"GainLoss\":0.96433846205821183,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT170838603\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T.BILLS 7.08% MAT 031024\",\"tit_dat_emi\":\"2017-10-12T00:00:00\",\"tit_dat_mat\":\"2024-10-03T00:00:00\",\"sumQty\":299380000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":4169925.8441547994,\"PosBalDevTitTot\":303549925.84,\"PosBalSysTot\":201359.81813598672,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":198593.69817578775,\"GainLoss\":1.3928538446121852,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT170835849\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T.Bills 7.08% MAT 120625\",\"tit_dat_emi\":\"2018-06-21T00:00:00\",\"tit_dat_mat\":\"2025-06-12T00:00:00\",\"sumQty\":49200000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":62298.553884,\"PosBalDevTitTot\":49262298.55,\"PosBalSysTot\":32678.141658374792,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":32636.815920398014,\"GainLoss\":0.12662306910566912,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180874842\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T.BILLS 7.08% MAT 15/05/2025\",\"tit_dat_emi\":\"2018-05-24T00:00:00\",\"tit_dat_mat\":\"2025-05-15T00:00:00\",\"sumQty\":66270000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":419565.5656395,\"PosBalDevTitTot\":66689565.57,\"PosBalSysTot\":44238.517791044775,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":43960.199004975133,\"GainLoss\":0.63311539157988594,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180867840\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON T.BILLS 7.08% MATURITY 311024\",\"tit_dat_emi\":\"2017-11-30T00:00:00\",\"tit_dat_mat\":\"2024-10-31T00:00:00\",\"sumQty\":412160000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":3653227.7150208,\"PosBalDevTitTot\":415813227.72,\"PosBalSysTot\":275829.67012935324,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":273406.30182421231,\"GainLoss\":0.88636153920806926,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT170839841\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON TREASURY NOTES 6.74% MAT 02/03/2023\",\"tit_dat_emi\":\"2018-03-08T00:00:00\",\"tit_dat_mat\":\"2023-03-02T00:00:00\",\"sumQty\":2496020000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":48140161.7334646,\"PosBalDevTitTot\":2544160161.73,\"PosBalSysTot\":1687668.4323250414,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1655734.6600331676,\"GainLoss\":1.9286769228611833,\"UnrealizedPnlUSD\":0.0,\"ISIN\":null,\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON TREASURY NOTES 7.08% 230ViewGroup.LayoutParams.WrapContent\",\"tit_dat_emi\":\"2018-02-01T00:00:00\",\"tit_dat_mat\":\"2025-01-23T00:00:00\",\"sumQty\":483650000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":12860662.740447499,\"PosBalDevTitTot\":496510662.74,\"PosBalSysTot\":329360.30695854063,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":320829.1873963516,\"GainLoss\":2.65908461490747,\"UnrealizedPnlUSD\":0.0,\"ISIN\":null,\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LEBANON TREASURY NOTES 7.08% MAT 25\",\"tit_dat_emi\":\"2018-03-01T00:00:00\",\"tit_dat_mat\":\"2025-02-20T00:00:00\",\"sumQty\":905890000.0,\"devSymb\":\"LBP\",\"TitCrs\":100.0,\"CrsMoyen\":100.0,\"IntVal\":19500118.458941203,\"PosBalDevTitTot\":925390118.46,\"PosBalSysTot\":613857.45834825875,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":2,\"titTypDesc\":\"Bond\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":600922.05638474307,\"GainLoss\":2.152592308116863,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"LBT180855845\",\"MODCOD\":\"%\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Total :\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":15549040000.0,\"devSymb\":\"USD\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":13466670.797280267,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"2\",\"Asset_Desc\":\"Fixed Income \",\"AssetGrp\":-1,\"Weight\":0.0,\"CostValueUSD\":13262497.512437809,\"GainLoss\":1.539478402548089,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Funds\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":0.0,\"devSymb\":\"\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":0.0,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"8\",\"Asset_Desc\":\"Funds\",\"AssetGrp\":1,\"Weight\":0.0,\"CostValueUSD\":0.0,\"GainLoss\":0.0,\"UnrealizedPnlUSD\":0.0,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"BLOM LIRA MONEY MARKET FUND\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":1768545.0,\"devSymb\":\"LBP\",\"TitCrs\":1299.04344,\"CrsMoyen\":1300.5035851,\"IntVal\":0.0,\"PosBalDevTitTot\":2297416780.59,\"PosBalSysTot\":1523991.2309054728,\"titnb\":0.0,\"UnrealizedPnl\":-2582332.31587966,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"8\",\"Asset_Desc\":\"Funds\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":1525704.2208362718,\"GainLoss\":-0.11227536159400531,\"UnrealizedPnlUSD\":-1712.9899276150315,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LUCID INCOME FUND (USD)\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":96531.0,\"devSymb\":\"USD\",\"TitCrs\":10.52827,\"CrsMoyen\":9.9903539,\"IntVal\":0.0,\"PosBalDevTitTot\":1016304.43,\"PosBalSysTot\":1016304.43,\"titnb\":0.0,\"UnrealizedPnl\":51925.579049099862,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"8\",\"Asset_Desc\":\"Funds\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":964378.85232090007,\"GainLoss\":5.3843546604256609,\"UnrealizedPnlUSD\":51925.579049099862,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"LUCID LEBNANI FUND\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":626584.0,\"devSymb\":\"LBP\",\"TitCrs\":14314.44615,\"CrsMoyen\":13700.9716785,\"IntVal\":0.0,\"PosBalDevTitTot\":8969202926.45,\"PosBalSysTot\":5949720.017545606,\"titnb\":0.0,\"UnrealizedPnl\":384393288.25035584,\"TitTyp\":1,\"titTypDesc\":\"Share\",\"Asset_Cod\":\"8\",\"Asset_Desc\":\"Funds\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":5694732.7616592,\"GainLoss\":4.4775982747277121,\"UnrealizedPnlUSD\":254987.25588746657,\"ISIN\":null,\"MODCOD\":\"\"},{\"ord\":1,\"Onhold\":0,\"tit_nom\":\"Total :\",\"tit_dat_emi\":\"1900-01-01T00:00:00\",\"tit_dat_mat\":\"1900-01-01T00:00:00\",\"sumQty\":2491660.0,\"devSymb\":\"USD\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":8490015.67845108,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"8\",\"Asset_Desc\":\"Funds\",\"AssetGrp\":-1,\"Weight\":0.0,\"CostValueUSD\":8184815.834816372,\"GainLoss\":3.7288541342183512,\"UnrealizedPnlUSD\":305199.84500895138,\"ISIN\":\"\",\"MODCOD\":\"\"},{\"ord\":2,\"Onhold\":0,\"tit_nom\":\"Grand Total :\",\"tit_dat_emi\":null,\"tit_dat_mat\":null,\"sumQty\":15551566669.0,\"devSymb\":\"USD\",\"TitCrs\":0.0,\"CrsMoyen\":0.0,\"IntVal\":0.0,\"PosBalDevTitTot\":0.0,\"PosBalSysTot\":28625898.484228857,\"titnb\":0.0,\"UnrealizedPnl\":0.0,\"TitTyp\":0,\"titTypDesc\":\"\",\"Asset_Cod\":\"-1\",\"Asset_Desc\":\"\",\"AssetGrp\":0,\"Weight\":0.0,\"CostValueUSD\":27437002.955750853,\"GainLoss\":4.3331829296202606,\"UnrealizedPnlUSD\":984722.24501227913,\"ISIN\":\"\",\"MODCOD\":\"\"}]";
                //List<Position> userAccountPositions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Position>>(dsValue);

                //List<Position> userAccountPositions = await MarketFlowService.GetPosition(user);

                //List<AssetAllocation> assetCodes = userAccountPositions.Select(u => new AssetAllocation() { Code = u.Asset_Cod, AssetDescription = u.Asset_Desc }).GroupBy(s => s.Code).Select(s => s.First()).ToList();

            }
        }

        // login is successful
        private void SuccessLoginResultMethod(LoginResult loginResult) {
            progressBar.Visibility = ViewStates.Invisible;
            Window.ClearFlags(WindowManagerFlags.NotTouchable);
            //loginResult.WebMessage = loginResult.Success ? "success" : "error";
            //loginResult.Success = false;

            if (loginResult.Success)
            {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, TEXT_COLOR);
                username.Background = gd;
                password.Background = gd;
              
                error.Visibility = ViewStates.Invisible;
                Intent home = new Intent(this, typeof(HomeActivity));
                home.PutExtra("username", username.Text);
                user.WebCliCode = loginResult.WebCliCode;
                user.CliCode = loginResult.CliCode;
                user.Username = loginResult.CliID;
                StartActivity(home);
            }
            else
            {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, TEXT_COLOR);
                username.Background = gd;
                password.Background = gd;
                error.Visibility = ViewStates.Visible;
                error.Text = loginResult.WebMessage;//?? "An Error Occured";
            }

        }

        //login failed
        private void FailLoginResultMethod() {
            progressBar.Visibility = ViewStates.Invisible;
            error.Visibility = ViewStates.Invisible;
            Window.ClearFlags(WindowManagerFlags.NotTouchable);
            Snackbar.Make(linearLayout, "You are not connected!", Snackbar.LengthLong).Show();
        }
    }
}