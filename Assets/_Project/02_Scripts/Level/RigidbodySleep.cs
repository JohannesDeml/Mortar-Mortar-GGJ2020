// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InteractableLevelBox.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Supyrb
{
	[RequireComponent(typeof(Rigidbody))]
	public class RigidbodySleep : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody rb = null;

		[SerializeField]
		private float sleepThreshold = 0.4f;

		[SerializeField]
		private bool sleepOnEnable = true;
		
		void Awake()
		{
			rb.sleepThreshold = sleepThreshold;
		}

		private void OnEnable()
		{
			if (sleepOnEnable)
			{
				ResetRigidbody();
			}
		}

		public void ResetRigidbody()
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			rb.Sleep();
		}

		#if UNITY_EDITOR
		private void Reset()
		{
			if(rb == null) {
				rb = GetComponent<Rigidbody>();
			}
		}
		#endif
	}
}