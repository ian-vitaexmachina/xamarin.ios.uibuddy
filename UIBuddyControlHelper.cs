using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    public class UIBuddyControlHelper
    {
        public List<IUIBuddyControl> animationList = new List<IUIBuddyControl>();

        public UIBuddyControlHelper()
        {
        }

        public void DoAnimations() {

            foreach (IUIBuddyControl v in animationList)
            {
                if (v.AnimDirection != UIBuddyAnimateDirection.None)
                    v.AnimateIn();
            }

        }

        public static UILabel H1(CGRect frame)
        {

            UILabel label = new UILabel();
            if (frame != CGRect.Empty)
            {
                label.Frame = frame;
            } 

            label.Font = UIFont.FromName("TrebuchetMS", 20);
            label.TextColor = UIColor.LightTextColor;

            return label;
        }
    }
}
