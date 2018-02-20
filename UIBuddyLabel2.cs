using System;
using UIKit;
using CoreGraphics;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    public class UIBuddyLabel2 : UIBuddyViewBase<UILabel>
    {
        

        public UIBuddyLabel2(UIView view, nfloat x, nfloat y, nfloat height, nfloat width)
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

    }
}
