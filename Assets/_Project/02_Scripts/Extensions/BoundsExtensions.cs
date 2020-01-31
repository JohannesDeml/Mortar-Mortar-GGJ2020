// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoundsExtensions.cs" company="Supyrb">
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
	public static class BoundsExtensions
	{
		/// <summary>
		/// Calculates all corner positions of a bounding box 
		/// </summary>
		/// <param name="bounds">Bounds input</param>
		/// <returns>The corners in an array, the numeration starts at the bottom
		/// Indices of the corners
		///    7-------6
		///   /|      /|
		///  / |     / |
		/// 4--|----5  |
		/// |  3----|--2
		/// | /     | /
		/// 0-------1
		/// </returns>
		public static Vector3[] GetCorners(this Bounds bounds)
		{
			Vector3[] corners = new Vector3[8];
			var center = bounds.center;
			var extents = bounds.extents;

			// Bottom
			corners[0] = center + new Vector3(-extents.x, -extents.y, -extents.z);
			corners[1] = center + new Vector3(+extents.x, -extents.y, -extents.z);
			corners[2] = center + new Vector3(+extents.x, -extents.y, +extents.z);
			corners[3] = center + new Vector3(-extents.x, -extents.y, +extents.z);
			// Top
			corners[4] = center + new Vector3(-extents.x, +extents.y, -extents.z);
			corners[5] = center + new Vector3(+extents.x, +extents.y, -extents.z);
			corners[6] = center + new Vector3(+extents.x, +extents.y, +extents.z);
			corners[7] = center + new Vector3(-extents.x, +extents.y, +extents.z);

			return corners;
		}
	}
}