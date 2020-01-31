// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransformExtensions.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

static public class TransformExtensions
{
	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T>(this Component child) where T : Component
	{
		T result = child.GetComponent<T>();
		if (result == null)
		{
			result = child.gameObject.AddComponent<T>();
		}

		return result;
	}

	static public string HierarchyToString(this Transform transform)
	{
		string hierarchyInfo = "";
		while (transform != transform.root)
		{
			hierarchyInfo = "." + transform.name + hierarchyInfo;
			transform = transform.parent;
		}

		hierarchyInfo = transform.root.name + hierarchyInfo;

		return hierarchyInfo;
	}

	public static Transform FindChildDeep(this Transform aParent, string aName)
	{
		var result = aParent.Find(aName);
		if (result != null)
		{
			return result;
		}

		foreach (Transform child in aParent)
		{
			result = child.FindChildDeep(aName);
			if (result != null)
			{
				return result;
			}
		}

		return null;
	}

	public static Transform FindChildDeepContaining(this Transform aParent, string namePart)
	{
		var result = aParent.name.Contains(namePart) ? aParent : null;
		if (result != null)
		{
			return result;
		}

		foreach (Transform child in aParent)
		{
			result = child.FindChildDeep(namePart);
			if (result != null)
			{
				return result;
			}
		}

		return null;
	}

	public static Transform FindParentContaining(this Transform transform, string namePart)
	{
		if (transform == null)
		{
			return null;
		}

		if (transform.name.Contains(namePart))
		{
			return transform;
		}

		return FindParentContaining(transform.parent, namePart);
	}

	public static Transform FindChildContaining(this Transform aParent, string namePart)
	{
		var result = aParent.name.Contains(namePart) ? aParent : null;
		if (result != null)
		{
			return result;
		}

		foreach (Transform child in aParent)
		{
			result = child.name.Contains(namePart) ? child : null;
			if (result != null)
			{
				return result;
			}
		}

		return null;
	}
}