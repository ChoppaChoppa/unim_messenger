using System;
using System.Diagnostics;
using System.Threading;
using CoreGraphics;
using Foundation;
using iAd;
using maui.Patterns.Commands;
using maui.Views;
using ObjCRuntime;
using UIKit;

namespace maui
{
    public partial class FirstViewController : UIViewController
    {
        private UIScrollView _cardScroller;
        private NSObject _keyboardObserverWillShow;
        private NSObject _keyboardObserverWillHide;


        public FirstViewController(IntPtr handle) : base(handle)
        {
            //TabBarController.TabBar.BackgroundColor = UIColor.FromRGB(40, 47, 68);
            _cardScroller = new UIScrollView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //_keyboardObserverWillShow = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardDidShowNotification);
            //_keyboardObserverWillHide = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHideNotification);

            var Contacts = Mock.ContactMock.GetContacts();


            Debug.WriteLine(View.Frame.Height);

            int startLocation = 50;
            _cardScroller.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);
            _cardScroller.Hidden = false;
            _cardScroller.ShowsVerticalScrollIndicator = false;
            
            
            foreach (var c in Contacts)
            {
                UserCardView card = new UserCardView(c);
                var commands = new ViewControllerCommands(ref _cardScroller, TabBarController.TabBar);
                card.SetCommand(commands);
                card.ViewFrame = View.Frame;
                card.Frame = new CGRect(20, startLocation, View.Frame.Width - 40, 130);
                startLocation += 150;

                _cardScroller.AddSubview(card);
                card.InitCard(_cardScroller.Frame);
            }
            _cardScroller.ContentSize = new CGSize(0, startLocation);

            View.AddSubviews(_cardScroller);
            InvokeOnMainThread(() => {
                View.BackgroundColor = UIColor.FromRGB(40, 47, 68);
                //UIImageView.Animate(animHeart.AnimationDuration, new Action(animHeart.StartAnimating));
            });
        }

        private void Card_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void _cardScroller_Scrolled(object sender, EventArgs e)
        {
            Debug.WriteLine(TabBarController.TabBar.Hidden);
        }

        //private void Card_TouchDownRepeat(object sender, EventArgs e)
        //{
        //    var control = sender as UserCardView;
        //    _cardScroller.BringSubviewToFront(control);
        //    _cardScroller.ContentOffset = new CGPoint(0, control.Frame.Y);
        //}

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        //private void KeyboardWillHideNotification(NSNotification notification)
        //{
        //    UIView activeView = View;
        //    if (activeView == null)
        //        return;

            
        //    UIScrollView scrollView = activeView.Superview.(this.View, typeof(UIScrollView)) as UIScrollView;
        //    if (scrollView == null)
        //        return;

        //    // Reset the content inset of the scrollView and animate using the current keyboard animation duration
        //    double animationDuration = UIKeyboard.AnimationDurationFromNotification(notification);
        //    UIEdgeInsets contentInsets = new UIEdgeInsets(0.0f, 0.0f, 0.0f, 0.0f);
        //    UIView.Animate(animationDuration, delegate {
        //        scrollView.ContentInset = contentInsets;
        //        scrollView.ScrollIndicatorInsets = contentInsets;
        //    });
        //}

        //private void KeyboardDidShowNotification(NSNotification notification)
        //{
        //    UIView activeView = View.FindFirstResponder();
        //    if (activeView == null)
        //        return;

        //    ((UITextField)activeView).ShowDoneButtonOnKeyboard();

        //    UIScrollView scrollView = activeView.FindSuperviewOfType(this.View, typeof(UIScrollView)) as UIScrollView;
        //    if (scrollView == null)
        //        return;


        //    RectangleF keyboardBounds = UIKeyboard.BoundsFromNotification(notification);

        //    UIEdgeInsets contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardBounds.Size.Height, 0.0f);
        //    scrollView.ContentInset = contentInsets;
        //    scrollView.ScrollIndicatorInsets = contentInsets;

        //    // If activeField is hidden by keyboard, scroll it so it's visible
        //    RectangleF viewRectAboveKeyboard = new RectangleF(this.View.Frame.Location, new SizeF(this.View.Frame.Width, this.View.Frame.Size.Height - keyboardBounds.Size.Height));

        //    RectangleF activeFieldAbsoluteFrame = activeView.Superview.ConvertRectToView(activeView.Frame, this.View);
        //    // activeFieldAbsoluteFrame is relative to this.View so does not include any scrollView.ContentOffset

        //    // Check if the activeField will be partially or entirely covered by the keyboard
        //    if (!viewRectAboveKeyboard.Contains(activeFieldAbsoluteFrame))
        //    {
        //        // Scroll to the activeField Y position + activeField.Height + current scrollView.ContentOffset.Y - the keyboard Height
        //        PointF scrollPoint = new PointF(0.0f, activeFieldAbsoluteFrame.Location.Y + activeFieldAbsoluteFrame.Height + scrollView.ContentOffset.Y - viewRectAboveKeyboard.Height);
        //        scrollView.SetContentOffset(scrollPoint, true);
        //    }
        //}
    }
}
//TODO: скрывать все карточки во время открытия нажатой карточки
//TODO: отключить скролл при открытии карточки
//TODO: изображение под именем контакта после открытия