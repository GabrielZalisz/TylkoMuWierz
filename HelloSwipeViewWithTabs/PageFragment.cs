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
        View view1;
        View view2;
        View view3;
        View view4;

        bool view1Ready;
        bool view2Ready;
        bool view3Ready;
        bool view4Ready;

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
                    if (!view1Ready)
                    {
                        view1 = inflater.Inflate(Resource.Layout.fragment_page_1, container, false);
                        view1Ready = true;
                    }
                    return view1;
                case 2:
                    if (!view2Ready)
                    {
                        view2 = inflater.Inflate(Resource.Layout.fragment_page_2, container, false);
                        var listView = view2.FindViewById<ListView>(Resource.Id.listView1);
                        var items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers", "Test1", "Test2", "Test3", "Test4" };
                        ArrayAdapter<string> adapter = new ArrayAdapter<string>(Activity.ApplicationContext, Android.Resource.Layout.SimpleListItem1, items);
                        listView.Adapter = adapter;
                        listView.ItemClick += ListView_ItemClick;
                        view2Ready = true;
                    }
                    return view2;
                case 3:
                    //if (!view3Ready)
                    //{
                        view3 = inflater.Inflate(Resource.Layout.fragment_page_3, container, false);
                        var tvNadpis = view3.FindViewById<TextView>(Resource.Id.tvNadpis);
                        if (pass % 2 == 0)
                            tvNadpis.SetTextColor(Color.Red);
                        else
                            tvNadpis.SetTextColor(Color.Black);
                        view3Ready = true;
                    pass++;
                    //}
                    return view3;
                case 4:
                    if (!view4Ready)
                    {
                        view4 = inflater.Inflate(Resource.Layout.fragment_page_4, container, false);
                        var btn = view4.FindViewById<Button>(Resource.Id.button1);
                        btn.Click += Btn_Click;
                        var chb = view4.FindViewById<CheckBox>(Resource.Id.chbRed);
                        chb.CheckedChange += Chb_CheckedChange;
                        view4Ready = true;
                    }
                    return view4;
                default:
                    var view_d = inflater.Inflate(Resource.Layout.fragment_page, container, false);
                    var textView_d = (TextView)view_d;
                    textView_d.Text = "Fragment #" + mPage;
                    return view_d;
            }
        }

        int pass = 0;

        public bool red
        {
            get;
            set;
        }

        private void Chb_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var chb = view4.FindViewById<CheckBox>(Resource.Id.chbRed);
            if (chb.Checked)
                red = true;
            else
                red = false;
            view3Ready = false;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var chb = view4.FindViewById<CheckBox>(Resource.Id.checkBox1);
            if (chb.Checked)
            {
                View view = (View)sender;
                Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
            }
            else
            { 
                Toast.MakeText(Activity.ApplicationContext, "Klik!", ToastLength.Short).Show();
            }
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(Activity.ApplicationContext, "Hello!", ToastLength.Short).Show();
        }
    }
}