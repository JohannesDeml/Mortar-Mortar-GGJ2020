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
		private GameState currentState = GameState.Game;

		private GameOverSignal gameOverSignal;
		private CountdownFinishedSignal countdownFinishedSignal;
		private ObjectFellOffFloorSignal objectFellOffFloorSignal;
		private RestartLevelSignal restartLevelSignal;

		private void Awake()
		{
			Signals.Get(out gameOverSignal);
			Signals.Get(out countdownFinishedSignal);
			Signals.Get(out objectFellOffFloorSignal);
			Signals.Get(out restartLevelSignal);

			countdownFinishedSignal.AddListener(OnCountdownFinishedSignal);
			objectFellOffFloorSignal.AddListener(OnObjectFellOffFloor);
			restartLevelSignal.AddListener(OnRestartLevel);
		}

		private void OnDestroy()
		{
			countdownFinishedSignal.RemoveListener(OnCountdownFinishedSignal);
			objectFellOffFloorSignal.RemoveListener(OnObjectFellOffFloor);
			restartLevelSignal.RemoveListener(OnRestartLevel);
		}
		
		private void OnCountdownFinishedSignal()
		{
			if (currentState == GameState.GameOver)
			{
				return;
			}
			gameOverSignal.Dispatch(true);
			currentState = GameState.GameOver;
		}

		private void OnObjectFellOffFloor(GameObject obj)
		{
			if (currentState == GameState.GameOver)
			{
				return;
			}
			Debug.Log($"Object fell off floor: {obj.name}", obj);
			gameOverSignal.Dispatch(false);
			currentState = GameState.GameOver;
		}
		
		private void OnRestartLevel()
		{
			currentState = GameState.Game;
		}
	}
}