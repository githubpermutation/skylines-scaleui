using System;
using UnityEngine;
using ColossalFramework.UI;

namespace ScaleUI
{
    public class ScaleUIPanel : UIPanel, IScaleUI
    {

        public UIButton IncreaseScaleButton {
            get {
                return this.increaseScaleButton;
            }
        }

        public UIButton DecreaseScaleButton {
            get {
                return this.decreaseScaleButton;
            }
        }

        UIButton increaseScaleButton;
        UIButton decreaseScaleButton;


        public ScaleUIPanel ()
        {
            InitPanel ();

            increaseScaleButton = createButton ();
            increaseScaleButton.text = "+";                     
            
            decreaseScaleButton = createButton ();
            decreaseScaleButton.text = "-";
        }


        private void InitPanel ()
        {
            this.backgroundSprite = "";
            this.width = 300;
            this.height = 300;
            
            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoLayoutStart = LayoutStart.TopLeft;
            this.autoLayoutPadding = new RectOffset (0, 0, 0, 0);
            this.autoLayout = true;
        }
        
        private UIButton createButton ()
        {
            UIButton button = this.AddUIComponent (typeof(UIButton)) as UIButton;
            
            button.horizontalAlignment = UIHorizontalAlignment.Center;
            button.verticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            
            button.autoSize = false;
            button.textScale = 1.5f;
            button.width = 46;
            button.height = 46;
            
            button.normalBgSprite = "OptionBase";
            button.disabledBgSprite = "OptionBaseDisabled";
            button.hoveredBgSprite = "OptionBaseHovered";
            button.focusedBgSprite = "OptionBaseFocused";
            button.pressedBgSprite = "OptionBasePressed";
            
            button.textColor = new Color32 (255, 255, 255, 255);
            button.disabledTextColor = new Color32 (7, 7, 7, 255);
            button.hoveredTextColor = new Color32 (7, 132, 255, 255);
            button.focusedTextColor = new Color32 (255, 255, 255, 255);
            button.pressedTextColor = new Color32 (30, 30, 44, 255);
            
            return button;
        }

        public void FixUI ()
        {
            //make scaling panel as big as it needs to be
            this.FitChildrenHorizontally ();
            this.FitChildrenVertically ();
            
            //position the panel below the menu button top right
            UIComponent uic = UIView.GetAView ().FindUIComponent ("Esc");
            float newX = uic.relativePosition.x + uic.width / 2 - this.width / 2;
            float newY = uic.relativePosition.y + uic.height + 10;
            this.relativePosition = new Vector3 (newX, newY);
        }
    }
}
