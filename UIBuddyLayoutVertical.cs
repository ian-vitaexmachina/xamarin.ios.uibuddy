using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    
    public class UIBuddyLayoutVertical : UIBuddyLayoutBase
    {
        public UIBuddyLayoutVertical(UIView view, int width = 0, int height = 0, int paddingLeft = 0, int paddingRight = 0, int paddingTop = 0, int paddingBottom = 0, int paddingInner = 0)
            : base(view, width, height, paddingLeft, paddingRight, paddingTop, paddingBottom, paddingInner)
        {}
    }
}
