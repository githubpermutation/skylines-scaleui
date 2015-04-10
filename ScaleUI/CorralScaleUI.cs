using System;
using UnityEngine;
using ColossalFramework.UI;
using ColossalFramework.Plugins;

namespace ScaleUI
{
    public class CorralScaleUI : MonoBehaviour, IScaleUI
    {
        public void FixUI ()
        {
        }

        public void SetIncreaseScaleCallBack (Action<String> callback)
        {
        }

        public void SetDecreaseScaleCallBack (Action<String> callback)
        {
        }

        GameObject corral;

        public CorralScaleUI (GameObject corralGameObject, Action<String> increaseScale, Action<String> decreaseScale)
        {
            this.corral = corralGameObject;
            
            createButton ("IncreaseScale", "Increase UI scale", increaseScale, "IncreaseScale");
            createButton ("DecreaseScale", "Decrease UI scale", decreaseScale, "DecreaseScale");
        }

        void OnDestroy ()
        {
            destroyButton ("IncreaseScale");
            destroyButton ("DecreaseScale");
        }

        void destroyButton (String text)
        {
            object[] paramArray = new object[2];
            paramArray [0] = "ScaleUI";
            paramArray [1] = text;
            corral.SendMessage ("DeRegisterMod", paramArray);
        }

        void createButton (String text, String hoverText, Action<String> callBack, String filenamePrefix)
        {
            object[] paramArray = new object[16];
            paramArray [0] = "ScaleUI";
            paramArray [1] = text;
            paramArray [2] = hoverText;
            Action<String> action = (String name) => callBack.Invoke("");
            paramArray [3] = action;

            paramArray [4] = filenamePrefix + "NormalBG";
            paramArray [5] = LoadIcon ("ScaleUI.icons." + filenamePrefix + "NormalBG.png");
            paramArray [6] = "randomstring";
            paramArray [7] = null;

            paramArray [8] = filenamePrefix + "HoveredBG";
            paramArray [9] = LoadIcon ("ScaleUI.icons." + filenamePrefix + "HoveredBG.png");
            paramArray [10] = "randomstring";
            paramArray [11] = null;

            paramArray [12] = filenamePrefix + "PressedBG";
            paramArray [13] = LoadIcon ("ScaleUI.icons." + filenamePrefix + "PressedBG.png");
            paramArray [14] = "randomstring";
            paramArray [15] = null;

            corral.SendMessage ("RegisterMod", paramArray);
        }

        private Texture2D LoadIcon (String filename)
        {
            System.IO.Stream s = System.Reflection.Assembly.GetExecutingAssembly ().GetManifestResourceStream (filename);
            
            byte[] bytes = new byte[s.Length];
            int bytesToRead = (int)s.Length;

            s.Read (bytes, 0, bytesToRead);
            s.Close ();
            
            Texture2D icon = new Texture2D (2, 2);
            icon.LoadImage (bytes);
            return icon;
        }
    }
}

