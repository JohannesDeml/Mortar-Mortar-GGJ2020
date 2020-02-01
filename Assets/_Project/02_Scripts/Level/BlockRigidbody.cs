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
	public class BlockRigidbody : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody rb = null;

		[SerializeField]
		private float sleepThreshold = 0.4f;

		[SerializeField]
		private bool sleepOnEnable = true;

		public Rigidbody Rb => rb;

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

		public void RemoveRigidbody()
		{
			if (rb == null)
			{
				return;
			}
			Destroy(rb);
		}
		
		public void ResetRigidbody()
		{
			if (rb == null)
			{
				// Yes, we destroy the rigidbody sometimes :O
				rb = gameObject.AddComponent<Rigidbody>();
			}
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