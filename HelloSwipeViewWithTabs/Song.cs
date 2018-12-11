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

        public string SlowaToDisplay
        {
            get
            {
                if (!(Slowa.Contains("[1]") || Slowa.Contains("[chorus]")))
                {
                    if (Nastaveni.NoLineBreaks)
                    {
                        string[] items = Slowa.Trim().Split("\n\n");
                        List<string> nlb = new List<string>();
                        items.ToList().ForEach(q => nlb.Add(q.Replace("\n", " ").Replace("  ", " ").Replace("  ", " ")));
                        return string.Join("\n\n", nlb);
                    }
                    else
                    {
                        return Slowa.Trim().Replace("  ", " ");
                    }
                }
                bool contains_chorus = Slowa.Contains("chorus");
                string ret = Slowa.Trim();
                ret = ret.Replace("  ", " ")
                    .Replace("  ", " ")
                    .Replace("  ", " ")
                    .Replace("  ", " ")
                    .Replace("  ", " ");
                ret = ret.Replace("]", "]\n");//za kazdym tagym jest enter
                ret = ret.Replace("\n\n", "\n")
                    .Replace("\n\n", "\n")
                    .Replace("\n\n", "\n")
                    .Replace("\n\n", "\n")
                    .Replace("\n\n", "\n");//nie zostanóm 2 entry
                ret = ret.Replace("[1]\n", "\n1. ");
                ret = ret.Replace("[2]\n", "\n2. ");
                ret = ret.Replace("[3]\n", "\n3. ");
                ret = ret.Replace("[4]\n", "\n4. ");
                ret = ret.Replace("[5]\n", "\n5. ");
                ret = ret.Replace("[6]\n", "\n6. ");
                ret = ret.Replace("[7]\n", "\n7. ");
                ret = ret.Replace("[8]\n", "\n8. ");
                ret = ret.Replace("[9]\n", "\n9. ");
                ret = ret.Replace("[10]\n", "\n10. ");
                ret = ret.Replace("[11]\n", "\n11. ");
                ret = ret.Replace("[12]\n", "\n12. ");
                ret = ret.Replace("[13]\n", "\n13. ");
                ret = ret.Replace("[14]\n", "\n14. ");
                ret = ret.Replace("[15]\n", "\n15. ");
                ret = ret.Replace("[chorus]\n", "\nR. ");
                ret = ret.Replace("[bridge]\n", "\nB. ");
                ret = ret.Replace("[chorus 2]\n", "\nR2. ");
                ret = ret.Trim();

                if (Nastaveni.NoLineBreaks)
                {
                    string[] items = ret.Split("\n\n");
                    List<string> nlb = new List<string>();
                    items.ToList().ForEach(q => nlb.Add(q.Replace("\n", " ").Replace("  ", " ").Replace("  ", " ")));
                    ret = string.Join("\n\n", nlb);
                }

                if (Nastaveni.ChorusMany && contains_chorus)
                {
                    string[] seq = Sekvence.Split(",");
                    string[] data = ret.Split("\n\n");
                    List<string> data2 = new List<string>();
                    foreach (string sss in seq)
                    {
                        string vzor = sss;
                        if (sss == "c")
                        {
                            vzor = "r";//chorus => refren;
                        }
                        string nalezeno = data.Where(q => q.Trim().ToLower().StartsWith(vzor.ToLower())).FirstOrDefault();
                        if (nalezeno != null)
                        {
                            data2.Add(nalezeno);
                        }
                    }
                    data2.AddRange(data.Where(q => !data2.Contains(q)));//reszta
                    ret = string.Join("\n\n", data2);
                }

                if (Nastaveni.Center)
                {
                    ret = ret.Replace("1. ", "1.\n")
                        .Replace("1. ", "1.\n")
                        .Replace("2. ", "2.\n")
                        .Replace("3. ", "3.\n")
                        .Replace("4. ", "4.\n")
                        .Replace("5. ", "5.\n")
                        .Replace("6. ", "6.\n")
                        .Replace("7. ", "7.\n")
                        .Replace("8. ", "8.\n")
                        .Replace("9. ", "9.\n")
                        .Replace("10. ", "10.\n")
                        .Replace("11. ", "11.\n")
                        .Replace("12. ", "12.\n")
                        .Replace("13. ", "13.\n")
                        .Replace("14. ", "14.\n")
                        .Replace("15. ", "15.\n")
                        .Replace("R. ", "R.\n")
                        .Replace("R2. ", "R2.\n");
                }

                return ret.Replace("B. ", "").Replace("R2", "R").Trim();
            }
        }

        public string Sekvence { get; set; }

        public string BookReference { get; set; }

        public string Folder { get; set; }

        public static List<Refren> GetRefreny(string slowa)
        {
            try
            {
                List<Refren> lr = new List<Refren>();
                List<string> casti = slowa.Split("\n\n", StringSplitOptions.None).ToList();
                List<string> refreny = casti.Where(q => q.StartsWith("R.")).ToList();
                foreach (string r in refreny)
                {
                    int poradi = casti.IndexOf(r);
                    string predchozi = string.Join("\n\n", casti.Take(poradi));
                    int ior = predchozi.Length;
                    int ioe = ior + r.Length;
                    lr.Add(new Refren(ior, ioe));
                    casti[poradi] = new string('x', casti[poradi].Length); //nuluji, aby se spočítaly i další refrény
                }
                return lr;
            }
            catch
            {
                return new List<Refren>();
            }
        }
    }

    public class Refren
    {
        public int Start;
        public int End;

        public Refren(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}