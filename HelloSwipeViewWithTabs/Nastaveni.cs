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

        private static bool _red;

        public static bool Red
        {
            get { return _red; }
            set
            {
                _red = value;
                TextView tv = PageFragment.view3.FindViewById<TextView>(Resource.Id.tvNadpis);
                if (value)
                {
                    tv.SetTextColor(Color.Red);
                }
                else
                {
                    tv.SetTextColor(Color.Black);
                }
            }
        }
    }
}