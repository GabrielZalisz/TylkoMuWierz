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

namespace HelloSwipeViewWithTabs
{
    public class MyAdapter<T> : ArrayAdapter
    {
        T[] prvky;

        public MyAdapter(Context context, int textViewResourceId, T[] objects) : base(context, textViewResourceId, objects)
        {
            prvky = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //return base.GetView(position, convertView, parent);
            View v = LayoutInflater.From(MainActivity.MyContext).Inflate(Resource.Layout.list_item, null);
            TextView tv = v.FindViewById<TextView>(Resource.Id.textView111);
            tv.Text = prvky[position].ToString();
            return v;
        }
    }
}