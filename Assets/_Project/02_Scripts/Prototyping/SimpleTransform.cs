// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleTransform.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace Supyrb.Common
{
	/// <summary>
	/// Types of transforms that unity supports
	/// TODO change this to <see cref="UnityEngine.Space"/>
	/// </summary>
	public enum TransformType
	{
		Local,
		World
	}

	/// <summary>
	/// A class to easily encapsulate data about a transform at a specific time.
	/// SimpleTransform does not keep a reference to a transform and therefore does not change with the transform.
	/// </summary>
	[Serializable]
	public struct SimpleTransform
	{
		/// <summary>
		/// The type of the simple transform is used to. 
		/// This information is stored so it can be used the right way if it is applied to Transforms.
		/// </summary>
		[SerializeField]
		private TransformType type;

		[SerializeField]
		private Vector3 position;

		[SerializeField]
		private Quaternion rotation;

		[SerializeField]
		private Vector3 scale;

		public TransformType Type => type;

		public Vector3 Position => position;

		public Quaternion Rotation => rotation;

		public Vector3 Scale => scale;

		public Vector3 Forward => Rotation * Vector3.forward;

		public Vector3 Right => Rotation * Vector3.right;

		public Vector3 Up => Rotation * Vector3.up;

		/// <summary>
		/// Encapsulate a transform information with its position, rotation, local scale and type
		/// The scale will always be the local scale to prevent strange effects with 
		/// lossy scale <see cref="Transform.lossyScale"/>
		/// </summary>
		/// <param name="position">Position of the transform</param>
		/// <param name="rotation">Rotation  of the transform</param>
		/// <param name="scale">Scale of the transform</param>
		/// <param name="type">Type of the transform that is stored. This can either be local or world.
		/// This value is important once the SimpleTransofrm is applied to a transform to set the right values.</param>
		public SimpleTransform(Vector3 position, Quaternion rotation, Vector3 scale, TransformType type = TransformType.World)
		{
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
			this.type = type;
		}

		/// <summary>
		/// Encapsulate a transform information with its position, rotation, local scale and type
		/// The scale will always be the local scale to prevent strange effects with 
		/// lossy scale <see cref="Transform.lossyScale"/>
		/// </summary>
		/// <param name="transform">The transform from which a snapshot should be taken</param>
		/// <param name="type">Type of the transform that is processed from the transform. This can either be local or world.
		/// This value is important once the SimpleTransofrm is applied to a transform to set the right values.</param>
		public SimpleTransform(Transform transform, TransformType type = TransformType.World)
		{
			this.position = type == TransformType.Local ? transform.localPosition : transform.position;
			this.rotation = type == TransformType.Local ? transform.localRotation : transform.rotation;
			this.scale = transform.localScale;
			this.type = type;
		}

		public override string ToString()
		{
			return string.Format("SimpleTransform: Type: {0} \nPosition: {1} \nRotation (euler): {2} \nScale: {3}",
				Type.ToString(),
				Position.ToFormattedString(),
				Rotation.eulerAngles.ToFormattedString(),
				Scale.ToFormattedString());
		}

		public static float Distance(SimpleTransform a, SimpleTransform b)
		{
			return Vector3.Distance(a.Position, b.Position);
		}

		public static float Distance(SimpleTransform st, Vector3 v)
		{
			return Vector3.Distance(st.Position, v);
		}

		public static SimpleTransform Lerp(SimpleTransform a, SimpleTransform b, float t)
		{
			return new SimpleTransform(Vector3.Lerp(a.position, b.position, t),
				Quaternion.Lerp(a.rotation, b.rotation, t),
				Vector3.Lerp(a.scale, b.scale, t),
				a.type);
		}
	}

	public static class SimpleTransformExtensions
	{
		/// <summary>
		/// Creates a snapshot of the current transform that is immune to changes
		/// The scale stored will always be the local scale to prevent strange effects with 
		/// lossy scale <see cref="Transform.lossyScale"/>
		/// </summary>
		/// <param name="transform">The transform the snapshot should be taken from</param>
		/// <returns>A simple transform that holds the world information of the transform</returns>
		public static SimpleTransform GetSimpleTransform(this Transform transform)
		{
			return new SimpleTransform(transform);
		}

		/// <summary>
		/// Creates a snapshot of the current transform that is immune to changes
		/// The scale stored will always be the local scale to prevent strange effects with 
		/// lossy scale <see cref="Transform.lossyScale"/>
		/// </summary>
		/// <param name="transform">The transform the snapshot should be taken from</param>
		/// <param name="type">Type of the transform that is processed from the transform. This can either be local or world.
		/// This value is important once the SimpleTransofrm is applied to a transform to set the right values.</param>
		/// <returns>A simple transform that holds the information of the given type of the transform</returns>
		public static SimpleTransform GetSimpleTransform(this Transform transform, TransformType type)
		{
			return new SimpleTransform(transform, type);
		}

		/// <summary>
		/// Applies the position, rotation and local scale of the simple transform to the properties of the transform 
		/// with their respective type. Only with scale their is just the local scale that is stored and will be set.
		/// </summary>
		/// <param name="transform">Transform that will be changed</param>
		/// <param name="sTransform">The simple transform that should be applied on the transform</param>
		/// <returns>Returns the changed transform. The transform is already changed, 
		/// but out of convenience you can just keep on working with the transform</returns>
		public static Transform ApplySimpleTransform(this Transform transform, SimpleTransform sTransform)
		{
			switch (sTransform.Type)
			{
				case TransformType.World:
					transform.position = sTransform.Position;
					transform.rotation = sTransform.Rotation;
					transform.localScale = sTransform.Scale;
					break;
				case TransformType.Local:
					transform.localPosition = sTransform.Position;
					transform.localRotation = sTransform.Rotation;
					transform.localScale = sTransform.Scale;
					break;
			}

			return transform;
		}
	}
}