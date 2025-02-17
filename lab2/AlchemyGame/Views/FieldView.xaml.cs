using AlchemyGame.Models;
using AlchemyGame.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlchemyGame.Views
{
	public partial class FieldView : UserControl
	{
		public FieldView()
		{
			InitializeComponent();
		}

		private bool _isDragging = false;
		private bool _isRemoving = false;
		private Point _startPoint;
		private VisualElement _draggedElement;
		private double _offsetX, _offsetY;

		private void ItemMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (sender is Image image && image.DataContext is VisualElement element)
			{
				_isDragging = true;
				_draggedElement = element;

				var canvas = (Canvas)CurrentElementsControl.FindName("FieldCanvas");
				_startPoint = e.GetPosition(canvas);

				Point elementPosition = e.GetPosition(image);
				_offsetX = elementPosition.X;
				_offsetY = elementPosition.Y;

				_draggedElement.ZIndex = 1;

				e.Handled = true;
			}
		}

		private void CanvasMouseMove(object sender, MouseEventArgs e)
		{
			var canvas = sender as Canvas;
			if (canvas == null) return;

			Point currentPosition = e.GetPosition(canvas);

			if (_isDragging && _draggedElement != null)
			{
				double canvasWidth = canvas.ActualWidth;
				double canvasHeight = canvas.ActualHeight;

				bool isCursorInsideCanvas = currentPosition.X >= 0 && currentPosition.X <= canvasWidth &&
											currentPosition.Y >= 0 && currentPosition.Y <= canvasHeight;

				if (isCursorInsideCanvas)
				{
					double newLeft = Math.Max(0, Math.Min(currentPosition.X - _offsetX, canvasWidth - VisualElement.Width));
					double newTop = Math.Max(0, Math.Min(currentPosition.Y - _offsetY, canvasHeight - VisualElement.Height));

					_draggedElement.Left = newLeft;
					_draggedElement.Top = newTop;
				}
			}

			if (_isDragging && !canvas.IsMouseCaptured)
			{
				canvas.CaptureMouse();
			}
		}

		private void CanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (_isDragging)
			{
				var positionRelativeToGarbage = e.GetPosition(Garbage);
				var vm = DataContext as GameViewModel;

				_draggedElement.ZIndex = 0;

				if (positionRelativeToGarbage.X >= 0 && positionRelativeToGarbage.X <= Garbage.ActualWidth &&
					positionRelativeToGarbage.Y >= 0 && positionRelativeToGarbage.Y <= Garbage.ActualHeight)
				{
					vm.RemoveElement(_draggedElement);
				}
				else
				{
					vm.CheckCollision(_draggedElement);
				}

				var canvas = sender as Canvas;

				_isDragging = false;
				_draggedElement = null;

				canvas.ReleaseMouseCapture();
			}
		}

		private void CanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			_isDragging = false;
			_draggedElement = null;
		}
	}
}
