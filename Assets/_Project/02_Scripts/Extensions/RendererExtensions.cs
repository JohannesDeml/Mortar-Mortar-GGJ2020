// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RendererExtensions.cs" company="Supyrb">
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
	public static class RendererExtensions
	{
		/// <summary>
		/// Calculate a precise AABB which considers the rotation and scale 
		/// of all vertices of the mesh.
		/// </summary>
		/// <param name="renderer">Renderer for the mesh the bounding box is calculated for</param>
		/// <returns>The precise Bounding box of the mesh</returns>
		public static Bounds CalculatePreciseBounds(this Renderer renderer)
		{
			var filter = renderer.GetComponent<MeshFilter>();
			return CalculatePreciseBounds(filter.sharedMesh, filter.transform);
		}

		public static Bounds CalculatePreciseBounds(this Mesh mesh, Transform transform)
		{
			if (mesh == null || transform == null || mesh.vertexCount == 0)
			{
				return new Bounds();
			}

			var verts = mesh.vertices;
			Bounds bounds = new Bounds(CalculateWorldPosition(verts[0], transform), Vector3.zero);
			for (int i = 0; i < verts.Length; i++)
			{
				bounds.Encapsulate(CalculateWorldPosition(verts[i], transform));
			}

			return bounds;
		}

		private static Vector3 CalculateWorldPosition(Vector3 vertex, Transform transform)
		{
			var local = transform.rotation * Vector3.Scale(vertex, transform.lossyScale);
			return local + transform.position;
		}
	}
}