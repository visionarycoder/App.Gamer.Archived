using System;
using System.Diagnostics;
using System.Reflection;

namespace Gamer.Framework
{

	public static class ReflectionHelper
	{

		public static string NameOfCallingClass()
		{
			string fullName;
			Type declaringType;
			int skipFrames = 2;
			do
			{
				MethodBase method = new StackFrame(skipFrames, false).GetMethod();
				declaringType = method.DeclaringType;
				if (declaringType == null)
				{
					return method.Name;
				}
				skipFrames++;
				fullName = declaringType.FullName;
			}
			while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

			return fullName;

		}

		public static Type TypeOfCallingClass()
		{

			return new StackFrame(2).GetMethod()?.ReflectedType;

		}
	}

}
