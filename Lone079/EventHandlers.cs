using EXILED;
using EXILED.Extensions;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lone079
{
	class EventHandlers
	{
		private System.Random rand = new System.Random();

		private Vector3 scp939pos;

		private List<RoleType> scp079Respawns = new List<RoleType>()
		{
			RoleType.Scp049,
			RoleType.Scp096,
			RoleType.Scp106,
			RoleType.Scp173,
			RoleType.Scp93953,
			RoleType.Scp93989
		};

		private List<RoleType> scp079RespawnLocations = new List<RoleType>()
		{
			RoleType.Scp049,
			RoleType.Scp096,
			RoleType.Scp93953
		};

		private IEnumerator<float> Check079()
		{
			yield return Timing.WaitForSeconds(1f);
			IEnumerable<ReferenceHub> enumerable = Player.GetHubs().Where(x => x.GetTeam() == Team.SCP);
			if (!Configs.countZombies) enumerable = enumerable.Where(x => x.GetRole() != RoleType.Scp0492);
			List<ReferenceHub> pList = enumerable.ToList(); 
			if (pList.Count == 1 && pList[0].GetRole() == RoleType.Scp079)
			{
				ReferenceHub player = pList[0];
				int level = player.GetLevel();
				player.characterClassManager.SetClassID(scp079Respawns[rand.Next(scp079Respawns.Count)]);
				player.SetPosition(scp939pos);
				player.playerStats.health = !Configs.scaleWithLevel ? player.playerStats.maxHP * (Configs.healthPercent / 100f) : player.playerStats.maxHP * ((Configs.healthPercent + ((level - 1) * 5)) / 100f);
				player.Broadcast(10, "<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>", false);
			}
		}

		public void OnWaitingForPlayers()
		{
			Configs.ReloadConfigs();
		}

		public void OnRoundStart()
		{
			Timing.CallDelayed(1f, () => scp939pos = GameObject.FindObjectOfType<SpawnpointManager>().GetRandomPosition(scp079RespawnLocations[rand.Next(scp079RespawnLocations.Count)]).transform.position);
		}

		public void OnPlayerDie(ref PlayerDeathEvent ev)
		{
			if (ev.Player.GetTeam() == Team.SCP)
			{
				Timing.RunCoroutine(Check079());
			}
		}
	}
}
