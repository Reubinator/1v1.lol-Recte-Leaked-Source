using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte.Cheats
{
    public class Aimbot : MonoBehaviour
    {
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
                Vector3 bPos = component.GetComponent<Animator>().GetBoneTransform(Vars.AimBone[Vars.AimBoneInt]).position;
                Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(bPos);
                var abs = Mathf.Abs(Vector2.Distance(new Vector2(w2s.x, w2s.y), new Vector2(Screen.width / 2f, Screen.height / 2f)));
                float distance = Vector3.Distance(bPos, PlayerController.LFNGIIPNIDN.transform.position);
                if (Utils.isOnScreen(w2s) && abs <= Vars.FOVSize)
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
                Vector3 bPos = component.GetComponent<Animator>().GetBoneTransform(Vars.AimBone[Vars.AimBoneInt]).position;
                Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(bPos);
                float distance = Vector3.Distance(bPos, PlayerController.LFNGIIPNIDN.transform.position);
                if (Utils.isOnScreen(w2s) && distance <= Vars.FOVSize)
                {
                    if (!IsVisible(bPos) || !Vars.VisibilityCheck)
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
            Transform body = pList.FirstOrDefault<PlayerController>().GetComponent<Animator>().GetBoneTransform(Vars.AimBone[Vars.AimBoneInt]).transform;
            Vector3 bPos = body.position;
            Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(bPos);
            float distance = Vector3.Distance(bPos, PlayerController.LFNGIIPNIDN.transform.position);
            if (Utils.isOnScreen(w2s) && distance <= Vars.FOVSize)
            {
                if (!IsVisible(bPos) || !Vars.VisibilityCheck)
                {
                    if (bPos != PlayerController.LFNGIIPNIDN.transform.position)
                    {
                        CameraManager.NFLLAGMKOCA.TPCamera.transform.LookAt(bPos);
                        CameraManager.NFLLAGMKOCA.TPCamera.SetRotation(CameraManager.NFLLAGMKOCA.transform.eulerAngles);
                    }
                }
            }

        }
        
        public void OnGUI()
        {
            if (Vars.DrawFOV && Vars.FOVShape < 8)
            {
                Render.DrawCircle(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vars.FOVSize, Vars.SideCount, Utils.GetColorFromString(Vars.FOVCircleColor), true, 4f);
            }
            if (Vars.DrawFOV && Vars.FOVShape == 8)
            {
                Render.DrawCircle(Utils.GetColorFromString(Vars.FOVCircleColor), new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vars.FOVSize);
                Render.DrawCircle(Color.black, new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Vars.FOVSize + 1.25f);
            }
            if (Vars.SilentAim && (Input.GetMouseButton(Vars.AimbotKey) || !Vars.UseAimbotKey) && PhotonNetwork.InRoom && !Cursor.visible)
            {
                SilentAim();
            }
            if (Vars.LockOn && Input.GetMouseButton(1) && PhotonNetwork.InRoom && !Cursor.visible)
            {
                LockOn();
            }

            if (Vars.MagicBullet && (Input.GetMouseButton(Vars.AimbotKey) || !Vars.UseAimbotKey) && PhotonNetwork.InRoom && !Cursor.visible)
            {
                UseMagicBullet();
            }
        }
    }
}
