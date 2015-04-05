using System;
using System.Collections.Generic;
using UnityEngine;
using ColossalFramework.UI;

namespace ScaleUI
{
    public interface IScaleUI
    {
        UIButton IncreaseScaleButton {
            get;
        }
        
        UIButton DecreaseScaleButton {
            get;
        }

        void FixUI();
    }

}
