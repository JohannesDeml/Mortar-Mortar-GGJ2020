// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FailZone.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb;
using UnityEngine;

public class FailZone : MonoBehaviour
{
	[SerializeField]
	private LayerMask failLayers = default;

	private ObjectFellOffFloorSignal objectFellOffFloorSignal;

	private void Awake()
	{
		Signals.Get(out objectFellOffFloorSignal);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!failLayers.Contains(other.gameObject.layer))
		{
			return;
		}

		objectFellOffFloorSignal.Dispatch(other.gameObject);
	}
}