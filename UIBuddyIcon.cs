using UIKit;
using CoreGraphics;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    public class UIBuddyIcon : UIImageView
    {
        public UIBuddyAnimateDirection AnimDirection { get; set; }
        public double AnimDelay { get; set; }
        public bool FadeIn { get; set; }

        public UIImage _image;
        public UIBuddyIconSize IconSize;
        public UIView ParentView { get; set; }

        public Align HorizontalAlign;
        public Align VerticalAlign;

        public UIBuddyIcon(UIView view, string iconPath)
        {
            AnimDirection = UIBuddyAnimateDirection.None;

            if (view != null)
            {
                ParentView = view;
                view.AddSubview(this);
                Frame = view.Frame;
            }

            this.Image = UIImage.FromFile(iconPath);
            this.Image = this.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            this.Image.Init();
            this.Opaque = false;
            this.TintColor = UIColor.LightTextColor;
            this.Frame = new CGRect(0, 0, 10, 10);

        }

      
        public UIBuddyIcon CentreHorizontal() {
            this.Center = new CGPoint(ParentView.Center.X, this.Center.Y) ;
            return this;
        }

        public UIBuddyIcon CentreVertical()
        {
            this.Center = new CGPoint(this.Center.X, ParentView.Center.Y);
            return this;
        }

        public UIBuddyIcon Translate(double x, double y)
        {
            this.Center = new CGPoint(this.Center.X + x, this.Center.Y + y);
            return this;
        }

        public UIBuddyIcon SetSize(UIBuddyIconSize size) {
            IconSize = size;

            if(IconSize == UIBuddyIconSize.Small) {
                Frame = new CGRect(0, 0, 50, 50);
            } else if(IconSize == UIBuddyIconSize.Medium) {
                Frame = new CGRect(0, 0, 100, 100);
            } else if(IconSize == UIBuddyIconSize.Large) {
                Frame = new CGRect(0, 0, 200, 200);
            }

            return this;
        }

        public UIBuddyIcon WillAnimate(UIBuddyAnimateDirection direction, double delay) {

            AnimDirection = direction;
            AnimDelay = delay;

            if(AnimDirection == UIBuddyAnimateDirection.Left) {
                this.Center = new CGPoint(this.Center.X + 40, this.Center.Y);
            } else {
                this.Center = new CGPoint(this.Center.X - 40, this.Center.Y);
            }

            return this;
        }

        public UIBuddyIcon WillFadeIn(bool willFadeIn = false, double duration = 1)
        {
            FadeIn = willFadeIn;
            AnimationDuration = duration;

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

        public void AnimateFadeIn(double duration = 1.0f)
        {
            if (FadeIn)
            {
                UIView.AnimateNotify(duration, AnimDelay, UIViewAnimationOptions.CurveEaseOut,
                () =>
                {
                    this.Alpha = 1;
                }, null);
            }

        }
    }
}
