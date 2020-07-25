using HarmonyLib;
using Exiled.API.Features;

namespace Lone079
{
	public class Lone079 : Plugin<Config>
	{
		public static Lone079 instance;
		private EventHandlers ev;

		private Harmony hInstance;

		public override void OnEnabled()
		{
			if (!Config.IsEnabled) return;

			instance = this;

			hInstance = new Harmony($"cyanox.lone079");
			hInstance.PatchAll();

			ev = new EventHandlers();

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died += ev.OnPlayerDie;
			Exiled.Events.Handlers.Player.Left += ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing += ev.OnScp106Contain;
		}

		public override void OnDisabled()
		{
			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died -= ev.OnPlayerDie;
			Exiled.Events.Handlers.Player.Left -= ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing -= ev.OnScp106Contain;

			hInstance.UnpatchAll();

			ev = null;
		}

		public override string Name => "Lone079";
	}
}
