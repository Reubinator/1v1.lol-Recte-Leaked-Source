using Photon.Pun;
using Photon.Realtime;
using Recte_1v1lol.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Recte_1v1lol.Cheats
{
    internal class MiscCheats : MonoBehaviour
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
            "Crayon Muncher"
        };

        public void Update()
        {
           
            try
            {
                if (PlayerPrefsX.GetBool("customScale") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.transform != null)
                {
                    PlayerController.LFNGIIPNIDN.transform.localScale = new Vector3(PlayerPrefs.GetFloat("LocalScale"), PlayerPrefs.GetFloat("LocalScale"), PlayerPrefs.GetFloat("LocalScale"));
                }
                if (!PlayerPrefsX.GetBool("customScale") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.transform != null)
                {
                    PlayerController.LFNGIIPNIDN.transform.localScale = new Vector3(1f, 1f, 1f);
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

                if (PlayerPrefsX.GetBool("BuildSpam") && PhotonNetwork.InRoom && EntityFinding.pcs != null && EntityFinding.buildNetwork != null)
                {
                    foreach (PlayerController pc in EntityFinding.pcs.Values)
                    {
                        if (!pc.IsMine())
                        {
                            foreach (BuildingNetworkController bnc in EntityFinding.buildNetwork)
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
                                foreach (Building building in FindObjectsOfType<Building>())
                                {
                                    building.DieOnlyMe();
                                }
                            }
                        }
                    }
                }

                if (PlayerPrefsX.GetBool("AntiDiscord") && PhotonNetwork.InRoom)
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

                if (PlayerPrefsX.GetBool("WeaponTrail") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN != null)
                {
                    trailTimer += Time.deltaTime;
                    if (trailTimer >= .2f)
                    {
                        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                        {
                            PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        }
                        Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                        PickupableSpawner.CreateWeaponDrop($"{Recte_1v1lol.Menu.WeaponNames[UnityEngine.Random.Range(0, 9)]}{UnityEngine.Random.Range(0, 6)}", localPlayerPosition);
                        trailTimer = 0f;
                    }
                }
                if (PlayerPrefsX.GetBool("AreBuildingsOneHit") && PhotonNetwork.InRoom && PlayerBuildingManager.Instance != null)
                {
                    PlayerBuildingManager.IsOneHitBuildings = true;
                }
                if (!PlayerPrefsX.GetBool("AreBuildingsOneHit") && PhotonNetwork.InRoom && PlayerBuildingManager.Instance != null)
                {
                    PlayerBuildingManager.IsOneHitBuildings = false;
                }

                if (PlayerPrefsX.GetBool("SpoofID"))
                {
                    idTimer += Time.deltaTime;
                    if (idTimer >= 15f)
                    {
                        FirebaseManager firebaseManager = UnityEngine.Object.FindObjectOfType<FirebaseManager>();
                        firebaseManager.PGBJEGGCGKK.ID = "Recte User! .gg/DZZ8cXTjG6 | " + RecteUtils.CreateRandomStringForDir(20);
                        idTimer = 0f;
                    }
                }

                if (PlayerPrefsX.GetBool("BreakAllToggle") && PhotonNetwork.InRoom)
                {
                    breakTimer += Time.deltaTime;
                    if (breakTimer >= .15f)
                    {
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        foreach (BuildingNetworkController builds in UnityEngine.Object.FindObjectsOfType<BuildingNetworkController>())
                        {
                            builds.KillAllBuildings(true);
                        }
                    }
                }

                 if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BreakAllToggleKeyCode", "L"))))
                 {
                     PlayerPrefsX.SetBool("BreakAllToggle", !PlayerPrefsX.GetBool("BreakAllToggle"));
                 }
                 if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DestroyAllBuildsKeyCode", "I"))))
                 {
                     PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                     foreach (BuildingNetworkController builds in UnityEngine.Object.FindObjectsOfType<BuildingNetworkController>())
                     {
                         builds.KillAllBuildings(true);
                     }
                 }
                 
                if (PlayerPrefsX.GetBool("JumpHeight") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.jumpHeight = PlayerPrefs.GetFloat("JH");
                }
                if (!PlayerPrefsX.GetBool("JumpHeight") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.jumpHeight = 5.5f;
                }

                if (PhotonNetwork.InRoom)
                {
                    PlayerController.LFNGIIPNIDN.SetGodMode(PlayerPrefsX.GetBool("Flight"));
                    PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.flySpeed = PlayerPrefs.GetFloat("FlightSpeed");
                }
                if (PlayerPrefsX.GetBool("UnlimitedBuilds") && PhotonNetwork.InRoom)
                {
                    if (PlayerBuildingManager.Instance.BuildingAmmo <= 10000)
                    {
                        PlayerBuildingManager.Instance.AddBuildingAmmo(25);
                    }
                }



                if (PlayerPrefsX.GetBool("RigSpam") && PhotonNetwork.InRoom)
                {
                    PhotonNetwork.Instantiate(
                        "PolyPlayer",
                        new Vector3(
                            (float)UnityEngine.Random.Range(-200, 200),
                            (float)UnityEngine.Random.Range(25, 100),
                            (float)UnityEngine.Random.Range(-200, 200)
                        ),
                        Quaternion.identity,
                        0,
                        null
                    );
                }
                
            }
            catch (NullReferenceException) { }
        }

        
        

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
