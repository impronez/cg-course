using AlchemyGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlchemyGame.Models
{
	public enum GameState
	{
		Playing, End
	}
	public class GameModel
	{
		private const int MaxCurrentElements = 20;
		private static readonly int MaxElements = Enum.GetValues(typeof(ElementType)).Length;
		public GameState State { get; private set; }
		public List<ElementType> OpenElements { get; private set; }
		public List<Element> CurrentElements { get; private set; }

		public event EventHandler? OpenElementsChanged;
		public event EventHandler<ElementChangeEventArgs>? CurrentElementsChanged;
		public event EventHandler StateChanged;

		public GameModel()
		{
			OpenElements = new List<ElementType>();
			CurrentElements = new List<Element>();
		}

		public void Start()
		{
			State = GameState.Playing;
			InitializeElements();
		}

		public void AddElement(ElementType type, bool isNeedNotify = true)
		{
			if (State == GameState.End
					|| CurrentElements.Count >= MaxCurrentElements
					|| !OpenElements.Contains(type))
				return;

			var newElement = new Element(type);

			CurrentElements.Add(newElement);

			if (isNeedNotify)
			{
				CurrentElementsChanged?.Invoke(this, new ElementChangeEventArgs(newElement.Id, ElementChangeType.Add));
			}
		}

		public void RemoveElement(int id)
		{
			var element = CurrentElements.FirstOrDefault(e => e.Id == id);

			if (!CurrentElements.Contains(element))
				return;

			CurrentElements.Remove(element);

			CurrentElementsChanged?.Invoke(this, new ElementChangeEventArgs(element.Id, ElementChangeType.Remove));
		}

		public bool TryCombineElements(int id1, int id2)
		{
			var element1 = CurrentElements.FirstOrDefault(e => e.Id == id1);
			var element2 = CurrentElements.FirstOrDefault(e => e.Id == id2);

			if (!CurrentElements.Contains(element1) && !CurrentElements.Contains(element2))
				return false;

			ElementType? result = ElementCombinations.GetCombinationResult(element1.Type, element2.Type);

			if (result is null)
				return false;

			if (!OpenElements.Contains(result.Value))
			{ 
				OpenElements.Add(result.Value);
				OpenElementsChanged?.Invoke(this, EventArgs.Empty);
			}

			RemoveElement(id1);
			RemoveElement(id2);

			AddElement(result.Value);

			UpdateState();

			return true;
		}

		public void SortOpenElements()
		{
			OpenElements.Sort();
			OpenElementsChanged?.Invoke(this, EventArgs.Empty);
		}

		private void UpdateState()
		{
			if (State == GameState.Playing && OpenElements.Count == MaxElements)
			{
				State = GameState.End;

				StateChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void InitializeElements()
		{
			OpenElements.Clear();
			CurrentElements.Clear();

			var initialElements = new[] { ElementType.Air, ElementType.Earth, ElementType.Fire, ElementType.Water };
			foreach (var element in initialElements)
			{
				OpenElements.Add(element);
				CurrentElements.Add(new Element(element));
			}

			OpenElementsChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
