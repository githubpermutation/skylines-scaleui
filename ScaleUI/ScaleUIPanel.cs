using System;
using System.Collections.Generic;
using UnityEngine;
using ColossalFramework.UI;

namespace ScaleUI
{
	public class ScaleUIPanel : UIPanel
	{

		UIButton increaseScaleButton;
		UIButton decreaseScaleButton;
		float thumbnailbarY = 0f;
		float scalingfactor = 0.05f;
		
		public override void OnDestroy ()
		{
			UIInput.eventProcessKeyEvent -= new UIInput.ProcessKeyEventHandler (this.processKeyEvent);
			base.OnDestroy ();
		}

		public override void Start ()
		{
			initPanel ();
												
			increaseScaleButton = createButton ();
			increaseScaleButton.text = "+";						
			increaseScaleButton.eventClick += increaseScale;						
						
			decreaseScaleButton = createButton();
			decreaseScaleButton.text = "-";
			decreaseScaleButton.eventClick += decreaseScale;
						
			UIInput.eventProcessKeyEvent += new UIInput.ProcessKeyEventHandler (this.processKeyEvent);
												
			fixUIPositions ();						
		}

		private void initPanel ()
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

		private void processKeyEvent (EventType eventType, KeyCode keyCode, EventModifiers modifiers)
		{
			if (eventType == EventType.KeyDown && modifiers == EventModifiers.Control && (keyCode == KeyCode.Alpha0 || keyCode == KeyCode.Keypad0)) {
				SetDefaultScale ();
			}
		}

		private void increaseScale (UIComponent component, UIMouseEventParameter eventParam)
		{
			UIView.GetAView ().scale += scalingfactor;
			fixUIPositions ();
		}
		
		private void decreaseScale (UIComponent component, UIMouseEventParameter eventParam)
		{
			UIView.GetAView ().scale = Math.Max (UIView.GetAView ().scale - scalingfactor, 1f);
			fixUIPositions ();
		}
		
		private void SetDefaultScale ()
		{
			UIView.GetAView ().scale = 1f;
			fixUIPositions ();
		}

		private void fixUIPositions ()
		{			
			fixFullScreenContainer();
			fixInfoMenu();
			fixInfoViewsContainer ();
			fixPoliciesPanel ();
			fixUnlockingPanel();

			fixScaleUIPanel ();
		}

		private void fixFullScreenContainer ()
		{
			//rescale the border around the window (when paused)
			UIComponent uic;
			uic = UIView.GetAView ().FindUIComponent ("ThumbnailBar");
			if (thumbnailbarY == 0f) {
				thumbnailbarY = uic.relativePosition.y;
			}
			float diffHeight = uic.relativePosition.y - thumbnailbarY;
			thumbnailbarY = uic.relativePosition.y;

			uic = UIView.GetAView ().FindUIComponent ("FullScreenContainer");
			uic.height += diffHeight;
			uic.relativePosition = new Vector2 (0, 0);
		}

		private void fixInfoMenu ()
		{
			//button top left
			UIComponent uic = UIView.GetAView ().FindUIComponent ("InfoMenu");
			uic.absolutePosition = new Vector3 (10, 10);
		}

		static void fixInfoViewsContainer ()
		{
			//container with info buttons
			UIComponent uic = UIView.GetAView ().FindUIComponent ("InfoViewsContainer");
			uic.absolutePosition = new Vector3 (0, 58);
		}

		private void fixPoliciesPanel ()
		{
			//much too big and can't be repositioned easily, need to reduce the size
			PoliciesPanel policies = ToolsModifierControl.policiesPanel;

			List<int> li = new List<int> ();
			li.Add (DistrictPolicies.CITYPLANNING_POLICY_COUNT);
			li.Add (DistrictPolicies.INDUSTRY_POLICY_COUNT);
			li.Add (DistrictPolicies.SERVICE_POLICY_COUNT);
			li.Add (DistrictPolicies.SPECIAL_POLICY_COUNT);
			li.Add (DistrictPolicies.TAXATION_POLICY_COUNT);
			li.Sort ();
			li.Reverse ();
			int maxPolicies = li [0];

			UIButton b = (UIButton)policies.Find ("PolicyButton");
			float buttonheight = b.height;
			policies.component.height = maxPolicies * buttonheight + 200f;
		}

		private void fixUnlockingPanel ()
		{
			//UnlockingPanel
			//position at top of screen so it's visible with scaled ui
			UnityEngine.Object obj = GameObject.FindObjectOfType (typeof(UnlockingPanel));
			ReflectionUtils.WritePrivate<UnlockingPanel> (obj, "m_StartPosition", new UnityEngine.Vector3 (-1f, 1f));
		}

		void fixScaleUIPanel ()
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
