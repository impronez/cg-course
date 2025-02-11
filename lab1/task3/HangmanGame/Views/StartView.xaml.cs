using System.Windows;
using System.Windows.Controls;

namespace HangmanGame.Views
{
	public partial class StartView : UserControl
	{
		public StartView()
		{
			InitializeComponent();	
		}

		private void Play_Button_Click(object sender, RoutedEventArgs e)
		{
			if (Application.Current.MainWindow is MainWindow mainWindow)
			{
				mainWindow.MainContent.Content = new GameView();
			}
		}
	}
}
