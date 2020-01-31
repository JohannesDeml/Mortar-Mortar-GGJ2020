// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimedUnityEvent.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using UnityEngine;

namespace Supyrb
{
	public class TimedUnityEvent : SimpleUnityEvent
	{
		[SerializeField]
		private TimeMode timeMode = TimeMode.Scaled;

		[SerializeField]
		private float timeTillActivation = 0.3f;

		[SerializeField]
		private bool running = false;

		private bool initialized = false;
		private WaitForSeconds waitTillActivationScaled;

		private WaitForSecondsCustomRealtime waitTillActivationUnscaled;
		// REFACTOR somehow storing the variable doesn't work
		//private IEnumerator activationCoroutine;


		protected override void InvokeEvent()
		{
			CancelTimer();
			if (timeTillActivation <= 0.001f)
			{
				TriggerEvent();
			}
			else
			{
				StartCoroutine(ActivationCoroutine());
			}
		}

		private IEnumerator ActivationCoroutine()
		{
			if (!initialized)
			{
				Initialize();
			}

			running = true;
			if (timeMode == TimeMode.Unscaled)
			{
				waitTillActivationUnscaled.UpdateEndTime();
				yield return waitTillActivationUnscaled;
			}
			else
			{
				yield return waitTillActivationScaled;
			}

			TriggerEvent();
			running = false;
		}

		private void TriggerEvent()
		{
			Event.Invoke();
		}

		public void CancelTimer()
		{
			if (running)
			{
				StopAllCoroutines();
				running = false;
			}
		}

		private void Initialize()
		{
			if (timeMode == TimeMode.Unscaled)
			{
				waitTillActivationUnscaled = new WaitForSecondsCustomRealtime(timeTillActivation);
			}
			else
			{
				waitTillActivationScaled = new WaitForSeconds(timeTillActivation);
			}

			initialized = true;
		}

		#if UNITY_EDITOR
		void OnValidate()
		{
			if (Application.isPlaying)
			{
				Initialize();
			}
		}
		#endif
	}
}