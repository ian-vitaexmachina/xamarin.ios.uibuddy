# Xamarin.iOS.StackLayout
A programmatic approach to building Xamarin.iOS UIs with stack views and animations

## Why? Doesn't Xamarin.iOS already have two stack layout classes?
Yes, it does. They also work quite well for many tasks, but I wanted to create more control (my way) and to include animations for the content inside the stack nodes. I also find coding fiddly (x, y, width, height) problems strangely amusing, so I wanted to create one myself.

## Key Features
1. VerticalLayout 
2. HorizontalLayout
3. Nested layouts inside other layouts
4. Flex and absolute heights and widths
5. Borders and padding commands for layouts and layout nodes
6. Alignment commands for all controls inside layout nodes
7. Works with any UIView based class (i.e. pretty much every UI control)
8. Slide animations for controls on segues into / out of a storyboard
9. Debug mode where you can see outlines of controls and nesting during development 

## An example
```c#
// Create a new vertical layout, as wide as the screen, 150 points hight, with border indents
UIBuddyLayoutVertical vLayout = new UIBuddyLayoutVertical(View, 0, 150, 5, 5, 20, 0, 0);

// Create a UILabel 100 points wide and 20 points high
buddyLabel = new UIBuddyViewBase<UILabel>(vLayout, UIBuddyControlHelper.H1(new CGRect(0,0,100,20)));

// Add the Lable to the vertical layout with Flex of 1 (takes up the whole control)
vLayout.AddWithFlex(buddyLabel, 1);

// And set some control details (buddyLabel.Control is of type UILabel in this case)
buddyLabel.Control.Text = "Vita Ex Machina";
buddyLabel.Control.TextAlignment = UITextAlignment.Center;
buddyLabel.HorizontalAlign = Align.Center;
```
