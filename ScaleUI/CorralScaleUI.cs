/*
The MIT License (MIT)

Copyright (c) 2015 brittanygh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 
*/
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
        
        GameObject corral;
        
        public CorralScaleUI (GameObject corralGameObject, Action<String> increaseScale, Action<String> decreaseScale)
        {
            this.corral = corralGameObject;
            
            createButton ("IncreaseScale", "Increase UI scale", increaseScale, "IncreaseScale");
            createButton ("DecreaseScale", "Decrease UI scale", decreaseScale, "DecreaseScale");
        }
        
        public void Destroy ()
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
            Action<String> action = (String name) => callBack.Invoke ("");
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

        //adapted from brittanygh
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