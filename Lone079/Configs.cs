namespace Lone079
{
	class Configs
	{
		internal static bool countZombies;

		internal static int healthPercent;

		public static void ReloadConfigs()
		{
			countZombies = Plugin.Config.GetBool("l079_count_zombies", false);

			healthPercent = Plugin.Config.GetInt("l079_health_percentage", 50);
		}
	}
}
