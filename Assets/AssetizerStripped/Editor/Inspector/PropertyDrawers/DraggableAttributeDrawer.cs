// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableAttributeDrawer.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using Supyrb.Assetizer;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Supyrb.AssetizerEditor
{
	/// <inheritdoc />
	[CustomPropertyDrawer(typeof(DraggableAttribute))]
	public class DraggableAttributeDrawer : PropertyDrawer
	{
		public static class Styles
		{
			private static GUIStyle selectStyleButton;
			private static GUIStyle gotoSubAssetStyle;

			public static GUIStyle SelectButtonStyle
			{
				get
				{
					if (selectStyleButton == null || true)
					{
						selectStyleButton = new GUIStyle((GUIStyle) "IconButton");
						#if UNITY_2018_1_OR_NEWER
						var texture = EditorGUIUtility.FindTexture("MoveTool");
						#else
							var texture = EditorGUIUtility.FindTexture("d_MoveTool");
						#endif
						selectStyleButton.margin.top = 4;
						selectStyleButton.margin.left = 4;
						selectStyleButton.margin.bottom = 4;
						selectStyleButton.margin.right = 4;
						selectStyleButton.active.background = texture;
						selectStyleButton.normal.background = texture;
						selectStyleButton.hover.background = texture;
					}

					return selectStyleButton;
				}
			}

			public static GUIStyle GotoSubAssetStyle
			{
				get
				{
					if (gotoSubAssetStyle == null)
					{
						gotoSubAssetStyle = new GUIStyle((GUIStyle) "IconButton");
						#if UNITY_2018_1_OR_NEWER
						var texture = EditorGUIUtility.FindTexture("PlayButton");
						#else
						var texture = EditorGUIUtility.FindTexture("d_PlayButton");
						#endif
						gotoSubAssetStyle.active.background = texture;
						gotoSubAssetStyle.normal.background = texture;
						gotoSubAssetStyle.hover.background = texture;
					}

					return gotoSubAssetStyle;
				}
			}
		}

		public static readonly float DragButtonWidth = 16f;
		public static readonly float GotoButtonWidth = 12f;
		public static readonly float GotoButtonHeight = 12f;
		private Object[] activeSelection = new Object[1];

		private Type assetType;
		private DraggableAttribute draggableAttribute;

		/// <inheritdoc />
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			assetType = fieldInfo.DeclaringType;

			// Field with wrong type
			if (assetType == null || !assetType.IsSubclassOf(typeof(ScriptableObject)))
			{
				EditorGUI.PropertyField(position, property);
				return;
			}

			Event currentEvent = Event.current;
			// Empty field
			if (property.objectReferenceValue == null)
			{
				if (currentEvent.type == EventType.ContextClick && position.Contains(currentEvent.mousePosition))
				{
					CreateAndShowMenuContext(property);
					currentEvent.Use();
				}

				EditorGUI.PropertyField(position, property);
				return;
			}

			var indentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			position.xMin += indentLevel * EditorGUIUtility.singleLineHeight;
			var rect = position;
			// Drag Area
			rect.width = DragButtonWidth;
			GUI.Box(rect, GUIContent.none, Styles.SelectButtonStyle);
			EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);
			switch (currentEvent.type)
			{
				case EventType.MouseDown:
					// Handle object dragging
					if (rect.Contains(currentEvent.mousePosition))
					{
						DragAndDrop.PrepareStartDrag();
						activeSelection[0] = property.objectReferenceValue;
						DragAndDrop.objectReferences = activeSelection;
						DragAndDrop.StartDrag("Drag scriptableObject");
						currentEvent.Use();
					}

					break;
				case EventType.DragUpdated:
				case EventType.DragPerform:
					// Handle clicking the icon
					if (rect.Contains(currentEvent.mousePosition))
					{
						break;
					}

					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

					if (currentEvent.type == EventType.DragPerform)
					{
						DragAndDrop.AcceptDrag();

						EditorGUIUtility.PingObject(DragAndDrop.objectReferences[0]);
					}

					break;
			}

			// Property field
			rect = position;
			rect.xMin += DragButtonWidth + 2f;
			rect.xMax -= GotoButtonWidth;
			EditorGUI.PropertyField(rect, property, GUIContent.none, false);

			// Goto button
			var goToItemRect = rect;
			goToItemRect.xMin = rect.xMax + 2;
			goToItemRect.width = GotoButtonWidth;
			goToItemRect.y += (rect.height - GotoButtonHeight) / 2.0f;
			goToItemRect.height = GotoButtonHeight;

			if (GUI.Button(goToItemRect, GUIContent.none, Styles.GotoSubAssetStyle))
			{
				if (draggableAttribute == null)
				{
					draggableAttribute = attribute as DraggableAttribute;
				}

				if (draggableAttribute != null && draggableAttribute.OpenInNewWindow)
				{
					ScriptableObjectInspectorWindow.Init(property.objectReferenceValue as ScriptableObject);
				}
				else
				{
					Selection.activeObject = property.objectReferenceValue;
				}
			}

			EditorGUI.indentLevel = indentLevel;
		}

		private GUIContent _createGuiContent;

		private GUIContent createGuiConent
		{
			get
			{
				if (_createGuiContent == null)
				{
					_createGuiContent = new GUIContent("Create");
				}

				return _createGuiContent;
			}
		}

		private void CreateAndShowMenuContext(SerializedProperty property)
		{
			var menu = new GenericMenu();
			menu.AddItem(createGuiConent, false, CreateAsset, property);
			menu.ShowAsContext();
		}

		private void CreateAsset(object propertyObject)
		{
			var property = propertyObject as SerializedProperty;
			if (property == null)
			{
				return;
			}

			ScriptableObject asset = ScriptableObject.CreateInstance(assetType);
			var propertyPath = AssetDatabase.GetAssetPath(property.serializedObject.targetObject);
			var folderPath = propertyPath.Substring(0, propertyPath.LastIndexOf("/", StringComparison.InvariantCulture) + 1);
			folderPath = Path.Combine(folderPath, property.serializedObject.targetObject.name);
			// Create folders if not set
			Directory.CreateDirectory(Path.GetFullPath(folderPath));
			
			var path = Path.Combine(folderPath, assetType.Name + ".asset");
			path = AssetDatabase.GenerateUniqueAssetPath(path);

			AssetDatabase.CreateAsset(asset, path);
			property.objectReferenceValue = asset;
			property.serializedObject.ApplyModifiedProperties();
			AssetDatabase.SaveAssets();
		}
	}
}