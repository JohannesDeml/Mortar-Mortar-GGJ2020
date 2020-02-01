// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LTRectTransformAnimator.cs" company="Supyrb">
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
	public class LTRectTransformAnimator : ALTAnimator
	{
		[SerializeField]
		private LTRectAnimationData[] animations = null;


		protected override ILTAnimationData[] Animations => animations;
	}
}