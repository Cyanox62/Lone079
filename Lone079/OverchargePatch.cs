using HarmonyLib;

namespace Lone079
{
	[HarmonyPatch(typeof(Recontainer079), nameof(Recontainer079.BeginOvercharge))]
	class OverchargePatch1
	{
		public static bool Prefix() => false;
	}

	[HarmonyPatch(typeof(Recontainer079), nameof(Recontainer079.Recontain))]
	class OverchargePatch2
	{
		public static bool Prefix() => false;
	}
}
