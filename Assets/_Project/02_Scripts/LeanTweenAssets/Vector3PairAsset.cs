// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Vector3PairAsset.cs" company="Supyrb">
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
	[CreateAssetMenu(fileName = "Vector3Pair", menuName = "LeanTween/Vector3PairAsset")]
	public class Vector3PairAsset : ScriptableObject
	{
		public Vector3 A = Vector3.zero;
		public Vector3 B = Vector3.zero;
	}
}