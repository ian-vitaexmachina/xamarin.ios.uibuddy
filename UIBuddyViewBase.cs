using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace vitaexmachina.xamarin.ios.uibuddy
{

    public class UIBuddyViewBase<T> : IUIBuddyControl where T : UIView
    {
        public T Control { get; set; }
        public UIBuddyAnimateDirection AnimDirection { get; set; }
        public double AnimDelay { get; set; }
        public nfloat AnimDistance { get; set; }
        public bool FadeIn { get; set; }
        public UIView BuddyControl { get; set; }
        public UIBuddyLayoutBase StackControl { get; set; }
        public bool IsNested { get; set; }

        public Align HorizontalAlign { get; set; }
        public Align VerticalAlign { get; set; }
        
        public UIBuddyViewBase(UIView parent, T control)
        {
            Control = control;
            BuddyControl = control;
            AnimDirection = UIBuddyAnimateDirection.None;
        }

        public UIBuddyViewBase<T> CentreHorizontal()
        {
            Control.Center = new CGPoint(Control.Superview.Center.X - Control.Superview.Frame.X, Control.Center.Y);
            return this;
        }

        public UIBuddyViewBase<T> CentreVertical()
        {
            Control.Center = new CGPoint(Control.Center.X, Control.Superview.Center.Y);
            return this;
        }

        public UIBuddyViewBase<T> Translate(double x, double y)
        {
            Control.Center = new CGPoint(Control.Center.X + x, Control.Center.Y + y);
            return this;
        }

        public UIBuddyViewBase<T> TextAlign(UITextAlignment alignment)
        {
            if (Control is UILabel) {
                (Control as UILabel).TextAlignment = alignment;
            }

            return this;
        }

        public UIBuddyViewBase<T> TextType(UIBuddyTextStyleBase textType)
        {
            if (Control is UILabel)
            {
                UILabel l = (Control as UILabel);
                l.Font = UIFont.FromName(textType.FontName, textType.FontSize);
                l.TextColor = textType.TextColor;
            }

            return this;
        }

        public UIBuddyViewBase<T> WillAnimate(UIBuddyControlHelper helper, UIBuddyAnimateDirection direction, nfloat distance, double delay)
        {
            AnimDirection = direction;
            AnimDelay = delay;
            AnimDistance = distance;

            helper.animationList.Add(this);

            return this;
        }

        public UIBuddyViewBase<T> WillFadeIn(bool willFadeIn = false)
        {
            FadeIn = willFadeIn;

            if (FadeIn) {
                Control.Alpha = 0;
            }
            else {
                Control.Alpha = 1;
            }

            return this;
        }

        public void AnimateIn(double duration = 1.0f)
        {
            if (AnimDirection == UIBuddyAnimateDirection.Left)
            {
                UIView.AnimateNotify(duration, AnimDelay, UIViewAnimationOptions.CurveEaseOut,
                (Action)(() =>
                {
                    this.Control.Center = new CGPoint((nfloat)(this.Control.Center.X - AnimDistance), (nfloat)this.Control.Center.Y);
                    this.Control.Alpha = 1;
                }), null);
            }
            else
            {
                UIView.AnimateNotify(duration, AnimDelay, UIViewAnimationOptions.CurveEaseOut,
                (Action)(() =>
                {
                    this.Control.Center = new CGPoint((nfloat)(this.Control.Center.X + AnimDistance), (nfloat)this.Control.Center.Y);
                    this.Control.Alpha = 1;
                }), null);
            }
        }
    }
}
