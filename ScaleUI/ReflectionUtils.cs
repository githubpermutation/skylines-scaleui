using UnityEngine;
using System.Reflection;

namespace ScaleUI
{
	public static class ReflectionUtils
	{
		//courtesy of nlight
		public static void WritePrivate<T> (UnityEngine.Object o, string fieldName, object value)
		{
			FieldInfo[] fields = typeof(T).GetFields (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			foreach (FieldInfo f in fields) {
				if (f.Name == fieldName) {
					f.SetValue (o, value);
					break;
				}
			}
		}
	}
}
