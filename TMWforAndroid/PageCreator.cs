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
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace TylkoMuWierz
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
                MainActivity.MySearchView.SetIconifiedByDefault(false);
                //MainActivity.MySearchView.SetQueryHint("Szukaj...");
                //HideKeyboard();
                //MainActivity.MySearchView.OnActionViewCollapsed();
            }
            else
            {
                int num;
                if (int.TryParse(query, out num))
                {
                    DataManager.SongsToDisplay = DataManager.Songbook.Where(q => q.Numer == num).ToList();
                }
                else
                {
                    DataManager.SongsToDisplay = DataManager.Songbook.Where(q => q.Tytul.ToLower().Contains(query) || q.Slowa.Replace("\n", " ").Replace("  ", " ").ToLower().Contains(query)).ToList();
                }
            }
            RefreshListView();
        }

        private void Fab_Click(object sender, EventArgs e)
        {
            Nastaveni.Alphabetically = !Nastaveni.Alphabetically;
            if (Nastaveni.Alphabetically)
            {
                MainActivity.MyFab.SetImageDrawable(ContextCompat.GetDrawable(c, Resource.Drawable.sort_by_numeric_order));
                DataManager.Songbook = DataManager.Songbook.OrderBy(q => q.Tytul).ToList();
                DataManager.SongsToDisplay = DataManager.SongsToDisplay.OrderBy(q => q.Tytul).ToList();
            }
            else
            {
                MainActivity.MyFab.SetImageDrawable(ContextCompat.GetDrawable(c, Resource.Drawable.sort_by_alphabet));
                DataManager.Songbook = DataManager.Songbook.OrderBy(q => q.Numer).ToList();
                DataManager.SongsToDisplay = DataManager.SongsToDisplay.OrderBy(q => q.Numer).ToList();
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
            View v = inflater.Inflate(Resource.Layout.fragment_page_3_new, container, false);

            var scrlv = v.FindViewById<ScrollView>(Resource.Id.scrollViewSong);
            MainActivity.MyScrollView = scrlv;
            scrlv.Click += Scrlv_Click;

            return v;
        }

        private void Scrlv_Click(object sender, EventArgs e)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Klik", Snackbar.LengthLong).SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public View CreatePage4(LayoutInflater inflater, ViewGroup container)
        {
            View v = inflater.Inflate(Resource.Layout.fragment_page_4, container, false);

            var btnShareSong = v.FindViewById<Button>(Resource.Id.btnShareSong);
            btnShareSong.Click += BtnShareSong_Click;

            var btnShareApp = v.FindViewById<Button>(Resource.Id.btnShareApp);
            btnShareApp.Click += BtnShareApp_Click;

            var btnMyBible = v.FindViewById<Button>(Resource.Id.btnMyBible);
            btnMyBible.Click += BtnMyBible_Click;
            
            var btnRadio = v.FindViewById<Button>(Resource.Id.btnRadio);
            btnRadio.Click += BtnRadio_Click;

            var btnSpiewajPanu = v.FindViewById<Button>(Resource.Id.btnSpiewajPanu);
            btnSpiewajPanu.Click += BtnSpiewajPanu_Click;

            //TextView textView = v.FindViewById<TextView>(Resource.Id.tvGitHub);
            //textView.Clickable = true;
            //textView.MovementMethod = LinkMovementMethod.Instance;
            //string text = "<a href='https://github.com/gabzdyl/test_songbook'>GitHub</a>";
            //textView.SetText("GitHub", TextView.BufferType.Normal);

            //na zrušení
            //var sw = v.FindViewById<Switch>(Resource.Id.switch1);
            //sw.CheckedChange += Sw_CheckedChange;
            //var sw1 = v.FindViewById<Switch>(Resource.Id.switch2);
            //sw1.CheckedChange += Sw_CheckedChange1;
            //var btn = v.FindViewById<Button>(Resource.Id.button1);
            //btn.Click += Btn_Click;


            var swChorusMany = v.FindViewById<Switch>(Resource.Id.swChorusMany);
            if (Nastaveni.ChorusMany)
                swChorusMany.Checked = true;
            swChorusMany.CheckedChange += SwChorusMany_CheckedChange;

            var swBigFont = v.FindViewById<Switch>(Resource.Id.swBigFont);
            if (Nastaveni.BigFont)
                swBigFont.Checked = true;
            swBigFont.CheckedChange += SwBigFont_CheckedChange;

            var swCenter = v.FindViewById<Switch>(Resource.Id.swCenter);
            if (Nastaveni.Center)
                swCenter.Checked = true;
            swCenter.CheckedChange += SwCenter_CheckedChange;

            var swLineBreaks = v.FindViewById<Switch>(Resource.Id.swLineBreaks);
            if (Nastaveni.NoLineBreaks)
                swLineBreaks.Checked = true;
            swLineBreaks.CheckedChange += SwLineBreaks_CheckedChange;




            var swHideHeader = v.FindViewById<Switch>(Resource.Id.swHideHeader);
            if (Nastaveni.HideHeader)
                swHideHeader.Checked = true;
            swHideHeader.CheckedChange += SwHideHeader_CheckedChange;

            var swStatusBar = v.FindViewById<Switch>(Resource.Id.swHideStatusBar);
            if (Nastaveni.HideStatusBar)
                swStatusBar.Checked = true;
            swStatusBar.CheckedChange += SwStatusBar_CheckedChange;

            var swLockPortrait = v.FindViewById<Switch>(Resource.Id.swLockPortrait);
            if (Nastaveni.NoRotate)
                swLockPortrait.Checked = true;
            swLockPortrait.CheckedChange += SwLockPortrait_CheckedChange;



            return v;
        }

        private void SwChorusMany_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swChorusMany);
            if (sw.Checked)
            {
                Nastaveni.ChorusMany = true;
                Nastaveni.SaveSetting("ChorusMany", true);
            }
            else
            {
                Nastaveni.ChorusMany = false;
                Nastaveni.SaveSetting("ChorusMany", false);
            }
        }

        private void SwBigFont_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swBigFont);
            if (sw.Checked)
            {
                Nastaveni.BigFont = true;
                Nastaveni.SaveSetting("BigFont", true);
            }
            else
            {
                Nastaveni.BigFont = false;
                Nastaveni.SaveSetting("BigFont", false);
            }
        }

        private void SwCenter_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swCenter);
            if (sw.Checked)
            {
                Nastaveni.Center = true;
                Nastaveni.SaveSetting("Center", true);
            }
            else
            {
                Nastaveni.Center = false;
                Nastaveni.SaveSetting("Center", false);
            }
        }

        private void SwLineBreaks_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swLineBreaks);
            if (sw.Checked)
            {
                Nastaveni.NoLineBreaks = true;
                Nastaveni.SaveSetting("NoLineBreaks", true);
            }
            else
            {
                Nastaveni.NoLineBreaks = false;
                Nastaveni.SaveSetting("NoLineBreaks", false);
            }
        }

        private void SwHideHeader_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swHideHeader);
            if (sw.Checked)
            {
                Nastaveni.HideHeader = true;
                Nastaveni.SaveSetting("HideHeader", true);
            }
            else
            {
                Nastaveni.HideHeader = false;
                Nastaveni.SaveSetting("HideHeader", false);
            }
        }

        private void SwStatusBar_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swHideStatusBar);
            if (sw.Checked)
            {
                Nastaveni.HideStatusBar = true;
                Nastaveni.SaveSetting("HideStatusBar", true);
            }
            else
            {
                Nastaveni.HideStatusBar = false;
                Nastaveni.SaveSetting("HideStatusBar", false);
            }
        }

        private void SwLockPortrait_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var sw = PageFragment.view4.FindViewById<Switch>(Resource.Id.swLockPortrait);
            if (sw.Checked)
            {
                Nastaveni.NoRotate = true;
                //neukládám: do konce aplikace nechám to co je aktuálně
                //Nastaveni.SaveSetting("LockPortrait", true);
            }
            else
            {
                Nastaveni.NoRotate = false;
                //Nastaveni.SaveSetting("LockPortrait", false);
            }
        }

        private void BtnShareSong_Click(object sender, EventArgs e)
        {
            if (Nastaveni.SelectedSong == null)
            {
                View view = (View)sender;
                Snackbar.Make(view, "Nie jest wybrana żadna pieśń.", Snackbar.LengthLong).SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                return;
            }
            try
            {
                string title = Nastaveni.SelectedSong.Tytul;
                string dur = Nastaveni.SelectedSong.Tonacja;
                int numer = Nastaveni.SelectedSong.Numer;

                bool pomb = Nastaveni.Center;
                Nastaveni.Center = false;
                string slowa = Nastaveni.SelectedSong.SlowaToDisplay;
                Nastaveni.Center = pomb;

                Intent i = new Intent(Intent.ActionSend);
                i.SetType("text/plain");
                i.PutExtra(Intent.ExtraSubject, "TMW " + numer.ToString());
                i.PutExtra(Intent.ExtraText, title + " (" + dur + ")\n\n" + slowa);
                MainActivity.MyActivity.StartActivity(Intent.CreateChooser(i, "Udostępnij pieśń " + title));
            }
            catch (Exception ee)
            {
                //ee.toString();
            }
        }

        private void BtnShareApp_Click(object sender, EventArgs e)
        {
            try
            {
                Intent i = new Intent(Intent.ActionSend);
                i.SetType("text/plain");
                i.PutExtra(Intent.ExtraSubject, "Tylko Mu Wierz");
                i.PutExtra(Intent.ExtraText, "Polecam aplikację Tylko Mu Wierz dla Androida: https://play.google.com/store/apps/details?id=com.xamarin.songbook.tmw");
                MainActivity.MyActivity.StartActivity(Intent.CreateChooser(i, "Udostępnij aplikację Tylko Mu Wierz"));
            }
            catch (Exception ee)
            {
                //ee.toString();
            }
        }

        private void BtnMyBible_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=ua.mybible&hl=en");
            var intent = new Intent(Intent.ActionView, uri);
            MainActivity.MyActivity.StartActivity(intent);
        }

        private void BtnRadio_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("http://radio.zapraszamy.pl/");
            var intent = new Intent(Intent.ActionView, uri);
            MainActivity.MyActivity.StartActivity(intent);
        }

        private void BtnSpiewajPanu_Click(object sender, EventArgs e)
        {
            var uri = Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=com.kupka.bku.piewajpanu&hl=en_US");
            var intent = new Intent(Intent.ActionView, uri);
            MainActivity.MyActivity.StartActivity(intent);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Nastaveni.SelectedSong = DataManager.SongsToDisplay[e.Position];
            MainActivity.MyScrollView.ScrollTo(0, 0);
            MainActivity.MyPager.SetCurrentItem(1, true);
            //MainActivity.MySearchView.OnActionViewCollapsed();
            HideKeyboard();

        }

        public static void HideKeyboard()
        {
            View view = MainActivity.MyActivity.CurrentFocus;
            if (view != null)
            {
                InputMethodManager imm = (InputMethodManager)MainActivity.MyActivity.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(view.WindowToken, 0);
            }
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