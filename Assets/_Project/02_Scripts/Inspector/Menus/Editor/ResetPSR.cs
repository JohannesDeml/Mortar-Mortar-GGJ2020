// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetPSR.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Supyrb.EditorTools
{
	public class ResetPSR
	{
		[MenuItem("GameObject/Reset PSR &r")]
		static void MoveSelectionToOrigin()
		{
			if (SceneObjectSelected())
			{
				ResetSceneSelectionToOrigin();
			}
			else
			{
				ResetAssetsToOrigin();
			}
		}

		private static void ResetSceneSelectionToOrigin()
		{
			Transform[] transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.Editable);
			ApplyActionOnSelectedTransforms(ResetTransform, transforms);
		}

		private static void ResetAssetsToOrigin()
		{
			var gameObjects = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets);
			var transforms = new Transform[gameObjects.Length];
			for (int i = 0; i < gameObjects.Length; i++)
			{
				GameObject obj = (GameObject) gameObjects[i];
				transforms[i] = obj.transform;
			}

			ApplyActionOnSelectedTransforms(ResetPosition, transforms);
			AssetDatabase.Refresh();
		}

		private static void ApplyActionOnSelectedTransforms(Action<Transform> action, Transform[] transforms)
		{
			int numberOfTransforms = transforms.Length;
			bool showProgressBar = (numberOfTransforms > 10);
			try
			{
				for (int i = 0; i < numberOfTransforms; i++)
				{
					if (showProgressBar)
					{
						EditorUtility.DisplayProgressBar("Reset PSR",
							"Reset position, scale and rotation (" + i + "/" + numberOfTransforms + ")",
							(float) i / (float) numberOfTransforms);
					}

					action(transforms[i]);
				}
			}
			finally
			{
				if (showProgressBar)
				{
					EditorUtility.ClearProgressBar();
				}
			}
		}

		private static void ResetPosition(Transform transformToReset)
		{
			Undo.RecordObject(transformToReset, "Reset PSR");
			transformToReset.localPosition = Vector3.zero;
		}

		private static void ResetTransform(Transform transformToReset)
		{
			Undo.RecordObject(transformToReset, "Reset PSR");
			transformToReset.localPosition = Vector3.zero;
			transformToReset.localScale = Vector3.one;
			transformToReset.localRotation = Quaternion.identity;
		}

		[MenuItem("GameObject/Reset position, scale and rotation &r", true)]
		static bool ValidateMoveSelectionToOrigin()
		{
			return SceneObjectSelected() || AssetsSelected();
		}

		static bool SceneObjectSelected()
		{
			return Selection.activeTransform != null;
		}

		static bool AssetsSelected()
		{
			return Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets).Length != 0;
		}
	}
}