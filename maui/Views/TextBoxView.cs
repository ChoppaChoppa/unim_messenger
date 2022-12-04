using System;
using UIKit;
using CoreGraphics;
using Foundation;
using maui.Patterns.Commands;
using NotificationCenter;
using static SystemConfiguration.NetworkReachability;

namespace maui.Views
{
	public class TextBoxView : UITextView
	{
		public SendMsgButton SendBtn { get; set; }

		private IScrollerCommand _command;
		private UITextView _textBox;
		private UIFont _previewFont;
		private UIColor _standartColor = UIColor.FromRGB(40, 47, 68);
		private UIColor _textColor = UIColor.FromRGB(229, 218, 218);
		private nfloat _voidFrame;

		public TextBoxView(CGRect frame, IScrollerCommand command, ref SendMsgButton SndBtn)
		{
			Frame = frame;
			_voidFrame = frame.Height;
			BackgroundColor = _standartColor;
			Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
			ClipsToBounds = true;
			Layer.CornerRadius = 10;
			Hidden = false;
			_previewFont = UIFont.FromName("Avenir Next", 18);
			Font = _previewFont;
			TextColor = _textColor;
			TextAlignment = UITextAlignment.Left;
			ShowsVerticalScrollIndicator = false;
			_command = command;


			SendBtn = SndBtn;
			//_textBox = new UITextView();
			//_textBox.Frame = new CGRect(10, 2, Frame.Width - 10, Frame.Height - 2);
			//_textBox.Font = _previewFont;
			//_textBox.Editable = true;
			//_textBox.BackgroundColor = _standartColor;
			//_textBox.

			//AddSubview(_textBox);
		}

		public void SetNewFrame(CGRect frame)
        {
			Frame = frame;
			SendBtn.SetNewFrame(frame);
			//_textBox.Frame = new CGRect(10, 2, Frame.Width - 10, Frame.Height - 2);
		}


		//TODO: получать данные клавиатуры, не задавать координаты тб на прямую
		public CGRect StartEditing(CGRect frame)
        {
			nfloat frameEnd = 0;
			var notification = UIKeyboard.Notifications.ObserveWillShow((s, e) =>
			{
				Frame = new CGRect(Frame.X, e.FrameEnd.Y - Frame.Height - 10, Frame.Width, Frame.Height);
				frame = new CGRect(frame.X, frame.Y, frame.Width, e.FrameEnd.Y- 200);

                SendBtn.SetNewFrame(Frame);
			});

			return frame;
		}

        //     public override void AccessibilityElementDidLoseFocus()
        //     {
        //         base.AccessibilityElementDidLoseFocus();

        //UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        //     }
    }
}