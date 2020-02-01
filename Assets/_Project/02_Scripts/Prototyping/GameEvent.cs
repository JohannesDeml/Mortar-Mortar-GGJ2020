// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Supyrb
{
	[CreateAssetMenu]
	public class GameEvent : ScriptableObject
	{
		public delegate void GameEventDelegate(GameEvent raisedEvent);

		public static event GameEventDelegate EventRaised;
		
		/// <summary>
		/// The list of listeners that this event will notify if it is raised.
		/// </summary>
		private readonly List<GameEventListener> eventListeners =
			new List<GameEventListener>();

		[Button()]
		public void Raise()
		{
			if (EventRaised != null)
			{
				EventRaised(this);
			}
			
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised();
			}
		}

		public void RegisterListener(GameEventListener listener)
		{
			if (!eventListeners.Contains(listener))
			{
				eventListeners.Add(listener);
			}
		}

		public void UnregisterListener(GameEventListener listener)
		{
			if (eventListeners.Contains(listener))
			{
				eventListeners.Remove(listener);
			}
		}

		#if UNITY_EDITOR
		[Button()]
		private void PrintAllListeners()
		{
			Debug.Log(eventListeners.Count);
			for (int i = 0; i < eventListeners.Count; i++)
			{
				var listener = eventListeners[i];
				Debug.Log(i + ": " + listener.gameObject.name, listener);
			}
		}
		#endif
	}
}