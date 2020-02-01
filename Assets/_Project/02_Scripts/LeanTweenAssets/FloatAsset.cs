// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FloatAsset.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
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
	[CreateAssetMenu(fileName = "Float", menuName = "LeanTween/FloatAsset")]
	public class FloatAsset : ScriptableObject
	{
		public float Value = 0f;
	}
}