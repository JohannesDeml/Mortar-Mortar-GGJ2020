// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameData.cs" company="Supyrb">
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
	public class GameData : ScriptableObject
	{
		[SerializeField]
		private LevelListAsset levelListAsset = null;
		
		[NonSerialized]
		private GameState currentState = GameState.Game;

		public GameState CurrentState => currentState;

		public LevelAsset CurrentLevel => levelListAsset.GetCurrentLevel();

		private GameOverSignal gameOverSignal;
		private CountdownFinishedSignal countdownFinishedSignal;
		private ObjectFellOffFloorSignal objectFellOffFloorSignal;
		private RestartLevelSignal restartLevelSignal;

		public void Initialize()
		{
			currentState = GameState.Game;
			levelListAsset.ResetIndex();

			Signals.Get(out gameOverSignal);
			Signals.Get(out countdownFinishedSignal);
			Signals.Get(out objectFellOffFloorSignal);
			Signals.Get(out restartLevelSignal);

			countdownFinishedSignal.AddListener(OnCountdownFinishedSignal);
			objectFellOffFloorSignal.AddListener(OnObjectFellOffFloor);
			restartLevelSignal.AddListener(OnRestartLevel);
		}

		public void Dispose()
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