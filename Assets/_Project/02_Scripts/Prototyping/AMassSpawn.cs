// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AMassSpawn.cs" company="Supyrb">
//   Copyright (c)  Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Supyrb
{
	public abstract class AMassSpawn : MonoBehaviour
	{		
		[SerializeField]
		private Vector3 spawnCount = new Vector3(100, 1, 100);

		[SerializeField]
		private Vector3 cellSize = new Vector3(1, 1, 1);
		
		[SerializeField]
		private Vector3 randomPositionOffset = new Vector3(0, 0, 0);

		[SerializeField]
		private bool changeName = false;
		
		[SerializeField]
		private string nameFormat = "{count}-{name}";
		
		[SerializeField]
		private bool spawnInSeparateScene = true;

		[SerializeField] private bool transformIsCenter = true;
		
		private Scene lastActiveScene;
		
		[ContextMenu("Spawn")]
		public void Spawn()
		{
			PrepareSpawning();
			
			var startPosition = GetStartPosition();
			var counter = 0;
			for (int z = 0; z < spawnCount.z; z++)
			{
				for (int y = 0; y < spawnCount.y; y++)
				{
					for (int x = 0; x < spawnCount.x; x++)
					{
						var position = startPosition + Vector3.Scale(new Vector3(x, y, z), cellSize);
						if (randomPositionOffset.sqrMagnitude > 0.0001f)
						{
							position += Vector3.Scale(Random.insideUnitSphere, randomPositionOffset);
						}
						var go = SpawnElement(position, counter);
						if (changeName)
						{
							go.name = GenerateGameObjectName(counter, go);
						}
						
						counter++;
					}
				}
			}
			
			FinishSpawning();
		}

		private string GenerateGameObjectName(int counter, GameObject go)
		{
			var goName = nameFormat;
			goName = goName.Replace("{count}", counter.ToString("0000"));
			goName = goName.Replace("{name}", go.name);
			return goName;
		}

		private Vector3 GetStartPosition()
		{
			var startPosition = transform.position;
			if (transformIsCenter)
			{
				startPosition -= Vector3.Scale(spawnCount, cellSize) * 0.5f;
			}

			return startPosition;
		}

		protected virtual void PrepareSpawning()
		{
			#if UNITY_EDITOR
			if (spawnInSeparateScene)
			{
				lastActiveScene = SceneManager.GetActiveScene();
				var spawnScene = SceneManager.GetSceneByName("MassSpawn");
				if (!spawnScene.IsValid())
				{
					spawnScene = SceneManager.CreateScene("MassSpawn");
				}

				SceneManager.SetActiveScene(spawnScene);
			}
			#endif
		}

		protected virtual void FinishSpawning()
		{
			#if UNITY_EDITOR
			if (spawnInSeparateScene)
			{
				SceneManager.SetActiveScene(lastActiveScene);
			}
			#endif
		}

		protected abstract GameObject SpawnElement(Vector3 position, int counter);
		
		#if UNITY_EDITOR
		private Bounds bounds = new Bounds();
		private void OnDrawGizmosSelected()
		{
			RefreshBoundingBox();
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(bounds.center, bounds.extents);

			if (randomPositionOffset.sqrMagnitude > 0.0001f)
			{
				Gizmos.color = Color.gray;
				Gizmos.DrawWireCube(bounds.center, bounds.extents + randomPositionOffset);
			}
		}

		private void RefreshBoundingBox()
		{
			bounds.extents = Vector3.Scale(spawnCount, cellSize);
			bounds.center = transform.position;
			if (!transformIsCenter)
			{
				bounds.center += bounds.extents * 0.5f;
			}
		}
		#endif
	}
}