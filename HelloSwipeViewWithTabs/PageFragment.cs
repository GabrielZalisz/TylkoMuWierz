using System;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace HelloSwipeViewWithTabs
{
    public class PageFragment : Fragment
    {
        const string ARG_PAGE = "ARG_PAGE";
        private int mPage;
        public static View view1;
        public static View view2;
        public static View view3;
        public static View view4;

        PageCreator pc;

        public PageFragment()
        {
            if (pc == null)
                pc = new PageCreator(MainActivity.MyContext);            
        }

        public static PageFragment newInstance(int page)
        {
            var args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            var fragment = new PageFragment();
            fragment.Arguments = args;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mPage = Arguments.GetInt(ARG_PAGE);
        }

        public override void OnResume()
        {            
            base.OnResume();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //FragmentTransaction ft = FragmentManager.BeginTransaction();
            //ft.SetAllowOptimization(false);
            //ft.Detach(this).Attach(this).CommitAllowingStateLoss();
            switch (mPage)
            {
                //case 1:
                //    if (view1 == null)
                //    {
                //        view1 = pc.CreatePage1(inflater, container);
                //    }
                //    return view1;
                case 1:
                    if (view2 == null)
                    {
                        view2 = pc.CreatePage2(inflater, container);
                    }
                    return view2;
                case 2:
                    if (view3 == null)
                    {
                        view3 = pc.CreatePage3(inflater, container);
                    }
                    return view3;
                case 3:
                    if (view4 == null)
                    {
                        view4 = pc.CreatePage4(inflater, container);
                    }
                    return view4;
                default:
                    var view_d = inflater.Inflate(Resource.Layout.fragment_page, container, false);
                    var textView_d = (TextView)view_d;
                    textView_d.Text = "Fragment #" + mPage;
                    return view_d;
            }
        }

        //private void Chb_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        //{
        //    var chb = view4.FindViewById<CheckBox>(Resource.Id.chbRed);
        //    if (chb.Checked)
        //    {
        //        red = true;
        //        TextView v = view3.FindViewById<TextView>(Resource.Id.tvNadpis);
        //        v.SetTextColor(Color.Violet);
        //    }
        //    else
        //    {
        //        red = false;
        //    }
        //}
    }
}