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
        IScaleUI scaleUIgui;
        GameObject corral;

        void OnDestroy ()
        {
            scaleUIgui.Destroy ();
        }

        void Start ()
        {
            try {
                InitUI ();
            } catch (Exception ex) {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, "ScaleUI: "+ex.ToString());
            }
            try {
                FixEverything ();
            } catch (Exception ex) {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, "ScaleUI: "+ex.ToString());
            }
        }

        void InitUI ()
        {
            corral = GameObject.Find ("CorralRegistrationGameObject");
            if (corral == null) {
                //use default ui
                UIView v = UIView.GetAView ();

                scaleUIgui = (ScaleUIPanel)v.AddUIComponent (typeof(ScaleUIPanel));
                ((ScaleUIPanel)scaleUIgui).SetIncreaseScaleCallBack (increaseScaleCallback);
                ((ScaleUIPanel)scaleUIgui).SetDecreaseScaleCallBack (decreaseScaleCallback);
            } else {
                scaleUIgui = new CorralScaleUI (corral, increaseScaleCallback, decreaseScaleCallback);
            }
        }

        public void keyhandle (EventType eventType, KeyCode keyCode, EventModifiers modifiers)
        {
            if (eventType == EventType.KeyDown && modifiers == EventModifiers.Control && (keyCode == KeyCode.Alpha0 || keyCode == KeyCode.Keypad0)) {
                SetDefaultScale ();
            }
        }

        private void increaseScaleCallback (String name)
        {
            increaseScale (null, null);
        }

        private void increaseScale (UIComponent component, UIMouseEventParameter eventParam)
        {
            UIView.GetAView ().scale += scalingfactor;
            FixEverything ();
        }

        private void decreaseScaleCallback (String name)
        {
            decreaseScale (null, null);
        }

        private void decreaseScale (UIComponent component, UIMouseEventParameter eventParam)
        {
            UIView.GetAView ().scale = Math.Max (UIView.GetAView ().scale - scalingfactor, 0.1f);
            FixEverything ();
        }
        
        private void SetDefaultScale ()
        {
            UIView.GetAView ().scale = 1f;
            FixEverything ();
        }

        private void FixEverything ()
        {
            FixCamera ();
            FixUIPositions ();
        }

        private void FixCamera ()
        {
            if (UIView.GetAView ().scale < 1.0f) {
                if (cameraIsFullscreen ()) {
                    return;
                }
                MakeCameraFullscreen.Initialize ();
            } else {
                //scaleui redirected camera
                if (MakeCameraFullscreen.cameraControllerRedirected) {
                    MakeCameraFullscreen.Deinitialize ();
                }
            }
        }

        private bool cameraIsFullscreen ()
        {
            if (MakeCameraFullscreen.cameraControllerRedirected) {
                return true;
            }
            CameraController cameraController = GameObject.FindObjectOfType<CameraController> ();
            if (cameraController != null) {
            
                Camera camera = cameraController.GetComponent<Camera> ();
                if (camera != null) {
            
                    if (Mathf.Approximately (camera.rect.width, 1) && Mathf.Approximately (camera.rect.height, 1)) {
                        //already fullscreen
                        return true;
                    }
                }
            }
            
            return false;
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
            UIComponent fullscreenContainer = UIView.GetAView ().FindUIComponent ("FullScreenContainer");
            UIComponent infomenu = UIView.GetAView ().FindUIComponent ("InfoMenu");

            infomenu.AlignTo (fullscreenContainer, UIAlignAnchor.TopLeft);
            infomenu.relativePosition += new Vector3 (20.0f, 15.0f);
        }
        
        private void fixInfoViewsContainer ()
        {
            //container with info buttons
            UIComponent infomenu = UIView.GetAView ().FindUIComponent ("InfoMenu");
            UIComponent infomenucontainer = UIView.GetAView ().FindUIComponent ("InfoViewsContainer");

            infomenucontainer.pivot = UIPivotPoint.TopCenter;
            infomenucontainer.transformPosition = new Vector3 (infomenu.GetBounds ().center.x, infomenu.GetBounds ().min.y);
            infomenucontainer.relativePosition += new Vector3 (0.0f, 5.0f);
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

