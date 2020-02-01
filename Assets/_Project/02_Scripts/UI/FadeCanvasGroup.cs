// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FadeCanvasGroup.cs" company="Supyrb">
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
using UnityEngine.Rendering;

namespace Supyrb
{
	public class FadeCanvasGroup : MonoBehaviour
	{
		public enum Status
		{
			None,
			ToVisible,
			Visible,
			ToInvisible,
			Invisible
		}

		public Signal VisibleSignal = new Signal();
		public Signal InvisibleSignal = new Signal();

		[SerializeField]
		private CanvasGroup target = null;

		[SerializeField]
		private AnimationCurveAsset fadeCurve = null;

		[SerializeField]
		[Tooltip("Offset for non relative animations before the update cycle starts")]
		private int animationFrameOffset = 5;

		[SerializeField]
		private float maxTimeChange = 0.03f;

		[SerializeField]
		private bool forceFullAnimationOnFirstRun = true;
		
		private Status status = Status.None;

		private int currentFrameOffset = 0;
		private float animationTime = 0f;

		private void Awake()
		{
			if (status == Status.None)
			{
				this.enabled = false;
			}
		}

		[Button]
		public void Show()
		{
			Show(true);
		}

		[Button]
		public void ForceToVisible()
		{
			FinishToVisible();
		}

		public void Show(bool relative)
		{
			if (status == Status.None && forceFullAnimationOnFirstRun)
			{
				relative = false;
			}

			if (!relative)
			{
				currentFrameOffset = animationFrameOffset;
				animationTime = 0f;
				target.alpha = 0f;
			}
			else if (status == Status.Visible)
			{
				return;
			}


			status = Status.ToVisible;
			this.enabled = true;

			target.gameObject.SetActive(true);
		}

		[Button]
		public void Hide()
		{
			Hide(true);
		}

		[Button]
		public void ForceToInvisible()
		{
			FinishToInvisible();
		}

		public void Hide(bool relative)
		{
			if (status == Status.None && forceFullAnimationOnFirstRun)
			{
				relative = false;
			}

			if (!relative)
			{
				currentFrameOffset = animationFrameOffset;
				animationTime = fadeCurve.Curve.Duration();
				target.alpha = fadeCurve.Curve.Evaluate(animationTime);
			}
			else if (status == Status.Invisible)
			{
				return;
			}

			status = Status.ToInvisible;
			this.enabled = true;

			target.gameObject.SetActive(true);
		}

		private float GetDeltaTime()
		{
			return Mathf.Min(maxTimeChange, Time.deltaTime);
		}

		private void Update()
		{
			if (!SplashScreen.isFinished)
			{
				return;
			}

			if (currentFrameOffset > 0)
			{
				currentFrameOffset--;
				return;
			}

			if (status == Status.ToVisible)
			{
				UpdateToVisible();
				return;
			}

			if (status == Status.ToInvisible)
			{
				UpdateToInvisible();
				return;
			}

			this.enabled = false;
		}

		private void UpdateToVisible()
		{
			animationTime += GetDeltaTime();
			if (animationTime > fadeCurve.Curve.Duration())
			{
				FinishToVisible();
				return;
			}

			target.alpha = fadeCurve.Curve.Evaluate(animationTime);
		}

		private void FinishToVisible()
		{
			animationTime = fadeCurve.Curve.Duration();
			target.alpha = fadeCurve.Curve.Evaluate(animationTime);
			status = Status.Visible;
			this.enabled = false;
			target.gameObject.SetActive(true);
			VisibleSignal.Dispatch();
		}

		private void UpdateToInvisible()
		{
			animationTime -= GetDeltaTime();
			if (animationTime < 0f)
			{
				FinishToInvisible();
				return;
			}

			target.alpha = fadeCurve.Curve.Evaluate(animationTime);
		}

		private void FinishToInvisible()
		{
			animationTime = 0f;
			target.alpha = 0f;
			status = Status.Invisible;
			target.gameObject.SetActive(false);
			this.enabled = false;
			InvisibleSignal.Dispatch();
		}


		#if UNITY_EDITOR
		private void Reset()
		{
			if (target == null)
			{
				target = GetComponent<CanvasGroup>();
			}
		}
		#endif
	}
}