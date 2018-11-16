using System;
using Android.OS;
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

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            switch (mPage)
            {
                case 1:
                    var view1 = inflater.Inflate(Resource.Layout.fragment_page_1, container, false);
                    return view1;
                case 2:
                    var view2 = inflater.Inflate(Resource.Layout.fragment_page_2, container, false);
                    return view2;
                case 3:
                    var view3 = inflater.Inflate(Resource.Layout.fragment_page_3, container, false);
                    return view3;
                case 4:
                    var view4 = inflater.Inflate(Resource.Layout.fragment_page_4, container, false);
                    return view4;
                default:
                    var view_d = inflater.Inflate(Resource.Layout.fragment_page, container, false);
                    var textView_d = (TextView)view_d;
                    textView_d.Text = "Fragment #" + mPage;
                    return view_d;
            }
        }
    }
}