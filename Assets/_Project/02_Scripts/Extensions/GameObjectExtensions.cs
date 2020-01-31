// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameObjectExtensions.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Supyrb
{
	public static class GameObjectExtensions
	{
		/// <summary>
		///   Returns the full path of a game object, i.e. the names of all
		///   ancestors and the game object itself.
		///   e.g. Canvas/RootPanel/UiGroup/Image
		/// </summary>
		/// <param name="gameObject">Game object to get the path of.</param>
		/// <returns>Full path of the game object.</returns>
		public static string GetFullPath(this GameObject gameObject)
		{
			return GetFullPath(gameObject, false);
		}

		/// <summary>
		///   Returns the full path of a game object, i.e. the names of all
		///   ancestors and the game object itself.
		///   e.g. GameScene/Canvas/RootPanel/UiGroup/Image
		/// </summary>
		/// <param name="gameObject">Game object to get the path of.</param>
		/// <param name="includeScene">Include the scene in the path information</param>
		/// <returns>Full path of the game object.</returns>
		public static string GetFullPath(this GameObject gameObject, bool includeScene)
		{
			if (gameObject == null)
			{
				return string.Empty;
			}

			string path = gameObject.name;
			var currentTransform = gameObject.transform;
			while (currentTransform.parent != null)
			{
				currentTransform = currentTransform.parent;
				var go = currentTransform.gameObject;
				path = go.name + "/" + path;
			}

			if (includeScene)
			{
				path = gameObject.scene.name + "/" + path;
			}

			return path;
		}

		/// <summary>
		/// Produces garbage
		/// </summary>
		/// <param name="go"></param>
		/// <returns></returns>
		public static List<GameObject> GetDescendants(this GameObject go)
		{
			var list = new List<GameObject>();
			go.GetDescendants(ref list);
			return list;
		}

		/// <summary>
		/// Produces garbage
		/// </summary>
		/// <param name="go"></param>
		/// <returns></returns>
		public static List<GameObject> GetDescendantsAndSelf(this GameObject go)
		{
			var list = new List<GameObject> {go};
			go.GetDescendants(ref list);
			return list;
		}

		public static void GetDescendants(this GameObject go, ref List<GameObject> list)
		{
			var transform = go.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				var childGo = transform.GetChild(i).gameObject;
				list.Add(childGo);
				childGo.GetDescendants(ref list);
			}
		}

		/// <summary>
		/// Produces garbage
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static List<GameObject> GetChildren(this GameObject parent)
		{
			var list = new List<GameObject>();
			var transform = parent.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i).gameObject;
				list.Add(child);
			}

			return list;
		}

		public static void SetLayers(this GameObject go, string layer)
		{
			go.SetLayers(LayerMask.NameToLayer(layer));
		}

		public static void SetLayers(this GameObject go, int layer)
		{
			go.layer = layer;
			var transform = go.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i).gameObject;
				child.SetLayers(layer);
			}
		}

		public static void SetActiveSave(this GameObject gameObject, bool active)
		{
			if (gameObject == null || gameObject.activeSelf == active)
			{
				return;
			}

			gameObject.SetActive(active);
		}

		public static GameObject AddChild(this GameObject parent, GameObject childPrefab)
		{
			return Object.Instantiate(childPrefab, parent.transform);
		}

		public static GameObject AddChild(this GameObject parent, GameObject childPrefab, Vector3 position, Quaternion localRotation)
		{
			return Object.Instantiate(childPrefab, position, localRotation, parent.transform);
		}

		public static GameObject AddChild(this GameObject parent, string name)
		{
			var go = new GameObject(name);
			go.transform.SetParent(parent.transform, false);
			return go;
		}

		public static void DestroyChildren(this GameObject parent)
		{
			var transform = parent.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i).gameObject;
				Object.Destroy(child);
			}
		}
	}
}