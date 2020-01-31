// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LookAtTransform.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public class LookAtTransform : MonoBehaviour
	{
		[SerializeField]
		private Transform target = null;

		[SerializeField]
		private Vector3 up = Vector3.up;

		private void Update()
		{
			transform.LookAt(target, up);
		}
	}
}