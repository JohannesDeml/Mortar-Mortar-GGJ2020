// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShaderParameter.cs" company="Supyrb">
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
	public class ShaderParameter
	{
		[SerializeField]
		private string name;

		private int hash = -1;
		private bool initialized;

		public string Name => name;

		public int Hash
		{
			get
			{
				if (!initialized)
				{
					hash = Shader.PropertyToID(name);
					initialized = true;
				}

				return hash;
			}
		}

		public ShaderParameter(string name)
		{
			this.name = name;
		}
	}
}