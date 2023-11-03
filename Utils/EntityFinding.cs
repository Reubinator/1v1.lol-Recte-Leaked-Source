using DEVMenu.Remote;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Recte_1v1lol
{
    internal class EntityFinding : MonoBehaviour
    {
        //public static List<PlayerController> Players;
        public static BuildingNetworkController[] buildNetwork;
        public static PlayerController[] Players;
        public static Dictionary<int, PlayerController> pcs = new Dictionary<int, PlayerController>();
        public static Pickupable[] picks;
        public static SupplyCrate[] crates;
        public static PlayerRemoteTools prt;
        public static List<Material> mat;
        public static SkinnedMeshRenderer[] smr;

        public void Start()
        {
            UnityEngine.QualitySettings.antiAliasing = 0;
            //StartCoroutine(FindPlayers());
            base.InvokeRepeating("SetVars", 1f, 1f);
            Resources.UnloadUnusedAssets();
            Application.targetFrameRate = 999;
           
                CameraManager.NFLLAGMKOCA.MainCamera.allowHDR = false;
               CameraManager.NFLLAGMKOCA.MainCamera.depthTextureMode = DepthTextureMode.None;
            
        }

        
        
        public static ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        public void SetVars()
        {
            
            if (Application.targetFrameRate < int.MaxValue){
                Application.targetFrameRate = int.MaxValue;
            }
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    picks = UnityEngine.Object.FindObjectsOfType<Pickupable>();
                    crates = UnityEngine.Object.FindObjectsOfType<SupplyCrate>();
                    smr = UnityEngine.Object.FindObjectsOfType<SkinnedMeshRenderer>();
                    pcs = PlayersManager.NFLLAGMKOCA.DKDBFGEGJJG;
                    



                }

            }
            catch (NullReferenceException) { }
            try
            {
                if (PhotonNetwork.LocalPlayer.CustomProperties["RecteUser"] == null)
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
                if (PlayerPrefs.GetInt("AimbotSort", 0) == 0)
                {
                    //Distance From Crosshair
                    pList = EntityFinding.pcs.Values.ToArray().OrderBy((PlayerController x) => Mathf.Abs(Vector3.Distance(new Vector3(Camera.main.WorldToScreenPoint(x.transform.position).x, Camera.main.WorldToScreenPoint(x.transform.position).y), new Vector3(Screen.width / 2f, Screen.height / 2f)))).ThenBy((PlayerController x) => Mathf.Abs(x.ABDABPEKBFM.CHFPIGOLOOB)).ToList<PlayerController>();
                }
                if (PlayerPrefs.GetInt("AimbotSort", 0) == 1)
                {
                    //Distance From Player
                    pList = EntityFinding.pcs.Values.ToArray().OrderBy((PlayerController x) => Mathf.Abs(Vector3.Distance(x.transform.position, PlayerController.LFNGIIPNIDN.transform.position))).ThenBy((PlayerController x) => Mathf.Abs(Vector3.Distance(new Vector3(Camera.main.WorldToScreenPoint(x.transform.position).x, Camera.main.WorldToScreenPoint(x.transform.position).y), new Vector3(Screen.width / 2f, Screen.height / 2f)))).ThenBy((PlayerController x) => Mathf.Abs(x.ABDABPEKBFM.CHFPIGOLOOB)).ToList<PlayerController>();
                }
                if (PlayerPrefs.GetInt("AimbotSort", 0) == 2)
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
