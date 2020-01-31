// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DraggableAttribute.cs" company="Supyrb">
//   Copyright (c) 2019 Supyrb. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Supyrb.Assetizer
{
	/// <summary>
	/// <para>
	/// <conceptualLink target="Manual_Supyrb_Assetizer_DragableAttribute">Jump to Manual</conceptualLink>
	/// </para>
	/// The draggable attribute is a powerful property drawer for <see cref="ScriptableObject"/> and its subclasses.
	/// The drawer adds a draggable area on the left in order to easily assign the scriptableObject to another field 
	/// (as if you would drag it from the project view).
	/// It also adds an arrow on the right to jump to show the scriptable object in the inspector without the need to search for it in the project view.
	/// If the field is empty, you can right click on the property and create a ScriptableObject of that class that will be stored in the same folder as its parent.
	/// If the parent is a <see cref="MonoBehaviour"/>, the asset will be stored in the root folder.
	/// 
	/// <example>
	/// <code language="cs">
	/// [Draggable]
	/// public ScriptableObject DraggableObject;
	/// </code>
	/// </example>
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
	public class DraggableAttribute : PropertyAttribute
	{
		/// <summary>
		/// Open the asset in a new window or in the inspector when clicking on the goto arrow
		/// </summary>
		public readonly bool OpenInNewWindow;

		/// <summary>
		/// Add a dragable icon at front and a goto arrow at the back of a ScriptableObject reference.
		/// </summary>
		/// <param name="openInNewWindow">If the asset should be opened in a new window when clicking on the goto arrow</param>
		public DraggableAttribute(bool openInNewWindow = false)
		{
			OpenInNewWindow = openInNewWindow;
		}
	}
}