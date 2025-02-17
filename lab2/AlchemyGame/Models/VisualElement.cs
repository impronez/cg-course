using System.ComponentModel;

namespace AlchemyGame.Models
{
	public class VisualElement : INotifyPropertyChanged
	{
		private Element _element;

		public int Id => _element.Id;

		private int _zIndex;
		public int ZIndex
		{
			get => _zIndex;
			set
			{
				if (_zIndex != value)
				{
					_zIndex = value;
					OnPropertyChanged(nameof(ZIndex));
				}
			}
		}

		public ElementType Type => _element.Type;

		public const double Width = 50;
		public const double Height = 50;

		private double _left;
		private double _top;
		public double Left
		{
			get => _left;
			set
			{
				if (_left != value)
				{
					_left = value;
					OnPropertyChanged(nameof(Left));
				}
			}
		}

		public double Top
		{
			get => _top;
			set
			{
				if (_top != value)
				{
					_top = value;
					OnPropertyChanged(nameof(Top));
				}
			}
		}

		public VisualElement(Element element)
		{
			ZIndex = 0;
			_element = element;
			_left = 150.0;
			_top = 150.0;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
