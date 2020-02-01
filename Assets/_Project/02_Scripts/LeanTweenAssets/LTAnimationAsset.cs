// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LTAnimationAsset.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
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
	[Serializable]
	[CreateAssetMenu(fileName = "Animation", menuName = "LeanTween/AnimationAsset")]
	public class LTAnimationAsset : ScriptableObject
	{
		public enum AnimationType
		{
			Absolute,
			Relative
		}

		public float Delay = 0f;
		public float Duration = 1f;
		public bool Loop = false;

		[ShowIf("Loop")]
		public int Loops = 1;

		[ShowIf("Loop")]
		public LeanTweenType LoopType = LeanTweenType.clamp;

		public LeanTweenType Easing = LeanTweenType.easeInOutCubic;
		
		[Tooltip("Only if easing is animation curve")]
		public AnimationCurve AnimationCurve = null;

		public bool IgnoreTimeScale = false;
		public AnimationType Type = AnimationType.Absolute;
		public Space Space = Space.Self;

		public float GetValue(float start, float end, float t)
		{
			switch (Easing)
			{
				case LeanTweenType.notUsed:
					return 0f;
				case LeanTweenType.animationCurve:
					return Mathf.Lerp(start, end, AnimationCurve.Evaluate(t));
				case LeanTweenType.linear:
					return LeanTween.linear(start, end, t);
				case LeanTweenType.easeOutQuad:
					return LeanTween.easeOutQuad(start, end, t);
				case LeanTweenType.easeInQuad:
					return LeanTween.easeInQuad(start, end, t);
				case LeanTweenType.easeInOutQuad:
					return LeanTween.easeInOutQuad(start, end, t);
				case LeanTweenType.easeInCubic:
					return LeanTween.easeInCubic(start, end, t);
				case LeanTweenType.easeOutCubic:
					return LeanTween.easeOutCubic(start, end, t);
				case LeanTweenType.easeInOutCubic:
					return LeanTween.easeInOutCubic(start, end, t);
				case LeanTweenType.easeInQuart:
					return LeanTween.easeInQuart(start, end, t);
				case LeanTweenType.easeOutQuart:
					return LeanTween.easeOutQuart(start, end, t);
				case LeanTweenType.easeInOutQuart:
					return LeanTween.easeInOutQuart(start, end, t);
				case LeanTweenType.easeInQuint:
					return LeanTween.easeInQuint(start, end, t);
				case LeanTweenType.easeOutQuint:
					return LeanTween.easeOutQuint(start, end, t);
				case LeanTweenType.easeInOutQuint:
					return LeanTween.easeInOutQuint(start, end, t);
				case LeanTweenType.easeInSine:
					return LeanTween.easeInSine(start, end, t);
				case LeanTweenType.easeOutSine:
					return LeanTween.easeOutSine(start, end, t);
				case LeanTweenType.easeInOutSine:
					return LeanTween.easeInOutSine(start, end, t);
				case LeanTweenType.easeInExpo:
					return LeanTween.easeInExpo(start, end, t);
				case LeanTweenType.easeOutExpo:
					return LeanTween.easeOutExpo(start, end, t);
				case LeanTweenType.easeInOutExpo:
					return LeanTween.easeInOutExpo(start, end, t);
				case LeanTweenType.easeInCirc:
					return LeanTween.easeInCirc(start, end, t);
				case LeanTweenType.easeOutCirc:
					return LeanTween.easeOutCirc(start, end, t);
				case LeanTweenType.easeInOutCirc:
					return LeanTween.easeInOutCirc(start, end, t);
				case LeanTweenType.easeInBounce:
					return LeanTween.easeInBounce(start, end, t);
				case LeanTweenType.easeOutBounce:
					return LeanTween.easeOutBounce(start, end, t);
				case LeanTweenType.easeInOutBounce:
					return LeanTween.easeInOutBounce(start, end, t);
				case LeanTweenType.easeInBack:
					return LeanTween.easeInBack(start, end, t);
				case LeanTweenType.easeOutBack:
					return LeanTween.easeOutBack(start, end, t);
				case LeanTweenType.easeInOutBack:
					return LeanTween.easeInOutBack(start, end, t);
				case LeanTweenType.easeInElastic:
					return LeanTween.easeInElastic(start, end, t);
				case LeanTweenType.easeOutElastic:
					return LeanTween.easeOutElastic(start, end, t);
				case LeanTweenType.easeInOutElastic:
					return LeanTween.easeInOutElastic(start, end, t);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}