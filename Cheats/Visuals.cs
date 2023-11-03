using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UIElements;

namespace Recte.Cheats
{
    public class Visuals : MonoBehaviour
    {
        public float hue;
        public float sat;
        public float bri;
        public void Skeleton()
        {

            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                Color color = Utils.GetColorFromString(Vars.SkeletonColor);
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {

                    if (pc != null)
                    {
                        Animator component = pc.GetComponent<Animator>();
                        PhotonView photonView = component.GetComponent<PhotonView>();

                        float distance = Vector3.Distance(component.transform.position, PlayerController.LFNGIIPNIDN.transform.position);

                        if ((!Vars.showSelfSkeleton && !pc.IsMine()) || Vars.showSelfSkeleton && photonView != null)
                        {
                            Vector3 leftShoulder = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftShoulder).transform.position);
                            Vector3 rightShoulder = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightShoulder).transform.position);

                            Vector3 leftUpperArm = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftUpperArm).transform.position);
                            Vector3 rightUpperArm = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightUpperArm).transform.position);

                            Vector3 leftLowerArm = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftLowerArm).transform.position);
                            Vector3 rightLowerArm = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightLowerArm).transform.position);

                            Vector3 spine = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Spine).transform.position);
                            Vector3 neck = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Neck).transform.position);

                            Vector3 head = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Head).transform.position);
                            Vector3 jaw = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Jaw).transform.position);
                            Vector3 hips = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.Hips).transform.position);

                            Vector3 rightUpperLeg = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightUpperLeg).transform.position);
                            Vector3 leftUpperLeg = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftUpperLeg).transform.position);

                            Vector3 rightLowerLeg = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightLowerLeg).transform.position);
                            Vector3 leftLowerLeg = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftLowerLeg).transform.position);

                            Vector3 rightFoot = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.RightFoot).transform.position);
                            Vector3 leftFoot = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(component.GetBoneTransform(HumanBodyBones.LeftFoot).transform.position);

                            if (Utils.isOnScreen(pc.transform.position))
                            {
                                Render.DrawNonESPLine(Render.EspMath(jaw), Render.EspMath(head), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(head), Render.EspMath(spine), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(spine), Render.EspMath(hips), color, 2f);

                                Render.DrawNonESPLine(Render.EspMath(neck), Render.EspMath(rightShoulder), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(neck), Render.EspMath(leftShoulder), color, 2f);

                                Render.DrawNonESPLine(Render.EspMath(rightShoulder), Render.EspMath(rightUpperArm), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(leftShoulder), Render.EspMath(leftUpperArm), color, 2f);

                                Render.DrawNonESPLine(Render.EspMath(rightUpperArm), Render.EspMath(rightLowerArm), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(leftUpperArm), Render.EspMath(leftLowerArm), color, 2f);

                                Render.DrawNonESPLine(Render.EspMath(hips), Render.EspMath(rightUpperLeg), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(hips), Render.EspMath(leftUpperLeg), color, 2f);

                                Render.DrawNonESPLine(Render.EspMath(rightUpperLeg), Render.EspMath(rightLowerLeg), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(leftUpperLeg), Render.EspMath(leftLowerLeg), color, 2f);

                                Render.DrawNonESPLine(Render.EspMath(rightLowerLeg), Render.EspMath(rightFoot), color, 2f);
                                Render.DrawNonESPLine(Render.EspMath(leftLowerLeg), Render.EspMath(leftFoot), color, 2f);
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
                                Render.DrawText(new Vector2(camPos.x, (float)Screen.height - camPos.y), $"<b><color=#080>{name}\n</color></b>", Color.black, true);
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
                Color color = Utils.GetColorFromString(Vars.CornerBoxColor);
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfCorner && !pc.IsMine()) || Vars.showSelfCorner)
                        {
                            Vector3 pos = pc.transform.position;
                            Vector3 vector = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos + new Vector3(0f, 0.1f, 0f));
                            Vector3 vector2 = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos + new Vector3(0f, 1.5f, 0f));
                            float num4 = Mathf.Abs(vector2.y - vector.y);
                            float num5 = (float)Screen.height - vector2.y;
                             float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            
                            if ((vector2.z > 0 && !Vars.MaxESPDist) || (vector2.z > 0 && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                //Render.CornerBox(new Vector2(head.x, (float)Screen.height - head.y - 20f), num2 - 20f, num2 + 20f, 2f, color, true);
                                Render.CornerBox(new Vector2(vector2.x, num5), num4 / 2f, num4, 2f, color, true);
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
                Color color = Utils.GetColorFromString(Vars.BoxFilledESPColor);
                Color color1 = Utils.GetColorFromString(Vars.BoxOutlineESPColor);
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
                        if ((Utils.isOnScreen(vector) && Utils.isOnScreen(vector2) && !Vars.MaxESPDist) || (Utils.isOnScreen(vector) && Utils.isOnScreen(vector2) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                        {

                            if (Vars.FilledBox)
                            {
                                if ((!Vars.showSelfFilled && !pc.IsMine()) || Vars.showSelfFilled)
                                {
                                    Render.RectFilled(new Vector2(vector2.x, (float)Screen.height - vector2.y - (height / 2f)), new Vector2(num / 2f, height), color, true);
                                }
                            }

                            if (Vars.OutlineBox)
                            {
                                if ((!Vars.showSelfOutline && !pc.IsMine()) || Vars.showSelfOutline)
                                {
                                    Render.DrawBox(new Vector2(vector2.x, (float)Screen.height - vector2.y - (height / 2f)), new Vector2(num / 2f, height), 2f, color1, true);
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
                Color colorFromString = Utils.GetColorFromString(Vars.ItemChamsAmmoColor);

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
                Color colorFromString = Utils.GetColorFromString(Vars.ItemChamsWeaponColor);

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
                Color colorFromString = Utils.GetColorFromString(Vars.ItemChamsBuildsColor);

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
                Color colorFromString = Utils.GetColorFromString(Vars.ChestChamsColor);

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
                Color color = Utils.GetColorFromString(Vars.TracerColor);

                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfTracers && !pc.IsMine()) || Vars.showSelfTracers)
                        {
                            Vector3 pos = pc.transform.position;
                            Vector3 v = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos + new Vector3(0f, 1.2f, 0f));
                            float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            if ((Utils.isOnScreen(v) && !Vars.MaxESPDist) || (Utils.isOnScreen(v) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                Render.DrawLine(Utils.CenterOfScreen(), Render.EspMath(v), color, 3f);
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
                Color colorFromString = Utils.GetColorFromString(Vars.ChamsColor);
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
            if (EntityFinding.pcs != null && PhotonNetwork.InRoom)
            {
                Color color = Utils.GetColorFromString(Vars.HeadESPColor);
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfHeadESP && !pc.IsMine()) || Vars.showSelfHeadESP)
                        {
                            Transform pos = pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                            Vector3 w2s = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos.position);
                            float distance = Vector3.Distance(pos.position, PlayerController.LFNGIIPNIDN.transform.position);

                            if ((Utils.isOnScreen(w2s) && !Vars.MaxESPDist) || (Utils.isOnScreen(w2s) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {

                                if (Vars.HeadESPShape == 0)
                                {
                                    Render.DrawCircle(new Vector2(w2s.x, (float)Screen.height - w2s.y), distance / -80f, 30, color);
                                }
                                if (Vars.HeadESPShape == 1)
                                {
                                    Render.DrawBox(new Vector2(w2s.x, (float)Screen.height - w2s.y), new Vector2(distance / -80f, distance / -80f), 3f, color);
                                }
                                if (Vars.HeadESPShape == 2)
                                {
                                    Render.CornerBox(new Vector2(w2s.x, (float)Screen.height - w2s.y), distance / -80f, distance / -80f, 3f, color, true);
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
                Color hpbg = Utils.GetColorFromString("ee99a0");
                Color shd = Utils.GetColorFromString("8aadf4");
                Color shdbg = Utils.GetColorFromString("363a4f");
                foreach (PlayerController pc in EntityFinding.pcs.Values)
                {
                    if (pc != null)
                    {
                        if ((!Vars.showSelfHealth && !pc.IsMine()) || Vars.showSelfHealth)
                        {
                            Vector3 pos = pc.transform.position;
                            Vector3 w2s = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pos + new Vector3(0f, 1.2f, 0f));
                            Vector3 w2s1 = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);
                            Vector3 w2s2 = CameraManager.NFLLAGMKOCA.TPCamera.gimmiCameraThx().WorldToScreenPoint(pc.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightFoot).position);
                            float distance = Vector3.Distance(pos, PlayerController.LFNGIIPNIDN.transform.position);
                            if ((Utils.isOnScreen(w2s) && !Vars.MaxESPDist) || (Utils.isOnScreen(w2s) && distance <= Vars.MaxESPDistance && Vars.MaxESPDist))
                            {
                                {
                                    Vector3 v = (w2s1 + w2s2) / 2f;
                                    float health = (float)pc.ABDABPEKBFM.CHFPIGOLOOB;
                                    float maxHealth = (float)pc.ABDABPEKBFM.MDFJCJDNLKI;
                                    float shield = (float)pc.ABDABPEKBFM.OLLCMEJHPIL;
                                    float maxShield = (float)pc.ABDABPEKBFM.INEKOEGBMFO;
                                    Color hp = Render.hpColor(health);
                                    if (Vars.HPBoxLocation == 1)
                                    {
                                        if (Vars.BoxOffset == 1)
                                        {
                                            Render.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 9f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, true);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                Render.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 18f), new Vector2((shield * maxShield) / maxShield, 5f), shd, true);
                                            }
                                        }
                                        if (Vars.BoxOffset == 2)
                                        {
                                            Render.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 9f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, false);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                Render.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 18f), new Vector2((shield * maxShield) / maxShield, 5f), shd, false);
                                            }
                                        }
                                    }
                                    if (Vars.HPBoxLocation == 2)
                                    {
                                        if (Vars.BoxOffset == 1)
                                        {
                                            Render.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 18f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, true);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                Render.RectFilled(new Vector2(v.x, (float)Screen.height - w2s.y - 9f), new Vector2((shield * maxShield) / maxShield, 5f), shd, true);
                                            }
                                        }
                                        if (Vars.BoxOffset == 2)
                                        {
                                            Render.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 18f), new Vector2((health * maxHealth) / maxHealth, 5f), hp, false);
                                            if (shield >= 1f || Vars.ShieldBoxCheck)
                                            {
                                                Render.RectFilled(new Vector2(w2s.x, (float)Screen.height - w2s.y - 9f), new Vector2((shield * maxShield) / maxShield, 5f), shd, false);
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
                        if (Utils.isOnScreen(w2s))
                        {
                            Render.DrawLine(Utils.CenterOfScreen(), new Vector2(w2s.x, (float)Screen.height - w2s.y), Color.red, 4f);
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
                            if (Utils.isOnScreen(w2s))
                            {
                                Render.DrawText(new Vector2(w2s.x, y), "Recte User", Color.black, true);
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
                            if (Utils.isOnScreen(w2s))
                            {
                                Render.DrawText(new Vector2(w2s.x, y), "Monoware User", Color.black, true);
                            }
                        }
                    }
                }
            }
        }
    }
}
