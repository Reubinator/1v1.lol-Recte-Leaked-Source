using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Recte_1v1lol.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Recte_1v1lol.Cheats
{
    internal class VisualCheats : MonoBehaviour
    {
        public float hue;
        public float sat;
        public float bri;
        public void Skeleton()
        {
            
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                Color color = RecteUtils.GetColorFromString(PlayerPrefs.GetString("SkeletonColor"));
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    
                    if (pc != null)
                    {
                        Animator component = pc.GetComponent<Animator>();
                        PhotonView photonView = component.GetComponent<PhotonView>();

                        float distance = Vector3.Distance(component.transform.position, PlayerController.LFNGIIPNIDN.transform.position);

                        if ((!Vars.showSelfSkeleton && !pc.IsMine()) || Vars.showSelfSkeleton && photonView != null)
                        {

                            if (RecteUtils.isOnScreen(CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.transform.position))  && !Vars.MaxESPDist || (RecteUtils.isOnScreen(CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.transform.position)) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {

                                Vector3 rightHand = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightHand).transform.position);
                                Vector3 rightForearm = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightLowerArm).transform.position);
                                Vector3 rightBicep = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightUpperArm).transform.position);

                                if (RecteUtils.isOnScreen(rightForearm) || RecteUtils.isOnScreen(rightHand))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(rightForearm), RecteUtils.EspMath(rightHand), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(rightForearm) || RecteUtils.isOnScreen(rightBicep))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(rightBicep), RecteUtils.EspMath(rightForearm), color, 1f);
                                }
                                Vector3 rightToe = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightToes).transform.position);
                                Vector3 rightFoot = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightFoot).transform.position);
                                Vector3 rightCalf = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightLowerLeg).transform.position);
                                Vector3 rightThigh = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightUpperLeg).transform.position);


                                if (RecteUtils.isOnScreen(rightFoot) || RecteUtils.isOnScreen(rightToe))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(rightFoot), RecteUtils.EspMath(rightToe), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(rightFoot) || RecteUtils.isOnScreen(rightCalf))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(rightCalf), RecteUtils.EspMath(rightFoot), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(rightCalf) || RecteUtils.isOnScreen(rightThigh))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(rightThigh), RecteUtils.EspMath(rightCalf), color, 1f);
                                }


                                Vector3 LeftHand = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftHand).transform.position);
                                Vector3 LeftForearm = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftLowerArm).transform.position);
                                Vector3 LeftBicep = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftUpperArm).transform.position);

                                if (RecteUtils.isOnScreen(LeftForearm) || RecteUtils.isOnScreen(LeftHand))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(LeftForearm), RecteUtils.EspMath(LeftHand), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(LeftBicep) || RecteUtils.isOnScreen(LeftForearm))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(LeftBicep), RecteUtils.EspMath(LeftForearm), color, 1f);
                                }

                                Vector3 LeftToe = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftToes).transform.position);
                                Vector3 LeftFoot = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftFoot).transform.position);
                                Vector3 LeftCalf = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftLowerLeg).transform.position);
                                Vector3 LeftThigh = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftUpperLeg).transform.position);


                                if (RecteUtils.isOnScreen(LeftFoot) || RecteUtils.isOnScreen(LeftToe))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(LeftFoot), RecteUtils.EspMath(LeftToe), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(LeftCalf) || RecteUtils.isOnScreen(LeftFoot))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(LeftCalf), RecteUtils.EspMath(LeftFoot), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(LeftThigh) || RecteUtils.isOnScreen(LeftCalf))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(LeftThigh), RecteUtils.EspMath(LeftCalf), color, 1f);
                                }

                                Vector3 hips = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Hips).transform.position);
                                Vector3 chest = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Chest).transform.position);
                                Vector3 upChest = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.UpperChest).transform.position);
                                Vector3 spine = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Spine).transform.position);
                                Vector3 head = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Head).transform.position);
                                Vector3 jaw = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Jaw).transform.position);


                                if (RecteUtils.isOnScreen(hips) || RecteUtils.isOnScreen(LeftThigh))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(hips), RecteUtils.EspMath(LeftThigh), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(hips) || RecteUtils.isOnScreen(rightThigh))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(hips), RecteUtils.EspMath(rightThigh), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(spine) || RecteUtils.isOnScreen(hips))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(spine), RecteUtils.EspMath(hips), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(chest) || RecteUtils.isOnScreen(spine))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(chest), RecteUtils.EspMath(spine), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(upChest) || RecteUtils.isOnScreen(chest))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(upChest), RecteUtils.EspMath(chest), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(upChest) || RecteUtils.isOnScreen(rightBicep))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(upChest), RecteUtils.EspMath(rightBicep), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(upChest) || RecteUtils.isOnScreen(LeftBicep))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(upChest), RecteUtils.EspMath(LeftBicep), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(upChest) || RecteUtils.isOnScreen(head))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(head), RecteUtils.EspMath(upChest), color, 1f);
                                }
                                if (RecteUtils.isOnScreen(jaw) || RecteUtils.isOnScreen(head))
                                {
                                    RecteUtils.DrawNonESPLine(RecteUtils.EspMath(jaw), RecteUtils.EspMath(head), color, 1f);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void StringESP()
        {
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfNametags && !pc.IsMine()) || Vars.showSelfNametags)
                        {
                            var name = pc.photonView.Owner.NickName;
                            Vector3 pos = pc.transform.position;
                            Vector3 camPos = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos);
                            float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            if ((camPos.z > 0 && !Vars.MaxESPDist) || (camPos.z > 0 && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                RecteUtils.DrawText(new Vector2(camPos.x, (float)Screen.height - camPos.y), $"<b><color=#080>{name}\n</color></b>", Color.black, true);
                            }
                        }
                    }
                }
            }
        }

        public void CornerBox()
        {

            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                Color color = RecteUtils.GetColorFromString(PlayerPrefs.GetString("CornerBoxColor"));
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfCorner && !pc.IsMine()) || Vars.showSelfCorner)
                        {
                            Vector3 pos = pc.transform.position;
                            Vector3 camPos = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos);
                            Vector2 head = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position);
                            float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            float num2 = Mathf.Abs(head.y - camPos.y);
                            if ((camPos.z > 0 && !Vars.MaxESPDist) || (camPos.z > 0 && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                RecteUtils.CornerBox(new Vector2(head.x, (float)Screen.height - head.y - 20f), num2 - 20f, num2 + 20f, 2f, color, true);
                            }
                        }


                    }
                }
            }
        }

        public void BoxESP()
        {
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                Color color = RecteUtils.GetColorFromString(PlayerPrefs.GetString("BoxFilledESPColor", "30303040"));
                Color color1 = RecteUtils.GetColorFromString(PlayerPrefs.GetString("BoxOutlineESPColor", "880000"));
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {

                        Vector3 pos = pc.transform.position;
                        Vector3 vector = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).transform.position);
                        Vector3 vector2 = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform.position);
                        float height = vector.y - vector2.y;
                        float num = Mathf.Abs(vector.y - vector2.y);
                        float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                        if ((RecteUtils.isOnScreen(vector) && RecteUtils.isOnScreen(vector2) && !Vars.MaxESPDist) || (RecteUtils.isOnScreen(vector) && RecteUtils.isOnScreen(vector2) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                        {

                            if (Vars.FilledBox)
                            {
                                if ((!Vars.showSelfFilled && !pc.IsMine()) || Vars.showSelfFilled)
                                {
                                    RecteUtils.RectFilled(new Vector2(vector2.x, (float)Screen.height - vector2.y - (height / 2f)), new Vector2(num / 2f, height), color, true);
                                }
                            }

                            if (Vars.OutlineBox)
                            {
                                if ((!Vars.showSelfOutline && !pc.IsMine()) || Vars.showSelfOutline)
                                {
                                    RecteUtils.DrawBox(new Vector2(vector2.x, (float)Screen.height - vector2.y - (height / 2f)), new Vector2(num / 2f, height), 2f, color1, true);
                                }
                            }
                        }

                    }
                }
                            
            }
        }

        public void AmmoChams()
        {
            if (PhotonNetwork.InRoom && EntityFinding.picks != null)
            {
                Color colorFromString = RecteUtils.GetColorFromString(PlayerPrefs.GetString("ItemChamsAmmoColor"));
            
                foreach (Pickupable pick in EntityFinding.picks)
                {
                    if (pick.BBJDGGLJCEJ == DHLIPKPGIFP.WeaponAmmo)
                    {
                        Renderer[] componentsInChildren = pick.GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < componentsInChildren.Length; j++)
                        {
                            foreach (Material material in componentsInChildren[j].materials)
                            {
                                if (material.color != colorFromString)
                                {
                                    Shader shader = Shader.Find("GUI/Text Shader");
                                    material.shader = shader;
                                    material.renderQueue = 4000;
                                    material.mainTexture = null;
                                    material.color = colorFromString;
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void WeaponsChams()
        {
            if (PhotonNetwork.InRoom && EntityFinding.picks != null)
            {
                Color colorFromString = RecteUtils.GetColorFromString(PlayerPrefs.GetString("ItemChamsWeaponColor"));
            
                foreach (Pickupable pick in EntityFinding.picks)
                {
                    if (pick.BBJDGGLJCEJ == DHLIPKPGIFP.WeaponDrop)
                    {
                        Renderer[] componentsInChildren = pick.GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < componentsInChildren.Length; j++)
                        {
                            foreach (Material material in componentsInChildren[j].materials)
                            {
                                if (material.color != colorFromString)
                                {
                                    Shader shader = Shader.Find("GUI/Text Shader");
                                    material.shader = shader;
                                    material.renderQueue = 4000;
                                    material.mainTexture = null;
                                    material.color = colorFromString;
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void MatsChams()
        {
            if (PhotonNetwork.InRoom && EntityFinding.picks != null)
            {
                Color colorFromString = RecteUtils.GetColorFromString(PlayerPrefs.GetString("ItemChamsBuildsColor"));
            
                foreach (Pickupable pick in EntityFinding.picks)
                {
                    if (pick.BBJDGGLJCEJ == DHLIPKPGIFP.BuildingAmmo)
                    {
                        Renderer[] componentsInChildren = pick.GetComponentsInChildren<Renderer>();
                        for (int j = 0; j < componentsInChildren.Length; j++)
                        {
                            foreach (Material material in componentsInChildren[j].materials)
                            {
                                if (material.color != colorFromString)
                                {
                                    Shader shader = Shader.Find("GUI/Text Shader");
                                    material.shader = shader;
                                    material.renderQueue = 4000;
                                    material.mainTexture = null;
                                    material.color = colorFromString;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ChestChams()
        {
            if (PhotonNetwork.InRoom && EntityFinding.picks != null)
            {
                Color colorFromString = RecteUtils.GetColorFromString(PlayerPrefs.GetString("ChestChamsColor"));
            
                foreach (SupplyCrate sc in EntityFinding.crates)
                {
                    Renderer[] componentsInChildren = sc.GetComponentsInChildren<Renderer>();
                    for (int j = 0; j < componentsInChildren.Length; j++)
                    {
                        foreach (Material material in componentsInChildren[j].materials)
                        {
                            if (material.color != colorFromString)
                            {
                                Shader shader = Shader.Find("GUI/Text Shader");
                                material.shader = shader;
                                material.renderQueue = 4000;
                                material.mainTexture = null;
                                material.color = colorFromString;
                            }
                        }
                    }
                }
            }
        }

        public void Tracers()
        {
            if (PhotonNetwork.InRoom && EntityFinding.pcs != null)
            {
                Color color = RecteUtils.GetColorFromString(PlayerPrefs.GetString("TracerColor"));

                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfTracers && !pc.IsMine()) || Vars.showSelfTracers)
                        {
                            Vector3 pos = pc.transform.position;
                            Vector3 v = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos);
                            float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            if ((RecteUtils.isOnScreen(v) && !Vars.MaxESPDist) || (RecteUtils.isOnScreen(v) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                RecteUtils.DrawLine(RecteUtils.CenterOfScreen(), RecteUtils.EspMath(v), color, 3f);
                            }
                        }
                    }
                }
            }
        }


        private void Chams()
        {
            if (PhotonNetwork.InRoom)
            {
                Color colorFromString = RecteUtils.GetColorFromString(PlayerPrefs.GetString("ChamsColor"));
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfChams && !pc.IsMine()) || Vars.showSelfChams)
                        {

                            Renderer[] componentsInChildren = pc.GetComponentsInChildren<Renderer>();
                            for (int j = 0; j < componentsInChildren.Length; j++)
                            {
                                foreach (Material material in componentsInChildren[j].materials)
                                {
                                    if (material.color != colorFromString)
                                    {
                                        Shader shader = Shader.Find("GUI/Text Shader");
                                        material.shader = shader;
                                        material.renderQueue = 4000;
                                        material.mainTexture = null;
                                        material.color = colorFromString;
                                    }
                                }
                            }

                        }
                        
                    }
                }
            }

        }

        public static void HeadESP()
        {
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom) {
                Color color = RecteUtils.GetColorFromString(PlayerPrefs.GetString("HeadESPColor"));
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfHeadESP && !pc.IsMine()) || Vars.showSelfHeadESP)
                        {
                            Transform pos = pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                            Vector3 w2s = CameraManager.NFLLAGMKOCA.MainCamera.WorldToScreenPoint(pos.position);
                            float distance = Vector3.Distance(pos.position, PlayerController.LFNGIIPNIDN.transform.position);

                            if ((RecteUtils.isOnScreen(w2s) && !Vars.MaxESPDist) || (RecteUtils.isOnScreen(w2s) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {

                                if (Vars.HeadESPShape == 0)
                                {
                                    RecteUtils.DrawCircle(new Vector2(w2s.x, (float)Screen.height - w2s.y), distance / -80f, 30, color);
                                }
                                if (Vars.HeadESPShape == 1)
                                {
                                    RecteUtils.DrawBox(new Vector2(w2s.x, (float)Screen.height - w2s.y), new Vector2(distance / -80f, distance / -80f), 3f, color);
                                }
                                if (Vars.HeadESPShape == 2)
                                {
                                    RecteUtils.CornerBox(new Vector2(w2s.x, (float)Screen.height - w2s.y), distance / -80f, distance / -80f, 3f, color, true);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public void HealthBars()
        {
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                Color hpbg = RecteUtils.GetColorFromString("ee99a0");
                Color shd = RecteUtils.GetColorFromString("8aadf4");
                Color shdbg = RecteUtils.GetColorFromString("363a4f");
                foreach (PlayerController pc in  EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfHealth && !pc.IsMine()) || Vars.showSelfHealth)
                        {
                            Vector3 pos = pc.transform.position;
                            Vector3 w2s = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos);
                            Vector3 w2s1 = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);
                            Vector3 w2s2 = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightFoot).position);
                            float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            if ((RecteUtils.isOnScreen(w2s) && !Vars.MaxESPDist) || (RecteUtils.isOnScreen(w2s) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                {
                                    Vector3 v = (w2s1 + w2s2) / 2f;
                                    float health = (float)pc.ABDABPEKBFM.CHFPIGOLOOB;
                                    float maxHealth = (float)pc.ABDABPEKBFM.MDFJCJDNLKI;
                                    float shield = (float)pc.ABDABPEKBFM.OLLCMEJHPIL;
                                    float maxShield = (float)pc.ABDABPEKBFM.INEKOEGBMFO;
                                    Color hp = RecteUtils.hpColor(health);
                                    if (Vars.HPBoxLocation == 1)
                                    {
                                        if (Vars.BoxOffset == 1)
                                        {
                                            RecteUtils.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 9f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, true);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                RecteUtils.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 18f), new Vector2((shield * maxShield) / maxShield, 5f), shd, true);
                                            }
                                        }
                                        if (Vars.BoxOffset == 2)
                                        {
                                            RecteUtils.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 9f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, false);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                RecteUtils.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 18f), new Vector2((shield * maxShield) / maxShield, 5f), shd, false);
                                            }
                                        }
                                    }
                                    if (Vars.HPBoxLocation == 2)
                                    {
                                        if (Vars.BoxOffset == 1)
                                        {
                                            RecteUtils.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 18f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, true);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                RecteUtils.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 9f), new Vector2((shield * maxShield) / maxShield, 5f), shd, true);
                                            }
                                        }
                                        if (Vars.BoxOffset == 2)
                                        {
                                            RecteUtils.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 18f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, false);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                RecteUtils.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 9f), new Vector2((shield * maxShield) / maxShield, 5f), shd, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void TargetLine()
        {
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                List<PlayerController> pList = EntityFinding.GetTarget();
                UnityEngine.Component component = pList.FirstOrDefault<PlayerController>();
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {

                    if (pc.gameObject == component.gameObject)
                    {
                        Vector3 w2s = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.transform.position);
                        if (RecteUtils.isOnScreen(w2s))
                        {
                            RecteUtils.DrawLine(RecteUtils.CenterOfScreen(), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 4f);
                        }
                    }
                }
            }
        }
       

        public void OnGUI()
        {
            if (Vars.TargetLine && PhotonNetwork.InRoom)
            {
                TargetLine();
            }
            if (Vars.RGBOverlay && PhotonNetwork.InRoom)
            {
                hue += 0.0007f;
                if (hue >= 1f)
                {
                    hue = 0f;
                }
                sat = .35f;
                bri = 1f;
                Color color = Color.HSVToRGB(hue, Vars.RGBOverlaySaturation, bri);
                PlayerController.LFNGIIPNIDN.AIACBMLLLFE.ToggleRendererOutline(color);
                PlayerController.LFNGIIPNIDN.OGGBDFMFBIB.ToggleRendererRimOutline(color);
            }
            if (Vars.Skeleton && PhotonNetwork.InRoom)
            {
                Skeleton();
            }

            if (Vars.Nametags && PhotonNetwork.InRoom)
            {
                StringESP();
            }
            if (Vars.CornerBox && PhotonNetwork.InRoom)
            {
                CornerBox();
            }
            if (Vars.OutlineBox || Vars.FilledBox && PhotonNetwork.InRoom)
            {
                BoxESP();
            }

            if (Vars.AmmoChams && PhotonNetwork.InRoom)
            {
                AmmoChams();
            }
            if (Vars.WeaponChams && PhotonNetwork.InRoom)
            {
                WeaponsChams();
            }
            if (Vars.MatsChams && PhotonNetwork.InRoom)
            {
                MatsChams();
            }
            if (Vars.ChestChams && PhotonNetwork.InRoom)
            {
                ChestChams();
            }
            if (Vars.HeadESP && PhotonNetwork.InRoom)
            {
                HeadESP();
            }

            if (Vars.Tracers && PhotonNetwork.InRoom)
            {
                Tracers();
            }
            
            if (Vars.Chams && PhotonNetwork.InRoom)
            {
                Chams();
            }
           
           
            if (Vars.HealthBar && PhotonNetwork.InRoom)
            {
                HealthBars();
            }
            if (Vars.RecteESP && PhotonNetwork.InRoom)
            {
                foreach (PlayerController p in EntityFinding.pcs.Values)
                {
                    if ((string)p.GetComponent<PhotonView>().Controller.CustomProperties["ImLiterallyAboutToNutWithRecte"] == "t")
                    {
                        if ((!Vars.showSelfRecteESP && !p.IsMine()) || Vars.showSelfRecteESP)
                        {
                            Vector3 w2s = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(p.transform.position);
                            float y = (float)Screen.height + w2s.y + 24f;
                            if (RecteUtils.isOnScreen(w2s))
                            {
                                RecteUtils.DrawText(new Vector2(w2s.x, y), "Recte User", Color.black, true);
                            }
                        }
                    }
                }
            }
            if (PlayerPrefsX.GetBool("MonowareESP") && PhotonNetwork.InRoom)
            {
                foreach (PlayerController p in EntityFinding.pcs.Values)
                {
                    if ((string)p.GetComponent<PhotonView>().Controller.CustomProperties["monoware"] == "monoware")
                    {
                        if (!p.IsMine())
                        {
                            Vector3 w2s = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(p.transform.position);
                            float y = (float)Screen.height + w2s.y + 24f;
                            if (RecteUtils.isOnScreen(w2s))
                            {
                                RecteUtils.DrawText(new Vector2(w2s.x, y), "Monoware User", Color.black, true);
                            }
                        }
                    }
                }
            }
        }
    }
}
