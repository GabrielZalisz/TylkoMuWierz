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
    public static class DataManager
    {
        //public static string[] Songs = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Test1", "Test2", "Test3", "Test4" };

        public static IList<Song> Songs = new List<Song>();

        public static void LoadSongs()
        {
            Songs.Add(new Song()
            {
                Numer = 1,
                Tytul = "Pokus pokus pokus",
                Tonacja = "G",
                Slowa = "qwe rtz uio pú) asd fgh jkl ů§ yx cvb nm, .-"
            });

            Songs.Add(new Song()
            {
                Numer = 2,
                Tytul = "Test test test",
                Tonacja = "C",
                Slowa = "qwe rtz uio pú) asd fgh jkl ů§ yx cvb nm, .-"
            });

            Songs.Add(new Song()
            {
                Numer = 3,
                Tytul = "Prvni prvni prvni",
                Tonacja = "E",
                Slowa = "qwe rtz uio pú) asd fgh jkl ů§ yx cvb nm, .-"
            });
        }
    }
}