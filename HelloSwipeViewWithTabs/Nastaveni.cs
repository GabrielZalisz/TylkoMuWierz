using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HelloSwipeViewWithTabs
{
    static class Nastaveni
    {
        private static bool _snackBar;

        public static bool SnackBar
        {
            get { return _snackBar; }
            set { _snackBar = value; }
        }

        public static bool Alphabetically { get; set; }

        public static int SongIndex { get; set; }

        public static bool Red { get; set; }

        public static void SetView3()
        {
            if (PageFragment.view3 == null)
                return;
            TextView tvTytul = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvTytul);
            if (Red)
            {
                tvTytul.SetTextColor(Color.Red);
            }
            else
            {
                tvTytul.SetTextColor(Color.Black);
            }
            tvTytul.Text = DataManager.SongsToDisplay[SongIndex].Tytul;

            TextView tvNumer = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvNumer);
            tvNumer.Text = DataManager.SongsToDisplay[SongIndex].Numer.ToString();

            TextView tvTonacja = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvTonacja);
            tvTonacja.Text = DataManager.SongsToDisplay[SongIndex].Tonacja;

            TextView tvSlowa = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvSlowa);
            tvSlowa.Text = DataManager.SongsToDisplay[SongIndex].Slowa;
        }
    }
}