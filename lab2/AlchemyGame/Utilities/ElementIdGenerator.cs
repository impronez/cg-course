namespace AlchemyGame.Utilities
{
	public static class ElementIdGenerator
	{
		private static int _nextId =0;

		public static int GetNextId()
		{
			return _nextId++;
		}

		public static void Reset()
		{
			_nextId = 0;
		}
	}
}
