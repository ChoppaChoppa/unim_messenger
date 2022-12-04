// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using maui.Views;
using UIKit;

namespace maui
{
	public partial class ProfileViewController : UIViewController
	{
		public ProfileViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var profileCard = new ProfileCardView(View.Frame);
            profileCard.InitProfile();


            View.AddSubview(profileCard);
            InvokeOnMainThread(() => {
                View.BackgroundColor = UIColor.FromRGB(40, 47, 68);
                //UIImageView.Animate(animHeart.AnimationDuration, new Action(animHeart.StartAnimating));
            });
        }
    }
}
