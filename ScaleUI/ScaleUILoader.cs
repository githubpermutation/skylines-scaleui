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

            go = new GameObject ("ScaleUI");
            scaleUIinstance = go.AddComponent<ScaleUI> ();
            UIInput.eventProcessKeyEvent += new UIInput.ProcessKeyEventHandler (scaleUIinstance.keyhandle);
        }

        public override void OnLevelUnloading ()
        {
            UIInput.eventProcessKeyEvent -= new UIInput.ProcessKeyEventHandler (scaleUIinstance.keyhandle);
            GameObject.Destroy (go);
        }
    }
}
