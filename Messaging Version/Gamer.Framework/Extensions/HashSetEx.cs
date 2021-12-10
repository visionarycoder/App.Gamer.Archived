using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Gamer.Framework.Extensions
{

	public static class HashSetEx
	{

		public static void AddRange<T>(this HashSet<T> target, ICollection<T> collection)
		{

			Contract.Assert(target != null, "Input target is null.");

			if (collection == null)
				return;

			foreach (var item in collection)
			{
				target.Add(item);
			}

		}

	}

}