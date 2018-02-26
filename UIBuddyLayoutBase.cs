using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace vitaexmachina.xamarin.ios.uibuddy
{
    
    public class UIBuddyLayoutBase : UIView, IUIBuddyControl
    {
        public UIBuddyAnimateDirection AnimDirection { get; set; }
        public double AnimDelay { get; set; }
        public nfloat AnimDistance { get; set; }
        public bool FadeIn { get; set; }
        public UIView BuddyControl { get; set; }
        public UIBuddyLayoutBase StackControl { get; set; }
        public Align HorizontalAlign { get; set; }
        public Align VerticalAlign { get; set; }
        public bool IsNested { get; set; }
        public bool ScaleToFit { get; set; }
        public bool AdjustFontSizeToFit { get; set; }

        public bool DebugMode { get; set; }
        public nfloat PaddingInner { get; set; }

        protected nfloat _height = 0;
        protected nfloat _width = 0;
        protected int _flexTotal = 0;

        protected int PaddingLeft = 0;
        protected int PaddingRight = 0;
        protected int PaddingTop = 0;
        protected int PaddingBottom = 0;

        public List<UIBuddyLayoutNode> Nodes = new List<UIBuddyLayoutNode>();

        public UIBuddyLayoutBase(UIView view, int width = 0, int height = 0, int paddingLeft = 0, int paddingRight = 0, int paddingTop = 0, int paddingBottom = 0, int paddingInner = 0)
        {
            PaddingTop = paddingTop;
            PaddingLeft = paddingLeft;
            PaddingRight = paddingRight;
            PaddingBottom = paddingBottom;
            PaddingInner = paddingInner;
            BuddyControl = this;

            if (view != null) {
                
                view.AddSubview(this);

                // Set the width and height based on inputs
                _width = (width == 0) ? (nfloat)(view.Frame.Width - (paddingLeft + paddingRight)) : width;
                _height = (height == 0) ? (nfloat)view.Frame.Height - (paddingTop + paddingBottom) : height;

                // Set the frame
                Frame = new CGRect(view.Frame.X + paddingLeft, view.Frame.Y + paddingTop, _width, _height);
            }

            // Store the padding
            
        }

        /// <summary>
        /// Reposition all child elements according to programmatic settings
        /// </summary>
        public void Reflow() {
            ReCalculate();
        }

        public UIView AddFlex(IUIBuddyControl subControl, int flex) {
            return Add(subControl, flex, 0, 0);
        }

        public UIView AddAbsolute(IUIBuddyControl subControl, int width, int height) {
            return Add(subControl, 0, height, width);
        }

        protected UIView Add(IUIBuddyControl subControl, int flex, int height, int width) 
        {
            if(subControl is UIBuddyLayoutBase) {
                subControl.StackControl = subControl as UIBuddyLayoutBase;
                subControl.StackControl.IsNested = true;
            }

            int containerHeight = 0;
            int containerWidth = 0;

            if (flex > 0) {
                _flexTotal += flex;

                // These get resized later
                containerWidth = 0;
                containerHeight = 0;
            }
            else {
                containerWidth = width;
                containerHeight = height;
            }

            // Creat a new containing view
            UIView containerView = new UIView();

            // Link ourselves into the UIView hierarchy, inherit the frame, and add the subcontrol
            AddSubview(containerView);
            containerView.AddSubview(subControl.BuddyControl);

            UIBuddyLayoutNode node = new UIBuddyLayoutNode(containerView, subControl, flex, containerHeight, containerWidth);
            Nodes.Add(node);

            if (DebugMode)
            {
                // Visualise the control
                containerView.Layer.BorderColor = new CGColor(1, 1, 1, 1);
                containerView.Layer.BorderWidth = 1.0f;

                // Visualise the layout view
                this.Layer.BorderColor = new CGColor(1, 1, 0, 1);
                this.Layer.BorderWidth = 2.0f;

                // Visualise the container
                if (!(subControl is UIBuddyLayoutBase)) {
                    subControl.BuddyControl.Layer.BorderColor = new CGColor(1, 0, 0, 1);
                    subControl.BuddyControl.Layer.BorderWidth = 1.0f;
                }
            }

            return containerView;
        }

        public void ReCalculate() {

            // Precalculate absolute heights and widths so that we know what is left for flexing
            nfloat totalAbsoluteHeight = 0;
            nfloat totalHeightLeft = 0;
            nfloat totalAbsoluteWidth = 0;
            nfloat totalWidthLeft = 0;

            foreach(UIBuddyLayoutNode node in Nodes) {
                if(this is UIBuddyLayoutVertical) {
                    totalAbsoluteHeight += node.Height;
                    totalHeightLeft = Frame.Height - totalAbsoluteHeight;
                } else {
                    totalAbsoluteWidth += node.Width;
                    totalWidthLeft = Frame.Width - totalAbsoluteWidth;
                }
            }

            // Run through the sub views and size
            int count = Nodes.Count;
            nfloat previousBottom = 0;// + this.Frame.Top;
            nfloat previousLeft = 0;// + this.Frame.Left;
                
            foreach (UIBuddyLayoutNode node in Nodes) {
                
                nfloat newHeight = 0;
                nfloat newWidth = 0;

                if (this is UIBuddyLayoutVertical)
                {
                    if (node.Flex == 0) {
                        // Just take the height we were given
                        newHeight = node.Height;

                    }
                    else {
                        // Calculate a new height based on flex
                        newHeight = (nfloat)totalHeightLeft * ((nfloat)node.Flex / (nfloat)_flexTotal);
                    }

                    // Always go full width
                    newWidth = node.Parent.Superview.Frame.Width;

                } else if (this is UIBuddyLayoutHorizontal) {
                    if (node.Flex ==0)
                    {
                        // Just take the width we were given
                        newWidth = node.Width;
                    }
                    else
                    {
                        // Calculate a new height based on flex
                        newWidth = (nfloat)totalWidthLeft * ((nfloat)node.Flex / (nfloat)_flexTotal);
                    }

                    // Always go full height
                    newHeight = node.Parent.Superview.Frame.Height;
                }

                if (node.Control.BuddyControl is UIBuddyLayoutBase) {
                    // Recalculate our widths and height based on the new enclosing container
                    _width = (node.Width == 0) ? (nfloat)(node.Parent.Frame.Width - (PaddingLeft + PaddingRight)) : node.Width;
                    _height = (node.Height == 0) ? (nfloat)node.Parent.Frame.Height - (PaddingTop + PaddingBottom) : node.Height;
                } 

                if (this is UIBuddyLayoutVertical) {
                    // Resize and reposition the vertical bounding layouts
                    node.Parent.Frame = new CGRect(0, previousBottom, newWidth, newHeight);
                    previousBottom = node.Parent.Frame.Bottom + PaddingInner;

                } else if (this is UIBuddyLayoutHorizontal) {

                    // Resize and reposition the horizontal bounding layouts
                    node.Parent.Frame = new CGRect(previousLeft, 0, newWidth, newHeight);
                    previousLeft = node.Parent.Frame.Right;
                } 

                if (!(node.Control.BuddyControl is UIBuddyLayoutBase)) {
                    // This is an actual control - see if there are absolute sizes provided, or if we need to expand to the parent
                    nfloat w, h;
                    w = (node.Control.BuddyControl.Frame.Width == 0) ? (nfloat)(node.Control.BuddyControl.Superview.Frame.Width) : node.Control.BuddyControl.Frame.Width;
                    h = (node.Control.BuddyControl.Frame.Height == 0) ? (nfloat)(node.Control.BuddyControl.Superview.Frame.Height) : node.Control.BuddyControl.Frame.Height;
                    node.Control.BuddyControl.Frame = new CGRect(0, 0, w, h);
                }

                if (node.Control.ScaleToFit) {
                    if (node.Control.BuddyControl is UILabel) {

                        UILabel label = (node.Control.BuddyControl) as UILabel;

                        UIFont font = label.Font;
                        CGSize size = label.Superview.Frame.Size;

                        for (var maxSize = label.Font.PointSize; maxSize >= label.MinimumScaleFactor * label.Font.PointSize; maxSize -= 1f) {
                            font = font.WithSize(maxSize);
                            CGSize constraintSize = new CGSize(size.Width, float.MaxValue);
                            CGSize labelSize = (label.Text.StringSize(font, constraintSize, UILineBreakMode.WordWrap));

                            if (labelSize.Height <= size.Height) {
                                label.Font = font;
                                label.SetNeedsLayout();
                                break;
                            }
                        }

                        // set the font to the minimum size anyway
                        label.Font = font;
                        label.SetNeedsLayout();
                    }
                }

                // Resize and reposition the contained controls
                if(node.Control.HorizontalAlign == Align.Center) {
                    node.Control.BuddyControl.Center = new CGPoint(node.Control.BuddyControl.Superview.Center.X - node.Control.BuddyControl.Superview.Frame.X, node.Control.BuddyControl.Center.Y);
                } 

                if (node.Control.VerticalAlign == Align.Center) {
                    node.Control.BuddyControl.Center = new CGPoint(node.Control.BuddyControl.Center.X, node.Control.BuddyControl.Superview.Center.Y - node.Control.BuddyControl.Superview.Frame.Top);
                }

                // Animation primings
                if (node.Control.AnimDirection == UIBuddyAnimateDirection.Left) {
                    node.Control.BuddyControl.Center = new CGPoint(node.Control.BuddyControl.Center.X + node.Control.AnimDistance, node.Control.BuddyControl.Center.Y);
                } else if (node.Control.AnimDirection == UIBuddyAnimateDirection.Right) {
                    node.Control.BuddyControl.Center = new CGPoint(node.Control.BuddyControl.Center.X - node.Control.AnimDistance, node.Control.BuddyControl.Center.Y);
                } else if (node.Control.AnimDirection == UIBuddyAnimateDirection.Up) {
                    node.Control.BuddyControl.Center = new CGPoint(node.Control.BuddyControl.Center.X, node.Control.BuddyControl.Center.Y + node.Control.AnimDistance);
                } else if (node.Control.AnimDirection == UIBuddyAnimateDirection.Down) {
                    node.Control.BuddyControl.Center = new CGPoint(node.Control.BuddyControl.Center.X, node.Control.BuddyControl.Center.Y - node.Control.AnimDistance);
                }

                // Resize and reposition contained stacks - but only stacks that are contained!
                if (node.Control.BuddyControl is UIBuddyLayoutBase && node.Control.StackControl.IsNested)  {
                node.Control.BuddyControl.Frame = new CGRect(node.Control.StackControl.PaddingLeft, node.Control.StackControl.PaddingTop, node.Parent.Frame.Width - (nfloat)(node.Control.StackControl.PaddingLeft + node.Control.StackControl.PaddingRight), node.Parent.Frame.Height - (nfloat)(node.Control.StackControl.PaddingTop + node.Control.StackControl.PaddingBottom));
                }
            }

            SetNeedsUpdateConstraints();
        }

       
        public UIBuddyLayoutBase WillAnimate(UIBuddyAnimateDirection direction, double delay) {

            AnimDirection = direction;
            AnimDelay = delay;

            if(AnimDirection == UIBuddyAnimateDirection.Left) {
                this.Center = new CGPoint(this.Center.X + 40, this.Center.Y);
            } else {
                this.Center = new CGPoint(this.Center.X - 40, this.Center.Y);
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
