namespace AlchemyGame.Utilities
{
	public enum ElementChangeType
	{
		Add, Remove
	}
	public class ElementChangeEventArgs
	{
		public int ChangedElementId { get; }

		public ElementChangeType ChangeType { get; }

		public ElementChangeEventArgs(int changedElementId, ElementChangeType type)
		{
			ChangedElementId = changedElementId;
			ChangeType = type;
		}
	}
}
