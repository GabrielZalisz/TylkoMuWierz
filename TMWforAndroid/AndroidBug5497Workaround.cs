using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TylkoMuWierz
{
    class MyOnGlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        public void OnGlobalLayout()
        {
            possiblyResizeChildOfContent();
        }

        private void possiblyResizeChildOfContent()
        {
            int usableHeightNow = computeUsableHeight();
            if (usableHeightNow != AndroidBug5497Workaround.usableHeightPrevious)
            {
                int usableHeightSansKeyboard = AndroidBug5497Workaround.mChildOfContent.RootView.Height;
                int heightDifference = usableHeightSansKeyboard - usableHeightNow;
                if (heightDifference > (usableHeightSansKeyboard / 4))
                {
                    // keyboard probably just became visible
                    AndroidBug5497Workaround.frameLayoutParams.Height = usableHeightSansKeyboard - heightDifference;
                }
                else
                {
                    // keyboard probably just became hidden
                    AndroidBug5497Workaround.frameLayoutParams.Height = usableHeightSansKeyboard;
                }
                AndroidBug5497Workaround.mChildOfContent.RequestLayout();
                AndroidBug5497Workaround.usableHeightPrevious = usableHeightNow;
            }
        }

        private int computeUsableHeight()
        {
            Rect r = new Rect();
            AndroidBug5497Workaround.mChildOfContent.GetWindowVisibleDisplayFrame(r);
            return (r.Bottom - r.Top);
        }
    }

    public class AndroidBug5497Workaround
    {

        // For more information, see https://issuetracker.google.com/issues/36911528
        // To use this class, simply invoke assistActivity() on an Activity that already has its content view set.

        public static void assistActivity(Activity activity)
        {
            new AndroidBug5497Workaround(activity);
        }

        public static View mChildOfContent;
        public static int usableHeightPrevious;
        public static ViewGroup.LayoutParams frameLayoutParams;

        private AndroidBug5497Workaround(Activity activity)
        {
            mChildOfContent = (RelativeLayout)activity.FindViewById(Resource.Id.rrr_main);
            //mChildOfContent = content.GetChildAt(0);

            mChildOfContent.ViewTreeObserver.AddOnGlobalLayoutListener(new MyOnGlobalLayoutListener());

            frameLayoutParams = mChildOfContent.LayoutParameters;
        }
    }
}