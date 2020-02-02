// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LevelTitelUI.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using TMPro;
using UnityEngine;

namespace Supyrb
{
	public class LevelTitleUI : MonoBehaviour
	{
		public enum Type
		{
			Title,
			Description
		}
		
		[SerializeField]
		private TMP_Text textComponent = null;

		[SerializeField]
		private GameData gameData = null;

		[SerializeField]
		private Type type = Type.Title;
		
		private LoadLevelSignal loadLevelSignal;

		private void Awake()
		{
			Signals.Get(out loadLevelSignal);

			OnLoadLevel(gameData.CurrentLevel);
			loadLevelSignal.AddListener(OnLoadLevel);
		}

		private void OnDestroy()
		{
			loadLevelSignal.RemoveListener(OnLoadLevel);
		}

		private void OnLoadLevel(LevelAsset level)
		{
			switch (type)
			{
				case Type.Title:
					textComponent.text = level.LevelName;
					break;
				case Type.Description:
					textComponent.text = level.LevelDescription;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#if UNITY_EDITOR

		private void Reset()
		{
			textComponent = GetComponent<TMP_Text>();
		}

		#endif
	}
}