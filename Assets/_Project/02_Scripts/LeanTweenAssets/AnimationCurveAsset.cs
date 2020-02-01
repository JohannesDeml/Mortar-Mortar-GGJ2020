// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnimationCurveAsset.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Supyrb
{
	[Serializable]
	[CreateAssetMenu(fileName = "AnimationCurve", menuName = "LeanTween/AnimationCurveAsset")]
	public class AnimationCurveAsset : ScriptableObject
	{
		public AnimationCurve Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	}
}