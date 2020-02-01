// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ALTAnimator.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Supyrb
{
	public abstract class ALTAnimator : MonoBehaviour, ILTAnimator
	{
		protected abstract ILTAnimationData[] Animations { get; }
		
		[SerializeField]
		private UnityEvent onFinishAnimations = null;

		[SerializeField]
		private bool callAnimationFinishedWhenSkipped = true;

		private int animationsFinishedCounter = 0;
		private int numAnimations = 0;
		private bool skip;
		private bool applicationQuitting;

		private void OnApplicationQuit()
		{
			applicationQuitting = true;
		}

		protected virtual void OnDisable()
		{
			if (applicationQuitting)
			{
				return;
			}

			for (var i = 0; i < Animations.Length; i++)
			{
				var anim = Animations[i];
				if (anim == null)
				{
					continue;
				}

				anim.SkipToEndIfStarted(this);
			}
		}

		[Button]
		public void TriggerAnimations()
		{
			skip = false;
			animationsFinishedCounter = 0;
			numAnimations = 0;
			for (var i = 0; i < Animations.Length; i++)
			{
				var anim = Animations[i];
				if (anim == null)
				{
					continue;
				}

				anim.TriggerAnimation(this);
				numAnimations++;
			}
		}

		[Button]
		public void ApplyStartValues()
		{
			for (var i = 0; i < Animations.Length; i++)
			{
				var anim = Animations[i];
				if (anim == null)
				{
					continue;
				}

				anim.ApplyStartValues(this);
			}
		}

		[Button]
		public void SkipToEnd()
		{
			skip = true;
			for (var i = 0; i < Animations.Length; i++)
			{
				var anim = Animations[i];
				if (anim == null)
				{
					continue;
				}

				anim.SkipToEnd(this);
			}

			if (callAnimationFinishedWhenSkipped)
			{
				FinishAnimation();
			}
		}

		public void SkipToEndIfStarted()
		{
			if (animationsFinishedCounter >= numAnimations)
			{
				// Not started or already finished
				return;
			}

			skip = true;
			for (var i = 0; i < Animations.Length; i++)
			{
				var anim = Animations[i];
				if (anim == null)
				{
					continue;
				}

				anim.SkipToEndIfStarted(this);
			}

			if (callAnimationFinishedWhenSkipped)
			{
				FinishAnimation();
			}
		}

		public void AnimationFinished()
		{
			if (skip && !callAnimationFinishedWhenSkipped)
			{
				return;
			}

			animationsFinishedCounter++;
			if (animationsFinishedCounter == numAnimations)
			{
				FinishAnimation();
			}
		}

		private void FinishAnimation()
		{
			if (gameObject.activeInHierarchy)
			{
				onFinishAnimations.Invoke();
			}
		}
	}
}