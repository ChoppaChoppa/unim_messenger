using System;
using UIKit;

namespace maui.Patterns.Commands
{
	public class ChangeUserCardSCroller : IScrollerCommand
	{
        private UIScrollView _scroller;

		public ChangeUserCardSCroller(ref UIScrollView scroller)
		{
            _scroller = scroller;
		}

        public void ChangeHeight(double Height)
        {
            _scroller.Frame = new CoreGraphics.CGRect(_scroller.Frame.X,
                _scroller.Frame.Y, _scroller.Frame.Width, Height);
        }
    }
}

