// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeScaleWindow.cs" company="Supyrb">
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
	public class TimeScaleWindow : EditorWindow
	{
		private static float timeScale;

		[MenuItem("Supyrb/Misc/TimeScale")]
		static void Init()
		{
			TimeScaleWindow window = (TimeScaleWindow) EditorWindow.GetWindow(typeof(TimeScaleWindow));
			window.Show();
		}

		private void OnInspectorUpdate()
		{
			Repaint();
		}

		void OnGUI()
		{
			EditorGUILayout.LabelField("Time Scale", EditorStyles.boldLabel);
			timeScale = Time.timeScale;

			EditorGUI.BeginChangeCheck();
			{
				var maxValue = (timeScale < 8f) ? 8f : timeScale * 1.1f;
				timeScale = EditorGUILayout.Slider("Scale ", timeScale, 0f, maxValue);
			}
			if (EditorGUI.EndChangeCheck())
			{
				Time.timeScale = timeScale;
			}

			// Instant focus to a near position without caring about the last pivot
			if (GUILayout.Button("Reset to 1"))
			{
				Time.timeScale = 1f;
			}
		}
	}
}