using System;
using UIKit;
using CoreGraphics;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    public class UIBuddyLabel : UILabel
    {
        public UIBuddyAnimateDirection AnimDirection { get; set; }
        public double AnimDelay { get; set; }
        public bool FadeIn { get; set; }
        public UIView ParentView { get; set; }

        public Align HorizontalAlign;
        public Align VerticalAlign;

        public UIBuddyLabel(UIView view, nfloat x, nfloat y, nfloat height, nfloat width)
        {
            AnimDirection = UIBuddyAnimateDirection.None;

            if(view != null) {
                ParentView = view;
                view.AddSubview(this);
            }
                                                    
            Frame = new CGRect(x, y, width, height);
            this.TextColor = UIColor.LightTextColor;
            this.Font = UIFont.FromName("TrebuchetMS", 20);
        }

      
        public UIBuddyLabel CentreHorizontal() {
            this.Center = new CGPoint(ParentView.Center.X, this.Center.Y) ;
            return this;
        }



        public UIBuddyLabel CentreVertical()
        {
            this.Center = new CGPoint(this.Center.X, ParentView.Center.Y);
            return this;
        }

        public UIBuddyLabel SetText(string text)
        {
            this.Text = text;
            return this;
        }

        public UIBuddyLabel Translate(double x, double y) {
            this.Center = new CGPoint(this.Center.X + x, this.Center.Y + y);
            return this;
        }

        public UIBuddyLabel Align(UITextAlignment alignment) {
            this.TextAlignment = alignment;
            return this;
        }

        public UIBuddyLabel WillAnimate(UIBuddyAnimateDirection direction, double delay) {


            AnimDirection = direction;
            AnimDelay = delay;

            if(AnimDirection == UIBuddyAnimateDirection.Left) {
                this.Center = new CGPoint(this.Center.X + 40, this.Center.Y);
            } else if (AnimDirection == UIBuddyAnimateDirection.Right) {
                this.Center = new CGPoint(this.Center.X - 40, this.Center.Y);
            } else if (AnimDirection == UIBuddyAnimateDirection.Up) {
                this.Center = new CGPoint(this.Center.X, this.Center.Y - 40);
            } else if (AnimDirection == UIBuddyAnimateDirection.Down) {
                this.Center = new CGPoint(this.Center.X, this.Center.Y + 40);
            }


            return this;
        }

        public UIBuddyLabel WillFadeIn(bool willFadeIn = false)
        {
            FadeIn = willFadeIn;

            if(FadeIn) {
                this.Alpha = 0;
            } else {
                this.Alpha = 1;
            }

            return this;
        }

        public void AnimateIn(double duration = 1.0f)
        {
            if(AnimDirection == UIBuddyAnimateDirection.Left){
                UIView.AnimateNotify(duration, AnimDelay, UIViewAnimationOptions.CurveEaseOut,
                () =>
                {
                    this.Center = new CGPoint(this.Center.X - 40, this.Center.Y);
                    this.Alpha = 1;
                }, null);
            } else {
                UIView.AnimateNotify(duration, AnimDelay, UIViewAnimationOptions.CurveEaseOut,
                () =>
                {
                    this.Center = new CGPoint(this.Center.X + 40, this.Center.Y);
                    this.Alpha = 1;
                }, null);
            }
        }
    }
}
