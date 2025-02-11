using HangmanGame.Models;
using HangmanGame.Utilities;
using HangmanGame.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HangmanGame.ViewModels
{
	public class GameViewModel : INotifyPropertyChanged
	{
		public static string ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

		private readonly GameModel _gameModel;

		public ICommand LetterCommand { get; }
		public ObservableCollection<string> Alphabet { get; }

		public Dictionary<char, bool> LetterStates;

		public string Word => _gameModel.Word;
		public ObservableCollection<char> WordLetters { get; }
		public StringBuilder DisplayedWord => _gameModel.DisplayedWord;
		public int Attempts => _gameModel.Attempts;
		public GameState State => _gameModel.State;
		public string Description => _gameModel.Description;
		public ObservableCollection<Visibility> VisibleParts { get; set; }

		public GameViewModel()
		{
			_gameModel = new GameModel();
			Alphabet = new ObservableCollection<string>(ALPHABET.Select(c => c.ToString()));
			WordLetters = new ObservableCollection<char>();
			LetterCommand = new RelayCommand<string>(GuessLetter, CanGuessLetter);
			LetterStates = new Dictionary<char, bool>();
			VisibleParts = new ObservableCollection<Visibility>(new Visibility[7]);

			_gameModel.PropertyChanged += (s, e) =>
			{
				OnPropertyChanged(e.PropertyName);

				if (e.PropertyName == nameof(State))
				{
					OnStateChanged();
				}

				if (e.PropertyName == nameof(Attempts))
				{
					UpdateVisibleParts();
				}
			};

			StartNewGame();
		}

		public void StartNewGame()
		{
			_gameModel.StartNewGame();
			UpdateWordLetters();
			LetterStates.Clear();
			OnPropertyChanged(nameof(DisplayedWord));
			OnPropertyChanged(nameof(Attempts));
			OnPropertyChanged(nameof(State));
			OnPropertyChanged(nameof(Description));
			OnPropertyChanged(nameof(WordLetters));
		}

		public bool IsLetterUsed(string letter)
		{
			return _gameModel._usedLetters.Contains(letter[0]);
		}

		private void UpdateVisibleParts()
		{
			for (int i = 0; i < 7; i++)
			{
				if (i < 7 - Attempts)
					VisibleParts[i] = Visibility.Visible;
				else
					VisibleParts[i] = Visibility.Collapsed;
			}

			OnPropertyChanged(nameof(VisibleParts));
		}

		private void OnStateChanged()
		{
			OnPropertyChanged(nameof(WordLetters));

			if (State != GameState.Playing)
			{
				ShowEndGameWindow();
			}
		}

		private void UpdateWordLetters()
		{
			WordLetters.Clear();
			foreach (var letter in _gameModel.DisplayedWord.ToString())
			{
				WordLetters.Add(letter);
			}
			OnPropertyChanged(nameof(WordLetters));
		}

		private void GuessLetter(string letter)
		{
			if (_gameModel.GuessLetter(letter[0]))
			{
				LetterStates[letter[0]] = true;
				UpdateWordLetters();
			}
			else
			{
				LetterStates[letter[0]] = false;
			}
			OnPropertyChanged(nameof(Attempts));
			OnPropertyChanged(nameof(State));
			OnPropertyChanged(nameof(LetterStates));
		}

		private async void ShowEndGameWindow()
		{
			string message = GetMessageFromState();

			await Task.Delay(1000);

			Application.Current.Dispatcher.Invoke(() =>
			{
				var mainWindow = (MainWindow)Application.Current.MainWindow;
				var gameView = mainWindow.MainContent.Content as GameView;
				mainWindow.MainContent.Content = new EndGameView(message, gameView);
			});
		}

		private string GetMessageFromState()
		{
			return State == GameState.Won ? "Вы выиграли!"
				: State == GameState.End ? "Игра окончена. При рестарте, игра начнется по новому кругу"
				: $"Вы проиграли.";
		}

		private bool CanGuessLetter(string letter)
		{
			return !string.IsNullOrEmpty(letter) && letter.Length == 1 && State == GameState.Playing && !IsLetterUsed(letter);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}