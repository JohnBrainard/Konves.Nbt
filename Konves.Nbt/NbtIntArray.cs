﻿/*
  Copyright 2014 Steve Konves

	Licensed under the Apache License, Version 2.0 (the "License");
	you may not use this file except in compliance with the License.
	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License. 
*/

namespace Konves.Nbt
{
	/// <summary>
	/// Represents a named binary tag whose value is an array of 32-bit signed integer values.
	/// </summary>
	public sealed class NbtIntArray : NbtTag<int[]>
	{
		/// <summary>
		/// Initializes a new instance of an <see cref="NbtIntArray"/> tag.
		/// </summary>
		/// <param name="name">The tag name.</param>
		/// <param name="value">The value.</param>
		public NbtIntArray(string name, int[] value)
			: base(name, NbtTagType.IntArray, value) { }
	}
}
