// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ALTAnimationData.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public abstract class ALTAnimationData : ILTAnimationData
	{
		[SerializeField]
		protected LTAnimationAsset animationSettings = null;

		protected bool skippingToEnd = false;
		protected int uniqueId = -1;
		protected ILTAnimator parent;

		protected float startFloat;
		protected float endFloat;
		protected Vector2 startVector2;
		protected Vector2 endVector2;
		protected Vector3 startVector3;
		protected Vector3 endVector3;
		protected Vector4 startVector4;
		protected Vector4 endVector4;
		protected Color startColor;
		protected Color endColor;
		protected Quaternion startQuaternion;
		protected Quaternion endQuaternion;

		protected GameObject gameObject => parent.gameObject;


		public virtual void TriggerAnimation(ILTAnimator parent)
		{
			this.parent = parent;
			this.skippingToEnd = false;
			// Handle animation type
		}

		public abstract void ApplyStartValues(ILTAnimator parent);

		public virtual void SetAnimation(LTAnimationAsset newAnimationSettings)
		{
			animationSettings = newAnimationSettings;
		}

		public virtual void SkipToEnd(ILTAnimator parent)
		{
			this.parent = parent;
			if (uniqueId == -1)
			{
				TriggerAnimation(parent);
			}

			skippingToEnd = true;
			LeanTween.cancel(gameObject, uniqueId, true);
		}

		public virtual void SkipToEndIfStarted(ILTAnimator parent)
		{
			if (uniqueId == -1)
			{
				return;
			}

			this.parent = parent;
			skippingToEnd = true;
			LeanTween.cancel(gameObject, uniqueId, true);
		}

		protected virtual void ApplyAdditionalSettings(LTDescr tween)
		{
			tween.setDelay(animationSettings.Delay)
				.setIgnoreTimeScale(animationSettings.IgnoreTimeScale);

			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				tween.setEase(animationSettings.AnimationCurve);
			}
			else
			{
				tween.setEase(animationSettings.Easing);
			}

			if (animationSettings.Loop)
			{
				tween.setLoopCount(animationSettings.Loops);
				tween.setLoopType(animationSettings.LoopType);
			}
		}

		protected virtual void StartNewTween()
		{
			if (uniqueId != -1)
			{
				LeanTween.cancel(gameObject, uniqueId, false);
			}
		}

		protected void AnimateNone()
		{
			var tween = LeanTween.value(gameObject, UpdateNoneValue, 0f, 0f, animationSettings.Duration)
				.setOnComplete(OnComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateNoneValue(float current)
		{
			// Do nothing in here
		}

		protected virtual void OnComplete()
		{
			uniqueId = -1;

			if (parent != null && !skippingToEnd)
			{
				parent.AnimationFinished();
			}

			skippingToEnd = false;
		}

		#region InitializeParameters

		protected virtual bool InitializeFromToValues(FloatPairAsset fromToValues)
		{
			if (fromToValues == null)
			{
				Debug.LogError("Missing Input type FloatPairAsset.", gameObject);
				return false;
			}

			StartNewTween();
			startFloat = fromToValues.A;
			endFloat = fromToValues.B;
			return true;
		}

		protected virtual bool InitializeFromToValues(Vector2PairAsset fromToValues)
		{
			if (fromToValues == null)
			{
				Debug.LogError("Missing Input type Vector2PairAsset.", gameObject);
				return false;
			}

			StartNewTween();
			startVector2 = fromToValues.A;
			endVector2 = fromToValues.B;
			return true;
		}

		protected virtual bool InitializeFromToValues(Vector3PairAsset fromToValues)
		{
			if (fromToValues == null)
			{
				Debug.LogError("Missing Input type Vector3PairAsset.", gameObject);
				return false;
			}

			StartNewTween();
			startVector3 = fromToValues.A;
			endVector3 = fromToValues.B;
			return true;
		}

		protected virtual bool InitializeFromToValues(Vector4PairAsset fromToValues)
		{
			if (fromToValues == null)
			{
				Debug.LogError("Missing Input type Vector4PairAsset.", gameObject);
				return false;
			}

			StartNewTween();
			startVector4 = fromToValues.A;
			endVector4 = fromToValues.B;
			return true;
		}

		protected virtual bool InitializeFromToValues(ColorPairAsset fromToValues)
		{
			if (fromToValues == null)
			{
				Debug.LogError("Missing Input type ColorPairAsset.", gameObject);
				return false;
			}

			StartNewTween();
			startColor = fromToValues.A;
			endColor = fromToValues.B;
			return true;
		}

		#endregion
	}
}