// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelListAsset.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

public class LevelListAsset : ScriptableObject
{
	[SerializeField]
	private LevelAsset[] levels = null;

	[NonSerialized]
	private int currentIndex = 0;

	public LevelAsset GetCurrentLevel()
	{
		return levels[currentIndex];
	}
	
	public void ResetIndex()
	{
		currentIndex = 0;
		levels[currentIndex].LoadLevel();
	}

	public void ChangeIndexRelative(int relativeChange)
	{
		currentIndex = (currentIndex + relativeChange + levels.Length) % levels.Length;
		levels[currentIndex].LoadLevel();
	}
}