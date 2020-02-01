// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatPairAsset.cs" company="Supyrb">
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
	[CreateAssetMenu(fileName = "FloatPair", menuName = "LeanTween/FloatPairAsset")]
	public class FloatPairAsset : ScriptableObject
	{
		public float A = 0f;
		public float B = 0f;
	}
}