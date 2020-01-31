// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PauseApplicationToggle.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEditor;

namespace Supyrb
{
	public static class PauseApplicationToggle
	{
		[MenuItem("Supyrb/Misc/TogglePauseApplication _F1", false, 10000)]
		public static void TogglePauseApplication()
		{
			EditorApplication.isPaused = !EditorApplication.isPaused;
		}

		//[MenuItem("Supyrb/Misc/TogglePauseApplication _F1", true)]
		//public static bool TogglePauseApplicationValidation()
		//{
		//    return EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPaused;
		//}
	}
}