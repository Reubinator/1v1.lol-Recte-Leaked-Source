using JustPlay.Equipment;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte.Cheats
{
    public class Combat : MonoBehaviour
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
                if (Vars.RapidFire && Input.GetMouseButton(0) && !Cursor.visible && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
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



                if (Vars.HitboxExpansion && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    foreach (PlayerController pc in EntityFinding.pcs.Values)
                    {
                        if (pc.KAIBGDBBIBO.transform.localScale.x != Vars.HitboxSize && !pc.IsMine())
                        {
                            pc.KAIBGDBBIBO.transform.localScale = new Vector3(Vars.HitboxSize, Vars.HitboxSize, Vars.HitboxSize);
                        }
                    }

                }


                if (Vars.InfiniteAmmo && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.AddAmmo(9999);
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.AddMagazine(9999);
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.SetCurrentAmmoAmount(9999);
                    PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB.SetCurrentMagazineAmount(9999);
                }
                if (Vars.NoRecoil && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    NoRecoil();
                }
                if (Vars.NoSpread && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.AIACBMLLLFE != null)
                {
                    NoSpread();
                }

                if (Vars.GodMode && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.ABDABPEKBFM != null)
                {
                    PlayerController.LFNGIIPNIDN.ABDABPEKBFM.SetPlayerImmunity(true);
                    PlayerController.LFNGIIPNIDN.ABDABPEKBFM.SetHealthLocally(int.MaxValue, int.MaxValue);
                }
                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ImmunityKeyCode", "H"))))
                {
                    Vars.GodMode = !Vars.GodMode;
                }

                if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("KllAllKeyCode", "K"))) && PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.ABDABPEKBFM != null)
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
