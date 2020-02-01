// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalEvents.cs" company="Supyrb">
//   Copyright (c)  Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Supyrb
{
	public class SignalEvents : MonoBehaviour
	{
		[SerializeField]
		private UnityEvent startGame = null;
		
		[SerializeField]
		private UnityEvent gameOverFail = null;
		
		[SerializeField]
		private UnityEvent gameOverWin = null;
		
		[SerializeField]
		private UnityEvent restartLevel = null;
		
		[SerializeField]
		private UnityEvent mortaShoot = null;
		
		private StartGameSignal startGameSignal;
		private GameOverSignal gameOverSignal;
		private RestartLevelSignal restartLevelSignal;
		private MortaShootSignal mortaShootSignal;
		
		
		private void Awake()
		{
			Signals.Get(out startGameSignal);
			Signals.Get(out gameOverSignal);
			Signals.Get(out restartLevelSignal);
			Signals.Get(out mortaShootSignal);

			startGameSignal.AddListener(OnStartGame);
			gameOverSignal.AddListener(OnGameOverSignal);
			restartLevelSignal.AddListener(OnRestartLevelSignal);
			mortaShootSignal.AddListener(OnMortaShoot);
		}

		private void OnDestroy()
		{
			startGameSignal.RemoveListener(OnStartGame);
			gameOverSignal.RemoveListener(OnGameOverSignal);
			restartLevelSignal.RemoveListener(OnRestartLevelSignal);
			mortaShootSignal.RemoveListener(OnMortaShoot);
		}

		private void OnStartGame()
		{
			startGame.Invoke();
		}
		
		private void OnGameOverSignal(bool success)
		{
			if (success)
			{
				gameOverWin.Invoke();
			}
			else
			{
				gameOverFail.Invoke();
			}
		}

		private void OnRestartLevelSignal()
		{
			restartLevel.Invoke();
		}
		
		private void OnMortaShoot()
		{
			mortaShoot.Invoke();
		}
	}
}