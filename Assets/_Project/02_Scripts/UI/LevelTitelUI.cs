// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelTitelUI.cs" company="Supyrb">
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
	public class LevelTitelUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text textComponent = null;

		private LoadLevelSignal loadLevelSignal;

		private void Awake()
		{
			Signals.Get(out loadLevelSignal);

			loadLevelSignal.AddListener(OnLoadLevel);
		}

		private void OnDestroy()
		{
			loadLevelSignal.RemoveListener(OnLoadLevel);
		}

		private void OnLoadLevel(LevelAsset level)
		{
			textComponent.text = level.LevelName;
		}

		#if UNITY_EDITOR

		private void Reset()
		{
			textComponent = GetComponent<TMP_Text>();
		}

		#endif
	}
}