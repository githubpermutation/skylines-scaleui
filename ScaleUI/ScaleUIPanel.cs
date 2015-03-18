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
			UIInput.eventProcessKeyEvent -= new UIInput.ProcessKeyEventHandler (this.ProcessKeyEvent);
			UnityEngine.Object.Destroy (decreaseScaleButton);
			UnityEngine.Object.Destroy (increaseScaleButton);
			base.OnDestroy ();
		}

		public override void Start ()
		{
			this.backgroundSprite = "";
			this.width = 300;
			this.height = 300;						
					
			this.autoLayoutDirection = LayoutDirection.Vertical;
			this.autoLayoutStart = LayoutStart.TopLeft;
			this.autoLayoutPadding = new RectOffset (0, 0, 0, 0);
			this.autoLayout = true;
												
			increaseScaleButton = this.AddUIComponent (typeof(UIButton)) as UIButton;
			increaseScaleButton.text = "+";
			increaseScaleButton.horizontalAlignment = UIHorizontalAlignment.Center;
			increaseScaleButton.verticalAlignment = UIVerticalAlignment.Middle;
			increaseScaleButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			increaseScaleButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			increaseScaleButton.autoSize = false;
			increaseScaleButton.textScale = 1.5f;
			increaseScaleButton.width = 46;
			increaseScaleButton.height = 46;				
						
			increaseScaleButton.normalBgSprite = "OptionBase";
			increaseScaleButton.disabledBgSprite = "OptionBaseDisabled";
			increaseScaleButton.hoveredBgSprite = "OptionBaseHovered";
			increaseScaleButton.focusedBgSprite = "OptionBaseFocused";
			increaseScaleButton.pressedBgSprite = "OptionBasePressed";
			increaseScaleButton.textColor = new Color32 (255, 255, 255, 255);
			increaseScaleButton.disabledTextColor = new Color32 (7, 7, 7, 255);
			increaseScaleButton.hoveredTextColor = new Color32 (7, 132, 255, 255);
			increaseScaleButton.focusedTextColor = new Color32 (255, 255, 255, 255);
			increaseScaleButton.pressedTextColor = new Color32 (30, 30, 44, 255);
						
			increaseScaleButton.eventClick += IncreaseScale;						
						
			decreaseScaleButton = this.AddUIComponent (typeof(UIButton)) as UIButton;
			decreaseScaleButton.text = "-";
			decreaseScaleButton.horizontalAlignment = UIHorizontalAlignment.Center;
			decreaseScaleButton.verticalAlignment = UIVerticalAlignment.Middle;
			decreaseScaleButton.textHorizontalAlignment = UIHorizontalAlignment.Center;
			decreaseScaleButton.textVerticalAlignment = UIVerticalAlignment.Middle;
			decreaseScaleButton.autoSize = false;
			decreaseScaleButton.textScale = 1.5f;
			decreaseScaleButton.width = 46;
			decreaseScaleButton.height = 46;
						
			decreaseScaleButton.normalBgSprite = "OptionBase";
			decreaseScaleButton.disabledBgSprite = "OptionBaseDisabled";
			decreaseScaleButton.hoveredBgSprite = "OptionBaseHovered";
			decreaseScaleButton.focusedBgSprite = "OptionBaseFocused";
			decreaseScaleButton.pressedBgSprite = "OptionBasePressed";
			decreaseScaleButton.textColor = new Color32 (255, 255, 255, 255);
			decreaseScaleButton.disabledTextColor = new Color32 (7, 7, 7, 255);
			decreaseScaleButton.hoveredTextColor = new Color32 (7, 132, 255, 255);
			decreaseScaleButton.focusedTextColor = new Color32 (255, 255, 255, 255);
			decreaseScaleButton.pressedTextColor = new Color32 (30, 30, 44, 255);
						
			decreaseScaleButton.eventClick += DecreaseScale;
						
			UIInput.eventProcessKeyEvent += new UIInput.ProcessKeyEventHandler (this.ProcessKeyEvent);
												
			ResetUIPositions ();						
		}

		private void ProcessKeyEvent (EventType eventType, KeyCode keyCode, EventModifiers modifiers)
		{
			if (eventType == EventType.KeyDown && modifiers == EventModifiers.Control && (keyCode == KeyCode.Alpha0 || keyCode == KeyCode.Keypad0)) {
				SetDefaultScale ();
			}
		}

		private void IncreaseScale (UIComponent component, UIMouseEventParameter eventParam)
		{
			UIView.GetAView ().scale += scalingfactor;
			ResetUIPositions ();
		}
		
		private void DecreaseScale (UIComponent component, UIMouseEventParameter eventParam)
		{
			UIView.GetAView ().scale = Math.Max (UIView.GetAView ().scale - scalingfactor, 1f);
			ResetUIPositions ();
		}
		
		private void SetDefaultScale ()
		{
			UIView.GetAView ().scale = 1f;
			ResetUIPositions ();
		}

		private void ResetUIPositions ()
		{
			UIComponent uic = null;
			
			//rescale the border around the window (when paused)
			uic = UIView.GetAView ().FindUIComponent ("ThumbnailBar");
			if (thumbnailbarY == 0f) {
				thumbnailbarY = uic.relativePosition.y;
			}
			float diffHeight = uic.relativePosition.y - thumbnailbarY;
			thumbnailbarY = uic.relativePosition.y;

			uic = UIView.GetAView ().FindUIComponent ("FullScreenContainer");
			uic.height += diffHeight;
			uic.relativePosition = new Vector2 (0, 0);
			
			//button top left
			uic = UIView.GetAView ().FindUIComponent ("InfoMenu");
			uic.absolutePosition = new Vector3 (10, 10);
						
			//container with info buttons
			uic = UIView.GetAView ().FindUIComponent ("InfoViewsContainer");
			uic.absolutePosition = new Vector3 (0, 58);

			//policiespanel
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

			//UnlockingPanel
			//position at top of screen so it's visible with scaled ui
			UnityEngine.Object obj = GameObject.FindObjectOfType (typeof(UnlockingPanel));
			ReflectionUtils.WritePrivate<UnlockingPanel> (obj, "m_StartPosition", new UnityEngine.Vector3 (-1f, 1f));
			
			//make scaling panel as big as it needs to be
			this.FitChildrenHorizontally ();
			this.FitChildrenVertically ();
		
			//position the panel below the menu button top right
			uic = UIView.GetAView ().FindUIComponent ("Esc");
			float newX = uic.relativePosition.x + uic.width / 2 - this.width / 2;
			float newY = uic.relativePosition.y + uic.height + 10;
			this.relativePosition = new Vector3 (newX, newY);

			// not sure if necessary
			UIView.GetAView ().Invalidate ();
		}
	}
}
