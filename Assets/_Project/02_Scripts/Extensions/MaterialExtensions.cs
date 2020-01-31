// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaterialExtensions.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public static class MaterialExtensions
	{
		/// <summary>
		/// Returns the material name without an '(Instance)' addition
		/// </summary>
		/// <param name="material"></param>
		/// <returns>The material name as set in the project window</returns>
		public static string GetInstancedMaterialName(this Material material)
		{
			var name = material.name;
			if (Application.isPlaying)
			{
				return name.Substring(0, name.Length - " (Instance)".Length);
			}
			else
			{
				return name;
			}
		}
	}
}