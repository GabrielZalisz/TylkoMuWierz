using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;

namespace TylkoMuWierz
{
    static class Nastaveni
    {
        //dočasné

        //app
        private static bool _hideHeader;

        public static bool HideHeader
        {
            get { return _hideHeader; }
            set
            {
                _hideHeader = value;
                if (value)
                {
                    MainActivity.MyTabLayout.Visibility = ViewStates.Gone;
                }
                else
                {
                    MainActivity.MyTabLayout.Visibility = ViewStates.Visible;
                }
            }
        }
        private static bool _hideStatusBar;

        public static bool HideStatusBar
        {
            get { return _hideStatusBar; }
            set
            {
                _hideStatusBar = value;
                if (value)
                {
                    MainActivity.MyActivity.Window.AddFlags(WindowManagerFlags.Fullscreen);
                }
                else
                {
                    MainActivity.MyActivity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
                }
            }
        }

        private static bool _lockPortrait;

        public static bool NoRotate
        {
            get { return _lockPortrait; }
            set
            {
                Android.Content.Res.Orientation ooo = MainActivity.MyActivity.Resources.Configuration.Orientation;
                _lockPortrait = value;
                if (value)
                {
                    if (ooo == Android.Content.Res.Orientation.Portrait)
                        MainActivity.MyActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
                    else
                        MainActivity.MyActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
                }
                else
                {
                    MainActivity.MyActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Unspecified;
                }
            }
        }



        //view2

        public static bool Alphabetically { get; set; }

        //view3

        public static Song SelectedSong
        {
            get;
            set;
        }
        public static bool Center { get; set; }
        public static bool BigFont { get; set; }//small=18dp, big=22dp
        public static bool ChorusMany { get; set; }
        public static bool NoLineBreaks { get; set; }
        public static bool ChorusItalic { get; set; }






        private static string _folder = "Śpiewnik Tylko Mu Wierz";

        public static string SelectedFolder
        {
            get { return _folder; }
            set { _folder = value; }
        }

        public static bool Loaded { get; internal set; }









        //public static void SetView1()
        //{
        //    if (PageFragment.view1 == null)
        //        return;

        //    MainActivity.MySearchView.OnActionViewCollapsed();
        //}

        static string last_songbook;

        public static void SetView2()
        {
            if (PageFragment.view2 == null)
                return;

            if (SelectedFolder != last_songbook)
            {
                PageCreator.RefreshListView();
                last_songbook = SelectedFolder;
            }

            MainActivity.MyActivity.Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
        }

        public static void SetView3()
        {
            if (PageFragment.view3 == null)
                return;

            if (Nastaveni.SelectedSong == null)
            {
                Toast.MakeText(MainActivity.MyContext, "Błąd!", ToastLength.Short).Show();
                return;
            }

            TextView tvTytul = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvTytul);
            tvTytul.Text = Nastaveni.SelectedSong.Tytul;

            TextView tvNumer = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvNumer);
            tvNumer.Text = Nastaveni.SelectedSong.Numer.ToString();

            TextView tvTonacja = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvTonacja);
            tvTonacja.Text = Nastaveni.SelectedSong.Tonacja;

            TextView tvSlowa = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvSlowa);
            if (Nastaveni.ChorusItalic)
            {
                tvSlowa.Text = Nastaveni.SelectedSong.SlowaToDisplay;
            }
            else
            {
                //SpannableString sss = new SpannableString("abc def");
                //sss.SetSpan(new UnderlineSpan(), 0, 2, 0);
                //sss.SetSpan(new StyleSpan(TypefaceStyle.Bold), 2, 4, 0);
                //tvSlowa.SetText(sss, TextView.BufferType.Spannable);
                string slowa = Nastaveni.SelectedSong.SlowaToDisplay;
                List<Refren> lr = Song.GetRefreny(slowa);
                SpannableString sss = new SpannableString(slowa);
                foreach (Refren r in lr)
                {
                    sss.SetSpan(new StyleSpan(TypefaceStyle.Italic), r.Start, r.End, 0);
                }
                tvSlowa.SetText(sss, TextView.BufferType.Spannable);
            }

            if (Nastaveni.Center)
                tvSlowa.Gravity = GravityFlags.CenterHorizontal;
            else
                tvSlowa.Gravity = GravityFlags.NoGravity;

            if (Nastaveni.BigFont)
                tvSlowa.SetTextSize(Android.Util.ComplexUnitType.Dip, 22);
            else
                tvSlowa.SetTextSize(Android.Util.ComplexUnitType.Dip, 18);

            MainActivity.MyActivity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            PageCreator.HideKeyboard();
        }


        public static void SetView4()
        {
            if (PageFragment.view4 == null)
                return;

            MainActivity.MyActivity.Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
            PageCreator.HideKeyboard();

            if (Nastaveni.SelectedSong != null)
            {
                Button btnShareSong = PageFragment.view4.FindViewById<Button>(Resource.Id.btnShareSong);
                btnShareSong.Text = "Pieśń " + Nastaveni.SelectedSong.Tytul;
            }
        }

        public static void SaveSetting(string key, bool value)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(MainActivity.MyContext);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean(key, value);
            editor.Apply();
        }

        public static bool GetSetting(string key)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(MainActivity.MyContext);
            return prefs.GetBoolean(key, false);
        }
    }
}