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

            var btnTMW = v.FindViewById<Button>(Resource.Id.btnTMW);
            btnTMW.Click += BtnTMW_Click;
            var btnPielgrzym = v.FindViewById<Button>(Resource.Id.btnPielgrzym);
            btnPielgrzym.Click += BtnPielgrzym_Click;
            var btnMlZp = v.FindViewById<Button>(Resource.Id.btnMlZp);
            btnMlZp.Click += BtnMlZp_Click;

            return v;
        }

        private void BtnTMW_Click(object sender, EventArgs e)
        {
            Nastaveni.SelectedFolder = "Tylko Mu Wierz";
            DataManager.Songbook = DataManager.AllSongs.Where(q => q.Folder == Nastaveni.SelectedFolder).ToList();
            DataManager.SongsToDisplay = DataManager.Songbook;
            MainActivity.MyPager.SetCurrentItem(1, true);
        }

        private void BtnPielgrzym_Click(object sender, EventArgs e)
        {
            Nastaveni.SelectedFolder = "Śpiewnik Pielgrzyma";
            DataManager.Songbook = DataManager.AllSongs.Where(q => q.Folder == Nastaveni.SelectedFolder).ToList();
            DataManager.SongsToDisplay = DataManager.Songbook;
            MainActivity.MyPager.SetCurrentItem(1, true);
        }

        private void BtnMlZp_Click(object sender, EventArgs e)
        {
            Nastaveni.SelectedFolder = "Mládežnický zpěvník";
            DataManager.Songbook = DataManager.AllSongs.Where(q => q.Folder == Nastaveni.SelectedFolder).ToList();
            DataManager.SongsToDisplay = DataManager.Songbook;
            MainActivity.MyPager.SetCurrentItem(1, true);
        }

        public View CreatePage2(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_2, container, false);

            var listView = v.FindViewById<ListView>(Resource.Id.listView1);
            listView.ItemClick += ListView_ItemClick;
            MainActivity.MyListView = listView;
            RefreshListView();

            var fab = v.FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += Fab_Click;
            MainActivity.MyFab = fab;

            var sv = v.FindViewById<SearchView>(Resource.Id.searchView1);
            sv.QueryTextChange += Sv_QueryTextChange;
            MainActivity.MySearchView = sv;


            return v;
        }

        private void Sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            string query = MainActivity.MySearchView.Query.ToLower();
            if (string.IsNullOrWhiteSpace(MainActivity.MySearchView.Query))
            {
                DataManager.SongsToDisplay = DataManager.Songbook;
            }
            else
            {
                DataManager.SongsToDisplay = DataManager.Songbook.Where(q => q.Tytul.ToLower().Contains(query) || q.Slowa.ToLower().Contains(query)).ToList();
            }
            RefreshListView();
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Nastaveni.Alphabetically = !Nastaveni.Alphabetically;
            if (Nastaveni.Alphabetically)
            {
                MainActivity.MyFab.SetImageDrawable(ContextCompat.GetDrawable(c, Resource.Drawable.sort_by_numeric_order));
                DataManager.SongsToDisplay = DataManager.Songbook.OrderBy(q => q.Tytul).ToList();
            }
            else
            {
                MainActivity.MyFab.SetImageDrawable(ContextCompat.GetDrawable(c, Resource.Drawable.sort_by_alphabet));
                DataManager.SongsToDisplay = DataManager.Songbook.OrderBy(q => q.Numer).ToList();
            }
            RefreshListView();
        }

        public static void RefreshListView()
        {
            MyAdapter<Song> adapter = new MyAdapter<Song>(MainActivity.MyContext, Android.Resource.Layout.SimpleListItem1, DataManager.SongsToDisplay);
            if (MainActivity.MyAdapter != null)
                MainActivity.MyAdapter.Clear();
            MainActivity.MyListView.Adapter = adapter;
            MainActivity.MyAdapter = adapter;
        }

        public View CreatePage3(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_3, container, false);

            var scrlv = v.FindViewById<ScrollView>(Resource.Id.scrollViewSong);
            MainActivity.MyScrollView = scrlv;

            return v;
        }

        public View CreatePage4(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_4, container, false);

            var btnMyBible = v.FindViewById<Button>(Resource.Id.btnMyBible);
            btnMyBible.Click += BtnMyBible_Click;

            //na zrušení
            //var sw = v.FindViewById<Switch>(Resource.Id.switch1);
            //sw.CheckedChange += Sw_CheckedChange;
            //var sw1 = v.FindViewById<Switch>(Resource.Id.switch2);
            //sw1.CheckedChange += Sw_CheckedChange1;
            //var btn = v.FindViewById<Button>(Resource.Id.button1);
            //btn.Click += Btn_Click;


            var swChorusMany = v.FindViewById<Switch>(Resource.Id.swChorusMany);
            swChorusMany.CheckedChange += SwChorusMany_CheckedChange;
            var swBigFont = v.FindViewById<Switch>(Resource.Id.swBigFont);
            swBigFont.CheckedChange += SwBigFont_CheckedChange;
            var swCenter = v.FindViewById<Switch>(Resource.Id.swCenter);
            swCenter.CheckedChange += SwCenter_CheckedChange;
            var swLineBreaks = v.FindViewById<Switch>(Resource.Id.swLineBreaks);
            swLineBreaks.CheckedChange += SwLineBreaks_CheckedChange;

            return v;
        }

        private void SwChorusMany_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swChorusMany);
            if (sw.Checked)
            {
                Nastaveni.ChorusMany = true;
            }
            else
            {
                Nastaveni.ChorusMany = false;
            }
        }

        private void SwBigFont_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swBigFont);
            if (sw.Checked)
            {
                Nastaveni.BigFont = true;
            }
            else
            {
                Nastaveni.BigFont = false;
            }
        }

        private void SwCenter_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swCenter);
            if (sw.Checked)
            {
                Nastaveni.Center = true;
            }
            else
            {
                Nastaveni.Center = false;
            }
        }

        private void SwLineBreaks_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swLineBreaks);
            if (sw.Checked)
            {
                Nastaveni.NoLineBreaks = true;
            }
            else
            {
                Nastaveni.NoLineBreaks = false;
            }
        }

        private void BtnMyBible_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=ua.mybible&hl=en");
            var intent = new Intent(Intent.ActionView, uri);
            Activity a = new Activity();
            a.StartActivity(intent);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Nastaveni.SelectedSong = DataManager.SongsToDisplay[e.Position];
            MainActivity.MyScrollView.ScrollTo(0, 0);
            MainActivity.MyPager.SetCurrentItem(2, true);
            MainActivity.MySearchView.OnActionViewCollapsed();
        }

        //private void Btn_Click(object sender, EventArgs e)
        //{
        //    View view = (View)sender;
        //    if (Nastaveni.SnackBar)
        //        Snackbar.Make(view, "Klik", Snackbar.LengthLong).SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        //    else
        //        Toast.MakeText(c, "Klik!", ToastLength.Short).Show();
        //}
    }
}