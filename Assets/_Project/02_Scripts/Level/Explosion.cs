// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Explosion.cs" company="Supyrb">
//   Copyright (c)  Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField]
	private float radius = 2.0f;

	[SerializeField]
	private float force = 1000f;

	[SerializeField]
	private float upwardsModifier = 3.0f;
	
	[SerializeField]
	private LayerMask affectedLayers = default;
	
	[SerializeField]
	private int maxAffectedRigidbodies = 30;
	
	private Collider[] colliders = null;
	private TriggerExplosionSignal triggerExplosionSignal;
	
	private void Awake()
	{
		colliders = new Collider[maxAffectedRigidbodies];
		Signals.Get(out triggerExplosionSignal);
		triggerExplosionSignal.AddListener(Explode);
	}

	private void OnDestroy()
	{
		triggerExplosionSignal.RemoveListener(Explode);
	}

	[Button()]
	public void Explode()
	{
		var explosionPosition = transform.position;
		var hits = Physics.OverlapSphereNonAlloc(explosionPosition, radius, colliders, affectedLayers, QueryTriggerInteraction.Ignore);
		for (int i = 0; i < hits; i++)
		{
			var hitCollider = colliders[i];
			var rb = hitCollider.attachedRigidbody;
			if (rb == null)
			{
				continue;
			}
			rb.AddExplosionForce(force, explosionPosition, radius, upwardsModifier);
		}
	}
	
	#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireSphere(Vector3.zero, radius);
	}

	#endif
}