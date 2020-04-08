namespace Lone079
{
	class Configs
	{
		internal static bool countZombies;
		internal static bool scaleWithLevel;

		internal static int healthPercent;

		public static void ReloadConfigs()
		{
			countZombies = Plugin.Config.GetBool("l079_count_zombies", false);
			scaleWithLevel = Plugin.Config.GetBool("l079_scale_with_level", false);

			healthPercent = Plugin.Config.GetInt("l079_health_percentage", 50);
		}
	}
}
