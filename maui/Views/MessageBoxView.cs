using System;
using CoreGraphics;
using CoreText;
using maui.Models;
using UIKit;

namespace maui.Views
{
	public class MessageBoxView : UIView
	{

        public Message msg { get; private set; }
        public CGRect StartFrame { get; set; }
        public CGRect EndFrame { get; set; }

        private UILabel _text;
        private UIFont _previewFont;
        private UIColor _standartColor = UIColor.FromRGB(25, 29, 50);
        private UIColor _textColor = UIColor.FromRGB(229, 218, 218);
        private int _lineLength;

		public MessageBoxView(Message message)
		{
            //Frame = new CGRect(50, 0, 200, 60);
            BackgroundColor = _standartColor;
            Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            ClipsToBounds = true;
            Layer.CornerRadius = 5;
            Hidden = false;
            
            //Font = new CTFont("Dubai", 18);
            _previewFont = UIFont.FromName("Avenir Next", 13);
            _lineLength = 20;
            _text = new UILabel();
            _text.TextColor = _textColor;

            msg = message;
            AddText(msg.MessageText);
        }

        public void AddText(string text)
        {
            double lineCount = Math.Ceiling((double)text.Length / _lineLength);
            _text.Text = text;
            _text.SizeToFit();
            _text.Frame = new CGRect(10, 10, _text.Frame.Width > 250 ? 250 : _text.Frame.Width, _text.Frame.Height);

            _text.Lines = (int)lineCount;
            _text.LineBreakMode = UILineBreakMode.WordWrap;
            _text.SizeToFit();
            AddSubview(_text);

            Frame = new CGRect(0, 0, _text.Frame.Width + 20, _text.Frame.Height + 20);
            EndFrame = new CGRect(0, 0, _text.Frame.Width + 20, _text.Frame.Height + 20);
            //TextAlignment = UITextAlignment.Center;
        }
	}
}

