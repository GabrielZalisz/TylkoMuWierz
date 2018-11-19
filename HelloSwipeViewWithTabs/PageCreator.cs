using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace HelloSwipeViewWithTabs
{
    class PageCreator
    {
        Context c;

        public PageCreator(Context c)
        {
            this.c = c;
        }

        public View CreatePage1(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_1, container, false);
            return v;
        }

        public View CreatePage2(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_2, container, false);
            var listView = v.FindViewById<ListView>(Resource.Id.listView1);
            var items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Test1", "Test2", "Test3", "Test4" };
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(c, Android.Resource.Layout.SimpleListItem1, items);
            listView.Adapter = adapter;
            listView.ItemClick += ListView_ItemClick;
            return v;
        }

        public View CreatePage3(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_3, container, false);
            var tvNadpis = v.FindViewById<TextView>(Resource.Id.tvNadpis);
            return v;
        }

        public View CreatePage4(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_4, container, false);
            var btn = v.FindViewById<Button>(Resource.Id.button1);
            btn.Click += Btn_Click;
            var sw = v.FindViewById<Switch>(Resource.Id.switch1);
            sw.CheckedChange += Sw_CheckedChange;
            var sw1 = v.FindViewById<Switch>(Resource.Id.switch2);
            sw1.CheckedChange += Sw_CheckedChange1;
            return v;
        }

        private void Sw_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.switch1);
            if (sw.Checked)
            {
                Nastaveni.SnackBar = true;
            }
            else
            {
                Nastaveni.SnackBar = false;
            }
        }

        private void Sw_CheckedChange1(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.switch2);
            if (sw.Checked)
            {
                Nastaveni.Red = true;
            }
            else
            {
                Nastaveni.Red = false;
            }
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Toast.MakeText(c, "Hello!", ToastLength.Short).Show();
            MainActivity.MyPager.SetCurrentItem(2, true);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            View view = (View)sender;
            if (Nastaveni.SnackBar)
                Snackbar.Make(view, "Klik", Snackbar.LengthLong).SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            else
                Toast.MakeText(c, "Klik!", ToastLength.Short).Show();
        }
    }
}