// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelAsset.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class LevelAsset : ScriptableObject
{
	[SerializeField]
	private string levelName = "Mortar Mortar";

	[SerializeField, TextArea(3, 8)]
	private string levelDescription = "This is a great level!";
	
	[SerializeField]
	private BulletListAsset bulletList = null;

	[SerializeField]
	[ShowAssetPreview(200, 200)]
	private GameObject towerPrefab = null;

	public string LevelName => levelName;

	public BulletListAsset BulletList => bulletList;

	public GameObject TowerPrefab => towerPrefab;

	public string LevelDescription => levelDescription;

	[Button()]
	public void LoadLevel()
	{
		Signals.Get<LoadLevelSignal>().Dispatch(this);
	}
}