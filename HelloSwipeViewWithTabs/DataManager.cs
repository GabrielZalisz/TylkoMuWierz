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

namespace TylkoMuWierz
{
	public static class DataManager
	{
		//public static string[] Songs = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Test1", "Test2", "Test3", "Test4" };

		public static IList<Song> AllSongs = new List<Song>();
		public static IList<Song> Songbook = new List<Song>();
		public static IList<Song> SongsToDisplay = new List<Song>();
		
		static string GetNormalTitle(string title, string obsah)
		{
			string tytul_bez_duru = (title.IndexOf('(') > -1 ? title.Substring(0, title.IndexOf('(') - 1) : title).Trim();
			if (obsah.ToLower().Contains(tytul_bez_duru.ToLower()))
			{
				return obsah.Substring(obsah.ToLower().IndexOf(tytul_bez_duru.ToLower()), tytul_bez_duru.Length);
			}
			else
			{
				return tytul_bez_duru;
			}
		}

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
									 Tytul = GetNormalTitle(song.Element("Title1").Value, song.Element("Contents").Value),
									 Numer = int.Parse(song.Element("SongNumber").Value),
									 Slowa = song.Element("Contents").Value,
									 Tonacja = song.Element("MusicKey").Value.EndsWith('m') ? song.Element("MusicKey").Value.Substring(0, 1).ToLower() : song.Element("MusicKey").Value,
									 Sekvence = song.Element("Sequence").Value,
									 BookReference = song.Element("BookReference").Value,
									 Folder = song.Element("Folder").Value

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

			AllSongs = piesni.OrderBy(qq => qq.Numer).ToList();

            Songbook = AllSongs.Where(q => q.Folder == Nastaveni.SelectedFolder).ToList();
			SongsToDisplay = Songbook;
			Nastaveni.SelectedSong = SongsToDisplay.FirstOrDefault();
		}
	}
}