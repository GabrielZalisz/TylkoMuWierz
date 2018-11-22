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
        IList<T> prvky;

        public MyAdapter(Context context, int textViewResourceId, IList<T> objects) : base(context, textViewResourceId, objects.ToArray())
        {
            prvky = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //return base.GetView(position, convertView, parent);
            View v = LayoutInflater.From(MainActivity.MyContext).Inflate(Resource.Layout.list_item, null);
            TextView tvNumer = v.FindViewById<TextView>(Resource.Id.tvNumer);
            TextView tvTytul = v.FindViewById<TextView>(Resource.Id.tvTytul);
            //TextView tvTonacja = v.FindViewById<TextView>(Resource.Id.tvTonacja);
            tvNumer.Text = (prvky[position] as Song).Numer.ToString();
            tvTytul.Text = (prvky[position] as Song).Tytul;
            //tvTonacja.Text = string.IsNullOrWhiteSpace((prvky[position] as Song).Tonacja) ? "" : "(" + (prvky[position] as Song).Tonacja + ")";
            return v;
        }
    }
}