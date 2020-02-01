// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShaderParameterDrawer.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Supyrb
{
	[CanEditMultipleObjects]
	[CustomPropertyDrawer(typeof(ShaderParameter))]
	public class ShaderParameterDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			{
				var nameProperty = property.FindPropertyRelative("name");

				// Don't make child fields be indented
				var indent = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;

				nameProperty.stringValue = EditorGUI.TextField(position, label, nameProperty.stringValue);
				EditorGUI.indentLevel = indent;
			}
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight;
		}
	}
}