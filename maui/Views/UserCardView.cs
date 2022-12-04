using System;
using CoreAnimation;
using CoreGraphics;
using CoreText;
using Foundation;
using maui.Models;
using maui.Patterns.Commands;
using UIKit;
using Vibrancy.Forms;
using Xamarin.Forms;

namespace maui.Views
{
    //TODO: blur effect
    public class UserCardView : UIButton
    {
        public ContactModel ContactData { get; private set; }
        public UIFont PreviewFont { get; set; }
        public CGRect ViewFrame { get; set; }
        public nfloat ViewHeight { get; set; }
        public string ContactName { get; private set; }
        public string LastMessage { get; private set; }
        public int UserID { get; set; }

        private ICommand command;
        private SendMsgButton SendBtn; 
        private TextBoxView _textBox;
        private UIScrollView msgArea;
        private UILabel lbContactName { get; set; }
        private UILabel lbLastMessage { get; set; }
        private UIImageView contactImg { get; set; }
        private CGRect _openFrame { get; set; }
        private CGRect _openGradFrame { get; set; }
        private CGRect _openMsgArea { get; set; }
        private CGRect _openTextBoxFrame;
        private CGRect _closeTextBoxFrame;
        private CGRect _openSendBtn;
        private CGRect _closeSendBtn;
        private CGRect _closeGradFrame { get; set; }
        private CGRect _closedFrame { get; set; }
        private CGRect _closeContactImgFrame { get; set; }
        private CGRect _closeLbNameFrame { get; set; }
        private CGRect _closeMsgArea { get; set; }
        private CGSize _msgAreaContentSize { get; set; }
        private CGPoint _closeScrollPoints { get; set; }
        private CGPoint _msgAreaContentOffset { get; set; }
        private bool touched { get; set; }
        private double _userMsgLocationX { get; set; }
        private double _contactMsgLocationX { get; set; }
        private double _msgLocationY { get; set; }

        CTFont Font;
        CAGradientLayer gradient;
        CGColor[] gradColor;
        UIColor _standartColor = UIColor.FromRGB(69, 58, 73);
        UIColor _touchedColor = UIColor.FromRGBA(69, 58, 73, 50);
        UIColor _textColor = UIColor.FromRGB(229, 218, 218);
        UIColor _contactImgColor = UIColor.FromRGB(109, 59, 71);

        public UserCardView(ContactModel Data)
        {
            Frame = new CGRect(50, 0, 200, 60);
            BackgroundColor = _standartColor;
            Layer.MaskedCorners = (CoreAnimation.CACornerMask)15;
            ClipsToBounds = true;
            Layer.CornerRadius = 20;
            Hidden = false;
            Font = new CTFont("Dubai", 18);
            PreviewFont = UIFont.FromName("Avenir Next", 18);
            ContactData = Data;
        }

        public UserCardView()
        {
        }

        //TODO: доделать frame open and close tb
        public void InitCard(CGRect SuperView)
        {
            _closeTextBoxFrame = new CGRect(10, ViewFrame.Height + 150, ViewFrame.Width - (ViewFrame.Width / 7) - 15, 50);
            _openTextBoxFrame = new CGRect(10, ViewFrame.Height - ViewFrame.Height / 10, ViewFrame.Width - (ViewFrame.Width / 7) - 15, 50);
            _openSendBtn = new CGRect(_openTextBoxFrame.X + _openTextBoxFrame.Width + 10,
                ViewFrame.Height - 100,
                ViewFrame.Width / 7, 50);
            _openGradFrame = new CGRect(0, 0, SuperView.Width, 150);
            _closeGradFrame = new CGRect(0, -150, Frame.Width, 150);
            _closedFrame = Frame;
            _openMsgArea = new CGRect(0, 150, ViewFrame.Width, _openTextBoxFrame.Y);
            _closeMsgArea = new CGRect(0, -ViewFrame.Height - 100, Frame.Width, ViewFrame.Height - 100);
            _closeScrollPoints = command.GetScrollPoint();

            //обработчик нажатий на uiscrollview
            UITapGestureRecognizer recognizer = new UITapGestureRecognizer(ToucheMsgArea);
            recognizer.NumberOfTapsRequired = 1;
            recognizer.Enabled = true;
            recognizer.CancelsTouchesInView = false;

            msgArea = new UIScrollView();
            msgArea.Frame = _closeMsgArea;
            msgArea.Hidden = false;
            msgArea.Bounces = false;
            msgArea.BouncesZoom = false;
            msgArea.AddGestureRecognizer(recognizer);
            _msgLocationY = _openGradFrame.Y;

            _contactMsgLocationX = 10;
            foreach (var msg in ContactData.Messages)
            {
                var msgBox = new MessageBoxView(msg);
                _userMsgLocationX = Superview.Frame.Width - msgBox.Frame.Width - 10;
                msgBox.Frame = msg.MessageFrom == UserID ?
                    new CGRect(_userMsgLocationX, _msgLocationY, msgBox.Frame.Width, msgBox.Frame.Height) :
                    new CGRect(_contactMsgLocationX, _msgLocationY, msgBox.Frame.Width, msgBox.Frame.Height);
                msgBox.Hidden = false;
                _msgLocationY += msgBox.Frame.Height + 10;

                msgArea.AddSubview(msgBox);
            }

           
            msgArea.ContentSize = new CGSize(0, _msgLocationY + 100);
            msgArea.SetContentOffset(new CGPoint(0, msgArea.ContentSize.Height), false);
            

            _msgAreaContentSize = msgArea.ContentSize;
            _msgAreaContentOffset = msgArea.ContentOffset;

            ContactName = ContactData.Name;
            LastMessage = ContactData.LastMessage;

            contactImg = new UIImageView();
            contactImg.Frame = _closeGradFrame;
            contactImg.Layer.CornerRadius = 20;
            contactImg.Image = UIImage.FromBundle("code_rain.jpg");
            _closeContactImgFrame = contactImg.Frame;

            gradColor = new CGColor[] { _standartColor.ColorWithAlpha(0).CGColor,
                _standartColor.ColorWithAlpha(1).CGColor };
            gradient = new CAGradientLayer();
            gradient.Colors = gradColor;
            contactImg.Layer.AddSublayer(gradient);

            lbContactName = new UILabel();
            lbContactName.Font = PreviewFont;
            lbContactName.Frame = new CGRect(20, 20, 100, 30);
            lbContactName.Text = ContactName;
            lbContactName.TextColor = _textColor;
            lbContactName.SizeToFit();
            _closeLbNameFrame = lbContactName.Frame;

            lbLastMessage = new UILabel();
            lbLastMessage.Font = PreviewFont;
            lbLastMessage.Frame = new CGRect(30, 60, Frame.Width * 3 / 4, 30);
            lbLastMessage.Text = LastMessage;
            lbLastMessage.TextColor = _textColor;

            var scrollerCommand = new ChangeUserCardSCroller(ref msgArea);

            SendBtn = new SendMsgButton(_closeTextBoxFrame);
            SendBtn.TouchDown += SendBtn_TouchDown;

            _textBox = new TextBoxView(_closeTextBoxFrame, scrollerCommand, ref SendBtn);
            _textBox.Started += _textBox_Started;
            _textBox.Changed += _textBox_Changed;

            AddSubviews(msgArea, contactImg, lbContactName, lbLastMessage, _textBox, SendBtn);
        }

        private void SendBtn_TouchDown(object sender, EventArgs e)
        {
            var msg = new Message
            {
                MessageFrom = UserID,
                MessageText = _textBox.Text
            };

            AddNewMsg(msg);
            LastMessage = msg.MessageText;
            lbLastMessage.Text = LastMessage;
            _textBox.Text = "";
        }

        public void AddNewMsg(Message msg)
        {
            var msgBox = new MessageBoxView(msg);
            if (msg.MessageFrom == UserID)
            {
                msgBox.Frame = _textBox.Frame;
            }

            Animate(0.5, () =>
            {
                _userMsgLocationX = Superview.Frame.Width - msgBox.EndFrame.Width - 10;
                msgBox.Frame = msg.MessageFrom == UserID ?
                    new CGRect(_userMsgLocationX, _msgLocationY, msgBox.EndFrame.Width, msgBox.EndFrame.Height) :
                    new CGRect(_contactMsgLocationX, _msgLocationY, msgBox.EndFrame.Width, msgBox.EndFrame.Height);
                msgBox.Hidden = false;
                _msgLocationY += msgBox.Frame.Height + 10;

                msgArea.AddSubview(msgBox);
                msgArea.ContentSize = new CGSize(0, msgArea.ContentSize.Height + msgBox.Frame.Height + 10);
                _msgAreaContentSize = msgArea.ContentSize;
            });
        }

        private void _textBox_Changed(object sender, EventArgs e)
        {
            //if (_textBox.Text.Length > 20)
            //{
            //    Animate(0.3, () =>
            //    {
            //        _textBox.Frame = new CGRect(_textBox.Frame.X, _textBox.Frame.Y - 20,
            //                _textBox.Frame.Width, _textBox.Frame.Height + 20);
            //    });
            //}
        }

        private void ToucheMsgArea()
        {
            if (_textBox.BecomeFirstResponder())
            {
                Animate(1, () =>
                {
                    _textBox.ResignFirstResponder();
                    _textBox.SetNewFrame(_openTextBoxFrame);
                    SendBtn.SetNewFrame(_openTextBoxFrame);
                    msgArea.Frame = _openMsgArea;
                    
                    //msgArea.ContentSize = new CGSize(msgArea.ContentSize.Width, msgArea.ContentSize.Height + 30);
                });

                // TODO:доработать слишком большой пробел
                msgArea.ContentSize = new CGSize(msgArea.ContentSize.Width, msgArea.ContentSize.Height - 120);
            }
        }

        private void _textBox_Started(object sender, EventArgs e)
        {
            _textBox.BecomeFirstResponder();
            Animate(0.5, new Action(() =>
            {
                var notification = UIKeyboard.Notifications.ObserveWillShow((s, E) =>
                {
                    _textBox.Frame = new CGRect(_textBox.Frame.X, E.FrameEnd.Y - _textBox.Frame.Height - 10, _textBox.Frame.Width, _textBox.Frame.Height);
                    SendBtn.SetNewFrame(_textBox.Frame);
                    msgArea.Frame = new CGRect(msgArea.Frame.X, msgArea.Frame.Y, msgArea.Frame.Width, E.FrameEnd.Y);
                    
                });
            }));
            msgArea.ContentSize = new CGSize(msgArea.ContentSize.Width, msgArea.ContentSize.Height + 120);
        }
        public void SetCommand(ICommand comm)
        {
            command = comm;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (!touched)
            {
                Superview.BringSubviewToFront(this);
                command.EnabledScroll(false);
                command.ContentOfSet(new CGPoint(0, Frame.Y));

                Action action = new Action(() =>
                {
                    Frame = new CGRect(0, Frame.Y, ViewFrame.Width, Superview.Frame.Height);
                    lbContactName.Frame = new CGRect(Frame.Width / 2 - lbContactName.Frame.Width / 2,
                        50, lbContactName.Frame.Width, lbContactName.Frame.Height);
                    lbLastMessage.Hidden = true;

                    gradient.Frame = _openGradFrame;
                    contactImg.Frame = _openGradFrame;
                    msgArea.Frame = _openMsgArea;
                    command.HiddenTabBar(true);
                });

                Animate(1, action, OpenAnimationEnd);
                touched = true;
            }
            else
            {
                if(_textBox.BecomeFirstResponder())
                {
                    msgArea.ContentSize = new CGSize(msgArea.ContentSize.Width, msgArea.ContentSize.Height - 120);
                }
                _textBox.ResignFirstResponder();

                command.ContentOfSet(_closeScrollPoints);
                command.EnabledScroll(true);
                Action actionClose = new Action(() =>
                {
                    contactImg.Frame = _closeContactImgFrame;
                    msgArea.Frame = _closeMsgArea;
                    Frame = _closedFrame;
                    lbContactName.Frame = _closeLbNameFrame;
                    lbLastMessage.Hidden = false;
                    _textBox.SetNewFrame(_closeTextBoxFrame);
                    SendBtn.SetNewFrame(_textBox.Frame);
                    command.HiddenTabBar(false);
                });
                Animate(0.5, actionClose);

                //msgArea.ContentSize = new CGSize(msgArea.ContentSize.Width, _msgLocationY);
                //msgArea.ContentOffset = new CGPoint(msgArea.ContentOffset.X, msgArea.ContentSize.Height);
                touched = false;
            }
            
        }

        private void OpenAnimationEnd()
        {
            Animate(1, () => {
                _textBox.SetNewFrame(_openTextBoxFrame);
                SendBtn.SetNewFrame(_textBox.Frame);
            });
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            //var context = UIGraphics.GetCurrentContext();
            //context.ScaleCTM(1, -1); // you flipped the context, now you must use negative Y values to draw "into" the view

            //var textHeight = Font.CapHeightMetric; // lets use the actaul height of the font captials.

            //DrawText(context, ContactData, textHeight, -25, 20);

        }

        private void DrawText(CGContext context, string text, nfloat textHeight, nfloat x, nfloat y)
        {
            context.TranslateCTM(-x, -(y + textHeight));
            context.SetFillColor(_textColor.CGColor);



            var attributedString = new NSAttributedString(text,
                new CTStringAttributes
                {
                    ForegroundColorFromContext = true,
                    Font = this.Font
                });

            CGRect sizeOfText;
            using (var textLine = new CTLine(attributedString))
            {
                textLine.Draw(context);
                sizeOfText = textLine.GetBounds(CTLineBoundsOptions.UseOpticalBounds);
            }
            // Reset the origin back to where is was
            context.TranslateCTM(x - sizeOfText.Width, y + sizeOfText.Height);
        }
    }
}