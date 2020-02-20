// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyUnityEvent.cs" company="Supyrb">
//   Copyright (c) 2018 Supyrb. All rights reserved.
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
	public class KeyUnityEvent : MonoBehaviour
	{
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
		[SerializeField]
		private KeyCode mappedButton = KeyCode.Escape;

		[SerializeField]
		private UnityEvent onKeyDown = null;

		void Update()
		{
			if (Input.GetKeyDown(mappedButton))
			{
				FireEvent();
			}
		}

		[Button()]
		private void FireEvent()
		{
			onKeyDown.Invoke();
		}
		#endif
	}
}