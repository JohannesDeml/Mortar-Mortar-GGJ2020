// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StateMachineEvents.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

namespace Supyrb
{
	public class StateMachineEvents : StateMachineBehaviour
	{
		public delegate void StateMachineDelegate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

		public event StateMachineDelegate OnAnimationStateEnter;
		public event StateMachineDelegate OnAnimationStateExit;
		public GameEvent EnterEvent;
		public GameEvent ExitEvent;

		public UnityEvent OnEnter = null;
		public UnityEvent OnExit = null;

		// OnStateEnter is called before OnStateEnter is called on any state inside this state machine
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (EnterEvent != null)
			{
				EnterEvent.Raise();
			}

			OnEnter.Invoke();
			
			if (OnAnimationStateEnter != null)
			{
				OnAnimationStateEnter(animator, stateInfo, layerIndex);
			}
		}

		// OnStateExit is called before OnStateExit is called on any state inside this state machine
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (ExitEvent != null)
			{
				ExitEvent.Raise();
			}
			
			OnExit.Invoke();

			if (OnAnimationStateExit != null)
			{
				OnAnimationStateExit(animator, stateInfo, layerIndex);
			}
		}


		// OnStateMachineEnter is called when entering a statemachine via its Entry Node
		//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
		//
		//}

		// OnStateMachineExit is called when exiting a statemachine via its Exit Node
		//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
		//
		//}
	}
}