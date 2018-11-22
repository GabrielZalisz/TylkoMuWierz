using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace HelloSwipeViewWithTabs
{
    public static class DataManager
    {
        //public static string[] Songs = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Test1", "Test2", "Test3", "Test4" };

        public static IList<Song> Songs = new List<Song>();
        public static IList<Song> SongsToDisplay = new List<Song>();

        public static void LoadSongs()
        {
            string songbook;
            using (StreamReader sr = new StreamReader(MainActivity.MyContext.Assets.Open("songbook_Tylko Mu Wierz.xml")))
            {
                songbook = sr.ReadToEnd();
            }

            IEnumerable<XElement> vstup;
            TextReader tr = new System.IO.StringReader(songbook);
            vstup = XElement.Load(tr).Descendants("Item");
            var piesni = from song in vstup
                                 select new Song
                                 {
                                     Tytul = song.Element("Title1").Value,
                                     Numer = int.Parse(song.Element("SongNumber").Value),
                                     Slowa = song.Element("Contents").Value,
                                     Tonacja = song.Element("MusicKey").Value,
                                     BookReference = song.Element("BookReference").Value

                                     //Poradi = int.Parse(song.Element("Poradi").Value),
                                     //Barva = song.Element("Barva").Value,
                                     //ExeAplikace = song.Element("ExeAplikace").Value,
                                     //ParametryAplikace = song.Element("ParametryAplikace").Value,
                                     //Identifikator = song.Element("Identifikator").Value,
                                     //TypPolozky = (Nastaveni.ItemType)Enum.Parse(typeof(Nastaveni.ItemType), song.Element("TypPolozky").Value),
                                     //NazevProjektu = song.Element("NazevProjektu").Value,
                                     //Autor = song.Element("Autor").Value,
                                     //IsViditelne = bool.Parse(song.Element("IsViditelne").Value),
                                     //IdeckoZastupce = song.Element("IdeckoZastupce").Value,
                                     //Poznamky = song.Element("Poznamky").Value,
                                     //Info1 = song.Element("Info1").Value,
                                 };

            Songs = piesni.OrderBy(qq => qq.Numer).ToList();
            SongsToDisplay = Songs;
            /*













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
                Tytul = "Amazing Grace ěščřžýżąę",
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
            });*/
        }
    }
}