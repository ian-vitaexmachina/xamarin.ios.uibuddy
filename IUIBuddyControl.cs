using System;
using UIKit;
using CoreGraphics;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    public interface IUIBuddyControl
    {
        UIBuddyAnimateDirection AnimDirection { get; set; }
        double AnimDelay { get; set; }
        nfloat AnimDistance { get; set; }
        bool FadeIn { get; set; }
        bool IsNested { get; set; }
        UIView BuddyControl { get; set; }
        UIBuddyLayoutBase StackControl { get; set; }
        void AnimateIn(double duration = 1.0f);
        Align HorizontalAlign { get; set; }
        Align VerticalAlign { get; set; }
        bool ScaleToFit { get; set; }
    }
}
