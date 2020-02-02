// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TowerBase.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
	[SerializeField]
	private GameObject towerParentObject = null;

	[SerializeField]
	private GameData gameData = null;
	private LoadLevelSignal loadLevelSignal;


	private void Awake()
	{
		Signals.Get(out loadLevelSignal);

		loadLevelSignal.AddListener(OnLoadLevel);
	}

	private void OnDestroy()
	{
		loadLevelSignal.RemoveListener(OnLoadLevel);
	}

	private void OnLoadLevel(LevelAsset levelAsset)
	{
		towerParentObject.DestroyChildren();
		var towerPrefab = levelAsset.TowerPrefab;

		Instantiate(towerPrefab, towerParentObject.transform);
	}

	public void RebuilLevel()
	{
		towerParentObject.DestroyChildren();
		var towerPrefab = gameData.CurrentLevel.TowerPrefab;

		Instantiate(towerPrefab, towerParentObject.transform);
	}
}