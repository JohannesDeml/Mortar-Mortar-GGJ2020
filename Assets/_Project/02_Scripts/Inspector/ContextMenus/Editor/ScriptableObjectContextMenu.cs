// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptableObjectContextMenu.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Supyrb.Assetizer
{
	/// <summary>
	/// <para>
	/// <conceptualLink target="Manual_Supyrb_Assetizer_ScriptableObjectContextMenu">Jump to Manual</conceptualLink>
	/// </para>
	/// Context menus for every <see cref="ScriptableObject"/>
	/// </summary>
	public class ScriptableObjectContextMenu
	{
		/// <summary>
		/// Select the <see cref="ScriptableObject"/> script in the project view
		/// </summary>
		/// <param name="menuCommand">Parsed menu command from MenuItem</param>
		[MenuItem("CONTEXT/ScriptableObject/Select Script", false, 10)]
		public static void SelectScriptabelObjectScript(MenuCommand menuCommand)
		{
			var serializedObject = new SerializedObject(menuCommand.context);
			var scriptProperty = serializedObject.FindProperty("m_Script");
			var scriptObject = scriptProperty.objectReferenceValue;
			Selection.activeObject = scriptObject;
		}

		/// <summary>
		/// Ping the <see cref="ScriptableObject"/> in the project view
		/// </summary>
		/// <param name="menuCommand">Parsed menu command from MenuItem</param>
		[MenuItem("CONTEXT/ScriptableObject/Ping Object")]
		public static void SelectScriptabelObject(MenuCommand menuCommand)
		{
			EditorGUIUtility.PingObject(menuCommand.context);
		}

		/// <summary>
		/// Validation Method for Assets/Create/Scriptable Object Instance
		/// Context menu only available when selected object is a ScriptableObject
		/// </summary>
		[MenuItem("Assets/Create/Scriptable Object Instance", true)]
		public static bool ValidateCreateScriptableObjectInstance()
		{
			var monoScript = Selection.activeObject as MonoScript;
			return IsScriptableObjectScript(monoScript);
		}

		/// <summary>
		/// Create an instance of the script that is selected. Only for ScriptableObject-subclasses
		/// </summary>
		[MenuItem("Assets/Create/Scriptable Object Instance")]
		public static void CreateScriptableObjectInstance()
		{
			var script = Selection.activeObject as MonoScript;
			if (!IsScriptableObjectScript(script))
			{
				return;
			}

			var instance = ScriptableObject.CreateInstance(script.GetClass());
			string path = AssetDatabase.GetAssetPath(script);
			path = Path.ChangeExtension(path, ".asset");

			AssetDatabase.CreateAsset(instance, path);
			AssetDatabase.SaveAssets();
		}
		
		/// <summary>
		/// Check if the script derives from ScriptableObject
		/// </summary>
		/// <param name="monoScript">A monoscript</param>
		/// <returns>True if the script derives from ScriptableObject</returns>
		public static bool IsScriptableObjectScript(MonoScript monoScript)
		{
			if (monoScript == null)
			{
				return false;
			}

			var scriptClass = monoScript.GetClass();
			return typeof(ScriptableObject).IsAssignableFrom(scriptClass);
		}
	}
}