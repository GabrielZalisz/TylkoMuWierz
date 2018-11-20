using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Views;
using Android.Content;
using static Android.Support.V4.View.ViewPager;

namespace HelloSwipeViewWithTabs
{
    [Activity(Label = "HelloSwipeViewWithTabs", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        public static Context MyContext;
        public static ViewPager MyPager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            MyContext = this.ApplicationContext;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            // Find views
            var pager = FindViewById<ViewPager>(Resource.Id.pager);
            var tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            var adapter = new CustomPagerAdapter(this, SupportFragmentManager);
            //var toolbar = FindViewById<Toolbar>(Resource.Id.my_toolbar);

            MyPager = pager;
            //IOnPageChangeListener l = new 
            pager.AddOnPageChangeListener(new MyOnPageChangeListener());

            // Setup Toolbar
            //SetSupportActionBar(toolbar);
            //SupportActionBar.Title = "Test";

            // Set adapter to view pager
            pager.Adapter = adapter;

            // Setup tablayout with view pager
            tabLayout.SetupWithViewPager(pager, true);

            // Iterate over all tabs and set the custom view
            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                TabLayout.Tab tab = tabLayout.GetTabAt(i);
                tab.SetCustomView(adapter.GetTabView(i));
            }
        }
    }
}

