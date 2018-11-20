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
                Tytul = "Ccc test",
                Tonacja = "E",
                Slowa = "text text text text text text text text text text text text text text text text text text "
            });

            Songs.Add(new Song()
            {
                Numer = 2,
                Tytul = "Bbb test",
                Tonacja = "C",
                Slowa = "text text text text text text text text text text text text text text text text text text "
            });

            Songs.Add(new Song()
            {
                Numer = 3,
                Tytul = "Amazing Grace",
                Tonacja = "G",
                Slowa = @"Amazing grace! (how sweet the sound)
That saved a wretch like me!
I once was lost,
but now am found,
Was blind, but now I see.

'T was grace that taught my heart to fear,
And grace my fears relieved;
How precious did that grace appear
The hour I first believed!

Thro' many dangers, toils, and snares,
I have already come;
'Tis grace hath brought me safe thus far,
And grace will lead me home.

The Lord has promis'd good to me,
His word my hope secures;
He will my shield and portion be
As long as life endures.

Yes, when this flesh and heart shall fail,
And mortal life shall cease;
I shall possess, within the veil,
A life of joy and peace.

The earth shall soon dissolve like snow,
The sun forbear to shine;
But God, who call'd me here below,
Will be forever mine."
            });
        }
    }
}