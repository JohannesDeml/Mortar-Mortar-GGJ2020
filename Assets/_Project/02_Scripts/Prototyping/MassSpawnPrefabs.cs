// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MassSpawnPrefabs.cs" company="Supyrb">
//   Copyright (c)  Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public sealed class MassSpawnPrefabs : AMassSpawn
	{
		[SerializeField]
		private GameObject[] prefabs = null;		

		protected override GameObject SpawnElement(Vector3 position, int counter)
		{
			#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				var instance = UnityEditor.PrefabUtility.InstantiatePrefab(prefabs[counter % prefabs.Length]) as GameObject;
				if (instance != null)
				{
					UnityEditor.Undo.RegisterCreatedObjectUndo(instance, "Spawn Prefab");
					instance.transform.position = position;
					instance.transform.SetAsLastSibling();
				}
				return instance;
			}
			#endif
			
			return Instantiate(prefabs[counter%prefabs.Length], position, Quaternion.identity);
		}
	}
}