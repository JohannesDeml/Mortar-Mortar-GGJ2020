// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LTRectAnimationData.cs" company="Supyrb">
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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Supyrb
{
	[Serializable]
	public class LTRectAnimationData : ALTAnimationData
	{
		public enum AnimationTarget
		{
			None,
			Scale,
			Rotation,
			Position,
			Width,
			Height,
			WidthAndHeight,
			MinAnchor,
			MaxAnchor,
			MinMaxAnchor,
			TextColor,
			CanvasGroupAlpha,
			ImageColor
		}

		[SerializeField]
		private AnimationTarget type = AnimationTarget.None;

		[SerializeField]
		[ShowIf("IsRectTransformTarget")]
		private RectTransform targetRectTransform = null;

		[SerializeField]
		[ShowIf("IsTmpTarget")]
		private TextMeshProUGUI targetText = null;

		[SerializeField]
		[ShowIf("IsCanvasGroupTarget")]
		private CanvasGroup targetCanvasGroup = null;

		[SerializeField]
		[ShowIf("IsImageTarget")]
		private Image targetImage = null;

		[ShowIf("NeedsFloatPair")]
		[SerializeField]
		private FloatPairAsset floatPair = null;

		[ShowIf("NeedsVector2Pair")]
		[SerializeField]
		private Vector2PairAsset vector2Pair = null;

		[ShowIf("NeedsVector3Pair")]
		[SerializeField]
		private Vector3PairAsset vector3Pair = null;

		[ShowIf("NeedsVector4Pair")]
		[SerializeField]
		private Vector4PairAsset vector4Pair = null;

		[ShowIf("NeedsColorPair")]
		[SerializeField]
		private ColorPairAsset colorPair = null;

		public override void TriggerAnimation(ILTAnimator parent)
		{
			base.TriggerAnimation(parent);
			switch (type)
			{
				case AnimationTarget.None:
					AnimateNone();
					return;
				case AnimationTarget.Scale:
					AnimateScale(vector3Pair);
					return;
				case AnimationTarget.Rotation:
					AnimateRotation(vector3Pair);
					return;
				case AnimationTarget.Position:
					AnimateRectTransformPosition(vector2Pair);
					return;
				case AnimationTarget.Width:
					AnimateRectTransformWidth(floatPair);
					return;
				case AnimationTarget.Height:
					AnimateRectTransformHeight(floatPair);
					return;
				case AnimationTarget.WidthAndHeight:
					AnimateRectTransformWidthAndHeight(vector2Pair);
					return;
				case AnimationTarget.MinAnchor:
					AnimateRectTransformMinAnchor(vector2Pair);
					return;
				case AnimationTarget.MaxAnchor:
					AnimateRectTransformMaxAnchor(vector2Pair);
					return;
				case AnimationTarget.MinMaxAnchor:
					AnimateRectTransformMinMaxAnchors(vector4Pair);
					return;
				case AnimationTarget.TextColor:
					AnimateTmpColor(colorPair);
					return;
				case AnimationTarget.CanvasGroupAlpha:
					AnimateCanvasGroupAlpha(floatPair);
					return;
				case AnimationTarget.ImageColor:
					AnimateImageColor(colorPair);
					return;
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
					return;
				case AnimationTarget.Scale:
					StartAnimateScale(vector3Pair);
					return;
				case AnimationTarget.Rotation:
					StartAnimateRotation(vector3Pair);
					return;
				case AnimationTarget.Position:
					StartAnimateRectTransformPosition(vector2Pair);
					return;
				case AnimationTarget.Width:
					StartAnimateRectTransformWidth(floatPair);
					return;
				case AnimationTarget.Height:
					StartAnimateRectTransformHeight(floatPair);
					return;
				case AnimationTarget.WidthAndHeight:
					StartAnimateRectTransformWidthAndHeight(vector2Pair);
					return;
				case AnimationTarget.MinAnchor:
					StartAnimateRectTransformMinAnchor(vector2Pair);
					return;
				case AnimationTarget.MaxAnchor:
					StartAnimateRectTransformMaxAnchor(vector2Pair);
					return;
				case AnimationTarget.MinMaxAnchor:
					StartAnimateRectTransformMinMaxAnchors(vector4Pair);
					return;
				case AnimationTarget.TextColor:
					StartAnimateTmpColor(colorPair);
					return;
				case AnimationTarget.CanvasGroupAlpha:
					StartAnimateCanvasGroupAlpha(floatPair);
					return;
				case AnimationTarget.ImageColor:
					StartAnimateImageColor(colorPair);
					return;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private bool ValidateTargetRectTransform()
		{
			if (targetRectTransform == null)
			{
				Debug.LogError("Can't start animation, no targetRectTransform defined!", gameObject);
				return false;
			}

			return true;
		}

		private bool ValidateTargetText()
		{
			if (targetText == null)
			{
				Debug.LogError("Can't start animation, no targetText defined!", gameObject);
				return false;
			}

			return true;
		}

		private bool ValidateTargetCanvasGroup()
		{
			if (targetCanvasGroup == null)
			{
				Debug.LogError("Can't start animation, no targetCanvasGroup defined!", gameObject);
				return false;
			}

			return true;
		}

		private bool ValidateTargetImage()
		{
			if (targetImage == null)
			{
				Debug.LogError("Can't start animation, no targetImage defined!", gameObject);
				return false;
			}

			return true;
		}

		#region Scale

		public bool StartAnimateScale(Vector3PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startVector3 += targetRectTransform.localScale;
				endVector3 += targetRectTransform.localScale;
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
			targetRectTransform.localScale = value;
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

		#region Rotation

		public bool StartAnimateRotation(Vector3PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			startQuaternion = Quaternion.Euler(startVector3);
			endQuaternion = Quaternion.Euler(endVector3);

			if (animationSettings.Space == Space.Self)
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var localRotation = targetRectTransform.localRotation;
					startQuaternion = MathX.RotateLocalSpace(localRotation, startQuaternion);
					endQuaternion = MathX.RotateLocalSpace(localRotation, endQuaternion);
				}

				UpdateLocalRotation(0f);
			}
			else
			{
				if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
				{
					var rotation = targetRectTransform.rotation;
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

		private void UpdateLocalRotation(float lerpValue)
		{
			targetRectTransform.localRotation = Quaternion.LerpUnclamped(startQuaternion, endQuaternion, lerpValue);
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

		private void UpdateRotation(float lerpValue)
		{
			targetRectTransform.rotation = Quaternion.LerpUnclamped(startQuaternion, endQuaternion, lerpValue);
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

		#region Position

		public bool StartAnimateRectTransformPosition(Vector2PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startVector2 += targetRectTransform.anchoredPosition;
				endVector2 += targetRectTransform.anchoredPosition;
			}

			UpdateRectTransformPosition(startVector2);

			return true;
		}

		public void AnimateRectTransformPosition(Vector2PairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformPosition(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformPosition, startVector2, endVector2, animationSettings.Duration)
				.setOnComplete(OnRectTransformPositionComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformPosition(Vector2 value)
		{
			targetRectTransform.anchoredPosition = value;
		}

		private void OnRectTransformPositionComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformPosition(Vector2.LerpUnclamped(startVector2, endVector2, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformPosition(endVector2);
			}

			OnComplete();
		}

		#endregion

		#region Height

		public bool StartAnimateRectTransformHeight(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startFloat += targetRectTransform.rect.height;
				endFloat += targetRectTransform.rect.height;
			}

			UpdateRectTransformHeight(startFloat);

			return true;
		}

		public void AnimateRectTransformHeight(FloatPairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformHeight(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformHeight, startFloat, endFloat, animationSettings.Duration)
				.setOnComplete(OnRectTransformHeightComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformHeight(float value)
		{
			targetRectTransform.SetHeight(value);
			LayoutRebuilder.MarkLayoutForRebuild(targetRectTransform);
		}

		private void OnRectTransformHeightComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformHeight(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformHeight(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region Width

		public bool StartAnimateRectTransformWidth(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startFloat += targetRectTransform.rect.width;
				endFloat += targetRectTransform.rect.width;
			}

			UpdateRectTransformWidth(startFloat);

			return true;
		}

		public void AnimateRectTransformWidth(FloatPairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformWidth(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformWidth, startFloat, endFloat, animationSettings.Duration)
				.setOnComplete(OnRectTransformWidthComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformWidth(float value)
		{
			targetRectTransform.SetWidth(value);
		}

		private void OnRectTransformWidthComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformWidth(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformWidth(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region WidthAndHeight

		public bool StartAnimateRectTransformWidthAndHeight(Vector2PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startVector2.x += targetRectTransform.rect.width;
				startVector2.y += targetRectTransform.rect.height;

				endVector2.x += targetRectTransform.rect.width;
				endVector2.y += targetRectTransform.rect.height;
			}

			UpdateRectTransformWidthAndHeight(startVector2);

			return true;
		}

		public void AnimateRectTransformWidthAndHeight(Vector2PairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformWidthAndHeight(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformWidthAndHeight, startVector2, endVector2, animationSettings.Duration)
				.setOnComplete(OnRectTransformWidthAndHeightComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformWidthAndHeight(Vector2 value)
		{
			targetRectTransform.SetSize(value);
		}

		private void OnRectTransformWidthAndHeightComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformWidthAndHeight(Vector2.LerpUnclamped(startVector2, endVector2, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformWidthAndHeight(endVector2);
			}

			OnComplete();
		}

		#endregion

		#region MinAnchor

		public bool StartAnimateRectTransformMinAnchor(Vector2PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startVector2 += targetRectTransform.anchorMin;
				endVector2 += targetRectTransform.anchorMin;
			}

			UpdateRectTransformMinAnchor(startVector2);

			return true;
		}

		public void AnimateRectTransformMinAnchor(Vector2PairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformMinAnchor(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformMinAnchor, startVector2, endVector2, animationSettings.Duration)
				.setOnComplete(OnRectTransformMinAnchorComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformMinAnchor(Vector2 value)
		{
			targetRectTransform.anchorMin = value;
		}

		private void OnRectTransformMinAnchorComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformMinAnchor(Vector2.LerpUnclamped(startVector2, endVector2, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformMinAnchor(endVector2);
			}

			OnComplete();
		}

		#endregion

		#region MaxAnchor

		public bool StartAnimateRectTransformMaxAnchor(Vector2PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startVector2 += targetRectTransform.anchorMax;
				endVector2 += targetRectTransform.anchorMax;
			}

			UpdateRectTransformMaxAnchor(startVector2);

			return true;
		}

		public void AnimateRectTransformMaxAnchor(Vector2PairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformMaxAnchor(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformMaxAnchor, startVector2, endVector2, animationSettings.Duration)
				.setOnComplete(OnRectTransformMaxAnchorComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformMaxAnchor(Vector2 value)
		{
			targetRectTransform.anchorMax = value;
		}

		private void OnRectTransformMaxAnchorComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformMaxAnchor(Vector2.LerpUnclamped(startVector2, endVector2, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformMaxAnchor(endVector2);
			}

			OnComplete();
		}

		#endregion

		#region MinMaxAnchor

		public bool StartAnimateRectTransformMinMaxAnchors(Vector4PairAsset fromToValues)
		{
			var initialized = ValidateTargetRectTransform() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startVector4.x += targetRectTransform.anchorMin.x;
				startVector4.y += targetRectTransform.anchorMin.y;
				startVector4.z += targetRectTransform.anchorMax.x;
				startVector4.w += targetRectTransform.anchorMax.y;

				endVector4.x += targetRectTransform.anchorMin.x;
				endVector4.y += targetRectTransform.anchorMin.y;
				endVector4.z += targetRectTransform.anchorMax.x;
				endVector4.w += targetRectTransform.anchorMax.y;
			}

			UpdateRectTransformMinMaxAnchor(0f);

			return true;
		}

		public void AnimateRectTransformMinMaxAnchors(Vector4PairAsset fromToValues)
		{
			var initialized = StartAnimateRectTransformMinMaxAnchors(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateRectTransformMinMaxAnchor, 0, 1, animationSettings.Duration)
				.setOnComplete(OnRectTransformMinMaxAnchorComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateRectTransformMinMaxAnchor(float percent)
		{
			var current = Vector4.LerpUnclamped(startVector4, endVector4, percent);
			targetRectTransform.anchorMin = new Vector2(current.x, current.y);
			targetRectTransform.anchorMax = new Vector2(current.z, current.w);
		}

		private void OnRectTransformMinMaxAnchorComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateRectTransformMinMaxAnchor(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateRectTransformMinMaxAnchor(1f);
			}

			OnComplete();
		}

		#endregion

		#region TextMeshPro

		public bool StartAnimateTmpColor(ColorPairAsset fromToValues)
		{
			var initialized = ValidateTargetText() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startColor += targetText.color;
				endColor += targetText.color;
			}

			UpdateTmpColor(startColor);

			return true;
		}

		public void AnimateTmpColor(ColorPairAsset fromToValues)
		{
			var initialized = StartAnimateTmpColor(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateTmpColor, startColor, endColor, animationSettings.Duration)
				.setOnComplete(OnTmpColorComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateTmpColor(Color value)
		{
			targetText.color = value;
		}

		private void OnTmpColorComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateTmpColor(Color.LerpUnclamped(startColor, endColor, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateTmpColor(endColor);
			}

			OnComplete();
		}

		#endregion

		#region CanvasGroup

		public bool StartAnimateCanvasGroupAlpha(FloatPairAsset fromToValues)
		{
			var initialized = ValidateTargetCanvasGroup() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startFloat += targetCanvasGroup.alpha;
				endFloat += targetCanvasGroup.alpha;
			}

			UpdateCanvasGroupAlpha(startFloat);

			return true;
		}

		public void AnimateCanvasGroupAlpha(FloatPairAsset fromToValues)
		{
			var initialized = StartAnimateCanvasGroupAlpha(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateCanvasGroupAlpha, startFloat, endFloat, animationSettings.Duration)
				.setOnComplete(OnCanvasGroupAlphaComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateCanvasGroupAlpha(float value)
		{
			targetCanvasGroup.alpha = value;
		}

		private void OnCanvasGroupAlphaComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateCanvasGroupAlpha(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateCanvasGroupAlpha(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region Image

		public bool StartAnimateImageColor(ColorPairAsset fromToValues)
		{
			var initialized = ValidateTargetImage() && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				startColor += targetImage.color;
				endColor += targetImage.color;
			}

			UpdateImageColor(startColor);

			return true;
		}

		public void AnimateImageColor(ColorPairAsset fromToValues)
		{
			var initialized = StartAnimateImageColor(fromToValues);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateImageColor, startColor, endColor, animationSettings.Duration)
				.setOnComplete(OnImageColorComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateImageColor(Color value)
		{
			targetImage.color = value;
		}

		private void OnImageColorComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateImageColor(Color.LerpUnclamped(startColor, endColor, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateImageColor(endColor);
			}

			OnComplete();
		}

		#endregion

		private bool IsRectTransformTarget()
		{
			return type == AnimationTarget.Scale ||
					type == AnimationTarget.Rotation ||
					type == AnimationTarget.Position ||
					type == AnimationTarget.Width ||
					type == AnimationTarget.Height ||
					type == AnimationTarget.WidthAndHeight ||
					type == AnimationTarget.MinAnchor ||
					type == AnimationTarget.MaxAnchor ||
					type == AnimationTarget.MinMaxAnchor;
		}

		private bool IsTmpTarget()
		{
			return type == AnimationTarget.TextColor;
		}

		private bool IsCanvasGroupTarget()
		{
			return type == AnimationTarget.CanvasGroupAlpha;
		}

		private bool IsImageTarget()
		{
			return type == AnimationTarget.ImageColor;
		}

		#if UNITY_EDITOR
		private bool NeedsFloatPair()
		{
			return type == AnimationTarget.Height ||
					type == AnimationTarget.Width ||
					type == AnimationTarget.CanvasGroupAlpha;
		}

		private bool NeedsVector2Pair()
		{
			return type == AnimationTarget.Position ||
					type == AnimationTarget.MaxAnchor ||
					type == AnimationTarget.MinAnchor ||
					type == AnimationTarget.WidthAndHeight;
		}

		private bool NeedsVector3Pair()
		{
			return type == AnimationTarget.Scale ||
					type == AnimationTarget.Rotation;
		}

		private bool NeedsVector4Pair()
		{
			return type == AnimationTarget.MinMaxAnchor;
		}

		private bool NeedsColorPair()
		{
			return type == AnimationTarget.TextColor ||
					type == AnimationTarget.ImageColor;
		}
		#endif
	}
}