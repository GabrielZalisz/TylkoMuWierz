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
    public class Song
    {
        public int Numer { get; set; }

        public string Tytul { get; set; }

        public string Tonacja { get; set; }

        public string Slowa { get; set; }

        public string Sekvence { get; set; }

        public string BookReference { get; set; }
    }
}