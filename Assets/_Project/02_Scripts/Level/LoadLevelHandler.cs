// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoadLevelHandler.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb;
using UnityEngine;

public class LoadLevelHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject towerParentObject = null;

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
}