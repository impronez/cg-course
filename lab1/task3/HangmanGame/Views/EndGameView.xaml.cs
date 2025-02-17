using HangmanGame.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HangmanGame.Views
{
	public partial class EndGameView : UserControl
	{
		public static readonly DependencyProperty MessageProperty =
			DependencyProperty.Register("Message", typeof(string), typeof(EndGameView), new PropertyMetadata(""));

		private readonly GameView _gameView;

		public string Message
		{
			get { return (string)GetValue(MessageProperty); }
			set { SetValue(MessageProperty, value); }
		}

		public EndGameView(string message, GameView gameView)
		{
			Message = message;
			_gameView = gameView;
			InitializeComponent();
			DataContext = this;
		}

		private void Restart_Button_Click(object sender, RoutedEventArgs e)
		{
			var gameViewModel = (GameViewModel)_gameView.DataContext;
			gameViewModel.StartNewGame();
			var mainWindow = (MainWindow)Application.Current.MainWindow;
			mainWindow.MainContent.Content = _gameView;
		}

		private void EndGame_Button_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
