// --------------------------------------------------------------------------------------------------------------------
 // <copyright file="AndroidPublishingCredentials" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Supyrb
{
	using UnityEditor;

	[InitializeOnLoad]
	public static class AndroidPublishingCredentials
	{
		/// <summary>
		/// Fills in android credentials at each unity start
		/// </summary>
		static AndroidPublishingCredentials()
		{
			PlayerSettings.Android.keystorePass = "";
			PlayerSettings.Android.keyaliasName = "happygolf";
			PlayerSettings.Android.keyaliasPass = "";
		}
	}
}

