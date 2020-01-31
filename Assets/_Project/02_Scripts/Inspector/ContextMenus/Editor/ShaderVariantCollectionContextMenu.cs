// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShaderVariantCollectionContextMenu.cs" company="Supyrb">
//   Copyright (c) 2018 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Supyrb
{
	public class ShaderVariantCollectionContextMenu
	{
		[MenuItem("CONTEXT/ShaderVariantCollection/LogInfo", false, 10)]
		public static void SelectScriptabelObjectScript(MenuCommand menuCommand)
		{
			var target = menuCommand.context as ShaderVariantCollection;
			Debug.LogFormat("{0}: Shader Count:{1}, Variant Count: {2}", target.name, target.shaderCount, target.variantCount);
		}

		[MenuItem("CONTEXT/ShaderVariantCollection/SplitIntoShaderChunks", false, 10)]
		public static void SplitIntoShaderChunks(MenuCommand menuCommand)
		{
			var target = menuCommand.context as ShaderVariantCollection;
			if (target == null)
			{
				return;
			}

			SerializedObject serializedObject = new UnityEditor.SerializedObject(target);

			SerializedProperty shaders = serializedObject.FindProperty("m_Shaders");
			var shaderCount = shaders.arraySize;
			var shaderCountString = shaderCount.ToString();

			var assetPath = AssetDatabase.GetAssetPath(target);
			var folderPath = Path.GetDirectoryName(assetPath) + "/ShaderVariants/";
			Directory.CreateDirectory(folderPath);

			try
			{
				for (int i = 0; i < shaderCount; i++)
				{
					EditorUtility.DisplayProgressBar("Splitting ShaderVariantCollection", String.Format("Splitting ShaderVariantCollection into separate Files {0}/{1}", i.ToString(), shaderCountString), (float) i / shaderCount);
					SerializedProperty shaderVariantProperty = shaders.GetArrayElementAtIndex(i);
					if (shaderVariantProperty == null)
					{
						continue;
					}

					CreateShaderVariantCollection(shaderVariantProperty, folderPath);
				}
			}
			finally
			{
				EditorUtility.ClearProgressBar();
			}
		}

		private static void CreateShaderVariantCollection(SerializedProperty shaderVariantProperty, string folderPath)
		{
			Shader shader = (Shader) shaderVariantProperty.FindPropertyRelative("first").objectReferenceValue;
			SerializedProperty variantsProperty = shaderVariantProperty.FindPropertyRelative("second.variants");

			if (shader == null || variantsProperty == null)
			{
				return;
			}

			var variantCollectionName = shader.name.Replace('/', '-');
			ShaderVariantCollection collection = new ShaderVariantCollection();
			collection.name = variantCollectionName;

			for (int i = 0; i < variantsProperty.arraySize; i++)
			{
				SerializedProperty variantProperty = variantsProperty.GetArrayElementAtIndex(i);
				string keywords = variantProperty.FindPropertyRelative("keywords").stringValue;
				PassType passType = (PassType) variantProperty.FindPropertyRelative("passType").intValue;
				collection.Add(new ShaderVariantCollection.ShaderVariant(shader, passType, keywords.Split(' ')));
			}

			var filePath = folderPath + variantCollectionName + ".shadervariants";

			CreateOrReplaceAsset(collection, filePath);
		}

		private static void CreateOrReplaceAsset<T>(T data, string filePath) where T : UnityEngine.Object
		{
			T existingFile = AssetDatabase.LoadMainAssetAtPath(filePath) as T;
			if (existingFile != null)
			{
				EditorUtility.CopySerialized(data, existingFile);
				AssetDatabase.SaveAssets();
			}
			else
			{
				AssetDatabase.CreateAsset(data, filePath);
			}
		}
	}
}