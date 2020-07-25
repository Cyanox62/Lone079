using Exiled.API.Interfaces;

namespace Lone079
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;

		public bool CountZombies { get; set; } = false;
		public bool ScaleWithLevel { get; set; } = false;

		public int HealthPercent { get; set; } = 50;
	}
}
