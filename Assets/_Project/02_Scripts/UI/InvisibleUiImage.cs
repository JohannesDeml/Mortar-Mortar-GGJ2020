// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvisibleUiImage.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Supyrb
{
	/// From http://answers.unity3d.com/answers/1157876/view.html
	/// A concrete subclass of the Unity UI `Graphic` class that just skips drawing.
	/// Useful for providing a raycast target without actually drawing anything.
	public class InvisibleUiImage : Graphic
	{
		public override void SetMaterialDirty()
		{
		}

		public override void SetVerticesDirty()
		{
		}

		/// Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}

		#if UNITY_EDITOR
		[SerializeField]
		private bool drawGizmo = false;

		[SerializeField]
		private Color gizmoColor = Color.red;

		private readonly Vector3[] corners = new Vector3[4];

		private void OnDrawGizmos()
		{
			if (!drawGizmo)
			{
				return;
			}

			rectTransform.GetWorldCorners(corners);
			Gizmos.color = gizmoColor;
			for (var i = 0; i < corners.Length; i++)
			{
				Gizmos.DrawLine(corners[i], corners[(i + 1) % corners.Length]);
			}
		}
		#endif
	}
}