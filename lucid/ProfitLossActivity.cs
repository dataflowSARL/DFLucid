
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MarketFlow;
using MKFLibrary;
using static Android.App.DatePickerDialog;
using Toolbar = Android.Widget.Toolbar;

namespace lucid
{
    [Activity(Label = "ProfitLossActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class ProfitLossActivity : Activity, IOnDateSetListener
    {
        #region vars
        private ImageButton back_button;
        private LinearLayout linearLayout;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewPLAdapter mAdapter;
        private List<ClosedOperations> items = new List<ClosedOperations>();
        private ProgressBar progressBar;
        private Button from_btn, to_btn, submit;
        private ParamDate paramDate = new ParamDate();
        private GradientDrawable gd = new GradientDrawable(), gd_submit = new GradientDrawable();
        private DateTime from, to;
        private TextView nothing;

        private int from_year = DateTime.Now.Year, from_month = DateTime.Now.Month - 1, from_day = 1;
        private int to_year = DateTime.Now.Year, to_month = DateTime.Now.Month - 1, to_day = DateTime.Now.Day;
        private int from_to = 0;
        private const int FROM_DIALOG = 1, TO_DIALOG = 0;

        private Timer pl_timer;
        private int COUNTDOWN = 5 * 60, INTERVAL = 1000, INITIAL = 5 * 60;

        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profit_loss_layout);
            // Create your application here
            setUpVariables();
        }


        //setup activity's views
        private void setUpVariables() {
            nothing = FindViewById<TextView>(Resource.Id.nothing_pl);
            nothing.Visibility = ViewStates.Gone;
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_profit_loss);
            progressBar.Visibility = ViewStates.Visible;
            paramDate.userMKF = MainActivity.user;
            linearLayout = FindViewById<LinearLayout>(Resource.Id.pl_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.pl_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button = FindViewById<ImageButton>(Resource.Id.pl_back_btn);
            back_button.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button.Click += Back_Button_Click;
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview_pl);
            mLayoutManager = new LinearLayoutManager(this);
            from_btn = FindViewById<Button>(Resource.Id.from_btn_pl);
            to_btn = FindViewById<Button>(Resource.Id.to_btn_pl);
            gd.SetCornerRadius(10);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            from_btn.Background = gd;
            to_btn.Background = gd;
            to = DateTime.Now.Date;
            from = DateTime.Now.Date;
            paramDate.DateFrom = from;
            paramDate.DateTo = to;
            from_btn.Text = paramDate.DateFrom.ToString("dd/MM/yyyy");
            to_btn.Text = paramDate.DateTo.ToString("dd/MM/yyyy");
            from_btn.Click += delegate {
                from_to = 1;
                ShowDialog(FROM_DIALOG);
            };
            to_btn.Click += delegate {
                from_to = 0;
                ShowDialog(TO_DIALOG);
            };
            Task.Run(async () =>
            {
                try
                {
                    items = await MKFApp.Current.GetRealisedProfits(paramDate);
                    this.RunOnUiThread(() => Success());
                }
                catch (Exception exception)
                {
                    Console.Write(exception);
                    this.RunOnUiThread(() => Failed());
                }
            });
            submit = FindViewById<Button>(Resource.Id.submit_btn_pl);
            gd_submit.SetCornerRadius(10);
            gd_submit.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            gd_submit.SetColor(Android.Graphics.Color.ParseColor("#47555e"));
            submit.Background = gd_submit;
            submit.Click += Submit_Click;
            Task.Run(() =>
            {
                pl_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                pl_timer.Elapsed += Timer_Elapsed;
                pl_timer.Start();
            });
        }

        // submit date range for data retrieval
        void Submit_Click(object sender, EventArgs e)
        {
            if (paramDate.DateFrom == null || paramDate.DateTo == null || paramDate.userMKF == null)
            {
                gd.SetCornerRadius(10);
                gd.SetStroke(3, Android.Graphics.Color.Red);
                from_btn.Background = gd;
                to_btn.Background = gd;
                Snackbar.Make(linearLayout, "An Error Occured. Please Select Dates.", Snackbar.LengthLong).Show();
            }
            else
            {
                nothing.Visibility = ViewStates.Gone;
                progressBar.Visibility = ViewStates.Visible;
                Task.Run(async () =>
                {
                    try
                    {
                        items = await MKFApp.Current.GetRealisedProfits(paramDate);
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

        //data retrieved successfully
        private void Success()
        {
            progressBar.Visibility = ViewStates.Gone;
            gd.SetCornerRadius(10);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            from_btn.Background = gd;
            to_btn.Background = gd;
            if (items.Count == 0)
            {
                nothing.Visibility = ViewStates.Visible;
            }
            else
            {
                nothing.Visibility = ViewStates.Gone;
            }
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerViewPLAdapter(items, this, MainActivity.user);
            mRecyclerView.SetAdapter(mAdapter);
        }

        //data failed to retrieve
        private void Failed()
        {
            nothing.Visibility = ViewStates.Visible;
            progressBar.Visibility = ViewStates.Gone;
            gd.SetCornerRadius(10);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            from_btn.Background = gd;
            to_btn.Background = gd;
            Snackbar.Make(linearLayout, "An Error Occured.", Snackbar.LengthLong).Show();
        }

        //shows calendar dialog
        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case FROM_DIALOG:
                    return new DatePickerDialog(this, this, from_year, from_month, from_day);
                case TO_DIALOG:
                    return new DatePickerDialog(this, this, to_year, to_month, to_day);
                default:
                    break;
            }
            return null;
        }

        //returns to parent activity
        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            Task.Run(() => pl_timer.Stop());
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Task.Run(() => pl_timer.Stop());
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Task.Run(() =>
        //    {
        //        pl_timer.Stop();
        //        pl_timer = new Timer(INTERVAL);
        //        COUNTDOWN = INITIAL;
        //        pl_timer.Elapsed += Timer_Elapsed;
        //        pl_timer.Start();
        //    });
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Task.Run(() => pl_timer.Stop());
        //}

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                pl_timer.Stop();
                pl_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                pl_timer.Elapsed += Timer_Elapsed;
                pl_timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                pl_timer.Stop();
                pl_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                pl_timer.Elapsed += Timer_Elapsed;
                pl_timer.Start();
            });
        }

        //detects user interaction
        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                pl_timer.Stop();
                pl_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                pl_timer.Elapsed += Timer_Elapsed;
                pl_timer.Start();
            });
        }

        //timer ticks
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            COUNTDOWN--;
            if (COUNTDOWN == 0)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        pl_timer.Stop();
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

        //logout -> inactivity
        public void LogoutSuccessful()
        {
            if (!IsFinishing)
            {
                ShowAlertDialog(HomeActivity.DIALOG_TITLE, HomeActivity.DIALOG_MESSAGE);
            }
        }

        //logout failed
        public void LogoutFailed()
        {
            Snackbar.Make(linearLayout, "An error occured", Snackbar.LengthLong).Show();
        }


        //alert dialog shows up due to inactivity
        private void ShowAlertDialog(String title, String message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Application.Context);
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

        //gets the dates from calendar
        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            if (from_to == 1)
            {
                this.from_year = year;
                this.from_month = month + 1;
                this.from_day = dayOfMonth;
                from = new DateTime(this.from_year, this.from_month, this.from_day).Date;
                from_btn.Text = from.ToString("dd/MM/yyyy");
            }
            else
            {
                this.to_year = year;
                this.to_month = month + 1;
                this.to_day = dayOfMonth;
                to = new DateTime(this.to_year, this.to_month, this.to_day);
                to_btn.Text = to.ToString("dd/MM/yyyy");
            }
            paramDate.DateFrom = from;
            paramDate.DateTo = to;
        }

    }
}
