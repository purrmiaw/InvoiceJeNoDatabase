using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;

namespace InvoiceJe.Droid.Extensions
{

    [Register("com.miaw.invoiceje.BottomNavigationViewBehavior")]
    public class BottomNavigationViewBehavior :
        CoordinatorLayout.Behavior
    {
        private int height;

        public BottomNavigationViewBehavior(Context context, IAttributeSet attrs) : base()
        {

        }

        public override bool LayoutDependsOn(CoordinatorLayout parent, Java.Lang.Object child, View dependency)
        {
            return dependency is FloatingActionButton || base.LayoutDependsOn(parent, child, dependency);
        }

        public override bool OnLayoutChild(CoordinatorLayout parent, Java.Lang.Object child, int layoutDirection)
        {
            BottomNavigationView childView = child.JavaCast<BottomNavigationView>();
            this.height = childView.Height;
            return base.OnLayoutChild(parent, child, layoutDirection);

        }

        public override bool OnStartNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View directTargetChild, View target, int nestedScrollAxes)
        {
            return nestedScrollAxes == ViewCompat.ScrollAxisVertical
                    || base.OnStartNestedScroll(coordinatorLayout, child, directTargetChild, target, nestedScrollAxes);
        }

        public override void OnNestedScroll(CoordinatorLayout coordinatorLayout, Java.Lang.Object child, View target, int dxConsumed, int dyConsumed, int dxUnconsumed, int dyUnconsumed)
        {
            // dyconsumed: user initiates movement on the y-axis and it happens on screen
            // dyunconsumed: user initiates movement on the y-axis but it doesn't happen on screen, for example, downwards movement at the bottom end of the list.

            // dy > 0: user swiping up, so screen in scrolling down
            // dy < 0: user swiping down, so screen is scrolling up

            base.OnNestedScroll(coordinatorLayout, child, target, dxConsumed, dyConsumed, dxUnconsumed, dyUnconsumed);

            BottomNavigationView childView = child.JavaCast<BottomNavigationView>();

            if (dyUnconsumed > 0 || dyConsumed > 0)
            {
                this.SlideDown(childView);
            }
            else if (dyUnconsumed < 0 || dyConsumed < 0)
            {
                this.SlideUp(childView);
            }

            // define our own behavior
            IList<View> dependencies = coordinatorLayout.GetDependencies(childView);

            foreach (View view in dependencies)
            {
                if (view is FloatingActionButton)
                {
                    FloatingActionButton floatingActionButtonView = view.JavaCast<FloatingActionButton>();

                    if (dyUnconsumed > 0 || dyConsumed > 0)
                    {
                        floatingActionButtonView.Hide();
                    }
                    else if (dyUnconsumed < 0 || dyConsumed < 0)
                    {
                        floatingActionButtonView.Show();
                    }

                }
            }

        }

        private void SlideUp(BottomNavigationView child)
        {
            child.ClearAnimation();
            child.Animate().TranslationY(0).SetDuration(200);
        }

        private void SlideDown(BottomNavigationView child)
        {
            child.ClearAnimation();
            child.Animate().TranslationY(height).SetDuration(200);
        }
    }
}