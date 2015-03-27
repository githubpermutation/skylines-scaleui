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
			if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame) {
				return;
			}
			UIView v = UIView.GetAView ();
			scaleuicomponent = v.AddUIComponent (typeof(ScaleUIPanel));
		}

		public override void OnLevelUnloading ()
		{
			if (scaleuicomponent != null) {
				GameObject.Destroy (scaleuicomponent.gameObject);
			}
			base.OnLevelUnloading ();
		}
	}
}
