using HangmanGame.Views;
using System.Windows;

namespace HangmanGame
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MainContent.Content = new StartView();
		}
	}
}
