// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LTTransformAnimationData.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NaughtyAttributes;
using Supyrb.Common;
using UnityEngine;

namespace Supyrb
{
	[Serializable]
	public class LTTransformAnimationData : ALTAnimationData
	{
		public enum AnimationTarget
		{
			None,
			Position,
			PositionX,
			PositionY,
			PositionZ,
			Scale,
			Rotation,
			RotationSpeedX,
			RotationSpeedY,
			RotationSpeedZ,
		}

		private static Vector3 forward = Vector3.forward;
		private static Vector3 up = Vector3.up;
		private static Vector3 right = Vector3.right;

		[SerializeField]
		private AnimationTarget type = AnimationTarget.None;

		[SerializeField]
		private Transform transformTarget = null;

		[ShowIf("NeedsFloatPair")]
		[SerializeField]
		private FloatPairAsset floatPair = null;

		[ShowIf("NeedsVector3Pair")]
		[SerializeField]
		private Vector3PairAsset vector3Pair = null;

		public override void TriggerAnimation(ILTAnimator parent)
		{
			base.TriggerAnimation(parent);
			switch (type)
			{
				case AnimationTarget.None:
					AnimateNone();
					break;
				case AnimationTarget.Position:
					AnimatePosition(vector3Pair);
					break;
				case AnimationTarget.PositionX:
					AnimatePositionX(floatPair);
					break;
				case AnimationTarget.PositionY:
					AnimatePositionY(floatPair);
					break;
				case AnimationTarget.PositionZ:
					AnimatePositionZ(floatPair);
					break;
				case AnimationTarget.Scale:
					AnimateScale(vector3Pair);
					break;
				case AnimationTarget.Rotation:
					AnimateRotation(vector3Pair);
					break;
				case AnimationTarget.RotationSpeedX:
					AnimateRotationSpeedX(floatPair);
					break;
				case AnimationTarget.RotationSpeedY:
					AnimateRotationSpeedY(floatPair);
					break;
				case AnimationTarget.RotationSpeedZ:
					AnimateRotationSpeedZ(floatPair);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public override void ApplyStartValues(ILTAnimator parent)
		{
			this.parent = parent;
			switch (type)
			{
				case AnimationTarget.None:
					break;
				case AnimationTarget.Position:
					StartAnimatePosition(vector3Pair);
					break;
				case AnimationTarget.PositionX:
					StartAnimatePositionX(floatPair);
					break;
				case AnimationTarget.PositionY:
					StartAnimatePositionY(floatPair);
					break;
				case AnimationTarget.PositionZ:
					StartAnimatePositionZ(floatPair);
					break;
				case AnimationTarget.Scale:
					StartAnimateScale(vector3Pair);
					break;
				case AnimationTarget.Rotation:
					StartAnimateRotation(vector3Pair);
					break;
				case AnimationTarget.RotationSpeedX:
					break;
				case AnimationTarget.RotationSpeedY:
					break;
				case AnimationTarget.RotationSpeedZ:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private bool ValidateTarget()
		{
			if (transformTarget == null)
			{
				Debug.LogError("Can't start animation, no targetTransform defined!", gameObject);
				return false;
			}

			return true;
		}

		#region Scale

		public bool StartAnimateScale(Vector3PairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				var localScale = transformTarget.localScale;
				startVector3 += localScale;
				endVector3 += localScale;
			}

			UpdateScaling(startVector3);
			return true;
		}

		public void AnimateScale(Vector3PairAsset fromToValues)
		{
			var initialized = StartAnimateScale(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateScaling, startVector3, endVector3, animationSettings.Duration)
				.setOnComplete(OnScalingComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateScaling(Vector3 value)
		{
			transformTarget.localScale = value;
		}

		private void OnScalingComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateScaling(Vector3.LerpUnclamped(startVector3, endVector3, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateScaling(endVector3);
			}

			OnComplete();
		}

		#endregion

		#region Position

		public bool StartAnimatePosition(Vector3PairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var localPosition = transformTarget.localPosition;
					startVector3 += localPosition;
					endVector3 += localPosition;
				}

				UpdateLocalPosition(startVector3);
			}
			else
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var position = transformTarget.position;
					startVector3 += position;
					endVector3 += position;
				}

				UpdatePosition(startVector3);
			}

			return true;
		}

		public void AnimatePosition(Vector3PairAsset fromToValues)
		{
			var initialized = StartAnimatePosition(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				tween = LeanTween.value(gameObject, UpdateLocalPosition, startVector3, endVector3, animationSettings.Duration)
					.setOnComplete(OnLocalPositionComplete);
			}
			else
			{
				tween = LeanTween.value(gameObject, UpdatePosition, startVector3, endVector3, animationSettings.Duration)
					.setOnComplete(OnPositionComplete);
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateLocalPosition(Vector3 value)
		{
			transformTarget.localPosition = value;
		}

		private void OnLocalPositionComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateLocalPosition(Vector3.LerpUnclamped(startVector3, endVector3, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateLocalPosition(endVector3);
			}

			OnComplete();
		}

		private void UpdatePosition(Vector3 value)
		{
			transformTarget.position = value;
		}

		private void OnPositionComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdatePosition(Vector3.LerpUnclamped(startVector3, endVector3, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdatePosition(endVector3);
			}

			OnComplete();
		}

		#endregion

		#region PositionX

		public bool StartAnimatePositionX(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var localPosition = transformTarget.localPosition;
					startFloat += localPosition.x;
					endFloat += localPosition.x;
				}

				UpdateLocalPositionX(startFloat);
			}
			else
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var position = transformTarget.position;
					startFloat += position.x;
					endFloat += position.x;
				}

				UpdatePositionX(startFloat);
			}

			return true;
		}

		public void AnimatePositionX(FloatPairAsset fromToValues)
		{
			var initialized = StartAnimatePositionX(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				tween = LeanTween.value(gameObject, UpdateLocalPositionX, startFloat, endFloat, animationSettings.Duration)
					.setOnComplete(OnLocalPositionXComplete);
			}
			else
			{
				tween = LeanTween.value(gameObject, UpdatePositionX, startFloat, endFloat, animationSettings.Duration)
					.setOnComplete(OnPositionXComplete);
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateLocalPositionX(float value)
		{
			var position = transformTarget.localPosition;
			position.x = value;
			transformTarget.localPosition = position;
		}

		private void OnLocalPositionXComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateLocalPositionX(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateLocalPositionX(endFloat);
			}

			OnComplete();
		}

		private void UpdatePositionX(float value)
		{
			var position = transformTarget.position;
			position.x = value;
			transformTarget.position = position;
		}

		private void OnPositionXComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdatePositionX(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdatePositionX(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region PositionY

		public bool StartAnimatePositionY(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var localPosition = transformTarget.localPosition;
					startFloat += localPosition.y;
					endFloat += localPosition.y;
				}

				UpdateLocalPositionY(startFloat);
			}
			else
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var position = transformTarget.position;
					startFloat += position.y;
					endFloat += position.y;
				}

				UpdatePositionY(startFloat);
			}

			return true;
		}

		public void AnimatePositionY(FloatPairAsset fromToValues)
		{
			var initialized = StartAnimatePositionY(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				tween = LeanTween.value(gameObject, UpdateLocalPositionY, startFloat, endFloat, animationSettings.Duration)
					.setOnComplete(OnLocalPositionYComplete);
			}
			else
			{
				tween = LeanTween.value(gameObject, UpdatePositionY, startFloat, endFloat, animationSettings.Duration)
					.setOnComplete(OnPositionYComplete);
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateLocalPositionY(float value)
		{
			var position = transformTarget.localPosition;
			position.y = value;
			transformTarget.localPosition = position;
		}

		private void OnLocalPositionYComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateLocalPositionY(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateLocalPositionY(endFloat);
			}

			OnComplete();
		}

		private void UpdatePositionY(float value)
		{
			var position = transformTarget.position;
			position.y = value;
			transformTarget.position = position;
		}

		private void OnPositionYComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdatePositionY(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdatePositionY(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region PositionZ

		public bool StartAnimatePositionZ(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var localPosition = transformTarget.localPosition;
					startFloat += localPosition.z;
					endFloat += localPosition.z;
				}

				UpdateLocalPositionZ(startFloat);
			}
			else
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var position = transformTarget.position;
					startFloat += position.z;
					endFloat += position.z;
				}

				UpdatePositionZ(startFloat);
			}

			return true;
		}

		public void AnimatePositionZ(FloatPairAsset fromToValues)
		{
			var initialized = StartAnimatePositionZ(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				tween = LeanTween.value(gameObject, UpdateLocalPositionZ, startFloat, endFloat, animationSettings.Duration)
					.setOnComplete(OnLocalPositionZComplete);
			}
			else
			{
				tween = LeanTween.value(gameObject, UpdatePositionZ, startFloat, endFloat, animationSettings.Duration)
					.setOnComplete(OnPositionZComplete);
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateLocalPositionZ(float value)
		{
			var position = transformTarget.localPosition;
			position.z = value;
			transformTarget.localPosition = position;
		}

		private void OnLocalPositionZComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateLocalPositionZ(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateLocalPositionZ(endFloat);
			}

			OnComplete();
		}

		private void UpdatePositionZ(float value)
		{
			var position = transformTarget.position;
			position.z = value;
			transformTarget.position = position;
		}

		private void OnPositionZComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdatePositionZ(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdatePositionZ(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region Rotation

		public bool StartAnimateRotation(Vector3PairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			startQuaternion = Quaternion.Euler(fromToValues.A);
			endQuaternion = Quaternion.Euler(fromToValues.B);

			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var localRotation = transformTarget.localRotation;
					startQuaternion = MathX.RotateLocalSpace(localRotation, startQuaternion);
					endQuaternion = MathX.RotateLocalSpace(localRotation, endQuaternion);
				}

				UpdateLocalRotation(0f);
			}
			else
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var rotation = transformTarget.rotation;
					startQuaternion = MathX.RotateWorldSpace(rotation, startQuaternion);
					endQuaternion = MathX.RotateWorldSpace(rotation, endQuaternion);
				}

				UpdateRotation(0f);
			}

			return true;
		}

		public void AnimateRotation(Vector3PairAsset fromToValues)
		{
			var initialized = StartAnimateRotation(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				tween = LeanTween.value(gameObject, UpdateLocalRotation, 0f, 1f, animationSettings.Duration)
					.setOnComplete(OnLocalRotationComplete);
			}
			else
			{
				tween = LeanTween.value(gameObject, UpdateRotation, 0f, 1f, animationSettings.Duration)
					.setOnComplete(OnRotationComplete);
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateLocalRotation(float value)
		{
			transformTarget.localRotation = Quaternion.LerpUnclamped(startQuaternion, endQuaternion, value);
		}

		private void OnLocalRotationComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateLocalRotation(animationSettings.AnimationCurve.Evaluate(1f));
			}
			else
			{
				UpdateLocalRotation(1f);
			}

			OnComplete();
		}

		private void UpdateRotation(float value)
		{
			transformTarget.rotation = Quaternion.LerpUnclamped(startQuaternion, endQuaternion, value);
		}

		private void OnRotationComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRotation(animationSettings.AnimationCurve.Evaluate(1f));
			}
			else
			{
				UpdateRotation(1f);
			}

			OnComplete();
		}

		#endregion

		#region RotationSpeed

		public void AnimateRotationSpeedX(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.IgnoreTimeScale)
				{
					tween = LeanTween.value(gameObject, UpdateLocalRotationSpeedXUnscaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
				else
				{
					tween = LeanTween.value(gameObject, UpdateLocalRotationSpeedXScaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
			}
			else
			{
				if (animationSettings.IgnoreTimeScale)
				{
					tween = LeanTween.value(gameObject, UpdateRotationSpeedXUnscaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
				else
				{
					tween = LeanTween.value(gameObject, UpdateRotationSpeedXScaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		public void AnimateRotationSpeedY(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.IgnoreTimeScale)
				{
					tween = LeanTween.value(gameObject, UpdateLocalRotationSpeedYUnscaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
				else
				{
					tween = LeanTween.value(gameObject, UpdateLocalRotationSpeedYScaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
			}
			else
			{
				if (animationSettings.IgnoreTimeScale)
				{
					tween = LeanTween.value(gameObject, UpdateRotationSpeedYUnscaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
				else
				{
					tween = LeanTween.value(gameObject, UpdateRotationSpeedYScaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		public void AnimateRotationSpeedZ(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTarget() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return;
			}

			LTDescr tween;
			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.IgnoreTimeScale)
				{
					tween = LeanTween.value(gameObject, UpdateLocalRotationSpeedZUnscaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
				else
				{
					tween = LeanTween.value(gameObject, UpdateLocalRotationSpeedZScaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
			}
			else
			{
				if (animationSettings.IgnoreTimeScale)
				{
					tween = LeanTween.value(gameObject, UpdateRotationSpeedZUnscaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
				else
				{
					tween = LeanTween.value(gameObject, UpdateRotationSpeedZScaledTime, startFloat, endFloat, animationSettings.Duration)
						.setOnComplete(OnRotationSpeedComplete);
				}
			}

			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateLocalRotationSpeedXScaledTime(float value)
		{
			UpdateLocalRotationSpeed(value * Time.deltaTime, right);
		}

		private void UpdateLocalRotationSpeedXUnscaledTime(float value)
		{
			UpdateLocalRotationSpeed(value * Time.unscaledDeltaTime, right);
		}

		private void UpdateLocalRotationSpeedYScaledTime(float value)
		{
			UpdateLocalRotationSpeed(value * Time.deltaTime, up);
		}

		private void UpdateLocalRotationSpeedYUnscaledTime(float value)
		{
			UpdateLocalRotationSpeed(value * Time.unscaledDeltaTime, up);
		}

		private void UpdateLocalRotationSpeedZScaledTime(float value)
		{
			UpdateLocalRotationSpeed(value * Time.deltaTime, forward);
		}

		private void UpdateLocalRotationSpeedZUnscaledTime(float value)
		{
			UpdateLocalRotationSpeed(value * Time.unscaledDeltaTime, forward);
		}

		private void UpdateLocalRotationSpeed(float value, Vector3 axis)
		{
			transformTarget.localRotation = transformTarget.localRotation * Quaternion.AngleAxis(value, axis);
		}

		private void UpdateRotationSpeedXScaledTime(float value)
		{
			UpdateRotationSpeed(value * Time.deltaTime, right);
		}

		private void UpdateRotationSpeedXUnscaledTime(float value)
		{
			UpdateRotationSpeed(value * Time.unscaledDeltaTime, right);
		}

		private void UpdateRotationSpeedYScaledTime(float value)
		{
			UpdateRotationSpeed(value * Time.deltaTime, up);
		}

		private void UpdateRotationSpeedYUnscaledTime(float value)
		{
			UpdateRotationSpeed(value * Time.unscaledDeltaTime, up);
		}

		private void UpdateRotationSpeedZScaledTime(float value)
		{
			UpdateRotationSpeed(value * Time.deltaTime, forward);
		}

		private void UpdateRotationSpeedZUnscaledTime(float value)
		{
			UpdateRotationSpeed(value * Time.unscaledDeltaTime, forward);
		}

		private void UpdateRotationSpeed(float value, Vector3 axis)
		{
			transformTarget.rotation = transformTarget.rotation * Quaternion.AngleAxis(value, axis);
		}

		private void OnRotationSpeedComplete()
		{
			OnComplete();
		}

		#endregion

		#if UNITY_EDITOR
		private bool NeedsFloatPair()
		{
			return type == AnimationTarget.PositionX ||
					type == AnimationTarget.PositionY ||
					type == AnimationTarget.PositionZ ||
					type == AnimationTarget.RotationSpeedX ||
					type == AnimationTarget.RotationSpeedY ||
					type == AnimationTarget.RotationSpeedZ;
		}

		private bool NeedsVector3Pair()
		{
			return type == AnimationTarget.Position ||
					type == AnimationTarget.Scale ||
					type == AnimationTarget.Rotation;
		}
		#endif
	}
}