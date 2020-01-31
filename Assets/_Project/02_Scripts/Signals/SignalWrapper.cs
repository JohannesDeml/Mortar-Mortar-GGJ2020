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
		public void RestartLevel()
		{
			Signals.Get<RestartLevelSignal>().Dispatch();
		}
	}
}