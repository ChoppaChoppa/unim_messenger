using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using CoreText;
using Foundation;

namespace maui.Views
{
	public class ProfileCardView : UIControl
	{
        // окно с данными открывающееся при нажатии на одну из кнопок
        private UIControl _window;

        private CAGradientLayer gradient;

        private UIImageView _profileImg;

        private UIColor _standartColor = UIColor.FromRGB(69, 58, 73);
        private UIColor _windowColor = UIColor.FromRGB(25, 29, 50);
        private UIColor _textColor = UIColor.FromRGB(229, 218, 218);
        private UIColor _lineColor = UIColor.FromRGB(40, 47, 68);
        private CGColor[] _gradColor;

        #region CGrect
        private CGRect _superViewFrame { get; }

        private CGRect _openWindowFrame;
        private CGRect _toucheBtnFrame;

        private CGRect _closeFrame;
        private CGRect _closeProfImgFrame;

        private CGRect _openFrame;
        private CGRect _openProfImgFrame;

        private CGRect _closeProfileBtn;
        private CGRect _openProfileBtn;

        private CGRect _closeSettingsBtn;
        private CGRect _openSettingsBtn;
        #endregion

        #region
        UIButton _profileBtn;
        UIButton _settingsBtn;
        UIButton _aboutBtn;
        UIButton _logOutBtn;

        UIButton _closeWindowBtn;
        #endregion

        public ProfileCardView(CGRect SuperViewFrame)
		{
            _superViewFrame = SuperViewFrame;

            Frame = new CGRect(20, 70, _superViewFrame.Width - 40, _superViewFrame.Height - 170);
			ClipsToBounds = true;
            Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
			BackgroundColor = _standartColor;
            Layer.CornerRadius = 20;
			Hidden = false;
        }

        public void InitProfile()
        {
            _closeFrame = new CGRect(0, 0, _superViewFrame.Width, _superViewFrame.Height);
            _openFrame = new CGRect(20, 70, _superViewFrame.Width - 40, _superViewFrame.Height - 170);

            _closeProfImgFrame = new CGRect(5, -_openFrame.Height * 0.2f, _openFrame.Width - 10, _openFrame.Height * 0.2f);
            _openProfImgFrame = new CGRect(_closeProfImgFrame.X, 5, _closeProfImgFrame.Width, _closeProfImgFrame.Height);

            #region _window
            _openWindowFrame = new CGRect(0, 0, Frame.Width, Frame.Height);
            #endregion

            // profileImg
            _profileImg = new UIImageView();
            _profileImg.Frame = _openProfImgFrame;
            _profileImg.Layer.CornerRadius = 20;
            _profileImg.ClipsToBounds = true;
            _profileImg.Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            _profileImg.Image = UIImage.FromBundle("code_rain.jpg");
            _profileImg.Hidden = false;
            _gradColor = new CGColor[] { _standartColor.ColorWithAlpha(0).CGColor,
                _standartColor.ColorWithAlpha(1).CGColor };
            gradient = new CAGradientLayer();
            gradient.Frame = _openProfImgFrame;
            gradient.Colors = _gradColor;
            //_profileImg.Layer.AddSublayer(gradient);
            //

            #region profileBtn init
            _closeProfileBtn = new CGRect(0, -_openFrame.Height * 0.05f, _openFrame.Width, _openFrame.Height * 0.07f);
            _openProfileBtn = new CGRect(_closeProfileBtn.X, _openProfImgFrame.Y + _openProfImgFrame.Height + 50,
                _closeProfileBtn.Width, _closeProfileBtn.Height);
            _profileBtn = new UIButton();
            _profileBtn.Frame = _openProfileBtn;
            _profileBtn.BackgroundColor = _standartColor;
            _profileBtn.Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            _profileBtn.Hidden = false;
            _profileBtn.Font = UIFont.FromName("Avenir Next", 18);
            _profileBtn.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            UIView line = new UIView(new CGRect(0, 0, _profileBtn.Frame.Width, 1));
            line.BackgroundColor = _lineColor;
            _profileBtn.TouchDown += _profileBtn_TouchDown;
            _profileBtn.TouchDragEnter += _profileBtn_TouchDragEnter;
            _profileBtn.TouchUpInside += _profileBtn_TouchUpInside;

            var lbProfileBtn = new UILabel();
            lbProfileBtn.Font = UIFont.FromName("Avenir Next", 20);
            lbProfileBtn.TextColor = _textColor;
            lbProfileBtn.Text = "Profile";
            lbProfileBtn.SizeToFit();
            lbProfileBtn.Frame = new CGRect(30, _profileBtn.Frame.Height / 2 - lbProfileBtn.Frame.Height / 2,
                lbProfileBtn.Frame.Width, lbProfileBtn.Frame.Height);

            _profileBtn.AddSubviews(line, lbProfileBtn);
            #endregion

            #region _settingsBtn init
            _closeSettingsBtn = new CGRect(0, _closeProfileBtn.Y - _openFrame.Height * 0.07f, _openFrame.Width, _openFrame.Height * 0.07f);
            _openSettingsBtn = new CGRect(_closeSettingsBtn.X, _openProfileBtn.Y + _openProfileBtn.Height, _closeSettingsBtn.Width, _closeSettingsBtn.Height);
            _settingsBtn = new UIButton();
            _settingsBtn.Frame = _openSettingsBtn;
            _settingsBtn.BackgroundColor = _standartColor;
            _settingsBtn.Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            _settingsBtn.Hidden = false;
            _settingsBtn.TouchUpInside += _settingsBtn_TouchUpInside;
            _settingsBtn.TouchDown += _settingsBtn_TouchDown;

            line = new UIView(new CGRect(0, 0, _settingsBtn.Frame.Width, 1));
            line.BackgroundColor = _lineColor;

            var lbSettingsBtn = new UILabel();
            lbSettingsBtn.Font = UIFont.FromName("Avenir Next", 20);
            lbSettingsBtn.TextColor = _textColor;
            lbSettingsBtn.Text = "Settings";
            lbSettingsBtn.SizeToFit();
            lbSettingsBtn.Frame = new CGRect(30, _settingsBtn.Frame.Height / 2 - lbSettingsBtn.Frame.Height / 2,
                lbSettingsBtn.Frame.Width, lbSettingsBtn.Frame.Height);

            _settingsBtn.AddSubviews(line, lbSettingsBtn);
            #endregion

            AddSubviews(_profileImg, _profileBtn, _settingsBtn);
        }

        #region settingsBtn handler
        private void _settingsBtn_TouchDown(object sender, EventArgs e)
        {
            _settingsBtn.BackgroundColor = _textColor.ColorWithAlpha(0.5f);
        }

        private void _settingsBtn_TouchUpInside(object sender, EventArgs e)
        {
            _settingsBtn.BackgroundColor = _standartColor;
        }
        #endregion

        #region profileBtn handler
        private void _profileBtn_TouchUpInside(object sender, EventArgs e)
        {
            _profileBtn.BackgroundColor = _standartColor;

            _window = new UIControl();
            _window.Frame = _openProfileBtn;
            _window.BackgroundColor = _windowColor;
            _window.Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            _window.Layer.CornerRadius = 20;
            _window.ClipsToBounds = true;
            _window.Hidden = false;

            _closeWindowBtn = new UIButton();
            _closeWindowBtn.Frame = new CGRect(5, 5, 60, 20);
            _closeWindowBtn.BackgroundColor = _windowColor;
            _closeWindowBtn.SetTitle("exit", UIControlState.Normal);
            _closeWindowBtn.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            _closeWindowBtn.Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            _closeWindowBtn.Layer.CornerRadius = 20;
            _closeWindowBtn.ClipsToBounds = true;
            _closeWindowBtn.TouchUpInside += _closeWindowBtn_TouchUpInside;
            _window.AddSubview(_closeWindowBtn);

            AddSubview(_window);

            Action action = new Action(() =>
            {
                _window.Frame = _openWindowFrame;
            });

            Animate(1, action);
            _toucheBtnFrame = _profileBtn.Frame;
        }

        private void _profileBtn_TouchDragEnter(object sender, EventArgs e)
        {
            
        }

        private void _profileBtn_TouchDown(object sender, EventArgs e)
        {
            _profileBtn.BackgroundColor = _textColor.ColorWithAlpha(0.5f);
        }
        #endregion

        #region window handler
        private void _closeWindowBtn_TouchUpInside(object sender, EventArgs e)
        {
            Action action = new Action(() =>
            {
                _window.Frame = new CGRect(0, _toucheBtnFrame.Y + _toucheBtnFrame.Height / 2, _window.Frame.Width, 0);
                WillRemoveSubview(_window);
            });

            Animate(1, action);
        }
        #endregion
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
        }

        //public void DrawLine(nfloat x1, nfloat y1, nfloat x2, nfloat y2)
        //{
        //    _context.SetLineWidth(4);
        //    UIColor.Clear.SetFill();
        //    UIColor.White.SetStroke();
        //    var currentPath = new CGPath();
        //    currentPath.AddLineToPoint(x1, y1);
        //    currentPath.AddLineToPoint(x2, y2);
        //    _context.AddPath(currentPath);
        //    _context.DrawPath(CGPathDrawingMode.Stroke);
        //    _context.SaveState();
        //}

    }
}

