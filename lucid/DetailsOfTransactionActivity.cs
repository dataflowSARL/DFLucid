
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
    [Activity(Label = "DetailsOfTransactionActivity", ParentActivity = typeof(HomeActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "HomeActivity")]
    public class DetailsOfTransactionActivity : Activity, IOnDateSetListener
    {
        #region vars
        private ImageButton back_button;
        private LinearLayout linearLayout;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerViewDOTAdapter mAdapter;
        private List<Operations> mItems = new List<Operations>();
        private ProgressBar progressBar;
        private Button from_btn, to_btn, submit_btn;
        private ParamDate paramDate = new ParamDate();
        private GradientDrawable gd = new GradientDrawable() , gd_submit = new GradientDrawable();
        private DateTime from, to;
        private TextView nothing;

        private int from_year = DateTime.Now.Year , from_month = DateTime.Now.Month - 1, from_day = 1;
        private int to_year = DateTime.Now.Year, to_month = DateTime.Now.Month - 1, to_day = DateTime.Now.Day;
        private int from_to = 0;
        private const int FROM_DIALOG = 1 , TO_DIALOG = 0;

        private Timer dot_timer;
        private int COUNTDOWN = 5 * 60, INTERVAL = 1000, INITIAL = 5 * 60;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.details_of_transaction_layout);

            // Create your application here
            SetUpVariables();

        }

        //setup acitivity's views
        private void SetUpVariables() {
            nothing = FindViewById<TextView>(Resource.Id.nothing_dot);
            nothing.Visibility = ViewStates.Gone;
            progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar_dettails_of_transaction);
            progressBar.Visibility = ViewStates.Visible;
            paramDate.userMKF = MainActivity.user;
            linearLayout = FindViewById<LinearLayout>(Resource.Id.dot_linear_layout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.dot_toolbar);
            toolbar.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button = FindViewById<ImageButton>(Resource.Id.dot_back_btn);
            back_button.SetBackgroundColor(MainActivity.TOOLBAR_COLOR);
            back_button.Click += Back_Button_Click;
            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview_dot);
            mLayoutManager = new LinearLayoutManager(this);
            from_btn = FindViewById<Button>(Resource.Id.from_btn_dot);
            to_btn = FindViewById<Button>(Resource.Id.to_btn_dot);
            gd_submit.SetCornerRadius(10);
            gd_submit.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            gd_submit.SetColor(Android.Graphics.Color.ParseColor("#47555e"));
            gd.SetCornerRadius(10);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            from_btn.Background = gd;
            to_btn.Background = gd;
            submit_btn = FindViewById<Button>(Resource.Id.submit_btn_dot);
            submit_btn.Background = gd_submit;
            submit_btn.Click += Submit_Btn_Click;
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
                try {
                    mItems = await MKFApp.Current.GetOperations(paramDate);
                    this.RunOnUiThread(() => Success());
                } catch(Exception exception) {
                    Console.Write(exception);
                    this.RunOnUiThread(() => Failed());
                }
            });
            Task.Run(() =>
            {
                dot_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                dot_timer.Elapsed += Timer_Elapsed;
                dot_timer.Start();
            });
        }

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

        // data retrieved successfully
        private void Success(){
            progressBar.Visibility = ViewStates.Gone;
            gd.SetCornerRadius(10);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            from_btn.Background = gd;
            to_btn.Background = gd;
            if (mItems.Count == 0)
            {
                nothing.Visibility = ViewStates.Visible;
            } else {
                nothing.Visibility = ViewStates.Gone;
            }
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerViewDOTAdapter(mItems, this, MainActivity.user);
            mRecyclerView.SetAdapter(mAdapter);
        }

        // data failed to be retrieved
        private void Failed() {
            nothing.Visibility = ViewStates.Visible;
            progressBar.Visibility = ViewStates.Gone;
            gd.SetCornerRadius(10);
            gd.SetStroke(3, Android.Graphics.Color.ParseColor("#47555e"));
            from_btn.Background = gd;
            to_btn.Background = gd;
            Snackbar.Make(linearLayout, "An Error Occured.", Snackbar.LengthLong).Show();
        }

        //submit date range button
        void Submit_Btn_Click(object sender, EventArgs e)
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
                        mItems = await MKFApp.Current.GetOperations(paramDate);
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

        //returns to parent activity
        void Back_Button_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
            Task.Run(() => dot_timer.Stop());
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Task.Run(() => dot_timer.Stop());
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Task.Run(() =>
        //    {
        //        dot_timer.Stop();
        //        dot_timer = new Timer(INTERVAL);
        //        COUNTDOWN = INITIAL;
        //        dot_timer.Elapsed += Timer_Elapsed;
        //        dot_timer.Start();
        //    });
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Task.Run(() => dot_timer.Stop());
        //}

        protected override void OnStart()
        {
            base.OnStart();
            Task.Run(() =>
            {
                dot_timer.Stop();
                dot_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                dot_timer.Elapsed += Timer_Elapsed;
                dot_timer.Start();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task.Run(() =>
            {
                dot_timer.Stop();
                dot_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                dot_timer.Elapsed += Timer_Elapsed;
                dot_timer.Start();
            });
        }

        //detects user interaction
        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            Task.Run(() =>
            {
                dot_timer.Stop();
                dot_timer = new Timer(INTERVAL);
                COUNTDOWN = INITIAL;
                dot_timer.Elapsed += Timer_Elapsed;
                dot_timer.Start();
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
                        dot_timer.Stop();
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

        //select dates on calendar
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
