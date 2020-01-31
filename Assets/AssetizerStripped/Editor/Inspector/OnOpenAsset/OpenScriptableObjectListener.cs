// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenScriptableObjectListener.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Supyrb.AssetizerEditor
{
	/// <summary>
	/// Listens <see cref="OnOpenAssetAttribute"/> (order 100) for <see cref="ScriptableObject"/> and its subclasses.
	/// Opens the asset in a new window (like an unchangeable inspector).
	/// </summary>
	public class OpenScriptableObjectListener
	{
		/// <summary>
		/// Listens <see cref="OnOpenAssetAttribute"/> (order 100) for <see cref="ScriptableObject"/> and its subclasses.
		/// </summary>
		/// <param name="instanceID"><see cref="OnOpenAssetAttribute"/></param>
		/// <param name="line"><see cref="OnOpenAssetAttribute"/></param>
		/// <returns>True if opening the asset is handled</returns>
		[OnOpenAsset(100)]
		public static bool HandleOpenAsset(int instanceID, int line)
		{
			Object obj = EditorUtility.InstanceIDToObject(instanceID);
			if (obj == null)
			{
				return false;
			}

			Type type = obj.GetType();

			if (type.IsSubclassOf(typeof(ScriptableObject)))
			{
				ScriptableObjectInspectorWindow.Init((ScriptableObject) obj);
				return true;
			}

			return false; // Not a scriptable object
		}
	}
}