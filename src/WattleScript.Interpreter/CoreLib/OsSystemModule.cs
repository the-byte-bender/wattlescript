﻿// Disable warnings about XML documentation
#pragma warning disable 1591

using System;

namespace WattleScript.Interpreter.CoreLib
{
	/// <summary>
	/// Class implementing system related Lua functions from the 'os' module.
	/// Proper support requires a compatible IPlatformAccessor
	/// </summary>
	[WattleScriptModule(Namespace = "os")]
	public class OsSystemModule
	{
		[WattleScriptModuleMethod]
		public static DynValue execute(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue v = args.AsType(0, "execute", DataType.String, true);

			if (v.IsNil())
			{
				return DynValue.NewBoolean(true);
			}
			else
			{
				try
				{
					int exitCode = Script.GlobalOptions.Platform.OS_Execute(v.String);

					return DynValue.NewTuple(
						DynValue.Nil,
						DynValue.NewString("exit"),
						DynValue.NewNumber(exitCode));
				}
				catch (Exception)
				{
					// +++ bad to swallow.. 
					return DynValue.Nil;
				}
			}
		}

		[WattleScriptModuleMethod]
		public static DynValue exit(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue v_exitCode = args.AsType(0, "exit", DataType.Number, true);
			int exitCode = 0;

			if (v_exitCode.IsNotNil())
				exitCode = (int)v_exitCode.Number;

			Script.GlobalOptions.Platform.OS_ExitFast(exitCode);

			throw new InvalidOperationException("Unreachable code.. reached.");
		}

		[WattleScriptModuleMethod]
		public static DynValue getenv(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue varName = args.AsType(0, "getenv", DataType.String, false);

			string val = Script.GlobalOptions.Platform.GetEnvironmentVariable(varName.String);

			if (val == null)
				return DynValue.Nil;
			else
				return DynValue.NewString(val);
		}

		[WattleScriptModuleMethod]
		public static DynValue remove(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string fileName = args.AsType(0, "remove", DataType.String, false).String;

			try
			{
				if (Script.GlobalOptions.Platform.OS_FileExists(fileName))
				{
					Script.GlobalOptions.Platform.OS_FileDelete(fileName);
					return DynValue.True;
				}
				else
				{
					return DynValue.NewTuple(
						DynValue.Nil,
						DynValue.NewString("{0}: No such file or directory.", fileName),
						DynValue.MinusOne);
				}
			}
			catch (Exception ex)
			{
				return DynValue.NewTuple(DynValue.Nil, DynValue.NewString(ex.Message), DynValue.MinusOne);
			}
		}

		[WattleScriptModuleMethod]
		public static DynValue rename(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			string fileNameOld = args.AsType(0, "rename", DataType.String, false).String;
			string fileNameNew = args.AsType(1, "rename", DataType.String, false).String;

			try
			{
				if (!Script.GlobalOptions.Platform.OS_FileExists(fileNameOld))
				{
					return DynValue.NewTuple(DynValue.Nil,
						DynValue.NewString("{0}: No such file or directory.", fileNameOld),
						DynValue.MinusOne);
				}

				Script.GlobalOptions.Platform.OS_FileMove(fileNameOld, fileNameNew);
				return DynValue.True;
			}
			catch (Exception ex)
			{
				return DynValue.NewTuple(DynValue.Nil, DynValue.NewString(ex.Message), DynValue.MinusOne);
			}
		}

		[WattleScriptModuleMethod]
		public static DynValue setlocale(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString("n/a");
		}

		[WattleScriptModuleMethod]
		public static DynValue tmpname(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(Script.GlobalOptions.Platform.IO_OS_GetTempFilename());
		}
	}
}
