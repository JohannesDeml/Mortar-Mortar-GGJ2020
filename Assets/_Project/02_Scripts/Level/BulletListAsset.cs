// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BulletListAsset.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class BulletListAsset : ScriptableObject
{
	[SerializeField]
	private GameObject[] prefabs = null;

	public GameObject[] Prefabs => prefabs;
}