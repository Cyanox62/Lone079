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

		private IEnumerator<float> Check079()
		{
			yield return Timing.WaitForSeconds(1f);
			List<ReferenceHub> pList = Player.GetHubs().Where(x => x.GetTeam() == Team.SCP && x.GetRole() != RoleType.Scp0492).ToList();
			if (pList.Count == 1 && pList[0].GetRole() == RoleType.Scp079)
			{
				ReferenceHub player = pList[0];
				player.characterClassManager.SetClassID(scp079Respawns[rand.Next(scp079Respawns.Count)]);
				player.playerStats.health = player.playerStats.maxHP / 2;
				player.SetPosition(scp939pos);
				player.Broadcast(10, "<i>You have been respawned as a random SCP with half health because all other SCPs have died.</i>", false);
			}
		}

		public void OnRoundStart()
		{
			Timing.CallDelayed(1f, () => scp939pos = GameObject.FindObjectOfType<SpawnpointManager>().GetRandomPosition(RoleType.Scp93953).transform.position);
		}

		public void OnPlayerDie(ref PlayerDeathEvent ev)
		{
			Timing.RunCoroutine(Check079());
		}
	}
}
