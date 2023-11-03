using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace Recte.Cheats
{
    public class Misc : MonoBehaviour
    {
        public int buildingIterations;
        public static float nameTimer;
        public static float dscTimer;
        public float trailTimer;
        public float idTimer;
        public float breakTimer;
        public static string[] PhotonNames = new string[]
        {
            "Nigga Man",
            ".gg/DZZ8cXTjG6",
            "Kanati Owns Me & All",
            "Recte On Top",
            "https://kanati.bio",
            "PornFlakes",
            "The Gay Kid On Netflix",
            "FurryEliminator007",
            "Stroking Grandma",
            "Jack Fuckin' Sparrow",
            "Black Man And Robbin",
            "xXSniperGodzXx",
            "2YearOldVirgin",
            "Crayon Muncher",
            "DickBoy01",
            "Literally Kanati",
            "Your Local School Slut",
            "Obama's Pickle",
            "Jesus Himself",
            "Crayon Kid",
            "Help I'm Fucking A Cow",
            "My Dick's Caught In A Blender",
            "Big Dick Randy"
        };

        public void Update()
        {

            try
            {
                if (Vars.PlayerScale && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.transform != null && PlayerController.LFNGIIPNIDN.transform.localScale.x != Vars.LocalScale)
                {
                    PlayerController.LFNGIIPNIDN.transform.localScale = new Vector3(Vars.LocalScale, Vars.LocalScale, Vars.LocalScale);
                }
                /*
                if (PlayerPrefsX.GetBool("SpinBot") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.transform != null)
                {
                    Quaternion originalRotation = PlayerController.LFNGIIPNIDN.transform.rotation;
                    i += 24;
                    PlayerController.LFNGIIPNIDN.transform.Rotate(originalRotation.x, (float)i, originalRotation.z);
                    if (i > 360)
                    {
                        i = 0;
                    }
                }
                if (!PlayerPrefsX.GetBool("SpinBot") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.transform != null)
                {
                    PlayerController.LFNGIIPNIDN.transform.rotation = Quaternion.identity;
                }
                */

                if (Vars.BuildSpam && PhotonNetwork.InRoom && EntityFinding.pcs != null && EntityFinding.bnc != null)
                {
                    foreach (PlayerController pc in EntityFinding.pcs.Values)
                    {
                        if (!pc.IsMine())
                        {
                            foreach (BuildingNetworkController bnc in EntityFinding.bnc)
                            {
                                bnc.CreateBuilding(LDGEAILCHNI.Wall, pc.gameObject.transform.position, Quaternion.Euler((float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360)));
                                bnc.CreateBuilding(LDGEAILCHNI.Floor, pc.gameObject.transform.position, Quaternion.Euler((float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360)));
                                bnc.CreateBuilding(LDGEAILCHNI.Ramp, pc.gameObject.transform.position, Quaternion.Euler((float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360)));
                                bnc.CreateBuilding(LDGEAILCHNI.Roof, pc.gameObject.transform.position, Quaternion.Euler((float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360), (float)UnityEngine.Random.Range(1, 360)));

                            }
                            buildingIterations += 1;

                            if (buildingIterations > 20)
                            {
                                buildingIterations = 0;
                                foreach (Building building in EntityFinding.builds)
                                {
                                    building.DieOnlyMe();
                                }
                            }
                        }
                    }
                }

                if (Vars.AntiDiscord && PhotonNetwork.InRoom)
                {
                    dscTimer += Time.deltaTime;
                    if (dscTimer >= .5f)
                    {
                        foreach (Player p in PhotonNetwork.PlayerList)
                        {
                            if (p.NickName.Contains(".gg/"))
                            {
                                if (!p.NickName.Contains(".gg/DZZ8cXTjG6"))
                                {
                                    PhotonNetwork.Disconnect();
                                }
                            }
                        }
                    }
                }
                if (Vars.RandomNames)
                {
                    nameTimer += Time.deltaTime;

                    if (nameTimer >= 1f)
                    {
                        FirebaseManager.Instance.PGBJEGGCGKK.Nickname = PhotonNames[UnityEngine.Random.Range(0, PhotonNames.Length)];
                        PhotonNetwork.NickName = PhotonNames[UnityEngine.Random.Range(0, PhotonNames.Length)];
                        nameTimer = 0f;
                    }
                }

                if (Vars.WeaponTrail && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN != null)
                {
                    trailTimer += Time.deltaTime;
                    if (trailTimer >= .2f)
                    {
                        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                        {
                            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        }
                        Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                        PickupableSpawner.CreateWeaponDrop($"{Menu.WeaponNames[UnityEngine.Random.Range(0, 9)]}{UnityEngine.Random.Range(0, 6)}", localPlayerPosition);
                        trailTimer = 0f;
                    }
                }
                if (Vars.OneHitBuilds && PhotonNetwork.InRoom && PlayerBuildingManager.Instance != null)
                {
                    PlayerBuildingManager.IsOneHitBuildings = true;
                }


                if (Vars.SpoofID)
                {
                    idTimer += Time.deltaTime;
                    if (idTimer >= 15f)
                    {
                        
                        EntityFinding.fbm.PGBJEGGCGKK.ID = "Recte User! .gg/DZZ8cXTjG6 | " + Utils.CreateRandomStringForDir(20);
                        idTimer = 0f;
                    }
                }

                if (Vars.BreakAll && PhotonNetwork.InRoom)
                {
                    breakTimer += Time.deltaTime;
                    if (breakTimer >= .25f)
                    {
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        foreach (BuildingNetworkController builds in EntityFinding.bnc)
                        {
                            builds.KillAllBuildings(true);
                        }
                    }
                }

                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BreakAllTogKeyCode", "L"))))
                {
                    Vars.BreakAll = !Vars.BreakAll;
                }
                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DestroyAllKeyCode", "I"))))
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                    foreach (BuildingNetworkController builds in EntityFinding.bnc)
                    {
                        builds.KillAllBuildings(true);
                    }
                }

                if (Vars.JumpHeight && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.jumpHeight = Vars.JH;
                }


                if (PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN != null)
                {
                    PlayerController.LFNGIIPNIDN.SetGodMode(Vars.Flight);
                    PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.flySpeed = Vars.FlightSpeed;
                }
                if (Vars.UnlimitedBuilds && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN != null && PlayerBuildingManager.Instance != null)
                {
                    if (PlayerBuildingManager.Instance.BuildingAmmo <= 10000)
                    {
                        PlayerBuildingManager.Instance.AddBuildingAmmo(25);
                    }
                }



                if (Vars.RigSpam && PhotonNetwork.InRoom)
                {
                    var pObj = PhotonNetwork.Instantiate("PolyPlayer", new Vector3((float)UnityEngine.Random.Range(-200, 200), (float)UnityEngine.Random.Range(25, 100), (float)UnityEngine.Random.Range(-200, 200)), Quaternion.identity, 0, null);
                    if (pObj != null && pObj != base.gameObject)
                    {
                        UnityEngine.Object.Destroy(pObj.gameObject);
                        //UnityEngine.Object.Destroy(pObj);
                    }
                }

            }
            catch (NullReferenceException) { }
            
        }
        /*
        public  void OnGUI()
        {
            KillAura();
        }
        public void KillAura()
        {
            if (EntityFinding.pcs != null)
            {
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        float distance = Vector3.Distance(pc.transform.position, PlayerController.LFNGIIPNIDN.transform.position);
                        if (distance < 15f)
                        {
                            PlayerHealth ph = pc.ABDABPEKBFM;
                            if (pc != null && !pc.IsMine() && !pc.BODBNDGJCLF)
                            {
                                ph.photonView.RPC("TakeHit", 0, new object[]
                                {
                                    999999,
                                    ph.transform.position,
                                    ph.photonView.CreatorActorNr,
                                    true
                                });
                            }
                        }
                        Vector3 v = Camera.main.WorldToScreenPoint(PlayerController.LFNGIIPNIDN.transform.position);
                        if (Utils.isOnScreen(v))
                        {
                            Render.DrawCircle(Color.red, new Vector2(v.x, (float)Screen.height - v.y), 15f * 25f);

                        }
                    }
                }
            }
            
        }*/

        public static void ChangeSkin(string id)
        {
            ServerUser serverUser = FirebaseManager.Instance.PGBJEGGCGKK;
            if (serverUser != null)
            {
                ServerSkinsEntry skins = serverUser.Skins;
                string text2 = "lol.1v1.playerskins.pack.";
                skins.EquippedCharacterSkin = text2 + id;


            }
        }
        public static void ChangePickaxe()
        {
            ServerUser serverUser = FirebaseManager.Instance.PGBJEGGCGKK;
            if (serverUser != null)
            {
                ServerSkinsEntry skins = serverUser.Skins;
                if (skins.EquippedWeaponSkins.Contains("lol.1v1.weaponskins.melee.pickaxe.default"))
                {
                    skins.EquippedWeaponSkins.Remove("lol.1v1.weaponskins.melee.pickaxe.default");
                }
                skins.EquippedWeaponSkins.Add("lol.1v1.weaponskins.melee.pickaxe.scifihammer");

                skins.WeaponSkins.Add("lol.1v1.weaponskins.melee.pickaxe.scifihammer");
            }
        }

        public static void UnlockAll()
        {
            ServerUser serverUser = FirebaseManager.Instance.PGBJEGGCGKK;
            if (serverUser != null)
            {
                ServerSkinsEntry skins = serverUser.Skins;
                string text = "lol.1v1.playeremotes.pack.";
                string text2 = "lol.1v1.playerstickers.pack.";
                for (int i = 1; i <= 50; i++)
                {
                    skins.OwnedEmotes.Add(string.Format("{0}{1}", text, i));
                }
                for (int j = 1; j <= 80; j++)
                {
                    skins.OwnedEmotes.Add(string.Format("{0}{1}", text2, j));
                }

            }
        }
        public static void UnlockAllSkins()
        {
            ServerUser serverUser = FirebaseManager.Instance.PGBJEGGCGKK;
            if (serverUser != null)
            {
                ServerSkinsEntry skins = serverUser.Skins;
                for (int i = 0; i < 300; i++)
                {
                    string text2 = "lol.1v1.playerskins.pack.";
                    skins.CharacterSkins.Add(text2 + i.ToString());
                }
            }
        }
    }
}
