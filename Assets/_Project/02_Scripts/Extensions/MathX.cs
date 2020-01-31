// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathX.cs" company="Supyrb">
//   Copyright (c) 2017 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   send@johannesdeml.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Supyrb.Common
{
	public static class MathX
	{
		/// <summary>
		/// Compares a float with a range and returns if the float is within the range
		/// </summary>
		/// <param name="number">The number that should be tested</param>
		/// <param name="min">The lower bound of the range</param>
		/// <param name="max">The upper bound of the range</param>
		/// <returns>True if the number is within the range</returns>
		public static bool InRange(this float number, float min, float max)
		{
			if (min > max)
			{
				float h = min;
				min = max;
				max = h;
			}

			return (number >= min && number <= max);
		}

		/// <summary>
		/// Calculates the minimum distance to a line for a point
		/// http://mathworld.wolfram.com/Point-LineDistance3-Dimensional.html
		/// </summary>
		/// <param name="point">The point for which the distance is calculated</param>
		/// <param name="linePointA">The start point of the line</param>
		/// <param name="linePointB">The end point of the line</param>
		/// <returns></returns>
		public static float MinDistanceToLine(this Vector3 point, Vector3 linePointA, Vector3 linePointB)
		{
			return Vector3.Cross(point - linePointA, point - linePointB).magnitude / Vector3.Distance(linePointA, linePointB);
		}

		/// <summary>
		/// Altered from plane.Raycast
		/// </summary>
		/// <param name="plane">Plane the position should be on</param>
		/// <param name="rayStart">Ray start position</param>
		/// <param name="rayDirection">Ray direction</param>
		/// <returns>Point on the plane that intersects with the ray. If no intersection happens, the ray start position is returned.</returns>
		public static Vector3 RaycastPosition(this Plane plane, Vector3 rayStart, Vector3 rayDirection)
		{
			float a = Vector3.Dot(rayDirection, plane.normal);
			if (Mathf.Approximately(a, 0.0f))
			{
				return rayStart;
			}

			float num = -Vector3.Dot(rayStart, plane.normal) - plane.distance;

			var distance = num / a;
			return rayStart + rayDirection * distance;
		}

		public static Quaternion RotateLocalSpace(Quaternion objectRotation, Quaternion localRotation)
		{
			return objectRotation * localRotation;
		}
		
		public static Quaternion RotateWorldSpace(Quaternion objectRotation, Quaternion globalRotation)
		{
			return globalRotation * objectRotation;
		}
		
		public static bool Approximately(Vector3 a, Vector3 b)
		{
			return Mathf.Approximately(a.x, b.x) &&
					Mathf.Approximately(a.y, b.y) &&
					Mathf.Approximately(a.z, b.z);
		}

		public static int RoundToNextPowerOfTwo(int a)
		{
			int next = CeilToNextPowerOfTwo(a);
			int prev = next >> 1;
			return next - a <= a - prev ? next : prev;
		}

		public static int CeilToNextPowerOfTwo(int number)
		{
			int a = number;
			int powOfTwo = 1;

			while (a > 1)
			{
				a = a >> 1;
				powOfTwo = powOfTwo << 1;
			}

			if (powOfTwo != number)
			{
				powOfTwo = powOfTwo << 1;
			}

			return powOfTwo;
		}

		public static float LinearToGammaSpace(float linear)
		{
			return Mathf.Max(1.055f * Mathf.Pow(linear, 0.416666667f) - 0.055f, 0f);
		}

		public static float GammaToLinearSpace(float gamma)
		{
			return Mathf.Pow(gamma + 0.055f, 2.4f) / 1.13711896582f;
		}

		public static Vector2 Abs(Vector2 a)
		{
			return new Vector2(Math.Abs(a.x), Math.Abs(a.y));
		}

		public static Vector3 Abs(Vector3 a)
		{
			return new Vector3(Math.Abs(a.x), Math.Abs(a.y), Math.Abs(a.z));
		}

		public static Vector2 Round(Vector2 a)
		{
			return new Vector2(Mathf.Round(a.x), Mathf.Round(a.y));
		}

		public static Vector3 Round(Vector3 a)
		{
			return new Vector3(Mathf.Round(a.x), Mathf.Round(a.y), Mathf.Round(a.z));
		}

		public static Vector2 ScalarMultiplication(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		internal static long Lerp(long a, long b, float t)
		{
			return Convert.ToInt64(a * (1f - t) + b * t);
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, Vector3 t)
		{
			Vector3 newVector = new Vector3(
				a.x * (1 - t.x) + b.x * t.x,
				a.y * (1 - t.y) + b.y * t.y,
				a.z * (1 - t.z) + b.z * t.z
			);
			return newVector;
		}

		/// <summary>
		/// See https://en.wikipedia.org/wiki/Scalar_projection
		/// </summary>
		/// <param name="a">A vector</param>
		/// <param name="unitVectorB">The vector that will be projected onto, has to have a length of 1</param>
		/// <returns></returns>
		public static float ScalarProjection(Vector3 a, Vector3 unitVectorB)
		{
			return Vector3.Dot(a, unitVectorB);
		}
	}
}