// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeshExtensions.cs" company="Supyrb">
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
	public static class MeshExtensions
	{
		public static Mesh ScaleMesh(this Mesh mesh, float scale)
		{
			var vertices = mesh.vertices;

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i];
				vertex += mesh.normals[i] * scale;
				vertices[i] = vertex;
			}

			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static Mesh ScaleCentered(this Mesh mesh, Vector3 scale)
		{
			var vertices = mesh.vertices;

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i] - mesh.bounds.center;
				vertex.x *= scale.x;
				vertex.y *= scale.y;
				vertex.z *= scale.z;
				vertices[i] = vertex + mesh.bounds.center;
			}

			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static Mesh ScaleCentered(this Mesh mesh, float scale)
		{
			return ScaleCentered(mesh, new Vector3(scale, scale, scale));
		}

		public static Mesh AddOffset(this Mesh mesh, float offset)
		{
			var vertices = mesh.vertices;

			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i];
				vertex += mesh.normals[i] * offset;
				vertices[i] = vertex;
			}

			mesh.vertices = vertices;
			mesh.RecalculateBounds();
			return mesh;
		}

		public static Mesh Clone(this Mesh mesh)
		{
			Mesh newMesh = new Mesh();
			newMesh.vertices = mesh.vertices;
			newMesh.normals = mesh.normals;
			newMesh.triangles = mesh.triangles;

			return newMesh;
		}

		public static Mesh InvertNormals(this Mesh mesh)
		{
			Vector3[] normals = mesh.normals;
			for (int i = 0; i < normals.Length; i++)
			{
				normals[i] *= -1;
			}

			mesh.normals = normals;
			mesh.RecalculateNormals();
			return mesh;
		}
	}
}