// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LTMaterialAnimationData.cs" company="Supyrb">
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
	public class LTMaterialAnimationData : ALTAnimationData
	{
		public enum AnimationTarget
		{
			None,
			MaterialColor,
			MaterialInstanceColor,
			MaterialFloat,
			MaterialInstanceFloat,
			MaterialVector4,
			MaterialInstanceVector4
		}

		[SerializeField]
		private AnimationTarget type = AnimationTarget.None;

		[ShowIf("HasMaterialInstanceTarget")]
		[SerializeField]
		private MeshRenderer rendererTarget = null;

		[ShowIf("HasMaterialInstanceTarget")]
		[SerializeField]
		private int materialSlot = 0;

		[ShowIf("HasMaterialTarget")]
		[SerializeField]
		private Material materialTarget = null;
		
		[SerializeField]
		private ShaderParameter targetParameter = null;

		[ShowIf("NeedsFloatPair")]
		[SerializeField]
		private FloatPairAsset floatPair = null;

		[ShowIf("NeedsVector4Pair")]
		[SerializeField]
		private Vector4PairAsset vector4Pair = null;

		[ShowIf("NeedsColorPair")]
		[SerializeField]
		private ColorPairAsset colorPair = null;

		private Material material;

		public void SetMaterialTarget(Material newMaterial)
		{
			materialTarget = newMaterial;
		}

		public override void TriggerAnimation(ILTAnimator parent)
		{
			base.TriggerAnimation(parent);
			switch (type)
			{
				case AnimationTarget.None:
					AnimateNone();
					break;
				case AnimationTarget.MaterialColor:
					AnimateMaterialColor(colorPair, false);
					break;
				case AnimationTarget.MaterialInstanceColor:
					AnimateMaterialColor(colorPair, true);
					break;
				case AnimationTarget.MaterialFloat:
					AnimateMaterialFloat(floatPair, false);
					break;
				case AnimationTarget.MaterialInstanceFloat:
					AnimateMaterialFloat(floatPair, true);
					break;
				case AnimationTarget.MaterialVector4:
					AnimateMaterialVector4(vector4Pair, false);
					break;
				case AnimationTarget.MaterialInstanceVector4:
					AnimateMaterialVector4(vector4Pair, true);
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
				case AnimationTarget.MaterialColor:
					StartAnimateMaterialColor(colorPair, false);
					break;
				case AnimationTarget.MaterialInstanceColor:
					StartAnimateMaterialColor(colorPair, true);
					break;
				case AnimationTarget.MaterialFloat:
					StartAnimateMaterialFloat(floatPair, false);
					break;
				case AnimationTarget.MaterialInstanceFloat:
					StartAnimateMaterialFloat(floatPair, true);
					break;
				case AnimationTarget.MaterialVector4:
					StartAnimateMaterialVector4(vector4Pair, false);
					break;
				case AnimationTarget.MaterialInstanceVector4:
					StartAnimateMaterialVector4(vector4Pair, true);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private bool ValidateMaterialTarget()
		{
			if (materialTarget == null)
			{
				Debug.LogError("Can't start animation, no materialTarget defined!", gameObject);
				return false;
			}

			return true;
		}

		private bool ValidateRendererTarget()
		{
			if (rendererTarget == null)
			{
				Debug.LogError("Can't start animation, no rendererTarget defined!", gameObject);
				return false;
			}

			return true;
		}

		public bool InitializeMaterial()
		{
			var initialized = ValidateMaterialTarget();
			if (!initialized)
			{
				return false;
			}

			material = materialTarget;
			return true;
		}

		public bool InitializeMaterialInstance()
		{
			var initialized = ValidateRendererTarget();
			if (!initialized)
			{
				return false;
			}

			material = rendererTarget.materials[materialSlot];
			return true;
		}

		#region Color

		public bool StartAnimateMaterialColor(ColorPairAsset fromToValues, bool instance)
		{
			var initialized = (instance ? InitializeMaterialInstance() : InitializeMaterial()) && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				var current = material.GetColor(targetParameter.Hash);
				startColor += current;
				endColor += current;
			}

			UpdateColor(startColor);
			return true;
		}

		public void AnimateMaterialColor(ColorPairAsset fromToValues, bool instance)
		{
			var initialized = StartAnimateMaterialColor(fromToValues, instance);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateColor, startColor, endColor, animationSettings.Duration)
				.setOnComplete(OnColorComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateColor(Color value)
		{
			material.SetColor(targetParameter.Hash, value);
		}

		private void OnColorComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateColor(Color.LerpUnclamped(startColor, endColor, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateColor(endColor);
			}

			OnComplete();
		}

		#endregion

		#region Float

		public bool StartAnimateMaterialFloat(FloatPairAsset fromToValues, bool instance)
		{
			var initialized = (instance ? InitializeMaterialInstance() : InitializeMaterial()) && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				var current = material.GetFloat(targetParameter.Hash);
				startFloat += current;
				endFloat += current;
			}

			UpdateFloat(startFloat);
			return true;
		}

		public void AnimateMaterialFloat(FloatPairAsset fromToValues, bool instance)
		{
			var initialized = StartAnimateMaterialFloat(fromToValues, instance);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateFloat, startFloat, endFloat, animationSettings.Duration)
				.setOnComplete(OnFloatComplete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateFloat(float value)
		{
			material.SetFloat(targetParameter.Hash, value);
		}

		private void OnFloatComplete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateFloat(Mathf.LerpUnclamped(startFloat, endFloat, animationSettings.AnimationCurve.Evaluate(1f)));
			}
			else
			{
				UpdateFloat(endFloat);
			}

			OnComplete();
		}

		#endregion

		#region Vector

		public bool StartAnimateMaterialVector4(Vector4PairAsset fromToValues, bool instance)
		{
			var initialized = (instance ? InitializeMaterialInstance() : InitializeMaterial()) && InitializeFromToValues(fromToValues);
			if (!initialized)
			{
				return false;
			}

			if (animationSettings.Type == LTAnimationAsset.AnimationType.Relative)
			{
				var current = material.GetVector(targetParameter.Hash);
				startVector4 += current;
				endVector4 += current;
			}

			UpdateVector4(0f);
			return true;
		}

		public void AnimateMaterialVector4(Vector4PairAsset fromToValues, bool instance)
		{
			var initialized = StartAnimateMaterialVector4(fromToValues, instance);
			if (!initialized)
			{
				return;
			}

			var tween = LeanTween.value(gameObject, UpdateVector4, 0f, 1f, animationSettings.Duration)
				.setOnComplete(OnVector4Complete);
			ApplyAdditionalSettings(tween);
			uniqueId = tween.uniqueId;
		}

		private void UpdateVector4(float t)
		{
			material.SetVector(targetParameter.Hash, Vector4.LerpUnclamped(startVector4, endVector4, t));
		}

		private void OnVector4Complete()
		{
			if (animationSettings.Easing == LeanTweenType.animationCurve)
			{
				UpdateVector4(animationSettings.AnimationCurve.Evaluate(1f));
			}
			else
			{
				UpdateVector4(1f);
			}

			OnComplete();
		}

		#endregion

		private bool HasMaterialInstanceTarget()
		{
			return type == AnimationTarget.MaterialInstanceColor ||
					type == AnimationTarget.MaterialInstanceFloat ||
					type == AnimationTarget.MaterialInstanceVector4;
		}

		private bool HasMaterialTarget()
		{
			return type == AnimationTarget.MaterialColor ||
					type == AnimationTarget.MaterialFloat ||
					type == AnimationTarget.MaterialVector4;
		}

		#if UNITY_EDITOR
		private bool NeedsFloatPair()
		{
			return type == AnimationTarget.MaterialFloat ||
					type == AnimationTarget.MaterialInstanceFloat;
		}

		private bool NeedsVector4Pair()
		{
			return type == AnimationTarget.MaterialVector4 ||
					type == AnimationTarget.MaterialInstanceVector4;
		}

		private bool NeedsColorPair()
		{
			return type == AnimationTarget.MaterialInstanceColor ||
					type == AnimationTarget.MaterialColor;
		}
		#endif
	}
}