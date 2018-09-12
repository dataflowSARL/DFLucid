
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MarketFlow;
using MKFLibrary;
using Toolbar = Android.Widget.Toolbar;
//using FragmentManager = Android.Support.V4.App.FragmentManager;
using Android.Support.V4.App;
using static Android.App.DatePickerDialog;
using Android.Graphics.Drawables;

namespace lucid
{
    [Activity(Label = "AccountSummaryDetailsActivity", ParentActivity = typeof(AccountSummaryActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "AccountSummaryActivity")]
    public class AccountSummaryDetailsActivity : Activity,IOnDateSetListener
    {
        #region variables
        private LinearLayout linearLayout;
        private ImageButton back_btn;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewASDAdapter recyclerViewASDAdapter;
        private List<TRNS> items = new List<TRNS>();
        private ProgressBar progressBar;
        private Button from_btn, to_btn , submit;
        private ParamDate paramDate = new ParamDate();
        private GradientDrawable gd = new GradientDrawable();
        private DateTime from, to;

        private int from_year = DateTime.Now.Year , from_month = DateTime.Now.Month - 1, from_day = 1;
        private int to_year = DateTime.Now.Year, to_month = DateTime.Now.Month - 1, to_day = DateTime.Now.Day;
        private int from_to = 0;
        private const int FROM_DIALOG = 1 , TO_DIALOG = 0;

        private Timer asd_timer;
        private int COUNTDOWN = 5 * 60, INITIAL = 5 * 60, INTERVAL = 1000;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_summary_details_layout);
            // Create your application here
            SetUpVariables();
        }

        private void SetUpVariables()
        {
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_account_summary_details);
            progressBar.Visibility = ViewStates.Visible;
            paramDate.userMKF = MainActivity.user;
            linearLayout = FindViewById<LinearLayout>(Resource.Id.account_summary_detail_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.asd_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_btn = FindViewById<ImageButton>(Resource.Id.asd_back_btn);
            back_btn.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview_asd);
            mLayoutManager = new LinearLayoutManager(this);
            from_btn = FindViewById<Button>(Resource.Id.from_btn);
            to_btn = FindViewById<Button>(Resource.Id.to_btn);
            paramDate.FromAcc = Intent.GetStringExtra("account") ?? string.Empty;
            paramDate.ToAcc = Intent.GetStringExtra("account") ?? string.Empty;
            to = DateTime.Now.Date;
            from = DateTime.Now.Date;
            paramDate.DateFrom = from;
            paramDate.DateTo = to;
            from_btn.Text = "From " + paramDate.DateFrom.ToString("dd/MM/yyyy");
            to_btn.Text = "To " + paramDate.DateTo.ToString("dd/MM/yyyy");
            Task.Run(async () =>
            {
                try
                {
                    items = await MKFApp.Current.GetStatement(paramDate);
                    this.RunOnUiThread(() => Success());
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    this.RunOnUiThread(() => Failed());
                }
            });
            gd.SetCornerRadius(10);
            gd.SetStroke(3, MainActivity.TOOLBAR_COLOR);
            from_btn.Background = gd;
            to_btn.Background = gd;
            from_btn.Click += delegate {
                from_to = 1;
                ShowDialog(FROM_DIALOG);
            };
            to_btn.Click += delegate {
                from_to = 0;
                ShowDialog(TO_DIALOG);
            };
            submit = FindViewById<Button>(Resource.Id.submit_btn);
            submit.SetTextColor(MainActivity.TOOLBAR_COLOR);
            submit.Click += Submit_Click;
            back_btn.Click += Back_Btn_Click;
            Task.Run(() =>
            {
                asd_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                asd_timer.Elapsed += Timer_Elapsed;
                asd_timer.Start();
            });
        }

        void Submit_Click(object sender, EventArgs e)
        {
            if(paramDate.DateFrom == null || paramDate.DateTo == null || paramDate.ToAcc.Equals("") || paramDate.FromAcc.Equals("") || paramDate.userMKF == null) {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Android.Graphics.Color.Red);
                from_btn.Background = gd;
                to_btn.Background = gd;
                Snackbar.Make(linearLayout, "An Error Occured. Please Select Dates.", Snackbar.LengthLong).Show();
            }
            else {
                progressBar.Visibility = ViewStates.Visible;
                Task.Run(async () =>
                {
                    try
                    {
                        items = await MKFApp.Current.GetStatement(paramDate);
                        this.RunOnUiThread(() => Success());
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        this.RunOnUiThread(() => Failed());
                    }
                });
            }
        }

        private void Success() {
            progressBar.Visibility = ViewStates.Gone;
            gd.SetCornerRadius(10);
            gd.SetStroke(3, MainActivity.TOOLBAR_COLOR);
            from_btn.Background = gd;
            to_btn.Background = gd;
            mRecyclerView.SetLayoutManager(mLayoutManager);
            recyclerViewASDAdapter = new RecyclerViewASDAdapter(items , MainActivity.user , this);
            mRecyclerView.SetAdapter(recyclerViewASDAdapter);
        }


        private void Failed() {
            progressBar.Visibility = ViewStates.Gone;
            gd.SetCornerRadius(10);
            gd.SetStroke(3, MainActivity.TOOLBAR_COLOR);
            from_btn.Background = gd;
            to_btn.Background = gd;
            Snackbar.Make(linearLayout, "An Error Occured.", Snackbar.LengthLong).Show();
        }


        protected override Dialog OnCreateDialog(int id)
        {
            switch(id) {
                case FROM_DIALOG:
                    return new DatePickerDialog(this, this, from_year, from_month, from_day);
                case TO_DIALOG:
                    return new DatePickerDialog(this, this, to_year, to_month, to_day);
                default:
                    break;
            }
            return null;
        }


        void Back_Btn_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            Task.Run(() => asd_timer.Stop());
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Task.Run(() => asd_timer.Stop());
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Task.Run(() =>
        //    {
        //        asd_timer.Stop();
        //        asd_timer = new Timer(INTERVAL);
        //        COUNTDOWN = INITIAL;
        //        asd_timer.Elapsed += Timer_Elapsed;
        //        asd_timer.Start();
        //    });
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Task.Run(() => asd_timer.Stop());
        //}

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                asd_timer.Stop();
                asd_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                asd_timer.Elapsed += Timer_Elapsed;
                asd_timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                asd_timer.Stop();
                asd_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                asd_timer.Elapsed += Timer_Elapsed;
                asd_timer.Start();
            });
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                asd_timer.Stop();
                asd_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                asd_timer.Elapsed += Timer_Elapsed;
                asd_timer.Start();
            });
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            COUNTDOWN--;
            if (COUNTDOWN == 0)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        asd_timer.Stop();
                        LoginResult loginResult = await MKFApp.Current.Logout();
                        this.RunOnUiThread(() => LogoutSuccessful());
                    }
                    catch (Exception exception)
                    {
                        this.RunOnUiThread(() => LogoutFailed());
                    }
                });
            }
        }

        public async void LogoutSuccessful()
        {
            if(!IsFinishing){
                ShowAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
            }
        }

        public void LogoutFailed()
        {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }

        private void ShowAlertDialog(String title, String message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetPositiveButton("OK", (sender, e) =>
            {
                Intent logout = new Intent(this, typeof(MainActivity));
                logout.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
                StartActivity(logout);
            });
            builder.Create().Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            if(from_to == 1) {
                this.from_year = year;
                this.from_month = month + 1;
                this.from_day = dayOfMonth;
                from_btn.Text = "From " + this.from_day + "/" + (this.from_month) + "/" + this.from_year;
                from = new DateTime(this.from_year, this.from_month, this.from_day).Date;
            } else {
                this.to_year = year;
                this.to_month = month + 1;
                this.to_day = dayOfMonth;
                to_btn.Text = "To " + this.to_day + "/" + this.to_month + "/" + this.to_year;
                to = new DateTime(this.to_year, this.to_month, this.to_day);
            }
            paramDate.DateFrom = from;
            paramDate.DateTo = to;
        }
    }
}