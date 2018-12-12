using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace TylkoMuWierz
{
    class MyOnPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
    {
        void ViewPager.IOnPageChangeListener.OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        void ViewPager.IOnPageChangeListener.OnPageScrollStateChanged(int state)
        {
            //throw new NotImplementedException();
        }

        void ViewPager.IOnPageChangeListener.OnPageSelected(int position)
        {
            switch (position)
            {
                //case 0:
                //    Nastaveni.SetView1();
                //    break;
                case 0:
                    Nastaveni.SetView2();
                    break;
                case 1:
                    Nastaveni.SetView3();
                    break;
                case 2:
                    Nastaveni.SetView4();
                    break;
            }
        }
    }
}