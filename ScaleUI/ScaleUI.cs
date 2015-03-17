using System;
using ICities;

namespace ScaleUI
{
	public class ScaleUIMod : IUserMod
	{
		public string Name {
			get {
				return "ScaleUI (beta)";
			}
		}

		public string Description {
			get {
				return "Adds buttons to scale the complete UI";
			}
		}
	}
}

