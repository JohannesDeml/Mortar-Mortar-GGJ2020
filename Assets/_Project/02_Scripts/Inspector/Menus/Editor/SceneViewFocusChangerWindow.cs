// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SceneViewFocusChangerWindow.cs" company="Supyrb">
//   Copyright (c) 2018 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Supyrb
{
	/// <summary>
	/// Handy little tool to quickly change the focus of unity to avoid clipping in the scene view
	/// From https://forum.unity.com/threads/change-near-clipping-plane-scene-camera.456078/#post-3525716
	/// And https://gist.github.com/JohannesDeml/47dde1617161acfcd533abb715475017
	/// </summary>
	public class SceneViewFocusChangerWindow : EditorWindow
	{
		private static float nearClipping;

		[MenuItem("Supyrb/Misc/Scene Camera clipping")]
		static void Init()
		{
			SceneViewFocusChangerWindow window = (SceneViewFocusChangerWindow) EditorWindow.GetWindow(typeof(SceneViewFocusChangerWindow));
			window.Show();
		}

		private void OnInspectorUpdate()
		{
			Repaint();
		}

		void OnGUI()
		{
			EditorGUILayout.LabelField("Scene Camera near clipping", EditorStyles.boldLabel);
			var lastSceneView = SceneView.lastActiveSceneView;
			if (lastSceneView == null || lastSceneView.camera == null)
			{
				EditorGUILayout.HelpBox("No Scene view found", MessageType.Error);
				return;
			}

			nearClipping = lastSceneView.camera.nearClipPlane;
			EditorGUI.BeginChangeCheck();
			{
				nearClipping = EditorGUILayout.Slider("Near clipping", nearClipping, 0.01f, 50f);
			}
			if (EditorGUI.EndChangeCheck())
			{
				lastSceneView.size = nearClipping * 100f;
				lastSceneView.Repaint();
			}

			// Instant focus to a near position without caring about the last pivot
			if (GUILayout.Button("Focus near"))
			{
				lastSceneView.LookAt(lastSceneView.camera.transform.position + lastSceneView.camera.transform.forward * 3f, lastSceneView.rotation, 3f);
			}
		}
	}
}