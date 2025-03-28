using System;

namespace WattleScript.Interpreter.Interop
{
	/// <summary>
	/// Specifies the default visibility of a class's members.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class WattleScriptDefaultVisibilityAttribute : Attribute
	{
		/// <summary>
		/// Gets a value indicating whether the class members are visible by default.
		/// </summary>
		public bool IsVisibleByDefault { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WattleScriptDefaultVisibilityAttribute"/> class.
		/// </summary>
		/// <param name="isVisibleByDefault">if set to <c>true</c> the class members are visible by default; otherwise, <c>false</c>.</param>
		public WattleScriptDefaultVisibilityAttribute(bool isVisibleByDefault)
		{
			IsVisibleByDefault = isVisibleByDefault;
		}
	}
}