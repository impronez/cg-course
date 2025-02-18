using AlchemyGame.Models;
using AlchemyGame.Utilities;
using AlchemyGame.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AlchemyGame.ViewModels
{
	public class GameViewModel : INotifyPropertyChanged
	{
		private readonly GameModel _gameModel;
		public GameState State => _gameModel.State;
		// перенести элемент виз во viewmodel
		public ObservableCollection<ElementType> OpenElements { get; }
		public ObservableCollection<VisualElement> CurrentElements { get; }

		public GameViewModel() 
		{
			_gameModel = new GameModel();

			OpenElements = new();
			CurrentElements = new();

			_gameModel.OpenElementsChanged += (s, e) => SyncOpenElements();
			_gameModel.CurrentElementsChanged += (s, e) => SyncCurrentElements(e);
			_gameModel.StateChanged += (s, e) => OnStateChanged();

			Start();
		}

		public void Start()
		{
			_gameModel.Start();

			SyncOpenElements();
			SyncCurrentElements();

			InitElementsInitializePosition();
		}

		public void CheckCollision(VisualElement elem1)
		{
			foreach (var elem2 in CurrentElements)
			{
				if (elem1.Equals(elem2))
					continue;

				double leftX = Math.Max(elem1.Left, elem2.Left);
				double rightX = Math.Min(elem1.Left + VisualElement.Width, elem2.Left + VisualElement.Width);
				double topY = Math.Max(elem1.Top, elem2.Top);
				double bottomY = Math.Min(elem1.Top + VisualElement.Height, elem2.Top + VisualElement.Height);

				if (leftX < rightX && topY < bottomY && TryCombineElements(elem1, elem2))
				{
					break;
				}
			}
		}

		public bool TryCombineElements(VisualElement element1, VisualElement element2)
		{
			bool isSuccess = _gameModel.TryCombineElements(element1.Id, element2.Id);
			if (isSuccess)
			{
				SetPositionOfLastElement(new Point(element2.Left, element2.Top));
			}

			return isSuccess;
		}

		public void AddElement(Point position, ElementType type)
		{
			_gameModel.AddElement(type);

			SetPositionOfLastElement(position);
		}

		public void RemoveElement(VisualElement element)
		{
			_gameModel.RemoveElement(element.Id);
		}

		public void SortOpenElements()
		{
			_gameModel.SortOpenElements();
		}

		public void CheckCollisionLastElement()
		{
			if (CurrentElements.Count > 0)
			{
				CheckCollision(CurrentElements.Last());
			}
		}

		private async void ShowEndGameWindow()
		{
			await Task.Delay(1000);

			Application.Current.Dispatcher.Invoke(() =>
			{
				var mainWindow = (MainWindow)Application.Current.MainWindow;
				var gameView = mainWindow.MainContent.Content as GameView;
				mainWindow.MainContent.Content = new EndGameView();
			});
		}

		private void SetPositionOfLastElement(Point position)
		{
			CurrentElements.Last().Left = position.X;
			CurrentElements.Last().Top = position.Y;
		}

		private void OnStateChanged()
		{
			ShowEndGameWindow();
		}

		private void SyncOpenElements()
		{
			OpenElements.Clear();
			foreach (var elem in _gameModel.OpenElements)
			{
				OpenElements.Add(elem);
			}

			OnPropertyChanged(nameof(OpenElements));
		}

		private void InitElementsInitializePosition()
		{
			double x = 100;

			foreach (var elem in CurrentElements)
			{
				elem.Left = x;
				elem.Top = 100;

				x += 100;
			}
		}

		private void SyncCurrentElements(ElementChangeEventArgs? args = null)
		{
			if (args == null)
			{
				CurrentElements.Clear();
				foreach (var elem in _gameModel.CurrentElements)
				{
					CurrentElements.Add(new VisualElement(elem));
				}
			}
			else if (args.ChangeType == ElementChangeType.Add)
				CurrentElements.Add(
					new VisualElement(
						_gameModel.CurrentElements.FirstOrDefault(elem => elem.Id == args.ChangedElementId)
					)
				);
			else if (args.ChangeType == ElementChangeType.Remove)
				CurrentElements.Remove(
					CurrentElements.FirstOrDefault(elem => elem.Id == args.ChangedElementId)
				);

			OnPropertyChanged(nameof(CurrentElements));
		}



		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
