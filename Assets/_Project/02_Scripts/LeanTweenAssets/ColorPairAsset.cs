// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorPairAsset.cs" company="Supyrb">
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
	[CreateAssetMenu(fileName = "ColorPair", menuName = "LeanTween/ColorPairAsset")]
	public class ColorPairAsset : ScriptableObject
	{
		#if UNITY_2018_1_OR_NEWER
		[ColorUsage(true, true)]
		#else
		[ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
		#endif
		public Color A = Color.white;

		#if UNITY_2018_1_OR_NEWER
		[ColorUsage(true, true)]
		#else
		[ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
		#endif
		public Color B = Color.white;
	}
}