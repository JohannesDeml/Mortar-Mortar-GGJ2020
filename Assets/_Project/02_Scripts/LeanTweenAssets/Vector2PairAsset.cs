// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector2PairAsset.cs" company="Supyrb">
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
	[CreateAssetMenu(fileName = "Vector2Pair", menuName = "LeanTween/Vector2PairAsset")]
	public class Vector2PairAsset : ScriptableObject
	{
		public Vector2 A = Vector2.zero;
		public Vector2 B = Vector2.zero;
	}
}