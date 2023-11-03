using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte_1v1lol.Cheats
{
    internal class AimbotCheats : MonoBehaviour
    {
        public static HumanBodyBones curBone = HumanBodyBones.Head;

        public static bool IsVisible(Vector3 playerPos)
        {
            Vector3 vector = playerPos - Camera.main.transform.position;
            Vector3 normalized = vector.normalized;
            float magnitude = vector.magnitude;
            RaycastHit raycastHit;
            if (Physics.Raycast(Camera.main.transform.position, normalized, out raycastHit, magnitude, ~(1 << LayerMask.NameToLayer("Zone")) | ~(1 << LayerMask.NameToLayer("Water")) | ~(1 << LayerMask.NameToLayer("Building Edit"))))
            {
                string text = LayerMask.LayerToName(raycastHit.collider.gameObject.layer);
                return text == "OurPlayer" || text == "AutoAim" || text == "PlayerDetection";
            }
            return false;
        }
        public static void MagicBullet(Vector3 pos)
        {
            WeaponModel wM = PlayerController.LFNGIIPNIDN.AIACBMLLLFE.PFPIKMMEICB;

            Vector3 vector = pos;
            vector.y += 0.5f;

            Quaternion quaternion3 = Quaternion.LookRotation((pos - vector).normalized);
            wM.KKJFLHIIJAF.position = vector;
            wM.KKJFLHIIJAF.rotation = quaternion3;
            wM.transform.position = vector;
            wM.transform.rotation = quaternion3;
            CameraManager.NFLLAGMKOCA.transform.position = vector;
            CameraManager.NFLLAGMKOCA.TPCamera.transform.rotation = quaternion3;
            CameraManager.NFLLAGMKOCA.transform.rotation = quaternion3;
        }

        public static void UseMagicBullet()
        {
            List<PlayerController> pList = EntityFinding.GetTarget();
            UnityEngine.Component component = pList.FirstOrDefault<PlayerController>();
            if (component != null)
            {
                Vector3 bPos = component.GetComponent<Animator>().GetBoneTransform(curBone).position;
                Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(bPos);
                var abs = Mathf.Abs(Vector2.Distance(new Vector2(w2s.x, w2s.y), new Vector2(Screen.width / 2f, Screen.height / 2f)));
                float distance = Vector3.Distance(bPos, PlayerController.LFNGIIPNIDN.transform.position);
                if (RecteUtils.isOnScreen(w2s) && abs <= PlayerPrefs.GetFloat("AimbotFOV", 100f))
                {
                        MagicBullet(bPos);
                }

            }
        }

        public static void SilentAim()
        {
            List<PlayerController> pList = EntityFinding.GetTarget();
            UnityEngine.Component component = pList.FirstOrDefault<PlayerController>();
            if (component != null)
            {
                Vector3 bPos = component.GetComponent<Animator>().GetBoneTransform(curBone).position;
                Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(bPos);
                var abs = Mathf.Abs(Vector2.Distance(new Vector2(w2s.x, w2s.y), RecteUtils.CenterOfScreen()));
                float distance = Vector3.Distance(bPos, PlayerController.LFNGIIPNIDN.transform.position);
                if (RecteUtils.isOnScreen(w2s) && abs <= PlayerPrefs.GetFloat("AimbotFOV", 100f))
                {
                    if (!IsVisible(bPos) || !PlayerPrefsX.GetBool("VisibilityCheck", false))
                    {
                        //CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().transform.LookAt(bPos);
                        CameraManager.NFLLAGMKOCA.MainCamera.transform.Rotate(bPos, Space.World);
                        CameraManager.NFLLAGMKOCA.MainCamera.transform.LookAt(bPos);
                        CameraManager.NFLLAGMKOCA.TPCamera.transform.Rotate(bPos, Space.World);
                        CameraManager.NFLLAGMKOCA.TPCamera.transform.LookAt(bPos);
                    }
                }
            }
        }

        public static void LockOn()
        {
            List<PlayerController> pList = EntityFinding.GetTarget();
            Transform body = pList.FirstOrDefault<PlayerController>().GetComponent<Animator>().GetBoneTransform(curBone).transform;
            Vector3 bPos = body.position;
            Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(bPos);
            var abs = Mathf.Abs(Vector2.Distance(new Vector2(w2s.x, w2s.y), RecteUtils.CenterOfScreen()));
            float distance = Vector3.Distance(bPos, PlayerController.LFNGIIPNIDN.transform.position);
            if (RecteUtils.isOnScreen(w2s) && abs <= PlayerPrefs.GetFloat("AimbotFOV", 100f))
            {
                if (!IsVisible(bPos) || !PlayerPrefsX.GetBool("VisibilityCheck", false))
                {
                    if (bPos != PlayerController.LFNGIIPNIDN.transform.position)
                    {
                        CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().transform.LookAt(bPos);
                        CameraManager.NFLLAGMKOCA.TPCamera.SetRotation(CameraManager.NFLLAGMKOCA.transform.eulerAngles);
                    }
                }
            }

        }

        public void OnGUI()
        {
            if (PlayerPrefsX.GetBool("DrawFOVCircle") && PlayerPrefs.GetInt("FOVCircleType") < 8)
            {
                RecteUtils.DrawCircle(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), PlayerPrefs.GetFloat("AimbotFOV"), PlayerPrefs.GetInt("SideCount"), RecteUtils.GetColorFromString(PlayerPrefs.GetString("FOVCircleColor", "880000")), true, 4f);
            }
            if (PlayerPrefsX.GetBool("DrawFOVCircle") && PlayerPrefs.GetInt("FOVCircleType") == 8)
            {
                RecteUtils.DrawCircle(RecteUtils.GetColorFromString(PlayerPrefs.GetString("FOVCircleColor", "880000")), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), PlayerPrefs.GetFloat("AimbotFOV"));
                RecteUtils.DrawCircle(Color.black, new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), PlayerPrefs.GetFloat("AimbotFOV")+1.25f);
            }
            if (PlayerPrefsX.GetBool("SilentAim") && (Input.GetMouseButton(PlayerPrefs.GetInt("AimbotButton")) || !PlayerPrefsX.GetBool("NoKeyAimbot")) && PhotonNetwork.InRoom && !Cursor.visible)
            {
                SilentAim();
            }
            if (PlayerPrefsX.GetBool("LockOn") && Input.GetMouseButton(1) && PhotonNetwork.InRoom && !Cursor.visible)
            {
                LockOn();
            }

            if (PlayerPrefsX.GetBool("MagicBullet") && (Input.GetMouseButton(PlayerPrefs.GetInt("AimbotButton")) || !PlayerPrefsX.GetBool("NoKeyAimbot")) && PhotonNetwork.InRoom && !Cursor.visible)
            {
                UseMagicBullet();
            }
        }

    }
}
