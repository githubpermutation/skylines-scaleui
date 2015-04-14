using System;
using UnityEngine;
using ICities;
using ColossalFramework.UI;

namespace ScaleUI
{
    public class ScaleUILoader : LoadingExtensionBase
    {
        GameObject go;
        ScaleUI scaleUIinstance;

        public override void OnLevelLoaded (LoadMode mode)
        {
            base.OnLevelLoaded (mode);          
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame) {
                return;
            }

            go = GameObject.Find("ScaleUI");
            if(go == null) {
                go = new GameObject ("ScaleUI");
                scaleUIinstance = go.AddComponent<ScaleUI> ();
            }

            try {
                UIInput.eventProcessKeyEvent += new UIInput.ProcessKeyEventHandler (scaleUIinstance.keyhandle);
            } catch (Exception ex) {
                ex.ToString();
            }
        }

        public override void OnLevelUnloading ()
        {
            try {
                UIInput.eventProcessKeyEvent -= new UIInput.ProcessKeyEventHandler (scaleUIinstance.keyhandle);
                GameObject.Destroy (go);
            } catch (Exception ex) {
                ex.ToString();
            }
        }
    }
}
