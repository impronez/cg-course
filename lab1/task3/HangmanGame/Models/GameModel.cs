using HangmanGame.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace HangmanGame.Models
{
	public enum GameState
	{
		Playing, Won, Lost, End
	}

	public class GameModel : INotifyPropertyChanged
	{
		private const int MaxAttempts = 7;
		private readonly WordsDictionary _dictionary;
		public HashSet<char> _usedLetters;
		public string Word { get; private set; }
		public string Description { get; private set; }

		private StringBuilder _displayedWord;
		public StringBuilder DisplayedWord
		{
			get => _displayedWord;
			private set
			{
				_displayedWord = value;
				OnPropertyChanged(nameof(DisplayedWord));
			}
		}

		private GameState _state;
		public GameState State
		{
			get => _state;
			private set
			{
				_state = value;
				OnPropertyChanged(nameof(State));
			}
		}

		private int _attempts;
		public int Attempts
		{
			get => _attempts;
			private set
			{
				_attempts = value;
				OnPropertyChanged(nameof(Attempts));
			}
		}

		public GameModel()
		{
			_dictionary = new WordsDictionary();
			DisplayedWord = new StringBuilder();
			_usedLetters = new HashSet<char>();
			State = GameState.Playing;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void StartNewGame()
		{
			var pair = _dictionary.GetRandomWord();

			Word = pair.Key.ToUpper();
			Description = pair.Value;

			DisplayedWord.Clear();
			DisplayedWord.Append('_', Word.Length);

			_usedLetters.Clear();
			Attempts = MaxAttempts;
			State = GameState.Playing;

			OnPropertyChanged(nameof(Word));
			OnPropertyChanged(nameof(Description));
			OnPropertyChanged(nameof(DisplayedWord));
			OnPropertyChanged(nameof(Attempts));
			OnPropertyChanged(nameof(State));
		}

		public bool GuessLetter(char letter)
		{
			if (State != GameState.Playing || _usedLetters.Contains(letter))
				return false;

			letter = char.ToUpper(letter);
			_usedLetters.Add(letter);

			if (Word.Contains(letter))
			{
				OpenLetter(letter);

				if (IsWin())
				{
					_dictionary.MarkWordAsUsed();

					if (IsGameCompleted())
					{
						State = GameState.End;
					}
                    else
                    {
						State = GameState.Won;
					}
				}

				return true;
			}
			else
			{
				Attempts--;

				if (IsLoss())
				{
					State = GameState.Lost;
				}

				return false;
			}
		}

		private bool IsWin()
		{
			return !DisplayedWord.ToString().Contains('_') && Attempts > 0;
		}

		private bool IsLoss()
		{
			return Attempts == 0;
		}

		private bool IsGameCompleted()
		{
			return _dictionary.IsAllWordsUsed();
		}

		private void OpenLetter(char letter)
		{
			for (int i = 0; i < Word.Length; i++)
			{
				if (Word[i] == letter)
				{
					DisplayedWord[i] = letter;
				}
			}
			OnPropertyChanged(nameof(DisplayedWord));
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}