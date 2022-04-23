using UnityEngine;

namespace ATH.GBS
{
	public static class FloatExtension
	{
		public static int SecondsToMils(this float value)
		{
			return (int)value * 1000;
		}
	}
}