// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptableObjectInspectorWindow.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Supyrb.AssetizerEditor
{
	/// <summary>
	/// <para>
	/// <conceptualLink target="Manual_Supyrb_Assetizer_ScriptableObjectInspectorWindow">Jump to Manual</conceptualLink>
	/// </para>
	/// Window that mimics an inspector. Used to show information of a <see cref="ScriptableObject"/> in a separate window.
	/// Opened through double-clicking a ScriptableObject (see OpenScriptableObjectListener).
	/// </summary>
	public class ScriptableObjectInspectorWindow : EditorWindow
	{
		public static class Styles
		{
			private static GUIStyle topBackground;

			public static GUIStyle TopBackgroundStyle
			{
				get
				{
					if (topBackground == null)
					{
						topBackground = (GUIStyle) "AnimationEventBackground";
					}

					return topBackground;
				}
			}
		}

		private static List<ScriptableObjectInspectorWindow> openWindows = new List<ScriptableObjectInspectorWindow>();
		
		[SerializeField]
		private ScriptableObject scriptableObject;
		private Editor editor;
		
		private Vector2 scrollPosition = Vector2.zero;
		private Object[] activeSelection = new Object[1];
		private readonly float topBarPaddingTop = 4f;
		private readonly float topBarPaddingBottom = 4f;


		/// <summary>
		/// Initialize the window
		/// </summary>
		/// <param name="scriptableObject">ScriptableObject the window is connected to.</param>
		public static void Init(ScriptableObject scriptableObject)
		{
			for (int i = 0; i < openWindows.Count; i++)
			{
				var currentWindow = openWindows[i];
				// Check for the name to offer solution when the editor was closed
				if (currentWindow.titleContent.text == scriptableObject.name)
				{
					if (currentWindow.scriptableObject == null)
					{
						currentWindow.SetScriptableObject(scriptableObject);
					}

					currentWindow.Focus();
					currentWindow.Show();
					return;
				}
			}

			ScriptableObjectInspectorWindow window = (ScriptableObjectInspectorWindow) EditorWindow.CreateInstance(typeof(ScriptableObjectInspectorWindow));
			var titleText = scriptableObject.name;
			var titleContent = new GUIContent(titleText);
			if (titleText.Length < 12)
			{
				titleContent.image = AssetPreview.GetMiniThumbnail(scriptableObject);
			}

			window.titleContent = titleContent;
			window.SetScriptableObject(scriptableObject);
			openWindows.Add(window);
			window.Show();
		}

		private void SetScriptableObject(ScriptableObject scriptableObject)
		{
			this.scriptableObject = scriptableObject;
			this.editor = Editor.CreateEditor(scriptableObject);
		}

		private void OnInspectorUpdate()
		{
			Repaint();
		}

		private void OnGUI()
		{
			if (scriptableObject == null)
			{
				if (!openWindows.Contains(this))
				{
					openWindows.Add(this);
				}

				GUILayout.Space(30);
				EditorGUILayout.HelpBox("This window forgot its scriptable object due to a editor restart.", MessageType.Error);
				if (GUILayout.Button("Find the scriptable object"))
				{
					string searchString = this.titleContent.text + " t:ScriptableObject";
					string[] assets = AssetDatabase.FindAssets(searchString);
					if (assets.Length == 0)
					{
						EditorUtility.DisplayDialog("No assets found", "No assets with the name " + searchString + " were found.", "Ok");
						return;
					}

					string assetPath = AssetDatabase.GUIDToAssetPath(assets[0]);
					SetScriptableObject(AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath));
				}

				return;
			}

			if (editor == null)
			{
				this.editor = Editor.CreateEditor(scriptableObject);
				if (!openWindows.Contains(this))
				{
					openWindows.Add(this);
				}
			}

			float width = this.position.width;
			float height = this.position.height;

			// Top Bar with selected object
			GUILayout.BeginHorizontal(Styles.TopBackgroundStyle);
			{
				GUILayout.Box(GUIContent.none, DraggableAttributeDrawer.Styles.SelectButtonStyle, GUILayout.Width(16f));
				HandleBoxDrag(GUILayoutUtility.GetLastRect());
				scriptableObject = (ScriptableObject) EditorGUILayout.ObjectField(GUIContent.none, scriptableObject, typeof(ScriptableObject), false);
			}
			GUILayout.EndHorizontal();

			// Scrollrect with ScriptableObject content
			var topHeight = topBarPaddingTop + EditorGUIUtility.singleLineHeight + topBarPaddingBottom;
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(width), GUILayout.Height(height - topHeight));
			{
				editor.OnInspectorGUI();
				GUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 4f);
			}
			GUILayout.EndScrollView();
		}

		/// <summary>
		/// Mimics the functionality of DragableAttribute
		/// Has to be called after the box is set with GUILayout.
		/// </summary>
		private void HandleBoxDrag(Rect dragableRect)
		{
			EditorGUIUtility.AddCursorRect(dragableRect, MouseCursor.Link);
			Event currentEvent = Event.current;
			switch (currentEvent.type)
			{
				case EventType.MouseDown:
					// Handle object dragging
					if (dragableRect.Contains(currentEvent.mousePosition))
					{
						DragAndDrop.PrepareStartDrag();
						activeSelection[0] = scriptableObject;
						DragAndDrop.objectReferences = activeSelection;
						DragAndDrop.StartDrag("Drag scriptableObject");
						currentEvent.Use();
					}

					break;
				case EventType.DragUpdated:
				case EventType.DragPerform:
					// Handle clicking the icon
					if (dragableRect.Contains(currentEvent.mousePosition))
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
		}

		private void OnDestroy()
		{
			openWindows.Remove(this);
		}
	}
}