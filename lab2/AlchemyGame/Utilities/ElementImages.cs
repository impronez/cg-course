using AlchemyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlchemyGame.Utilities
{
	public static class ElementImages
	{
		private static readonly Dictionary<ElementType, string> _imagePaths = new()
		{
			{ ElementType.Fire, "pack://application:,,,/Resources/Images/Fire.png" },
			{ ElementType.Earth, "pack://application:,,,/Resources/Images/Earth.png" },
			{ ElementType.Water, "pack://application:,,,/Resources/Images/Water.png" },
			{ ElementType.Air, "pack://application:,,,/Resources/Images/Air.png" },
			{ ElementType.Alcohol, "pack://application:,,,/Resources/Images/Alcohol.png" },
			{ ElementType.Bacteria, "pack://application:,,,/Resources/Images/Bacteria.png" },
			{ ElementType.Cloud, "pack://application:,,,/Resources/Images/Cloud.png" },
			{ ElementType.Crater, "pack://application:,,,/Resources/Images/Crater.png" },
			{ ElementType.Dust, "pack://application:,,,/Resources/Images/Dust.png" },
			{ ElementType.Electricity, "pack://application:,,,/Resources/Images/Electricity.png" },
			{ ElementType.Energy, "pack://application:,,,/Resources/Images/Energy.png" },
			{ ElementType.Explosion, "pack://application:,,,/Resources/Images/Explosion.png" },
			{ ElementType.Geyser, "pack://application:,,,/Resources/Images/Geyser.png" },
			{ ElementType.Gunpowder, "pack://application:,,,/Resources/Images/Gunpowder.png" },
			{ ElementType.Hydrogen, "pack://application:,,,/Resources/Images/Hydrogen.png" },
			{ ElementType.Lava, "pack://application:,,,/Resources/Images/Lava.png" },
			{ ElementType.Life, "pack://application:,,,/Resources/Images/Life.png" },
			{ ElementType.Metal, "pack://application:,,,/Resources/Images/Metal.png" },
			{ ElementType.MolotovCoctail, "pack://application:,,,/Resources/Images/MolotovCoctail.png" },
			{ ElementType.Mud, "pack://application:,,,/Resources/Images/Mud.png" },
			{ ElementType.Oxygen, "pack://application:,,,/Resources/Images/Oxygen.png" },
			{ ElementType.Ozone, "pack://application:,,,/Resources/Images/Ozone.png" },
			{ ElementType.Pressure, "pack://application:,,,/Resources/Images/Pressure.png" },
			{ ElementType.Rain, "pack://application:,,,/Resources/Images/Rain.png" },
			{ ElementType.RattlesnakeGas, "pack://application:,,,/Resources/Images/RattlesnakeGas.png" },
			{ ElementType.Rust, "pack://application:,,,/Resources/Images/Rust.png" },
			{ ElementType.Shockwave, "pack://application:,,,/Resources/Images/Shockwave.png" },
			{ ElementType.Steam, "pack://application:,,,/Resources/Images/Steam.png" },
			{ ElementType.SteamBoiler, "pack://application:,,,/Resources/Images/SteamBoiler.png" },
			{ ElementType.Stone, "pack://application:,,,/Resources/Images/Stone.png" },
			{ ElementType.Storm, "pack://application:,,,/Resources/Images/Storm.png" },
			{ ElementType.Swamp, "pack://application:,,,/Resources/Images/Swamp.png" },
			{ ElementType.Tsunami, "pack://application:,,,/Resources/Images/Tsunami.png" },
			{ ElementType.Vodka, "pack://application:,,,/Resources/Images/Vodka.png" },
			{ ElementType.Volcano, "pack://application:,,,/Resources/Images/Volcano.png" },
		};


		public static string GetImagePath(ElementType elementType)
		{
			return _imagePaths.TryGetValue(elementType, out var path) ? path : "pack://application:,,,/Resources/Images/None.png";
		}
	}
}
