// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Block.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb;
using Supyrb.Common;
using UnityEngine;

public class Block : MonoBehaviour
{
	[SerializeField]
	private RigidbodySleep rigidbodySleep = null;

	private SimpleTransform awakeTransform;
	private RestartLevelSignal restartLevelSignal;

	private void Awake()
	{
		awakeTransform = transform.GetSimpleTransform(TransformType.Local);
		Signals.Get(out restartLevelSignal);
		restartLevelSignal.AddListener(OnRestartLevel);
	}

	private void OnDestroy()
	{
		restartLevelSignal.RemoveListener(OnRestartLevel);
	}

	private void OnRestartLevel()
	{
		transform.ApplySimpleTransform(awakeTransform);
		rigidbodySleep.ResetRigidbody();
	}

	#if UNITY_EDITOR

	private void Reset()
	{
		if (rigidbodySleep == null)
		{
			rigidbodySleep = GetComponent<RigidbodySleep>();
		}
	}

	#endif
}