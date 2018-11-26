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
        //na zrušení
        public static bool SnackBar { get; set; }
        public static bool Red { get; set; }

        //view2

        public static bool Alphabetically { get; set; }

        //view3

        public static int SongIndex { get; set; }
        public static bool Center { get; set; }
        public static bool BigFont { get; set; }//small=17dp, big=21dp
        public static bool ChorusMany { get; set; }
        public static bool NoLineBreaks { get; set; }















        public static void SetView3()
        {
            if (PageFragment.view3 == null)
                return;

            TextView tvTytul = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvTytul);
            tvTytul.Text = DataManager.SongsToDisplay[SongIndex].Tytul;
            //if (Red)
            //{
            //    tvTytul.SetTextColor(Color.Red);
            //}
            //else
            //{
            //    tvTytul.SetTextColor(Color.Black);
            //}

            TextView tvNumer = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvNumer);
            tvNumer.Text = DataManager.SongsToDisplay[SongIndex].Numer.ToString();

            TextView tvTonacja = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvTonacja);
            tvTonacja.Text = DataManager.SongsToDisplay[SongIndex].Tonacja;

            TextView tvSlowa = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvSlowa);
            tvSlowa.Text = DataManager.SongsToDisplay[SongIndex].SlowaToDisplay;

            if (Nastaveni.Center)
                tvSlowa.Gravity = GravityFlags.CenterHorizontal;
            else
                tvSlowa.Gravity = GravityFlags.NoGravity;

            if (Nastaveni.BigFont)
                tvSlowa.SetTextSize(Android.Util.ComplexUnitType.Dip, 21);
            else
                tvSlowa.SetTextSize(Android.Util.ComplexUnitType.Dip, 17);
        }
    }
}