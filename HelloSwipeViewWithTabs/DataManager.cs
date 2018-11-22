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

        public static void LoadSongs()
        {
            string songbook;
            using (StreamReader sr = new StreamReader(MainActivity.MyContext.Assets.Open("songbook_English.xml")))
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

                                     //Id = Int32.Parse(song.Element("Id").Value),
                                     //Parentid = Int32.Parse(song.Element("ParentId").Value),
                                     //Poradi = int.Parse(song.Element("Poradi").Value),
                                     //Barva = song.Element("Barva").Value,
                                     //Nazev = song.Element("Nazev").Value,
                                     //Popis = song.Element("Popis").Value,
                                     //Obrazek = song.Element("Obrazek").Value,
                                     //TypAplikace = song.Element("TypAplikace").Value,
                                     //CestaAplikace = song.Element("CestaAplikace").Value,
                                     //CestaAplikace2 = song.Element("CestaAplikace2").Value,
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
                                     //Info2 = song.Element("Info2").Value,
                                     //Info3 = song.Element("Info3").Value,
                                     //Info4 = song.Element("Info4").Value,
                                     //Info5 = song.Element("Info5").Value,
                                     //Info6 = song.Element("Info6").Value,
                                     //Info7 = song.Element("Info7").Value,
                                     //Info8 = song.Element("Info8").Value,
                                     //Info9 = song.Element("Info9").Value,
                                     //Info10 = song.Element("Info10").Value,
                                 };

            Songs = piesni.ToList();
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