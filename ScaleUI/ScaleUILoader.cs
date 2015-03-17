using System;
using UnityEngine;
using ICities;
using ColossalFramework.UI;

namespace ScaleUI
{

	public class ScaleUILoader : LoadingExtensionBase
	{
		UIComponent scaleuicomponent;

		public override void OnLevelLoaded (LoadMode mode)
		{
			base.OnLevelLoaded (mode);			
			if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame) {
				//get the root UIView, add the panel
				UIView v = UIView.GetAView ();
				//the game seems to cache UI classes, rename them to force a new instance while developing
				scaleuicomponent = v.AddUIComponent (typeof(ScaleUIPanel));
			}
		}

		public override void OnLevelUnloading ()
		{
			if (scaleuicomponent != null) {
				GameObject.Destroy (scaleuicomponent);
			}
			base.OnLevelUnloading ();
		}

	}
	
}
