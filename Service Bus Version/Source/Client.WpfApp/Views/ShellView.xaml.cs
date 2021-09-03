using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Gamer.Client.WpfApp.ViewModels;

namespace Gamer.Client.WpfApp.Views
{

	[Export]
	public partial class ShellView
	{

		[Import]
		public ShellViewModel ViewModel
		{
			get { return DataContext as ShellViewModel; }
			set { DataContext = value; }
		}

		[ImportingConstructor]
		public ShellView()
		{

			InitializeComponent();

			GameBoardPanel.Visibility = Visibility.Collapsed;
			StartGamePanel.Visibility = Visibility.Visible;

		}

		private void Tile_OnClick(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			if (button == null)
				return;
			var address = button.Name;
			ViewModel.PlayCommand.Execute(address);
		}

		private void StartGame_OnClick(object sender, RoutedEventArgs e)
		{

			GameBoardPanel.Visibility = Visibility.Visible;
			StartGamePanel.Visibility = Visibility.Collapsed;

			ViewModel.StartGameCommand.Execute();

		}

		private void EndGame_OnClick(object sender, RoutedEventArgs e)
		{

			GameBoardPanel.Visibility = Visibility.Collapsed;
			StartGamePanel.Visibility = Visibility.Visible;

			ViewModel.EndGameCommand.Execute();

		}

	}

}