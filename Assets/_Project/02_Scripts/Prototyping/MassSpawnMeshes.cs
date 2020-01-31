// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MassSpawnStuff.cs" company="Supyrb">
//   Copyright (c) 2018 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public sealed class MassSpawnMeshes : AMassSpawn
	{
		[SerializeField]
		private Material[] materials = null;

		[SerializeField]
		private Mesh[] meshes = null;

		private GameObject prototype;
		

		protected override void PrepareSpawning()
		{
			base.PrepareSpawning();
			prototype = new GameObject("MassSpawnElement", typeof(MeshFilter), typeof(MeshRenderer));
		}

		protected override void FinishSpawning()
		{
			base.FinishSpawning();
			Destroy(prototype);
		}

		protected override GameObject SpawnElement(Vector3 position, int counter)
		{
			var go = Instantiate(prototype, position, Quaternion.identity);
			go.GetComponent<MeshRenderer>().material = materials[counter % materials.Length];
			go.GetComponent<MeshFilter>().mesh = meshes[counter % meshes.Length];

			return go;
		}
	}
}