// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalEditorUtils.cs" company="Supyrb">
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
	public static class SignalEditorUtils
	{
		[InitializeOnLoadMethod]
		private static void OnProjectLoadedInEditor()
		{
			EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
		}

		private static void OnPlayModeStateChanged(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.EnteredEditMode)
			{
				Debug.Log($"Clearing Signals hub with {Signals.Count} registered signals");
				ClearSignalHub();
			}
		}

		private static void ClearSignalHub()
		{
			Signals.Clear();
		}
	}
}