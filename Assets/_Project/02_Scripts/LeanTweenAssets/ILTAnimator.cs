// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILTAnimator.cs" company="Supyrb">
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
	public interface ILTAnimator
	{
		void AnimationFinished();
		GameObject gameObject { get; }
	}
}