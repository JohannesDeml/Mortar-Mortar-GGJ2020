// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalWrapper.cs" company="Supyrb">
//   Copyright (c)  Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using NaughtyAttributes;
using UnityEngine;

namespace Supyrb
{
	public class SignalWrapper : ScriptableObject
	{
		[Button]
		public void StartGame()
		{
			Signals.Get<StartGameSignal>().Dispatch();
		}
		
		[Button]
		public void GameOverSuccess()
		{
			Signals.Get<GameOverSignal>().Dispatch(true);
		}
		
		[Button]
		public void GameOverFailed()
		{
			Signals.Get<GameOverSignal>().Dispatch(false);
		}
		
		[Button]
		public void RestartLevel()
		{
			Signals.Get<RestartLevelSignal>().Dispatch();
		}
		
		[Button]
		public void ToMenu()
		{
			Signals.Get<ToMenuSignal>().Dispatch();
		}
		
		[Button]
		public void TriggerExplosion()
		{
			Signals.Get<TriggerExplosionSignal>().Dispatch();
		}
		
		[Button]
		public void TriggerAllBulletsShot()
		{
			Signals.Get<AllBulletsShotSignal>().Dispatch();
		}
		
		[Button]
		public void TriggerCountdownFinished()
		{
			Signals.Get<CountdownFinishedSignal>().Dispatch();
		}
	}
}