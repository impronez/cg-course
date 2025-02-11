using HangmanGame.ViewModels;
using System.Windows.Controls;

namespace HangmanGame.Views
{
	public partial class GameView : UserControl
	{
		public GameView()
		{
			InitializeComponent();

			if (DataContext == null)
			{
				DataContext = new GameViewModel();
			}
		}
	}
}
