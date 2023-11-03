using DEVMenu.Remote;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Recte;

namespace Recte
{
    internal class EntityFinding : MonoBehaviour
    {
        public static PlayerController[] Players;
        public static Dictionary<int, PlayerController> pcs = new Dictionary<int, PlayerController>();
        public static Pickupable[] picks;
        public static SupplyCrate[] crates;
        public static PlayerRemoteTools prt;
        public static List<Material> mat;
        public static SkinnedMeshRenderer[] smr;
        public static BuildingNetworkController[] bnc;
        public static Building[] builds;
        public static FirebaseManager fbm;

        public void Start()
        {
            UnityEngine.QualitySettings.antiAliasing = 0;
            base.InvokeRepeating("SetVars", 1f, 1f);
            Resources.UnloadUnusedAssets();
            Application.targetFrameRate = 999;

            CameraManager.NFLLAGMKOCA.MainCamera.allowHDR = false;
            CameraManager.NFLLAGMKOCA.MainCamera.depthTextureMode = DepthTextureMode.None;

        }



        public static ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        public void SetVars()
        {

            if (Application.targetFrameRate < 999)
            {
                Application.targetFrameRate = 999;
            }
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    fbm = UnityEngine.Object.FindObjectOfType<FirebaseManager>();
                    picks = UnityEngine.Object.FindObjectsOfType<Pickupable>();
                    crates = UnityEngine.Object.FindObjectsOfType<SupplyCrate>();
                    smr = UnityEngine.Object.FindObjectsOfType<SkinnedMeshRenderer>();
                    pcs = PlayersManager.NFLLAGMKOCA.DKDBFGEGJJG;
                    builds = UnityEngine.Object.FindObjectsOfType<Building>();
                    bnc = UnityEngine.Object.FindObjectsOfType<BuildingNetworkController>();
                }

            }
            catch (NullReferenceException) { }
            try
            {
                if (PhotonNetwork.LocalPlayer.CustomProperties["ImLiterallyAboutToNutWithRecte"] == null)
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                    hash.Add("ImLiterallyAboutToNutWithRecte", "t");
                }
            }
            catch (ArgumentException) { }


        }
        public static List<PlayerController> GetTarget()
        {
            List<PlayerController> pList = pcs.Values.ToList<PlayerController>();
            if (pList != null)
            {
                if (Vars.AimbotSort == 0)
                {
                    //Distance From Crosshair
                    pList = EntityFinding.pcs.Values.ToArray().OrderBy((PlayerController x) => Mathf.Abs(Vector3.Distance(new Vector3(Camera.main.WorldToScreenPoint(x.transform.position).x, Camera.main.WorldToScreenPoint(x.transform.position).y), new Vector3(Screen.width / 2f, Screen.height / 2f)))).ThenBy((PlayerController x) => Mathf.Abs(x.ABDABPEKBFM.CHFPIGOLOOB)).ToList<PlayerController>();
                }
                if (Vars.AimbotSort == 1)
                {
                    //Distance From Player
                    pList = EntityFinding.pcs.Values.ToArray().OrderBy((PlayerController x) => Mathf.Abs(Vector3.Distance(x.transform.position, PlayerController.LFNGIIPNIDN.transform.position))).ThenBy((PlayerController x) => Mathf.Abs(Vector3.Distance(new Vector3(Camera.main.WorldToScreenPoint(x.transform.position).x, Camera.main.WorldToScreenPoint(x.transform.position).y), new Vector3(Screen.width / 2f, Screen.height / 2f)))).ThenBy((PlayerController x) => Mathf.Abs(x.ABDABPEKBFM.CHFPIGOLOOB)).ToList<PlayerController>();
                }
                if (Vars.AimbotSort == 2)
                {
                    //Health
                    pList = EntityFinding.pcs.Values.ToArray().OrderBy((PlayerController x) => Mathf.Abs(x.ABDABPEKBFM.OLLCMEJHPIL)).ThenBy((PlayerController x) => Mathf.Abs(x.ABDABPEKBFM.CHFPIGOLOOB)).ThenBy((PlayerController x) => Mathf.Abs(Vector3.Distance(new Vector3(Camera.main.WorldToScreenPoint(x.transform.position).x, Camera.main.WorldToScreenPoint(x.transform.position).y), new Vector3(Screen.width / 2f, Screen.height / 2f)))).ToList<PlayerController>();
                }
                pList.Remove(PlayerController.LFNGIIPNIDN);
                return pList;
            }
            return null;
        }


    }
}
