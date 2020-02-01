// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILTAnimationData.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace Supyrb
{
	public interface ILTAnimationData
	{
		void TriggerAnimation(ILTAnimator parent);
		void ApplyStartValues(ILTAnimator parent);
		void SetAnimation(LTAnimationAsset newAnimationSettings);
		void SkipToEnd(ILTAnimator parent);
		void SkipToEndIfStarted(ILTAnimator parent);
	}
}