//using System;
//using UnityEngine;
//using ICities;
//using ColossalFramework.UI;
//
//namespace FrameworkExampleNamespace
//{
//	public class FrameworkMod : IUserMod
//	{
//		public string Name { get { return "Framework"; } }
//		public string Description { get { return "Framework"; } }
//	}
//
//	public class FrameworkThreading : ThreadingExtensionBase
//	{
//		UIPanel panel;
//
//		public override void OnUpdate (float realTimeDelta, float simulationTimeDelta)
//		{
//			//before every recompile, hit ctrl+shift+d to remove the old panel
//			//then recompile / copy the dll, switch to game, hit ctrl+d to spawn the panel
//			if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D)) {
//				if(Input.GetKey(KeyCode.LeftShift)) {
//					DestroyPanel();
//				} else {
//					InitPanel();
//				}
//			}
//		}
//
//		void DestroyPanel ()
//		{
//			if(panel != null) {
//				GameObject.Destroy(panel);
//			}
//		}
//
//		void InitPanel ()
//		{
//			DestroyPanel();
//
//			//the game caches (UI?) classes, so while developing init your UI here
//			//alternatively use your own class, but rename it before each recompile
//
//			//UIPanel or any UIComponent you want
//			panel = UIView.GetAView().AddUIComponent(typeof(UIPanel)) as UIPanel;
//			panel.backgroundSprite = "GenericPanel";
//			panel.color = new Color32(255,0,0,100);
//			panel.width = 100;
//			panel.height = 200;
//		}
//
//
//	}
//}
//
