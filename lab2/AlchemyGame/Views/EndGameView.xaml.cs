using System.Windows;
using System.Windows.Controls;

namespace AlchemyGame.Views
{
	public partial class EndGameView : UserControl
	{
		public EndGameView()
		{
			InitializeComponent();
		}

		private void End_Game_Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
