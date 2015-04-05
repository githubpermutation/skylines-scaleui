using System;
using UnityEngine;
using ColossalFramework.UI;
using System.Collections.Generic;

namespace ScaleUI
{
    public class ScaleUI : MonoBehaviour
    {
        float thumbnailbarY = 0f;
        float scalingfactor = 0.05f;
        String modTag = "[ScaleUI]";
        IScaleUI scaleUIgui;

        void Start ()
        {
            try {
                InitUI ();

                scaleUIgui.IncreaseScaleButton.eventClick += increaseScale;
                scaleUIgui.DecreaseScaleButton.eventClick += decreaseScale;

                this.FixUIPositions();
                            
            } catch (Exception ex) {
                String msg = this.modTag + " Could not initialize, aborting. Exception: " + ex.ToString ();
                DebugOutputPanel.AddMessage (ColossalFramework.Plugins.PluginManager.MessageType.Error, msg);
            } finally {
                UnityEngine.Object.Destroy (this.gameObject);
            }


        }

        void InitUI ()
        {
            UIView v = UIView.GetAView ();
            scaleUIgui = (ScaleUIPanel)v.AddUIComponent (typeof(ScaleUIPanel));
        }

        public void keyhandle (EventType eventType, KeyCode keyCode, EventModifiers modifiers)
        {
            if (eventType == EventType.KeyDown && modifiers == EventModifiers.Control && (keyCode == KeyCode.Alpha0 || keyCode == KeyCode.Keypad0)) {
                SetDefaultScale ();
            }
        }

        private void increaseScale (UIComponent component, UIMouseEventParameter eventParam)
        {
            UIView.GetAView ().scale += scalingfactor;
            FixUIPositions ();
        }
        
        private void decreaseScale (UIComponent component, UIMouseEventParameter eventParam)
        {
            UIView.GetAView ().scale = Math.Max (UIView.GetAView ().scale - scalingfactor, 1f);
            FixUIPositions ();
        }
        
        private void SetDefaultScale ()
        {
            UIView.GetAView ().scale = 1f;
            FixUIPositions ();
        }
        
        private void FixUIPositions ()
        {           
            try {
                fixFullScreenContainer ();
                fixInfoMenu ();
                fixInfoViewsContainer ();
                fixPoliciesPanel ();
                fixUnlockingPanel ();
                
                scaleUIgui.FixUI ();
            } catch (Exception ex) {
                String msg = modTag + " Could not move UI elements. Exception: " + ex.ToString ();
                DebugOutputPanel.AddMessage (ColossalFramework.Plugins.PluginManager.MessageType.Error, msg);
            }
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
        
        private void fixInfoViewsContainer ()
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
    }
}

