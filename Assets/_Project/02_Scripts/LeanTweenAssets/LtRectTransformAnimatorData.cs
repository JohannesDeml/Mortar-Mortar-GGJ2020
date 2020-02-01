// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LtRectTransformAnimatorData.cs" company="Supyrb">
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
	public class LtRectTransformAnimatorData : ALTAnimatorData
	{
		[SerializeField]
		private LTRectAnimationData[] animations = null;


		protected override ILTAnimationData[] Animations => animations;
	}
}