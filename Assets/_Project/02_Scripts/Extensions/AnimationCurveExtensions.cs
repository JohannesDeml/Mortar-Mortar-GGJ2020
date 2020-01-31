// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnimationCurveExtensions.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public static class AnimationCurveExtensions
	{
		/// <summary>
		/// Returns the time of the last animation key which is also the overall duration of the animation
		/// </summary>
		/// <param name="curve">Animation from which the duration is evaluated</param>
		/// <returns>The duration of the animation in seconds</returns>
		public static float Duration(this AnimationCurve curve)
		{
			if (curve.length == 0)
			{
				return 0f;
			}

			return curve[curve.length - 1].time;
		}

		public static float FirstValue(this AnimationCurve curve)
		{
			if (curve.length == 0)
			{
				return 0f;
			}

			return curve[0].value;
		}

		public static float LastValue(this AnimationCurve curve)
		{
			if (curve.length == 0)
			{
				return 0f;
			}

			return curve[curve.length - 1].value;
		}

		public static void ScaleValues(this AnimationCurve curve, float scaleFactor)
		{
			var keys = curve.keys;
			for (int i = 0; i < curve.length; i++)
			{
				var key = keys[i];
				key.value *= scaleFactor;
				curve.MoveKey(i, key);
			}
		}
	}
}