// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleUnityEvent.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Supyrb
{
	public class SimpleUnityEvent : MonoBehaviour
	{
		[SerializeField]
		private bool invokeOnAwake = false;

		[SerializeField]
		private bool invokeOnStart = false;

		[SerializeField]
		private bool invokeOnEnable = false;

		[SerializeField]
		private bool invokeOnDisable = false;

		[SerializeField]
		private bool invokeOnDestroy = false;

		[Space]
		[SerializeField]
		private bool onlyInvokeWhenActive = false;
		
		[SerializeField]
		protected UnityEvent Event;

		void Awake()
		{
			if (invokeOnAwake)
			{
				Invoke();
			}
		}

		void OnEnable()
		{
			if (invokeOnEnable)
			{
				Invoke();
			}
		}

		void Start()
		{
			if (invokeOnStart)
			{
				Invoke();
			}
		}

		void OnDisable()
		{
			if (invokeOnDisable)
			{
				Invoke();
			}
		}

		void OnDestroy()
		{
			if (invokeOnDestroy)
			{
				Invoke();
			}
		}
		
		[Button()]
		[ContextMenu("Invoke")]
		public virtual void Invoke()
		{
			if (onlyInvokeWhenActive && !gameObject.activeInHierarchy)
			{
				return;
			}

			InvokeEvent();
		}

		protected virtual void InvokeEvent()
		{
			Event.Invoke();
		}
	}
}