using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
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
            Refresh(listView);

            //var btn123 = v.FindViewById<Button>(Resource.Id.btn123);
            //var btnABC = v.FindViewById<Button>(Resource.Id.btnABC);
            var fab = v.FindViewById<FloatingActionButton>(Resource.Id.fab);

            //btn123.Click += Btn123_Click;
            //btnABC.Click += BtnABC_Click;
            fab.Click += Fab_Click;

            MainActivity.MyFab = fab;

            return v;
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Nastaveni.Alphabetically = !Nastaveni.Alphabetically;
            if (Nastaveni.Alphabetically)
            {
                MainActivity.MyFab.SetImageDrawable(ContextCompat.GetDrawable(c, Resource.Drawable.sort_by_numeric_order));
                DataManager.Songs = DataManager.Songs.OrderBy(q => q.Tytul).ToList();
            }
            else
            {
                MainActivity.MyFab.SetImageDrawable(ContextCompat.GetDrawable(c, Resource.Drawable.sort_by_alphabet));
                DataManager.Songs = DataManager.Songs.OrderBy(q => q.Numer).ToList();
            }
            Refresh(MainActivity.MyListView);
        }

        void Refresh(ListView listView)
        {
            MyAdapter<Song> adapter = new MyAdapter<Song>(c, Android.Resource.Layout.SimpleListItem1, DataManager.Songs);
            if (MainActivity.MyAdapter != null)
                MainActivity.MyAdapter.Clear();
            listView.Adapter = adapter;
            listView.ItemClick += ListView_ItemClick;
            MainActivity.MyAdapter = adapter;
            MainActivity.MyListView = listView;
        }

        private void Btn123_Click(object sender, EventArgs e)
        {
            DataManager.Songs = DataManager.Songs.OrderBy(q => q.Numer).ToList();
            Refresh(MainActivity.MyListView);
        }

        private void BtnABC_Click(object sender, EventArgs e)
        {
            DataManager.Songs = DataManager.Songs.OrderBy(q => q.Tytul).ToList();
            Refresh(MainActivity.MyListView);
        }

        public View CreatePage3(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_3, container, false);
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
            Nastaveni.SongIndex = e.Position;
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