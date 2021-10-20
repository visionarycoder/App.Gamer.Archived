using System.Collections.Generic;

namespace Gamer.Framework.Helpers
{

	public static class HashSetEx
	{

		public static void AddRange<T>(this HashSet<T> target, ICollection<T> collection)
		{

			if (collection == null)
				return;
			foreach (var item in collection)
			{
				target.Add(item);
			}

		}

	}

}