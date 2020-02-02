// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemainingItemsUI.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using TMPro;
using UnityEngine;

namespace Supyrb
{
	public class RemainingItemsUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text textComponent = null;

		private RemainingItemsSignal remainingItemsSignal;

		private void Awake()
		{
			Signals.Get(out remainingItemsSignal);

			remainingItemsSignal.AddListener(OnRemainingItems);
		}

		private void OnDestroy()
		{
			remainingItemsSignal.RemoveListener(OnRemainingItems);
		}

		private void OnRemainingItems(int remainingItems)
		{
			textComponent.text = remainingItems.ToString();
		}

		#if UNITY_EDITOR
		private void Reset()
		{
			textComponent = GetComponent<TMP_Text>();
		}
		#endif
	}
}