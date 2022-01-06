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
			base.OnEnabled();

			if (!Config.IsEnabled) return;

			instance = this;

			hInstance = new Harmony("cyanox.lone079");
			hInstance.PatchAll();

			ev = new EventHandlers();

			Exiled.Events.Handlers.Server.RoundStarted += ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died += ev.OnPlayerDied;
			Exiled.Events.Handlers.Player.Left += ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing += ev.OnScp106Contain;
			Exiled.Events.Handlers.Warhead.Detonated += ev.OnDetonated;
			Exiled.Events.Handlers.Cassie.SendingCassieMessage += ev.OnCassie;
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			Exiled.Events.Handlers.Server.RoundStarted -= ev.OnRoundStart;
			Exiled.Events.Handlers.Player.Died -= ev.OnPlayerDied;
			Exiled.Events.Handlers.Player.Left -= ev.OnPlayerLeave;
			Exiled.Events.Handlers.Scp106.Containing -= ev.OnScp106Contain;
			Exiled.Events.Handlers.Warhead.Detonated -= ev.OnDetonated;
			Exiled.Events.Handlers.Cassie.SendingCassieMessage -= ev.OnCassie;

			hInstance.UnpatchAll(hInstance.Id);

			ev = null;
		}

		public override string Name => "Lone079";
	}
}
