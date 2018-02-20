using System;
using UIKit;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    public enum UIBuddyIconSize
    {
        Small,
        Medium,
        Large
    }

    public class UIBuddyTextStyleBase
    {

        public UIBuddyTextStyleBase(string fontName, UIColor textColor, int fontSize)
        {
            FontName = fontName;
            TextColor = textColor;
            FontSize = fontSize;
        }

        public string FontName { get; set; }
        public UIColor TextColor { get; set; }
        public int FontSize { get; set; }
    }


    public class UIBuddyH1 : UIBuddyTextStyleBase {

        public UIBuddyH1()
            : base("TrebuchetMS", UIColor.LightTextColor, 20)
        {}
    }


    public enum UIBuddyAnimateDirection
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public enum Align {
        Top,
        Center,
        Bottom,
        Left,
        Right
    }

    public class UIBuddyLayoutNode 
    {
        public UIBuddyLayoutNode(UIView parent, IUIBuddyControl control, int flex, int height, int width) {
            Parent = parent;
            Control = control;
            Flex = flex;
            Height = height;
            Width = width;
        }

        public UIView Parent { get; set; }
        public IUIBuddyControl Control { get; set; }
        public int Flex { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
