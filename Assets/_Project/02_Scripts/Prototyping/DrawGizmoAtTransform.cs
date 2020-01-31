// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DrawGizmoAtTransform.cs" company="Supyrb">
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
	public class DrawGizmoAtTransform : MonoBehaviour
	{
		#if UNITY_EDITOR
		public bool DrawGizmo = true;
		public Color Color = Color.yellow;
		public GizmoType Type = GizmoType.Sphere;
		public float SphereSize = 0.07f;
		public Vector3 BoxSize = Vector3.one;
		public Mesh Mesh = null;

		[Tooltip("Icon in Assets/Gizmos")]
		public string IconName = "Icon";

		public bool DrawWireRepresentation = false;
		public bool ShowInEditMode = true;
		public bool ShowInPlayMode = true;


		public void OnDrawGizmos()
		{
			if (!DrawGizmo || !Application.isPlaying && !ShowInEditMode || Application.isPlaying && !ShowInPlayMode)
			{
				return;
			}

			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.color = Color;
			switch (Type)
			{
				case GizmoType.Sphere:
					DrawSphere();
					break;
				case GizmoType.Cube:
					DrawCube();
					break;
				case GizmoType.Icon:
					DrawIcon();
					break;
				case GizmoType.Mesh:
					DrawMesh();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			Gizmos.matrix = Matrix4x4.identity;
		}

		private void DrawSphere()
		{
			if (DrawWireRepresentation)
			{
				Gizmos.DrawWireSphere(Vector3.zero, SphereSize);
			}
			else
			{
				Gizmos.DrawSphere(Vector3.zero, SphereSize);
			}
		}

		private void DrawCube()
		{
			if (DrawWireRepresentation)
			{
				Gizmos.DrawWireCube(Vector3.zero, BoxSize);
			}
			else
			{
				Gizmos.DrawCube(Vector3.zero, BoxSize);
			}
		}

		private void DrawIcon()
		{
			Gizmos.DrawIcon(Vector3.zero, IconName);
		}

		private void DrawMesh()
		{
			if (DrawWireRepresentation)
			{
				Gizmos.DrawWireMesh(Mesh, Vector3.zero, Quaternion.identity);
			}
			else
			{
				Gizmos.DrawMesh(Mesh, Vector3.zero, Quaternion.identity);
			}
		}
		#endif
	}
}