// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RectExtensions.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb
{
	public static class RectExtensions
	{
		/// <summary>
		/// Given a Rect, it returns a left anchored copy with a width of "width".
		/// If a negative number is passed, it behaves as if there is a space on the right with a width of "width".
		/// </summary>
		/// <param name="position"></param>
		/// <param name="width"></param>
		/// <returns></returns>
		public static Rect Left(this Rect position, float width)
		{
			if (width > 0.0f)
			{
				position.width = width;
			}
			else
			{
				position.width += width;
			}

			return position;
		}

		/// <summary>
		/// Given a Rect, it returns a right anchored copy with a width of "width".
		/// If a negative number is passed, it behaves as if there is a space on the left with a width of "width".
		/// </summary>
		/// <param name="position"></param>
		/// <param name="width"></param>
		/// <returns></returns>
		public static Rect Right(this Rect position, float width)
		{
			if (width > 0.0f)
			{
				position.xMin = position.xMax - width;
			}
			else
			{
				position.xMin -= width;
			}

			return position;
		}
	}
}