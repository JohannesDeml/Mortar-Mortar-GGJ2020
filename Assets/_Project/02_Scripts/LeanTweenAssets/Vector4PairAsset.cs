// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector4PairAsset.cs" company="Supyrb">
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
	[CreateAssetMenu(fileName = "Vector4Pair", menuName = "LeanTween/Vector4PairAsset")]
	public class Vector4PairAsset : ScriptableObject
	{
		public Vector4 A = Vector4.zero;
		public Vector4 B = Vector4.zero;
	}
}