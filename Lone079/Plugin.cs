using EXILED;
using Harmony;

namespace Lone079
{
	public class Plugin : EXILED.Plugin
	{
		private EventHandlers ev;

		public static HarmonyInstance harmonyInstance { private set; get; }
		public static int harmonyCounter;

		public override void OnEnable()
		{
			harmonyCounter++;
			harmonyInstance = HarmonyInstance.Create($"cyanox.lone079{harmonyCounter}");
			harmonyInstance.PatchAll();

			ev = new EventHandlers();

			Events.WaitingForPlayersEvent += ev.OnWaitingForPlayers;
			Events.RoundStartEvent += ev.OnRoundStart;
			Events.PlayerDeathEvent += ev.OnPlayerDie;
		}

		public override void OnDisable()
		{
			Events.WaitingForPlayersEvent -= ev.OnWaitingForPlayers;
			Events.RoundStartEvent -= ev.OnRoundStart;
			Events.PlayerDeathEvent -= ev.OnPlayerDie;

			harmonyInstance.UnpatchAll();

			ev = null;
		}

		public override void OnReload() { }

		public override string getName { get; } = "Lone079";
	}
}
