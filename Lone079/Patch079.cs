using Harmony;
using RemoteAdmin;
using UnityEngine;

namespace Lone079
{
    [HarmonyPatch(typeof(NineTailedFoxAnnouncer), nameof(NineTailedFoxAnnouncer.Update))]
    class Patch1
    {
        public static bool Prefix(NineTailedFoxAnnouncer __instance)
        {
            if (NineTailedFoxAnnouncer.scpDeaths.Count <= 0)
                return false;
            __instance.scpListTimer += Time.deltaTime;
            if ((double)__instance.scpListTimer <= 1.0)
                return false;
            for (int index1 = 0; index1 < NineTailedFoxAnnouncer.scpDeaths.Count; ++index1)
            {
                string str1 = "";
                for (int index2 = 0; index2 < NineTailedFoxAnnouncer.scpDeaths[index1].scpSubjects.Count; ++index2)
                {
                    string str2 = "";
                    string fullName = NineTailedFoxAnnouncer.scpDeaths[index1].scpSubjects[index2].fullName;
                    char[] chArray = new char[1] { '-' };
                    foreach (char ch in fullName.Split(chArray)[1])
                        str2 = str2 + ch.ToString() + " ";
                    str1 = index2 != 0 ? str1 + ". SCP " + str2 : str1 + "SCP " + str2;
                }
                DamageTypes.DamageType damageType = NineTailedFoxAnnouncer.scpDeaths[index1].hitInfo.GetDamageType();
                string tts;
                if (damageType == DamageTypes.Tesla)
                    tts = str1 + "SUCCESSFULLY TERMINATED BY AUTOMATIC SECURITY SYSTEM";
                else if (damageType == DamageTypes.Nuke)
                    tts = str1 + "SUCCESSFULLY TERMINATED BY ALPHA WARHEAD";
                else if (damageType == DamageTypes.Decont)
                {
                    tts = str1 + "LOST IN DECONTAMINATION SEQUENCE";
                }
                else
                {
                    CharacterClassManager characterClassManager = (CharacterClassManager)null;
                    foreach (GameObject player in PlayerManager.players)
                    {
                        if (player.GetComponent<QueryProcessor>().PlayerId == NineTailedFoxAnnouncer.scpDeaths[index1].hitInfo.PlyId)
                            characterClassManager = player.GetComponent<CharacterClassManager>();
                    }
                    if ((UnityEngine.Object)characterClassManager != (UnityEngine.Object)null)
                    {
                        string str2 = NineTailedFoxAnnouncer.scpDeaths[index1].scpSubjects[0].roleId != RoleType.Scp106 || NineTailedFoxAnnouncer.scpDeaths[index1].hitInfo.GetDamageType() != DamageTypes.RagdollLess ? "TERMINATED" : "CONTAINEDSUCCESSFULLY";
                        switch (characterClassManager.Classes.SafeGet(characterClassManager.CurClass).team)
                        {
                            case Team.MTF:
                                string str3 = NineTailedFoxUnits.host.list[characterClassManager.NtfUnit];
                                char ch1 = str3[0];
                                string str4 = int.Parse(str3.Split('-')[1]).ToString("00");
                                string str5 = str1;
                                string str6 = ch1.ToString();
                                char ch2 = str4[0];
                                string str7 = ch2.ToString();
                                string str8 = "CONTAINEDSUCCESSFULLY CONTAINMENTUNIT NATO_" + str6 + " " + str7;
                                ch2 = str4[1];
                                string str9 = ch2.ToString();
                                tts = str5 + str8 + str9;
                                break;
                            case Team.CHI:
                                tts = str1 + str2 + " BY CHAOSINSURGENCY";
                                break;
                            case Team.RSC:
                                tts = str1 + str2 + " BY SCIENCE PERSONNEL";
                                break;
                            case Team.CDP:
                                tts = str1 + str2 + " BY CLASSD PERSONNEL";
                                break;
                            default:
                                tts = str1 + "SUCCESSFULLY TERMINATED . CONTAINMENTUNIT UNKNOWN";
                                break;
                        }
                    }
                    else
                        tts = str1 + "SUCCESSFULLY TERMINATED . TERMINATION CAUSE UNSPECIFIED";
                }
                int num1 = 0;
                bool flag = false;
                foreach (GameObject player in PlayerManager.players)
                {
                    CharacterClassManager component = player.GetComponent<CharacterClassManager>();
                    if (component.CurClass == RoleType.Scp079)
                        flag = true;
                    if (component.Classes.SafeGet(component.CurClass).team == Team.SCP)
                        ++num1;
                }
                /*if (num1 == 1 & flag && Generator079.mainGenerator.totalVoltage < 4 && !Generator079.mainGenerator.forcedOvercharge)
                {
                    Generator079.mainGenerator.forcedOvercharge = true;
                    Recontainer079.BeginContainment(true);
                    tts += " . ALLSECURED . SCP 0 7 9 RECONTAINMENT SEQUENCE COMMENCING . FORCEOVERCHARGE";
                }*/
                float num2 = (double)AlphaWarheadController.Host.timeToDetonation <= 0.0 ? 3.5f : 1f;
                __instance.ServerOnlyAddGlitchyPhrase(tts, UnityEngine.Random.Range(0.1f, 0.14f) * num2, UnityEngine.Random.Range(0.07f, 0.08f) * num2);
            }
            __instance.scpListTimer = 0.0f;
            NineTailedFoxAnnouncer.scpDeaths.Clear();
            return false;
        }
    }

    [HarmonyPatch(typeof(NineTailedFoxAnnouncer), nameof(NineTailedFoxAnnouncer.CheckForZombies))]
    class Patch2
    {
        public static bool Prefix() => false;
    }
}
