using System;
using CoreGraphics;
using maui.Views;
using UIKit;

namespace maui.Patterns.Commands
{
    public class ViewControllerCommands : ICommand
    {
        public event EventHandler tabBarHide;

        private UIScrollView _scroller;
        private UITabBar tabBarHidden;
        private CGRect startingFrame;

        public ViewControllerCommands(ref UIScrollView scroller, UITabBar tb)
        {
            _scroller = scroller;
            tabBarHidden = tb;
            startingFrame = tb.Frame;
        }

        public void ContentOfSet(CGPoint rect)
        {
            _scroller.SetContentOffset(rect, true);
        }

        private void _scroller_ScrollAnimationEnded(object sender, EventArgs e)
        {
            _scroller.ScrollEnabled = false;
            //_scroller.ScrollAnimationEnded -= _scroller_ScrollAnimationEnded;
        }

        public void EnabledScroll(bool enable)
        {
            _scroller.ScrollEnabled = enable;
        }

        public CGPoint GetScrollPoint()
        {
            return _scroller.ContentOffset;
        }

        public void HiddenTabBar(bool hidden)
        {
            if (hidden)
            {
                //tabBarHidden.Frame = new CGRect(0, tabBarHidden.Frame.Y,
                //    tabBarHidden.Frame.Width, 0);
                tabBarHidden.Alpha = 0;
            }
            else
            {
                //tabBarHidden.Frame = startingFrame;
                tabBarHidden.Alpha = 1;
            }
            
        }
    }
}
