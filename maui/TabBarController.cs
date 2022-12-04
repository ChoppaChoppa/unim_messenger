using System;
using UIKit;

namespace maui
{
	public class TabBarController : UITabBarController
	{
        UIViewController messageView, profileView;

		public TabBarController()
		{
            IntPtr ptr = new IntPtr(1);
            messageView = new FirstViewController(ptr);
            messageView.Title = "messages";
            //profileView = new SecondViewController();
            //profileView.Title = "profile";

            var Views = new UIViewController[]{
                messageView
            };

            ViewControllers = Views;
        }
	}
}

