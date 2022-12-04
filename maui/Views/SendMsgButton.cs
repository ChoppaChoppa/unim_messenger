using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace maui.Views
{
	public class SendMsgButton : UIButton
	{
		private UIScrollView _msgArea;

		public SendMsgButton(CGRect textBox)
		{
			double w = textBox.Width / 7;
			double h = textBox.Height;
			Frame = new CGRect(textBox.Width + 10, textBox.Y, w, h);
			BackgroundColor = UIColor.FromRGB(109, 59, 71);
			Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
			ClipsToBounds = true;
			Layer.CornerRadius = 10;
			Hidden = false;
		}

		public void SetNewFrame(CGRect textBox)
        {
            Frame = new CGRect(textBox.X + textBox.Width + 5, textBox.Y, Frame.Width, Frame.Height);
        }
    }
}