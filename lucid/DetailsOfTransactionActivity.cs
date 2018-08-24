
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

namespace lucid
{
    [Activity(Label = "DetailsOfTransactionActivity")]
    public class DetailsOfTransactionActivity : Activity
    {
        #region vars
        private ImageButton back_button;
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.details_of_transaction_layout);

            // Create your application here
            setUpVariables();
            back_button.Click += Back_Button_Click;
        }
        private void setUpVariables() {
            back_button = FindViewById<ImageButton>(Resource.Id.dot_back_btn);
        }

        void Back_Button_Click(object sender, EventArgs e)
        {
            Intent home = new Intent(this, typeof(HomeActivity));
            Bundle bndlanimation = ActivityOptions.MakeCustomAnimation(this, Resource.Drawable.animation, Resource.Drawable.animation2).ToBundle();
            StartActivity(home, bndlanimation);
        }

    }
}
