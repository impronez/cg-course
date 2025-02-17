using System.Windows;

namespace AlchemyGame.Views
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
