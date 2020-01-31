// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LayerMaskExtension.cs" company="Supyrb">
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
	public static class LayerMaskExtension
	{
		public static bool Contains(this LayerMask layerMask, int layer)
		{
			return ((layerMask.value & 1 << layer) != 0);
		}
	}
}