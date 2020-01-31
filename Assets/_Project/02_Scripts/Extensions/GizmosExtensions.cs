// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GizmosExtensions.cs" company="Supyrb">
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
	public static class GizmosExtensions
	{
		private static Mesh _cone;

		/// <summary>
		/// Draws a cone on the position of a Transform
		/// </summary>
		/// <param name="transform">The cone will be drawn </param>
		public static void DrawCone(Transform transform)
		{
			#if UNITY_EDITOR
			if (_cone == null)
			{
				_cone = UnityEditor.EditorGUIUtility.Load("Assets/Editor Default Resources/cone.fbx") as Mesh;
			}

			if (_cone == null)
			{
				return;
			}

			Gizmos.DrawMesh(_cone, transform.position, transform.rotation);
			#endif
		}
	}
}