using System.Data.Entity;

namespace Gamer.Framework.Helpers
{

	public static class DbContextHelper<T> where T : DbContext, new()
	{

		public static T GetContext(T defaultContext = null)
		{

			// This is for QA / DI
			if (defaultContext != null)
				return defaultContext;

			var context = new T();
			context.Configuration.LazyLoadingEnabled = false;
			context.Configuration.ProxyCreationEnabled = false;
			return context;

		}

	}

}