using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MKFLibrary;
using static Android.Support.V7.Widget.RecyclerView;

namespace lucid
{
    public class RecyclerViewASDAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public Context mContext;
        public MKFUser mUser;

        public RecyclerViewASDAdapter(MKFUser user , Context context)
        {
            mContext = context;
            mUser = user;
        }

        public override int ItemCount => throw new NotImplementedException();

        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}
