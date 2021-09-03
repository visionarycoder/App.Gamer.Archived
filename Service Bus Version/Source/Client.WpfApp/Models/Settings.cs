namespace Gamer.Client.WpfApp.Models
{

	public class Settings
	{

		public bool IsExpanded { get; set; }

		public static Settings Load()
		{

			var settings = new Settings
			{
				IsExpanded = Properties.Settings.Default.IsExpanded
			};
			return settings;

		}

		public void Save()
		{

			Properties.Settings.Default.IsExpanded = IsExpanded;
			Properties.Settings.Default.Save();

		}

	}

}