using AlchemyGame.Utilities;

namespace AlchemyGame.Models
{
	public class Element
	{
		public ElementType Type { get; private set; }

		public int Id { get; private set; }
		
		public Element(ElementType type)
		{
			Id = ElementIdGenerator.GetNextId();
			Type = type;
		}
	}
}
