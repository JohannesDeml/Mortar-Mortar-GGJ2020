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

	[SerializeField]
	private BulletListAsset bulletList = null;

	[SerializeField]
	private GameObject towerPrefab = null;

	public string LevelName => levelName;

	public BulletListAsset BulletList => bulletList;

	public GameObject TowerPrefab => towerPrefab;

	[Button()]
	public void LoadLevel()
	{
		Signals.Get<LoadLevelSignal>().Dispatch(this);
	}
}