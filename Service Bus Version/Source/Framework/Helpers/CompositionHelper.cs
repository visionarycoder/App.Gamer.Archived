using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;

namespace Gamer.Framework.Helpers
{

	public static class CompositionHelper
	{

		/// <summary>
		/// Compose all available exported objects
		/// The catalog is based upon the environments current directory
		/// </summary>
		/// <returns>composed composition container</returns>

		public static CompositionContainer Compose()
		{

			var catalogs = new AggregateCatalog();

			var assemblyCatalog = new AssemblyCatalog(Assembly.GetEntryAssembly());
			catalogs.Catalogs.Add(assemblyCatalog);

			var dictionaryCatalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
			catalogs.Catalogs.Add(dictionaryCatalog);

			var container = new CompositionContainer(catalogs);
			container.ComposeParts();

			return container;

		}

		/// <summary>
		/// Compose all available exported objects
		/// The catalog is based upon the composition mode
		/// </summary>
		/// <param name="compositionMode"></param>
		/// <param name="args"></param>
		/// <returns>composed composition container</returns>
		public static CompositionContainer Compose(CompositionMode compositionMode, params string[] args)
		{

			var catalogs = new AggregateCatalog();

			switch ( compositionMode )
			{
				case CompositionMode.LocalDirectory:
					{
						var dictionaryCatalog = new DirectoryCatalog(Environment.CurrentDirectory);
						catalogs.Catalogs.Add(dictionaryCatalog);
					}
					break;
				case CompositionMode.InputDirectories:
					{
						foreach ( var arg in args )
						{
							var dictionaryCatalog = new DirectoryCatalog(arg);
							catalogs.Catalogs.Add(dictionaryCatalog);
						}
					}
					break;
				case CompositionMode.InputAssemblyNames:
					{
						foreach ( var arg in args )
						{
							var assembly = Assembly.LoadFrom(arg);
							var assemblyCatalog = new AssemblyCatalog(assembly);
							catalogs.Catalogs.Add(assemblyCatalog);
						}
					}
					break;
				default:
					throw new ArgumentException();
			}

			foreach ( var catalog in catalogs.Catalogs )
			{
				foreach ( var part in catalog.Parts )
				{
					foreach ( var kvp in part.Metadata )
					{
						Trace.TraceInformation(kvp.Key);
					}
				}
			}
			var compositionContainer = new CompositionContainer(catalogs);
			compositionContainer.ComposeParts();
			return compositionContainer;

		}

		public enum CompositionMode
		{
			LocalDirectory = 1,
			InputDirectories = 2,
			InputAssemblyNames = 4,
		}

	}

}
