using AlchemyGame.Models;
using AlchemyGame.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AlchemyGame.Views
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

		private bool _isAdding = false;
		private UIElement _addedElement;
		private Point _offset;
		private void AddElementMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is Image image && image.DataContext is ElementType type)
			{
				var canvas = MainCanvas;
				if (canvas == null)
					return;

				_isAdding = true;

				var mousePosRelativeToImage = e.GetPosition(image);
				var mousePosRelativeToCanvas = e.GetPosition(canvas);

				var newElement = new Image
				{
					Width = VisualElement.Width,
					Height = VisualElement.Height,
					Source = image.Source,
					Tag = type
				};

				Canvas.SetLeft(newElement, mousePosRelativeToCanvas.X - mousePosRelativeToImage.X);
				Canvas.SetTop(newElement, mousePosRelativeToCanvas.Y - mousePosRelativeToImage.Y);

				canvas.Children.Add(newElement);
				_addedElement = newElement;

				_offset = new Point(
					mousePosRelativeToCanvas.X - Canvas.GetLeft(newElement),
					mousePosRelativeToCanvas.Y - Canvas.GetTop(newElement));

				e.Handled = true;
			}
		}

		private void AddingElementOnMouseMove(object sender, MouseEventArgs e)
		{
			if (!_isAdding || _addedElement == null)
				return;

			var canvas = MainCanvas;
			if (canvas == null)
				return;

			var posRelativeToImage = e.GetPosition(_addedElement);
			var currentPos = e.GetPosition(canvas);

			double canvasWidth = canvas.ActualWidth;
			double canvasHeight = canvas.ActualHeight;

			bool isCursorInsideCanvas = currentPos.X >= 0 && currentPos.X + posRelativeToImage.X <= canvasWidth &&
										currentPos.Y >= 0 && currentPos.Y + posRelativeToImage.Y <= canvasHeight;

			if (isCursorInsideCanvas)
			{
				double newLeft = Math.Max(0, Math.Min(currentPos.X - _offset.X, canvasWidth - VisualElement.Width));
				double newTop = Math.Max(0, Math.Min(currentPos.Y - _offset.Y, canvasHeight - VisualElement.Height));

				Canvas.SetLeft(_addedElement, newLeft);
				Canvas.SetTop(_addedElement, newTop);
			}
		}

		private void AddingElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			MainCanvas.Children.Remove(_addedElement);

			var fieldCanvas = FindChildByName<Canvas>(this, "FieldCanvas");

			var garbage = FindChildByName<Image>(this, "Garbage");
			var posRelativeToGarbage = e.GetPosition(garbage);
			bool isInGarbage = posRelativeToGarbage.X >= 0 && posRelativeToGarbage.X <= garbage.ActualWidth &&
						posRelativeToGarbage.Y >= 0 && posRelativeToGarbage.Y <= garbage.ActualHeight;


			if (fieldCanvas != null && _addedElement is Image addedImage && addedImage.Tag is ElementType type)
			{
				var mousePosRelativeToCanvas = e.GetPosition(fieldCanvas);

				var currentPos = new Point(mousePosRelativeToCanvas.X - _offset.X,
					mousePosRelativeToCanvas.Y - _offset.Y);

				double canvasWidth = fieldCanvas.ActualWidth;
				double canvasHeight = fieldCanvas.ActualHeight;

				if (!isInGarbage && currentPos.X >= 0 && currentPos.X <= canvasWidth
					&& currentPos.Y >= 0 && currentPos.Y <= canvasHeight)
				{
					var vm = DataContext as GameViewModel;
                    vm.AddElement(currentPos, type);
					vm.CheckCollisionLastElement();
				}
			}

			_isAdding = false;
			_addedElement = null;
		}

		private void SortButton_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as GameViewModel;
			vm.SortOpenElements();
		}

		private T FindChildByName<T>(DependencyObject parent, string name) where T : FrameworkElement
		{
			if (parent == null)
				return null;

			int count = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < count; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);

				if (child is T frameworkElement && frameworkElement.Name == name)
					return frameworkElement;

				var result = FindChildByName<T>(child, name);
				if (result != null)
					return result;
			}
			return null;
		}
	}
}
