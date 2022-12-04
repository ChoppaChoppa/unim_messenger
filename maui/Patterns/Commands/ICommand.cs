using System;
using CoreGraphics;
using maui.Views;
using UIKit;

namespace maui.Patterns.Commands
{
    public interface ICommand
    {
        void ContentOfSet(CGPoint rect);
        void HiddenTabBar(bool hidden);
        void EnabledScroll(bool enable);
        CGPoint GetScrollPoint();
    }
}
