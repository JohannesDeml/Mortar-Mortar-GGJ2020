// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ALTAnimatorData.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
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
	[Serializable]
	public abstract class ALTAnimatorData : ILTAnimator
	{
		public delegate void AnimatorAssetDelegate();

		public event AnimatorAssetDelegate AnimationsFinished;
		
		public bool Playing { get; private set; }

		protected abstract ILTAnimationData[] Animations { get; }

		public GameObject gameObject { get; private set; }

		[NonSerialized]
		private int animationsFinishedCounter = 0;

		[NonSerialized]
		private int numAnimations = 0;

		[NonSerialized]
		private bool skip;

		public void TriggerAnimations(GameObject go)
		{
			gameObject = go;
			skip = false;
			Playing = true;
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

			if (numAnimations == 0)
			{
				FinishAnimation();
			}
		}

		public void SkipToEnd()
		{
			SkipToEnd(false);
		}

		public void SkipToEnd(bool callAnimationFinishedWhenSkipped)
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

			Playing = false;

			if (callAnimationFinishedWhenSkipped)
			{
				FinishAnimation();
			}
		}

		public void SkipToEndIfPlaying()
		{
			SkipToEndIfPlaying(false);
		}

		public void SkipToEndIfPlaying(bool callAnimationFinishedWhenSkipped)
		{
			if (!Playing)
			{
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

			Playing = false;

			if (callAnimationFinishedWhenSkipped)
			{
				FinishAnimation();
			}
		}

		public void AnimationFinished()
		{
			animationsFinishedCounter++;

			if (skip)
			{
				// Should actually not be called
				Debug.LogWarning("Should not be called.", gameObject);
				return;
			}

			if (animationsFinishedCounter == numAnimations)
			{
				FinishAnimation();
			}
		}

		private void FinishAnimation()
		{
			if (gameObject.activeInHierarchy && AnimationsFinished != null)
			{
				AnimationsFinished();
			}

			Playing = false;
		}
	}
}