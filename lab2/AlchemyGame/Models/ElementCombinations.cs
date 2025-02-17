using System.Collections.Generic;

namespace AlchemyGame.Models
{
	public static class ElementCombinations
	{
		public static readonly Dictionary<(ElementType, ElementType), ElementType> CombinationResults = new()
		{
			{ (ElementType.Fire, ElementType.Water), ElementType.Steam },
			{ (ElementType.Fire, ElementType.Earth), ElementType.Lava },
			{ (ElementType.Water, ElementType.Air), ElementType.Cloud },
			{ (ElementType.Water, ElementType.Earth), ElementType.Mud },
			{ (ElementType.Earth, ElementType.Pressure), ElementType.Stone },

			{ (ElementType.Fire, ElementType.Gunpowder), ElementType.Explosion },
			{ (ElementType.Fire, ElementType.Air), ElementType.Energy },
			{ (ElementType.Fire, ElementType.Alcohol), ElementType.MolotovCoctail },
			{ (ElementType.Fire, ElementType.Metal), ElementType.Electricity },

			{ (ElementType.Water, ElementType.Pressure), ElementType.Geyser },
			{ (ElementType.Water, ElementType.Energy), ElementType.Life },
			{ (ElementType.Water, ElementType.Bacteria), ElementType.Life },
			{ (ElementType.Water, ElementType.Alcohol), ElementType.Vodka },

			{ (ElementType.Air, ElementType.Dust), ElementType.Storm },
			{ (ElementType.Air, ElementType.Energy), ElementType.Electricity },
			{ (ElementType.Air, ElementType.Ozone), ElementType.Cloud },

			{ (ElementType.Lava, ElementType.Water), ElementType.Stone },
			{ (ElementType.Mud, ElementType.Pressure), ElementType.Stone },
			{ (ElementType.Lava, ElementType.Pressure), ElementType.Volcano },

			{ (ElementType.Cloud, ElementType.Pressure), ElementType.Rain },
			{ (ElementType.Cloud, ElementType.Electricity), ElementType.Storm },

			{ (ElementType.Explosion, ElementType.Air), ElementType.Shockwave },
			{ (ElementType.Explosion, ElementType.Water), ElementType.Tsunami },
			{ (ElementType.Explosion, ElementType.Earth), ElementType.Crater },

			{ (ElementType.Oxygen, ElementType.Hydrogen), ElementType.Water },
			{ (ElementType.Metal, ElementType.Oxygen), ElementType.Rust },

			{ (ElementType.Swamp, ElementType.Bacteria), ElementType.RattlesnakeGas },
			{ (ElementType.Swamp, ElementType.Life), ElementType.Alcohol },
			{ (ElementType.Alcohol, ElementType.Fire), ElementType.MolotovCoctail },
			{ (ElementType.Vodka, ElementType.Bacteria), ElementType.Life },

			/*{ (ElementType.Metal, ElementType.Stone), ElementType.Machine },
			{ (ElementType.Machine, ElementType.Energy), ElementType.Robot },
			{ (ElementType.Robot, ElementType.Life), ElementType.Cyborg }*/
		};

		public static ElementType? GetCombinationResult(ElementType element1, ElementType element2)
		{
			if (CombinationResults.TryGetValue((element1, element2), out ElementType result) ||
				CombinationResults.TryGetValue((element2, element1), out result))
			{
				return result;
			}

			return null;
		}
	}
}
