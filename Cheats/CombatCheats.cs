using Invector.CharacterController;
using JustPlay.Equipment;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Recte_1v1lol.Cheats
{
    internal class CombatCheats : MonoBehaviour
    {
        public float rapidFireTimer;

        public void OnGUI()
        {
            


        }

        public void NoRecoil()
        {
            WeaponsController wC = PlayerController.LFNGIIPNIDN.AIACBMLLLFE;
            WeaponModel wM = PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB;

            WeaponStats stats3 = wM.KKCHMFIKLMC;
            stats3.RangeDescription = KGNLNPNGJOO.VeryFar;
            stats3.FireRateDescription = JHOOCPKIKNO.VeryFast;
            stats3.StatsForLevel.BurstFireDelay = 0f;
            stats3.StatsForLevel.RecoilDuration = 0f;
            stats3.StatsForLevel.RecoilForce = 0f;
            stats3.StatsForLevel.RecoilReturnForce = 0f;
            stats3.StatsForLevel.DamageSettings.IsDamageAffectedByDistance = false;

        }
        

        public void NoSpread()
        {
            WeaponsController wC = PlayerController.LFNGIIPNIDN.AIACBMLLLFE;
            WeaponModel wM = PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB;
            WeaponStats stats3 = wM.KKCHMFIKLMC;

            stats3.StatsForLevel.SpreadSettings.DefaultSpread = 0f;
            stats3.StatsForLevel.SpreadSettings.DoShotsAlwaysSpread = false;
            stats3.StatsForLevel.SpreadSettings.AimingSpread = 0f;
            stats3.StatsForLevel.SpreadSettings.SpreadOutTime = 0f;
        }

        public void Update()
        {
            try
            {
                if (PlayerPrefsX.GetBool("RapidFire") && Input.GetMouseButton(0) && !Cursor.visible && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    rapidFireTimer += Time.deltaTime;
                    if (rapidFireTimer >= .001333333)
                    {
                        WeaponsController weapons = PlayerController.LFNGIIPNIDN.AIACBMLLLFE;
                        weapons.photonView.RPC("FireWeaponRemote", 0, new object[]
                            {
                            weapons.FFPENFLINEB.point,
                            true,
                            1
                            });
                        rapidFireTimer = 0;
                    }
                }



                if (PlayerPrefsX.GetBool("HitboxExpansion") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    foreach (PlayerController pc in EntityFinding.pcs.Values)
                    {
                        if (pc.KAIBGDBBIBO.transform.localScale.x != PlayerPrefs.GetFloat("HitboxExpand") && !pc.IsMine())
                        {
                            pc.KAIBGDBBIBO.transform.localScale = new Vector3(PlayerPrefs.GetFloat("HitboxExpand"), PlayerPrefs.GetFloat("HitboxExpand"), PlayerPrefs.GetFloat("HitboxExpand"));
                        }
                    }
                    
                }
                

                if (PlayerPrefsX.GetBool("InfiniteAmmo") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.AddAmmo(9999);
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.AddMagazine(9999);
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.SetCurrentAmmoAmount(9999);
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.SetCurrentMagazineAmount(9999);
                }
                if (PlayerPrefsX.GetBool("NoRecoil") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    NoRecoil();
                }
                if (PlayerPrefsX.GetBool("NoSpread") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    NoSpread();
                }

                if (PlayerPrefsX.GetBool("GodMode") && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.ABDABPEKBFM != null)
                {
                    PlayerController.LFNGIIPNIDN.ABDABPEKBFM.SetPlayerImmunity(true);
                    PlayerController.LFNGIIPNIDN.ABDABPEKBFM.SetHealthLocally(int.MaxValue, int.MaxValue);
                }
                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GodModeKeyCode", "H"))))
                {
                    PlayerPrefsX.SetBool("GodMode", !PlayerPrefsX.GetBool("GodMode"));
                }

                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KillAllKeyCode", "K"))) && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.ABDABPEKBFM != null)
                {
                    foreach (PlayerController pc in EntityFinding.pcs.Values)
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
                }
            }
            catch (NullReferenceException) { }
        }

        
    }
}
