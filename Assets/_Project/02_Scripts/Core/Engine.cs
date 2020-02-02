// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Engine.cs" company="Supyrb">
//   Copyright (c) 2020 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NaughtyAttributes;
using UnityEngine;

namespace Supyrb
{
	public enum GameState
	{
		Game,
		GameOver
	}
	public class Engine : MonoBehaviour
	{
		[SerializeField]
		private GameData gameData = null;

		private void Awake()
		{
			gameData.Initialize();
		}

		private void OnDestroy()
		{
			gameData.Dispose();
		}
	}
}