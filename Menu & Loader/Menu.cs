using JustPlay.Localization;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Networking;
using Recte_1v1lol.Cheats;
using Recte_1v1lol.Utils;
using System.IO;
using System.Security.Cryptography;
using Photon.Realtime;
using Assets.Scripts;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
using Invector.CharacterController;
using UnityEngine.UI;

namespace Recte_1v1lol
{
    internal class Menu : MonoBehaviour
    {
        public static float hue;
        public static float sat;
        public static float bri;
        public static string version;
        public static Vector2 ScrollPos8;
        public static string renameName;
        public static int LoadError;
        public static Rect rect2;
        public static Rect TitleRect;
        public static float scrollWidth;
        public static Vector2 scrollPos1;
        private Vector2 scrollPos;
        public static Rect curRect;
        public static float curWidth;
        private Rect rect1;
        public static string ids;
        public Rect mainMenu;
        public bool isOn;
        public static string[] WeaponNames = new string[]
        {
            "lol.1v1.weapons.shotgun_br_",
            "lol.1v1.weapons.9mmpistol_br_",
            "lol.1v1.weapons.tacitcalsmg_br_",
            "lol.1v1.weapons.shorty_br_",
            "lol.1v1.weapons.microsmg_br_",
            "lol.1v1.weapons.ak_br_",
            "lol.1v1.weapons.autosniper_br_",
            "lol.1v1.weapons.sniper_br_",
            "lol.1v1.weapons.doublebarrelshotgun_br_",
            "lol.1v1.weapons.autopistol_br_"
        };


        public static void BoolToggle(Rect rect, Vector2 togSize, ref bool toggle, string text)
        {
            Texture2D bg = new Texture2D(1, 1);
            bg.SetPixel(0, 0, RecteUtils.GetColorFromString("1e2030"));
            bg.Apply();
            Texture2D en = new Texture2D(1, 1);
            en.SetPixel(0, 0, RecteUtils.GetColorFromString("a6da95"));
            en.Apply();
            Texture2D dis = new Texture2D(1, 1);
            dis.SetPixel(0, 0, RecteUtils.GetColorFromString("ee99a0"));
            dis.Apply();
            if (GUI.Button(rect, null, GUIStyle.none))
            {
                toggle = !toggle;
            }
            RecteUtils.DrawRoundedTex(new Vector2(rect.x, rect.y), new Vector2(rect.width, rect.height), bg, 25);
            if (toggle)
            {
                RecteUtils.DrawRoundedTex(new Vector2(rect.x + rect.width - 25f, rect.y + 5f), togSize, en, 25);
            }
            else
            {
                RecteUtils.DrawRoundedTex(new Vector2(rect.x + 5f, rect.y + 5f), togSize, dis, 25);
            }
            GUI.Label(new Rect(rect.x + rect.width + 10f, rect.y, 200f, 20f), text);
        }
        private void Toggle(Vector2 position, Vector2 size, ref bool toggle, int radius, Color startColor, Color targetColor, Color hoverOn, Color hoverOff)
        {
            if (GUI.Button(new Rect(position.x, position.y, size.x, size.y), GUIContent.none, GUIStyle.none))
            {
                toggle = !toggle;
            }

            if (toggle)
            {

                RecteUtils.DrawRoundedTex(new Vector2(position.x - 2f, position.y - 2f), new Vector2(size.x + 4f, size.y + 4f), new Color(targetColor.r, targetColor.g, targetColor.b, .45f), radius);
                RecteUtils.DrawRoundedTex(position, size, targetColor, radius);
                if (new Rect(position.x, position.y, size.x, size.y).Contains(Event.current.mousePosition))
                {
                    RecteUtils.DrawRoundedTex(new Vector2(position.x - 2f, position.y - 2f), new Vector2(size.x + 4f, size.y + 4f), new Color(hoverOn.r, hoverOn.g, hoverOn.b, .45f), radius);
                    RecteUtils.DrawRoundedTex(position, size, hoverOn, radius);
                }
            }
            else
            {
                RecteUtils.DrawRoundedTex(new Vector2(position.x - 2f, position.y - 2f), new Vector2(size.x + 4f, size.y + 4f), new Color(startColor.r, startColor.g, startColor.b, .45f), radius);
                RecteUtils.DrawRoundedTex(position, size, startColor, radius);
                if (new Rect(position.x, position.y, size.x, size.y).Contains(Event.current.mousePosition))
                {
                    RecteUtils.DrawRoundedTex(new Vector2(position.x - 2f, position.y - 2f), new Vector2(size.x + 4f, size.y + 4f), new Color(hoverOff.r, hoverOff.g, hoverOff.b, .45f), radius);
                    RecteUtils.DrawRoundedTex(position, size, hoverOff, radius);
                }
            }
        
            
        }


        public float SliderMath(float float1, float float2, float float3)
        {
            float num = float3 - float2;
            float num2 = (float1 - float2) / num;
            if (num2 < 0f)
            {
                num2 = 0f;
            }
            else if (num2 > 1f)
            {
                num2 = 1f;
            }
            return num2;
        }
        private void OnGUI()
        {
            /*
            if (PhotonNetwork.InRoom && EntityFinding.pcs != null && Camera.main != null && SceneManager.GetActiveScene().name != "MainMenu" && PlayerPrefsX.GetBool("CustomSkybox"))
            {
                Color color = RecteUtils.GetColorFromString(PlayerPrefs.GetString("SkyboxColor1", "87cefa"));
                Color color1 = RecteUtils.GetColorFromString(PlayerPrefs.GetString("SkyboxColor2", "ff69b4"));
                foreach (Camera camera in UnityEngine.Object.FindObjectsOfType<Camera>())
                {
                    if (camera.clearFlags != CameraClearFlags.Color)
                    {
                        camera.backgroundColor = Color.Lerp(color, color1, Mathf.PingPong(Time.time, 0.75f));
                        camera.clearFlags = CameraClearFlags.Color;
                    }
                }
            }
            if (PhotonNetwork.InRoom && EntityFinding.pcs != null && Camera.main != null && SceneManager.GetActiveScene().name != "MainMenu" && !PlayerPrefsX.GetBool("CustomSkybox"))
            {
                foreach (Camera camera in UnityEngine.Object.FindObjectsOfType<Camera>())
                {
                    if(camera.clearFlags != CameraClearFlags.Color)
                    {
                        camera.clearFlags = CameraClearFlags.Skybox;
                    }
                }
            }*/
            GUI.backgroundColor = RecteUtils.GetColorFromString("24273a");
            if (PlayerPrefsX.GetBool("Watermark", true))
            {
                Watermark();
            }
            
            if (!string.Equals(version, RecteUtils.Version, StringComparison.OrdinalIgnoreCase) && version != string.Empty)
            {
                RecteUtils.DrawText(RecteUtils.CenterOfScreen(), "Client Needs Update! Get It At .gg/DZZ8cXTjG6", Color.white, Color.black, true, 12, FontStyle.Bold, 1);
            }
            if (Vars.MenuStyle == 0)
            {
                if (Vars.open && PlayerPrefs.GetInt("Banned") != 1)
                {
                    mainMenu = GUI.Window(0, mainMenu, new GUI.WindowFunction(FakeWindow), "", GUIStyle.none);

                    Color tabSelect = RecteUtils.GetColorFromString("5b6078");
                    Color tabUnSelect = RecteUtils.GetColorFromString("494d64");
                    Color hover = RecteUtils.GetColorFromString("6e738d");
                    //Base Of GUI
                    RecteUtils.DrawRoundedTex(new Vector2(mainMenu.x, mainMenu.y), new Vector2(mainMenu.width, 475f), RecteUtils.GetColorFromString("24273a"), 15);
                    RecteUtils.DrawRoundedTex(new Vector2(mainMenu.x, mainMenu.y), new Vector2(mainMenu.width, 35f), RecteUtils.GetColorFromString("1e2030"), 15);
                    RecteUtils.DrawText(new Vector2(mainMenu.x + mainMenu.width / 2f - 15f, mainMenu.y + 5f), "<color=#cad3f5>Recte</color>", Color.black, true, 22, FontStyle.Bold, 0);

                    //Tab Selector
                    Rect tabRect = new Rect(mainMenu.xMin, mainMenu.yMin + 35f, 125f, 440f);
                    RecteUtils.DrawRoundedTex(new Vector2(mainMenu.xMin, mainMenu.yMin + 35f), new Vector2(125f, 440f), RecteUtils.GetColorFromString("363a4f"), 15);


                    //Render Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 25f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 0;
                    }
                    if (Vars.selectedTab == 0 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 25f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 25f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 0 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 25f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 25f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 25f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 25f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 35f), "Visuals", Color.black, true, 14, FontStyle.Bold, 0);

                    //Combat Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 75f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 1;
                    }
                    if (Vars.selectedTab == 1 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 75f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 75f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 1 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 75f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 75f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 75f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 75f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 85f), "Combat", Color.black, true, 14, FontStyle.Bold, 0);
                    

                    //Misc Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 125f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 2;
                    }

                    if (Vars.selectedTab == 2 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 125f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 125f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 2 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 125f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 125f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 125f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 125f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 135f), "Misc.", Color.black, true, 14, FontStyle.Bold, 0);

                    //Player List Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 175f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 3;
                    }


                    if (Vars.selectedTab == 3 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 175f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 175f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 3 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 175f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 175f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 175f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 175f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 185f), "Player List", Color.black, true, 14, FontStyle.Bold, 0);

                    //Client Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 225f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 4;
                    }

                    if (Vars.selectedTab == 4 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 225f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 225f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 4 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 225f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 225f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 225f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 225f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 235f), "Client", Color.black, true, 14, FontStyle.Bold, 0);

                    //Keybinds Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 275f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 5;
                    }
                    if (Vars.selectedTab == 5 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 275f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 275f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 5 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 275f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 275f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 275f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 275f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 285f), "Keybinds", Color.black, true, 14, FontStyle.Bold, 0);

                    //Config Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 325f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 6;
                    }
                    if (Vars.selectedTab == 6 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 325f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 325f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 6 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 325f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 325f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 325f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 325f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin +60f, tabRect.yMin + 335f), "Config", Color.black, true, 14, FontStyle.Bold, 0);

                    //Console Button
                    if (GUI.Button(new Rect(tabRect.xMin + 15f, tabRect.yMin + 375f, tabRect.width - 30f, 35f), new GUIContent(""), GUIStyle.none))
                    {
                        Vars.selectedTab = 7;
                    }
                    if (Vars.selectedTab == 7 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 375f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 375f), new Vector2(tabRect.width - 30f, 35f), tabSelect, 15);
                    }
                    if (Vars.selectedTab != 7 && !new Rect(tabRect.xMin + 15f, tabRect.yMin + 375f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                    {
                        RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 375f), new Vector2(tabRect.width - 30f, 35f), tabUnSelect, 15);
                    }
                    else
                    {
                        if (new Rect(tabRect.xMin + 15f, tabRect.yMin + 375f, tabRect.width - 30f, 35f).Contains(Event.current.mousePosition))
                        {
                            RecteUtils.DrawRoundedTex(new Vector2(tabRect.xMin + 15f, tabRect.yMin + 375f), new Vector2(tabRect.width - 30f, 35f), hover, 15);
                        }
                    }
                    RecteUtils.DrawText(new Vector2(tabRect.xMin + 60f, tabRect.yMin + 385f), "Console", Color.black, true, 14, FontStyle.Bold, 0);

                    //Combat Window
                    //(mainMenu.xMin, mainMenu.yMin + 35f, 175f
                    Rect windowRect = new Rect(mainMenu.xMin + 175f, mainMenu.yMin + 35f, mainMenu.width - 175f, 475f);


                    if (Vars.selectedTab == 0)
                    {
                        Visuals();
                    }
                    if (Vars.selectedTab == 1)
                    {
                        newWeapons();
                    }

                }
            }
            if (Vars.MenuStyle == 1)
            {
                curRect = rect1;
                scrollWidth = curRect.width / 2.261f;
                curWidth = curRect.width / 2.165f;
                if (Vars.open && PlayerPrefs.GetInt("Banned") != 1)
                {
                    rect1 = GUILayout.Window(
                        0,
                        rect1,
                        new GUI.WindowFunction(MenuWindow),
                        $"<b>Recte {RecteUtils.Version} Beta</b>",
                        new GUILayoutOption[0]
                    );
                }
            }
            if (Vars.MenuStyle == 2)
            {
                if (Vars.open && PlayerPrefs.GetInt("Banned") != 1)
                {

                    TitleRect = GUILayout.Window(888, TitleRect, new GUI.WindowFunction(MenuWindow1), "<b>Menus:</b>", new GUILayoutOption[0]);
                    rect2 = GUILayout.Window(889, rect2, new GUI.WindowFunction(MenuWindow2), $"<b>Recte {RecteUtils.Version} Beta</b>", new GUILayoutOption[0]);
                    if (rect2.x - TitleRect.x != 247.2727f)
                    {
                        TitleRect.x = rect2.x - 247.2727f;
                        rect2.x = TitleRect.x + 247.2727f;
                    }
                    if (rect2.y - TitleRect.y != 0f)
                    {
                        TitleRect.y = rect2.y;
                    }
                    if (TitleRect.height != rect2.height)
                    {
                        rect2.height = TitleRect.height;
                    }
                    curRect = rect2;


                    //Small = Bigger, Big = Smaller
                    scrollWidth = curRect.width / 2.207f;
                    curWidth = curRect.width / 2.207f;
                }
            }
        }
        public void FakeWindow(int wID)
        {
            GUI.DragWindow();
        }
        public Vector2 scrollVieww;
        public void Visuals()
        {
            Rect windowRect = new Rect(mainMenu.xMin + 125f, mainMenu.yMin + 35f, mainMenu.width - 175f, 475f);
            
            //Rainbow Overlay
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 15f, windowRect.y + 15f), new Vector2(225f, 100f), RecteUtils.GetColorFromString("181926"), 15);
            Toggle(new Vector2(windowRect.x + 27f, windowRect.y + 22.5f), new Vector2(35f, 35f), ref Vars.RGBOverlay, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f, windowRect.y + 30f), "RGB Overlay", Color.black, true, 18, FontStyle.Bold, 0);
            RecteUtils.DrawText(new Vector2(windowRect.x + 130f, windowRect.y + 60), $"<color=cad3f5>RGB Saturation: {Math.Round((double)Vars.RGBOverlaySaturation, 2)}</color>", Color.black, true, 14, FontStyle.Bold, 0);
            Vars.RGBOverlaySaturation = GUI.HorizontalSlider(new Rect(windowRect.x + 75f, windowRect.y + 90f, 105f, 25f), Vars.RGBOverlaySaturation, .1f, 1f, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 75f, windowRect.y + 90f), new Vector2(120f, 15f), RecteUtils.GetColorFromString("363a4f"), 45);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 75f + SliderMath(Vars.RGBOverlaySaturation * 250f, 25f, 550) * 248f, windowRect.y + 89f), new Vector2(16f, 17f), RecteUtils.GetColorFromString("7dc4e4"), 45);

            //Max ESP Distance
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30 + 225f, windowRect.y + 15f), new Vector2(225f, 100f), RecteUtils.GetColorFromString("181926"), 15);
            Toggle(new Vector2(windowRect.x + 30f + 225f + 12f, windowRect.y + 22.5f), new Vector2(35f, 35f), ref Vars.MaxESPDist, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 225f + 135f, windowRect.y + 30f), "Max ESP Distance", Color.black, true, 18, FontStyle.Bold, 0);
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 225f + 115f, windowRect.y + 60), $"<color=cad3f5>Max ESP Distance: {Mathf.Round(Vars.MaxESPDistance)}</color>", Color.black, true, 14, FontStyle.Bold, 0);
            Vars.MaxESPDistance = GUI.HorizontalSlider(new Rect(windowRect.x + 30f + 225f + 55f, windowRect.y + 90f, 105f, 25f), Vars.MaxESPDistance, 25f, 350f, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30f + 225f + 55f, windowRect.y + 90f), new Vector2(115f, 15f), RecteUtils.GetColorFromString("363a4f"), 45);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30f + 225f + 55f + SliderMath(Vars.MaxESPDistance, 25f, 350f) * 100f, windowRect.y + 89f), new Vector2(16f, 17f), RecteUtils.GetColorFromString("7dc4e4"), 45);

            //Nametags - String ESP
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 15f, windowRect.y + 130f), new Vector2(225f, 75f), RecteUtils.GetColorFromString("181926"), 15);
            Toggle(new Vector2(windowRect.x + 27f, windowRect.y + 22.5f + 115f), new Vector2(35f, 35f), ref Vars.Nametags, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f, windowRect.y + 30f + 115f), "Nametags", Color.black, true, 18, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 35f + 35f, windowRect.y + 22.5f + 155f), new Vector2(20f, 20f), ref Vars.showSelfNametags, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f, windowRect.y + 30f + 150f), "Show Self", Color.black, true, 12, FontStyle.Bold, 0);

            //Health Bars
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30 + 225f, windowRect.y + 130f), new Vector2(225f, 185f), RecteUtils.GetColorFromString("181926"), 15);
            Toggle(new Vector2(windowRect.x + 30f + 225 + 12f, windowRect.y + 22.5f + 115f), new Vector2(35f, 35f), ref Vars.HealthBar, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 225f + 15f, windowRect.y + 30f + 115f), "Health Bars", Color.black, true, 18, FontStyle.Bold, 0);

            //Show Self
            Toggle(new Vector2(windowRect.x + 30f + 225f + 12f + 43f, windowRect.y + 22.5f + 115f + 35f), new Vector2(20f, 20f), ref Vars.showSelfHealth, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 225f + 15f, windowRect.y + 30f + 115f + 35f), "Show Self", Color.black, true, 12, FontStyle.Bold, 0);

            //Shield Check
            Toggle(new Vector2(windowRect.x + 30f + 225f + 12f + 43f, windowRect.y + 22.5f + 115f + 65f), new Vector2(20f, 20f), ref Vars.ShieldBoxCheck, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 225f + 15f, windowRect.y + 30f + 115f + 65f), "Shiled Check", Color.black, true, 12, FontStyle.Bold, 0);

            //Bar Position
            if (!new Rect(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 205f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 205f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 205f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 205f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 205f, 30f, 30f), new GUIContent(""), GUIStyle.none))
            {
                Vars.HPBoxLocation = RecteUtils.tabSystem(0, 3, Vars.HPBoxLocation, -1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 130f + 34f, windowRect.y + 30f + 213f), "<", Color.black);
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 210f + 25f, windowRect.y + 30f + 213f), $"Box Location: {Vars.HPBoxLocation}", Color.black, true, 13, FontStyle.Bold, 0);
            if (!new Rect(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 205f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 205f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 205f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 205f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 205f, 30f, 30f), new GUIContent(""), GUIStyle.none))
            {
                Vars.HPBoxLocation = RecteUtils.tabSystem(0, 3, Vars.HPBoxLocation, 1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 270f + 36f, windowRect.y + 30f + 213f), ">", Color.black);

            //Bar Offset
            if (!new Rect(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 245f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 245f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 245f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 245f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 135f + 130f + 22.5f, windowRect.y + 30f + 245f, 30f, 30f), new GUIContent(""), GUIStyle.none))
            {
                Vars.BoxOffset = RecteUtils.tabSystem(1, 2, Vars.BoxOffset, -1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 130f + 34f, windowRect.y + 30f + 253f), "<", Color.black);
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 210f + 25f, windowRect.y + 30f + 253f), $"Box Offset: {Vars.BoxOffset}", Color.black, true, 13, FontStyle.Bold, 0);
            if (!new Rect(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 250f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 245f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 245f, 30f, 30).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 245f), new Vector2(30f, 30f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 135f + 270f + 25f, windowRect.y + 30f + 245f, 30f, 30f), new GUIContent(""), GUIStyle.none))
            {
                Vars.BoxOffset = RecteUtils.tabSystem(1, 2, Vars.BoxOffset, 1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 270f + 36f, windowRect.y + 30f + 253f), ">", Color.black);


            //Box ESP
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 15f, windowRect.y + 221f), new Vector2(225f, 210f), RecteUtils.GetColorFromString("181926"), 15);
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f, windowRect.y + 30f + 200f), "Boxes", Color.black, true, 20, FontStyle.Bold, 0);

            //Corner Box
            RecteUtils.DrawText(new Vector2(windowRect.x + 140f, windowRect.y + 30f + 240f), "Corner Boxes", Color.black, true, 12, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 70f, windowRect.y + 30f + 240f), new Vector2(20f, 20f), ref Vars.CornerBox, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            RecteUtils.DrawText(new Vector2(windowRect.x + 140f, windowRect.y + 30f + 265f), "Show Self", Color.black, true, 10, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 90f, windowRect.y + 30f + 265f), new Vector2(16f, 16f), ref Vars.showSelfCorner, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Outline
            RecteUtils.DrawText(new Vector2(windowRect.x + 140f, windowRect.y + 30f + 295f), "Outline Boxes", Color.black, true, 12, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 70f, windowRect.y + 30f + 295f), new Vector2(20f, 20f), ref Vars.OutlineBox, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            RecteUtils.DrawText(new Vector2(windowRect.x + 140f, windowRect.y + 30f + 320f), "Show Self", Color.black, true, 10, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 90f, windowRect.y + 30f + 320f), new Vector2(16f, 16f), ref Vars.showSelfOutline, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Filled Boxes
            RecteUtils.DrawText(new Vector2(windowRect.x + 140f, windowRect.y + 30f + 350f), "Filled Boxes", Color.black, true, 12, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 70f, windowRect.y + 30f + 350f), new Vector2(20f, 20f), ref Vars.FilledBox, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            RecteUtils.DrawText(new Vector2(windowRect.x + 140f, windowRect.y + 30f + 375f), "Show Self", Color.black, true, 10, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 90f, windowRect.y + 30f + 375f), new Vector2(16f, 16f), ref Vars.showSelfFilled, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Head ESP
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30 + 225f, windowRect.y + 331f), new Vector2(225f, 100f), RecteUtils.GetColorFromString("181926"), 15);
            Toggle(new Vector2(windowRect.x + 30f + 235 + 12f, windowRect.y + 22.5f + 315f), new Vector2(35f, 35f), ref Vars.HeadESP, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 225f + 15f, windowRect.y + 30f + 315f), "Head ESP", Color.black, true, 18, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 285f, windowRect.y + 22.5f + 350f), new Vector2(20f, 20f), ref Vars.showSelfHeadESP, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 240f, windowRect.y + 30f + 345f), "Show Self", Color.black, true, 12, FontStyle.Bold, 0);

            //Head ESP Shape
            if (!new Rect(windowRect.x + 135f + 147.5f + 22.5f, windowRect.y + 30f + 367.5f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 147.5f + 22.5f, windowRect.y + 30f + 367.5f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 135f + 147.5f + 22.5f, windowRect.y + 30f + 367.5f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 147.5f + 22.5f, windowRect.y + 30f + 367.5f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 135f + 147.5f + 22.5f, windowRect.y + 30f + 367.5f, 25f, 25f), new GUIContent(""), GUIStyle.none))
            {
                Vars.HeadESPShape = RecteUtils.tabSystem(1, 3, Vars.HeadESPShape, -1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 145f + 34f, windowRect.y + 30f + 373f), "<", Color.black);
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 210f + 30f, windowRect.y + 30f + 373f), $"Shape: {Vars.HeadESPShape}", Color.black, true, 13, FontStyle.Bold, 0);
            if (!new Rect(windowRect.x + 135f + 262.5f + 25f, windowRect.y + 30f + 367.5f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 262.5f + 25f, windowRect.y + 30f + 367.5f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 135f + 262.5f + 25f, windowRect.y + 30f + 367.5f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 135f + 262.5f + 25f, windowRect.y + 30f + 367.5f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 135f + 262.5f + 25f, windowRect.y + 30f + 367.5f, 25f, 25f), new GUIContent(""), GUIStyle.none))
            {
                Vars.HeadESPShape = RecteUtils.tabSystem(1, 3, Vars.HeadESPShape, 1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f + 260f + 36f, windowRect.y + 30f + 373f), ">", Color.black);

            //Chams
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30 + 465f, windowRect.y + 15f), new Vector2(225f, 215f), RecteUtils.GetColorFromString("181926"), 15);
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f), "Chams", Color.black, true, 21, FontStyle.Bold, 0);

            //Player Chams
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 60f), "Players", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 60f), new Vector2(20f, 20f), ref Vars.Chams, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 360f + 235f, windowRect.y + 85f), "Show Self", Color.black, true, 12, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 185f, windowRect.y + 85f), new Vector2(16f, 16f), ref Vars.showSelfChams, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Chest Chams
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 110f), "Chests", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 110f), new Vector2(20f, 20f), ref Vars.ChestChams, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Weapon Chams
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 235f, windowRect.y + 140f), "Weapons", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 140f), new Vector2(20f, 20f), ref Vars.WeaponChams, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Ammo Chams
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 170f), "Ammo", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 170f), new Vector2(20f, 20f), ref Vars.AmmoChams, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Mats
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 220f, windowRect.y + 200f), "Mats", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 200f), new Vector2(20f, 20f), ref Vars.MatsChams, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));

            //Other

            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 30 + 465f, windowRect.y + 245f), new Vector2(225f, 185f), RecteUtils.GetColorFromString("181926"), 15);
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f + 225f), "Other", Color.black, true, 20, FontStyle.Bold, 0);

            //Recte ESP
            Toggle(new Vector2(windowRect.x + 30f + 350f + 135f, windowRect.y + 22.5f + 260f), new Vector2(26f, 26f), ref Vars.RecteESP, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f + 255f), "Recte ESP", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 22.5f + 290f), new Vector2(20f, 20f), ref Vars.showSelfRecteESP, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f + 285f), "Show Self", Color.black, true, 12, FontStyle.Bold, 0);

            //Target Line
            Toggle(new Vector2(windowRect.x + 30f + 350f + 135f, windowRect.y + 22.5f + 315f), new Vector2(26f, 26f), ref Vars.TargetLine, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f + 315f), "Target Line", Color.black, true, 16, FontStyle.Bold, 0);

            //Tracers
            Toggle(new Vector2(windowRect.x + 30f + 350f + 135f, windowRect.y + 22.5f + 350f), new Vector2(26f, 26f), ref Vars.Tracers, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f + 350f), "Tracers", Color.black, true, 16, FontStyle.Bold, 0);
            Toggle(new Vector2(windowRect.x + 30f + 350f + 165f, windowRect.y + 22.5f + 380f), new Vector2(20f, 20f), ref Vars.showSelfTracers, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 30f + 350f + 225f, windowRect.y + 30f + 375f), "Show Self", Color.black, true, 12, FontStyle.Bold, 0);

        }

        public void newWeapons()
        {
            Rect windowRect = new Rect(mainMenu.xMin + 125f, mainMenu.yMin + 35f, mainMenu.width - 175f, 475f);

            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 15f, windowRect.y + 15f), new Vector2(225f, 225f), RecteUtils.GetColorFromString("181926"), 15);
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f, windowRect.y + 30f), "Aimbot", Color.black, true, 18, FontStyle.Bold, 0);
            //Draw FOV
            Toggle(new Vector2(windowRect.x + 55f, windowRect.y + 22.5f + 40f), new Vector2(26f, 26f), ref Vars.DrawFOV, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 130f, windowRect.y + 65f), $"<color=cad3f5>Draw FOV</color>", Color.black, true, 15, FontStyle.Bold, 0);

            //FOV Logic
            Toggle(new Vector2(windowRect.x + 60f, windowRect.y + 125f), new Vector2(26f, 26f), ref Vars.FOVLogic, 8, RecteUtils.GetColorFromString("24273a"), RecteUtils.GetColorFromString("a6da95"), RecteUtils.GetColorFromString("d8ffc4"), RecteUtils.GetColorFromString("5b6078"));
            RecteUtils.DrawText(new Vector2(windowRect.x + 130f, windowRect.y + 160f), $"<color=cad3f5>Use FOV Logic</color>", Color.black, true, 15, FontStyle.Bold, 0);
            
            //FOV Size
            RecteUtils.DrawText(new Vector2(windowRect.x + 130f, windowRect.y + 100), $"<color=cad3f5>FOV Size: {Mathf.Round(Vars.FOVSize)}</color>", Color.black, true, 14, FontStyle.Bold, 0);
            Vars.FOVSize = GUI.HorizontalSlider(new Rect(windowRect.x + 75f, windowRect.y + 130f, 105f, 25f), Vars.FOVSize, 20f, 750f, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 75f, windowRect.y + 130f), new Vector2(117f, 15f), RecteUtils.GetColorFromString("363a4f"), 45);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 75f + SliderMath(Vars.FOVSize, 20f, 750f) * 100f, windowRect.y + 129f), new Vector2(16f, 17f), RecteUtils.GetColorFromString("7dc4e4"), 45);

            //FOV Shape
            if (!new Rect(windowRect.x + 40f, windowRect.y + 185f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 40f, windowRect.y + 185f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 40f, windowRect.y + 185f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 40f, windowRect.y + 185f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 70f, windowRect.y + 185f, 25f, 25f), new GUIContent(""), GUIStyle.none))
            {
                Vars.FOVShape = RecteUtils.tabSystem(0, 8, Vars.FOVShape, -1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 42.5f, windowRect.y + 192.5f), "<", Color.black);
            RecteUtils.DrawText(new Vector2(windowRect.x + 135f, windowRect.y + 192.5f), $"Shape: {Vars.fovShapeName[Vars.FOVShape]}", Color.black, true, 13, FontStyle.Bold, 0);
            if (!new Rect(windowRect.x + 210f, windowRect.y + 195f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 210f, windowRect.y + 185f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("363a4f"), 10);
            }
            if (new Rect(windowRect.x + 210f, windowRect.y + 185f, 25f, 25f).Contains(Event.current.mousePosition))
            {
                RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 210f, windowRect.y + 185f), new Vector2(25f, 25f), RecteUtils.GetColorFromString("494d64"), 10);
            }
            if (GUI.Button(new Rect(windowRect.x + 210f, windowRect.y + 185f, 25f, 25f), new GUIContent(""), GUIStyle.none))
            {
                Vars.FOVShape = RecteUtils.tabSystem(0, 8, Vars.FOVShape, 1);
            }
            RecteUtils.DrawText(new Vector2(windowRect.x + 217.5f, windowRect.y + 192.5f), ">", Color.black);


            /*
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 15f, 45f, 25f), new Vector2(20f, 15f), ref Vars.LockOn, "Lock On");
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 50f, 45f, 25f), new Vector2(20f, 15f), ref Vars.SilentAim, "Silent Aim");
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 85f, 45f, 25f), new Vector2(20f, 15f), ref Vars.MagicBullet, "Magic Bullet");
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 120f, 45f, 25f), new Vector2(20f, 15f), ref Vars.DrawFOV, "Draw FOV");
            //GUI.skin.horizontalSlider.normal.background = RecteUtils.GetColorFromString("5b6078");
            RecteUtils.DrawText(new Vector2(windowRect.x + 75f, windowRect.y + 155), $"<color=cad3f5>FOV Size: {Mathf.Round(Vars.FOVSize)}</color>", Color.black, true, 16, FontStyle.Bold, 0);
            Vars.FOVSize = GUI.HorizontalSlider(new Rect(windowRect.x + 25f, windowRect.y + 180, 105f, 25f), Vars.FOVSize, 25f, 550, GUIStyle.none, GUIStyle.none);

            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 25f, windowRect.y + 180), new Vector2(117f, 15f), RecteUtils.GetColorFromString("181926"), 45);
            RecteUtils.DrawRoundedTex(new Vector2(windowRect.x + 25f + SliderMath(Vars.FOVSize, 25f, 550) * 100f, windowRect.y + 179), new Vector2(17f, 17f), RecteUtils.GetColorFromString("7dc4e4"), 45);
            //RecteUtils.DrawNonESPLine(new Vector2(windowRect.x + 25f, windowRect.y + 320), new Vector2(windowRect.x + 25 + method_5(Vars.FOVSize, 20f, 550) * 100f, windowRect.y + 320f), RecteUtils.GetColorFromString("c6a0f6"), 6f);
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 200f, 45f, 25f), new Vector2(20f, 15f), ref Vars.VisibilityCheck, "Visibility Check");
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 235f, 45f, 25f), new Vector2(20f, 15f), ref Vars.UseAimbotKey, "Use Aimbot Key");
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 270, 45f, 25f), new Vector2(20f, 15f), ref Vars.InfiniteAmmo, "Infinite Ammo");
            BoolToggle(new Rect(windowRect.x + 25f, windowRect.y + 305, 45f, 25f), new Vector2(20f, 15f), ref Vars.NoRecoil, "No Recoil");*/
        }

        private void MenuWindow(int wID)
        {
            GUI.backgroundColor = RecteUtils.GetColorFromString("6e738d");
            GUI.contentColor = RecteUtils.GetColorFromString("cad3f5");
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);

            if (GUILayout.Button("<b>Render</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 0);
            }
            if (GUILayout.Button("<b>Weapon</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 1);
            }
            if (GUILayout.Button("<b>Misc</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 2);
            }
            if (
                GUILayout.Button(
                    "<b>Player List</b>",
                    new GUILayoutOption[] { GUILayout.Height(30f) }
                )
            )
            {
                PlayerPrefs.SetInt("selectmenu", 3);
            }
            if (GUILayout.Button("<b>Console</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 4);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<b>Client</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 5);
            }
            if (GUILayout.Button("<b>Keybinds</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 6);
            }
            if (GUILayout.Button("<b>Config</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 7);
            }
            GUILayout.EndHorizontal();



            scrollPos = GUILayout.BeginScrollView(
                scrollPos,
                new GUILayoutOption[] { GUILayout.Height(500f) }
            );
            if (PlayerPrefs.GetInt("selectmenu") == 0)
            {
                Render();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 1)
            {
                Weapons();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 2)
            {
                Misc();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 3)
            {
                PlayerList();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 4)
            {
                qLogger.instance.LoggingPart();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 5)
            {
                ClientMenu();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 6)
            {
                KeyCodes();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 7)
            {
                ConfigManager();
            }


            if (GUILayout.Button("Unload", new GUILayoutOption[0]))
            {
                Loader.Unload();
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();
        }

        public void MenuWindow1(int wID)
        {
            scrollPos1 = GUILayout.BeginScrollView(scrollPos1, new GUILayoutOption[] { GUILayout.Height(300f) });
            GUI.backgroundColor = RecteUtils.GetColorFromString("6e738d");
            GUI.contentColor = RecteUtils.GetColorFromString("cad3f5");

            if (GUILayout.Button("<b>Render</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 0);
            }
            if (GUILayout.Button("<b>Weapon</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 1);
            }
            if (GUILayout.Button("<b>Misc</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 2);
            }
            if (GUILayout.Button("<b>Player List</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 3);
            }
            if (GUILayout.Button("<b>Console</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 4);
            }
            if (GUILayout.Button("<b>Client</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 5);
            }
            if (GUILayout.Button("<b>Keybinds</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 6);
            }
            if (GUILayout.Button("<b>Config</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("selectmenu", 7);
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();

        }
        public void MenuWindow2(int ID)
        {
            GUI.backgroundColor = RecteUtils.GetColorFromString("6e738d");
            GUI.contentColor = RecteUtils.GetColorFromString("cad3f5");
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUILayoutOption[]
            {
            GUILayout.Width(500f),
            GUILayout.Height(300f)
            });
            if (PlayerPrefs.GetInt("selectmenu") == 0)
            {
                Render();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 1)
            {
                Weapons();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 2)
            {
                Misc();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 3)
            {
                PlayerList();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 4)
            {
                qLogger.instance.LoggingPart();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 5)
            {
                ClientMenu();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 6)
            {
                KeyCodes();
            }
            if (PlayerPrefs.GetInt("selectmenu") == 7)
            {
                ConfigManager();
            }


            if (GUILayout.Button("Unload", new GUILayoutOption[0]))
            {
                Loader.Unload();
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();

        }

        public void Start()
        {
            UiManager uiManager = UnityEngine.Object.FindObjectOfType<UiManager>();
            uiManager.ShowToast(new DefaultedLocalizedString(new LocalizedString("", ""), "Recte <3"), 10f);
            base.StartCoroutine(GetVersion());

            TitleRect = new Rect((float)((double)Screen.width / 4.0), 70f, 250f, 50f);
            rect2 = new Rect((float)((double)Screen.width / 4.0), 70f, 250f, 50f);
            rect1 = new Rect((float)((double)Screen.width / 2.64), 70f, 431f, 50f);
            mainMenu = new Rect((float)((double)Screen.width / 2.64), 70f, 865f, 25f);
            base.Invoke("AddTypes", .1f);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/");
            }
            if (PlayerPrefsX.GetBool("AddAllOnStart"))
            {
                MiscCheats.UnlockAll();
                MiscCheats.ChangePickaxe();
            }
        }
        public float waveSpeed = .1f;
        public void Update()
        {
            hue += waveSpeed * Time.deltaTime;
            if (hue >= 1f)
            {
                hue -= 1f;
            }
            KeyControls();
        }

        public static void KeyControls()
        {

            if (Input.GetKeyUp((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("MenuToggleKeyCode", "RightShift"))))
            {
                Vars.open = !Vars.open;
            }
            if (Input.GetKeyUp(KeyCode.F11))
            {
                Screen.fullScreen = true;
            }
        }

        private void AddTypes()
        {
            if (base.GetComponent<qLogger>() == null)
            {
                base.gameObject.AddComponent<qLogger>();
            }
        }

        public static IEnumerator GetVersion()
        {
            UnityWebRequest www = UnityWebRequest.Get("http://kanati.bio/RecteVersion.txt");
            yield return www.SendWebRequest();
            version = www.downloadHandler.text;
            yield break;
        }
        public void Render()
        {
            GUILayout.BeginHorizontal();



            if (
                !PlayerPrefsX.GetBool("RainbowOverlay")
                && GUILayout.Button("RGB Player Overlay: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("RainbowOverlay", true);
            }
            if (
                PlayerPrefsX.GetBool("RainbowOverlay")
                && GUILayout.Button("RGB Player Overlay: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("RainbowOverlay", false);
            }/*
            if (
                !PlayerPrefsX.GetBool("CustomSkybox")
                && GUILayout.Button("Custom Skybox: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("CustomSkybox", true);
            }
            if (
                PlayerPrefsX.GetBool("CustomSkybox")
                && GUILayout.Button("Custom Skybox: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("CustomSkybox", false);
            }*/

            GUILayout.EndHorizontal();

            if (PlayerPrefsX.GetBool("RainbowOverlay"))
            {
                GUILayout.Label("Rainbow Saturation: " + PlayerPrefs.GetFloat("RainbowSat").ToString());
                PlayerPrefs.SetFloat("RainbowSat", GUILayout.HorizontalSlider((float)Math.Round((double)PlayerPrefs.GetFloat("RainbowSat", .35f), 3), .01f, 1f, new GUILayoutOption[0]));
            }
            
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("ShowSelfESP")
                && GUILayout.Button("Show Self: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("ShowSelfESP", true);
            }
            if (
                PlayerPrefsX.GetBool("ShowSelfESP")
                && GUILayout.Button("Show Self: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("ShowSelfESP", false);
            }
            if (
                !PlayerPrefsX.GetBool("MaxESPDist")
                && GUILayout.Button("Max ESP Distance: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("MaxESPDist", true);
            }
            if (
                PlayerPrefsX.GetBool("MaxESPDist")
                && GUILayout.Button("Max ESP Distance: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("MaxESPDist", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("MaxESPDist"))
            {
                GUILayout.Label("Max ESP Distance: " + PlayerPrefs.GetFloat("MaxESPDistance"));
                PlayerPrefs.SetFloat("MaxESPDistance", Mathf.Round(GUILayout.HorizontalSlider(PlayerPrefs.GetFloat("MaxESPDistance", 150f), 25f, 400f)));
            }
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("StringESP")
                && GUILayout.Button("StringESP: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("StringESP", true);
            }
            if (
                PlayerPrefsX.GetBool("StringESP")
                && GUILayout.Button("StringESP: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("StringESP", false);
            }
            if (
                !PlayerPrefsX.GetBool("CornerBox")
                && GUILayout.Button("Corner Box: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("CornerBox", true);
            }
            if (
                PlayerPrefsX.GetBool("CornerBox")
                && GUILayout.Button("Corner Box: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("CornerBox", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("BoxFilledESP")
                && GUILayout.Button("Filled Box: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("BoxFilledESP", true);
            }
            if (
                PlayerPrefsX.GetBool("BoxFilledESP")
                && GUILayout.Button("Filled Box: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("BoxFilledESP", false);
            }

            if (
                !PlayerPrefsX.GetBool("BoxOutlineESP")
                && GUILayout.Button("Outline Box: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("BoxOutlineESP", true);
            }
            if (
                PlayerPrefsX.GetBool("BoxOutlineESP")
                && GUILayout.Button("Outline Box: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("BoxOutlineESP", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("Tracers")
                && GUILayout.Button("Tracers: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("Tracers", true);
            }
            if (
                PlayerPrefsX.GetBool("Tracers")
                && GUILayout.Button("Tracers: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("Tracers", false);
            }
            if (
                !PlayerPrefsX.GetBool("HealthShieldESP")
                && GUILayout.Button("Health Bars: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("HealthShieldESP", true);
            }
            if (
                PlayerPrefsX.GetBool("HealthShieldESP")
                && GUILayout.Button("Health Bars: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("HealthShieldESP", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("HealthShieldESP"))
            {
                if (PlayerPrefs.GetInt("HPBoxLoc", 0) == 0 && GUILayout.Button("Healthbox Position: Top", new GUILayoutOption[0]))
                {
                    PlayerPrefs.SetInt("HPBoxLoc", 1);
                }
                if (PlayerPrefs.GetInt("HPBoxLoc", 0) == 1 && GUILayout.Button("Healthbox Position: Bottom", new GUILayoutOption[0]))
                {
                    PlayerPrefs.SetInt("HPBoxLoc", 0);
                }
                if (PlayerPrefs.GetInt("HPBoxOffset", 0) == 0 && GUILayout.Button("Healthbox Offset: Center", new GUILayoutOption[0]))
                {
                    PlayerPrefs.SetInt("HPBoxOffset", 1);
                }
                if (PlayerPrefs.GetInt("HPBoxOffset", 0) == 1 && GUILayout.Button("Healthbox Offset: Sides", new GUILayoutOption[0]))
                {
                    PlayerPrefs.SetInt("HPBoxOffset", 0);
                }
                if (
                !PlayerPrefsX.GetBool("ShieldBoxCheck")
                && GUILayout.Button("Shield Check: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[0])
            )
                {
                    PlayerPrefsX.SetBool("ShieldBoxCheck", true);
                }
                if (
                    PlayerPrefsX.GetBool("ShieldBoxCheck")
                    && GUILayout.Button("Shield Check: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[0])
                )
                {
                    PlayerPrefsX.SetBool("ShieldBoxCheck", false);
                }
            }
            GUILayout.BeginHorizontal();

            if (
                !PlayerPrefsX.GetBool("Skeleton")
                && GUILayout.Button(
                    "Skeleton: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("Skeleton", true);
            }
            if (
                PlayerPrefsX.GetBool("Skeleton")
                && GUILayout.Button(
                    "Skeleton: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("Skeleton", false);
            }
            if (
                !PlayerPrefsX.GetBool("Chams")
                && GUILayout.Button(
                    "Chams: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("Chams", true);
            }
            if (
                PlayerPrefsX.GetBool("Chams")
                && GUILayout.Button(
                    "Chams: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("Chams", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("RecteESP")
                && GUILayout.Button(
                    "Recte ESP: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("RecteESP", true);
            }
            if (
                PlayerPrefsX.GetBool("RecteESP")
                && GUILayout.Button(
                    "Recte ESP: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("RecteESP", false);
            }
            if (
                !PlayerPrefsX.GetBool("TargetLine")
                && GUILayout.Button(
                    "Target Lins: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("TargetLine", true);
            }
            if (
                PlayerPrefsX.GetBool("TargetLine")
                && GUILayout.Button(
                    "Target Line: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("TargetLine", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("HeadESP")
                && GUILayout.Button("HeadESP: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("HeadESP", true);
            }
            if (
                PlayerPrefsX.GetBool("HeadESP")
                && GUILayout.Button("HeadESP: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) })
            )
            {
                PlayerPrefsX.SetBool("HeadESP", false);
            }
            if (PlayerPrefs.GetInt("HeadESPShape") == 0 && GUILayout.Button("Head ESP Shade: Circle", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                PlayerPrefs.SetInt("HeadESPShape", 1);
            }
            if (PlayerPrefs.GetInt("HeadESPShape") == 1 && GUILayout.Button("Head ESP Shape: Square", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                PlayerPrefs.SetInt("HeadESPShape", 2);
            }
            if (PlayerPrefs.GetInt("HeadESPShape") == 2 && GUILayout.Button("Head ESP Shade: Corner Box", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                PlayerPrefs.SetInt("HeadESPShape", 0);
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("ChestChams")
                && GUILayout.Button(
                    "Chest Chams: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("ChestChams", true);
            }
            if (
                PlayerPrefsX.GetBool("ChestChams")
                && GUILayout.Button(
                    "Chest Chams: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("ChestChams", false);
            }

            if (
                !PlayerPrefsX.GetBool("WeaponChams")
                && GUILayout.Button(
                    "Weapon Chams: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("WeaponChams", true);
            }
            if (
                PlayerPrefsX.GetBool("WeaponChams")
                && GUILayout.Button(
                    "Weapon Chams: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("WeaponChams", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("AmmoChams")
                && GUILayout.Button(
                    "Ammo Chams: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("AmmoChams", true);
            }
            if (
                PlayerPrefsX.GetBool("AmmoChams")
                && GUILayout.Button(
                    "Ammo Chams: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("AmmoChams", false);
            }

            if (
                !PlayerPrefsX.GetBool("MatsChams")
                && GUILayout.Button(
                    "Mats Chams: <b><color=#ed8796>Disabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("MatsChams", true);
            }
            if (
                PlayerPrefsX.GetBool("MatsChams")
                && GUILayout.Button(
                    "Mats Chams: <b><color=#a6da95>Enabled</color></b>",
                    new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }
                )
            )
            {
                PlayerPrefsX.SetBool("MatsChams", false);
            }
            GUILayout.EndHorizontal();
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = RecteUtils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("ChamsColor", "8a2be280")}>Chams Color: </color></b>");
            PlayerPrefs.SetString("ChamsColor", GUILayout.TextField(PlayerPrefs.GetString("ChamsColor", "8a2be280"), sty, new GUILayoutOption[0]));


            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("TracerColor", "87cefa")}>Tracers Color: </color></b>");
            PlayerPrefs.SetString("TracerColor", GUILayout.TextField(PlayerPrefs.GetString("TracerColor", "87cefa"), sty, new GUILayoutOption[0]));


            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("CornerBoxColor", "880000")}>Corner Box Color: </color></b>");
            PlayerPrefs.SetString("CornerBoxColor", GUILayout.TextField(PlayerPrefs.GetString("CornerBoxColor", "880000"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("BoxOutlineESPColor", "87c4a3")}>Box Outline ESP Color: </color></b>");
            PlayerPrefs.SetString("BoxOutlineESPColor", GUILayout.TextField(PlayerPrefs.GetString("BoxOutlineESPColor", "89c4a3"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("BoxFilledESPColor", "30303070")}>Box Filled ESP Color: </color></b>");
            PlayerPrefs.SetString("BoxFilledESPColor", GUILayout.TextField(PlayerPrefs.GetString("BoxFilledESPColor", "30303040"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("SkeletonColor", "ff69b4")}>Skeleton Color: </color></b>");
            PlayerPrefs.SetString("SkeletonColor", GUILayout.TextField(PlayerPrefs.GetString("SkeletonColor", "ff69b4"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("HeadESPColor", "9fa32e")}>Head ESP Color: </color></b>");
            PlayerPrefs.SetString("HeadESPColor", GUILayout.TextField(PlayerPrefs.GetString("HeadESPColor", "880000"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("ChestChamsColor", "ffff6b")}>Chest Chams Color: </color></b>");
            PlayerPrefs.SetString("ChestChamsColor", GUILayout.TextField(PlayerPrefs.GetString("ChestChamsColor", "ffff6b"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("ItemChamsWeaponColor", "87c4a3")}>Weapons Chams Color: </color></b>");
            PlayerPrefs.SetString("ItemChamsWeaponColor", GUILayout.TextField(PlayerPrefs.GetString("ItemChamsWeaponColor", "f0601d"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("ItemChamsAmmoColor", "42f578")}>Ammo Chams Color: </color></b>");
            PlayerPrefs.SetString("ItemChamsAmmoColor", GUILayout.TextField(PlayerPrefs.GetString("ItemChamsAmmoColor", "42f578"), sty, new GUILayoutOption[0]));

            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("ItemChamsBuildsColor", "\"4287f5")}>Mats Chams Color: </color></b>");
            PlayerPrefs.SetString("ItemChamsBuildsColor", GUILayout.TextField(PlayerPrefs.GetString("ItemChamsBuildsColor", "4287f5"), sty, new GUILayoutOption[0]));

            //GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("SkyboxColor1", "\"87cefa")}>Skybox Color1: </color></b>");
            //PlayerPrefs.SetString("SkyboxColor1", GUILayout.TextField(PlayerPrefs.GetString("SkyboxColor1", "87cefa"), sty, new GUILayoutOption[0]));

            //GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("SkyboxColor2", "\"ff69b4")}>Skybox Color2: </color></b>");
            //PlayerPrefs.SetString("SkyboxColor2", GUILayout.TextField(PlayerPrefs.GetString("SkyboxColor2", "ff69b4"), sty, new GUILayoutOption[0]));
        }

        public void Weapons()
        {

            if (
               !PlayerPrefsX.GetBool("LockOn")
               && GUILayout.Button("LockOn: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("LockOn", true);
            }
            if (
                PlayerPrefsX.GetBool("LockOn")
                && GUILayout.Button("LockOn: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("LockOn", false);
            }
            if (
               !PlayerPrefsX.GetBool("SilentAim")
               && GUILayout.Button("Silent Aim: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("SilentAim", true);
            }
            if (
                PlayerPrefsX.GetBool("SilentAim")
                && GUILayout.Button("Silent Aim: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("SilentAim", false);
            }
            if (
               !PlayerPrefsX.GetBool("DrawFOVCircle")
               && GUILayout.Button("Draw FOV Shape: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("DrawFOVCircle", true);
            }
            if (
                PlayerPrefsX.GetBool("DrawFOVCircle")
                && GUILayout.Button("Draw FOV Shape: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("DrawFOVCircle", false);
            }
            if (
               !PlayerPrefsX.GetBool("VisibilityCheck")
               && GUILayout.Button("Visibility Check: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("VisibilityCheck", true);
            }
            if (
                PlayerPrefsX.GetBool("VisibilityCheck")
                && GUILayout.Button("Visibility Check: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("VisibilityCheck", false);
            }
            if (
               !PlayerPrefsX.GetBool("NoKeyAimbot")
               && GUILayout.Button("Use Aimbot Key: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("NoKeyAimbot", true);
            }
            if (
                PlayerPrefsX.GetBool("NoKeyAimbot")
                && GUILayout.Button("Use Aimbot Key: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("NoKeyAimbot", false);
            }
            if (PlayerPrefs.GetInt("AimbotButton") == 0 && GUILayout.Button("Mouse Bind: Right Mouse", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("AimbotButton", 1);
            }
            if (PlayerPrefs.GetInt("AimbotButton") == 1 && GUILayout.Button("Mouse Bind: Left Mouse", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("AimbotButton", 0);
            }
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = RecteUtils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            GUILayout.Label("Fov: " + Math.Round(PlayerPrefs.GetFloat("AimbotFOV", 100f)));
            PlayerPrefs.SetFloat("AimbotFOV", GUILayout.HorizontalSlider(PlayerPrefs.GetFloat("AimbotFOV", 100f), 20f, 750f, new GUILayoutOption[0]));
            GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("FOVCircleColor", "880000")}>FOV Shape Color: </color></b>");
            PlayerPrefs.SetString("FOVCircleColor", GUILayout.TextField(PlayerPrefs.GetString("FOVCircleColor", "880000"), sty, new GUILayoutOption[0]));
            if (PlayerPrefs.GetInt("FOVCircleType") == 0 && GUILayout.Button("FOV Shape: Triangle", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 4);
                PlayerPrefs.SetInt("FOVCircleType", 1);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 1 && GUILayout.Button("FOV Shape: Square", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 5);
                PlayerPrefs.SetInt("FOVCircleType", 2);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 2 && GUILayout.Button("FOV Shape: Pentagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 6);
                PlayerPrefs.SetInt("FOVCircleType", 3);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 3 && GUILayout.Button("FOV Shape: Hexagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 7);
                PlayerPrefs.SetInt("FOVCircleType", 4);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 4 && GUILayout.Button("FOV Shape: Heptagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 8);
                PlayerPrefs.SetInt("FOVCircleType", 5);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 5 && GUILayout.Button("FOV Shape: Octogon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 9);
                PlayerPrefs.SetInt("FOVCircleType", 6);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 6 && GUILayout.Button("FOV Shape: Nonagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 10);
                PlayerPrefs.SetInt("FOVCircleType", 7);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 7 && GUILayout.Button("FOV Shape: Decagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 30);
                PlayerPrefs.SetInt("FOVCircleType", 8);
            }
            if (PlayerPrefs.GetInt("FOVCircleType") == 8 && GUILayout.Button("FOV Shape: Circle", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("SideCount", 3);
                PlayerPrefs.SetInt("FOVCircleType", 0);
            }
            //curBone
            if (PlayerPrefs.GetInt("CurBone") == 0 && GUILayout.Button("Aimbot Bone: Head", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Recte_1v1lol.Cheats.AimbotCheats.curBone = HumanBodyBones.Neck;
                PlayerPrefs.SetInt("CurBone", 1);
            }
            if (PlayerPrefs.GetInt("CurBone") == 1 && GUILayout.Button("Aimbot Bone: Neck", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Recte_1v1lol.Cheats.AimbotCheats.curBone = HumanBodyBones.UpperChest;
                PlayerPrefs.SetInt("CurBone", 2);
            }
            if (PlayerPrefs.GetInt("CurBone") == 2 && GUILayout.Button("Aimbot Bone: Upper Chest", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Recte_1v1lol.Cheats.AimbotCheats.curBone = HumanBodyBones.Hips;
                PlayerPrefs.SetInt("CurBone", 3);
            }
            if (PlayerPrefs.GetInt("CurBone") == 3 && GUILayout.Button("Aimbot Bone: Hips", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Recte_1v1lol.Cheats.AimbotCheats.curBone = HumanBodyBones.Head;
                PlayerPrefs.SetInt("CurBone", 0);
            }
            if (PlayerPrefs.GetInt("AimbotSort") == 0 && GUILayout.Button("Aimbot Sort By: Crosshair Distance", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("AimbotSort", 1);
            }
            if (PlayerPrefs.GetInt("AimbotSort") == 1 && GUILayout.Button("Aimbot Sort By: Player Distance", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("AimbotSort", 2);
            }
            if (PlayerPrefs.GetInt("AimbotSort") == 2 && GUILayout.Button("Aimbot Sort By: Health", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("AimbotSort", 0);
            }
            if (
               !PlayerPrefsX.GetBool("RapidFire")
               && GUILayout.Button("RapidFire: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("RapidFire", true);
            }
            if (
                PlayerPrefsX.GetBool("RapidFire")
                && GUILayout.Button("RapidFire: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("RapidFire", false);
            }
            if (
                !PlayerPrefsX.GetBool("InfiniteAmmo")
                && GUILayout.Button("InfiniteAmmo: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("InfiniteAmmo", true);
            }
            if (
                PlayerPrefsX.GetBool("InfiniteAmmo")
                && GUILayout.Button("InfiniteAmmo: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("InfiniteAmmo", false);
            }
            if (
               !PlayerPrefsX.GetBool("NoRecoil")
               && GUILayout.Button("NoRecoil: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("NoRecoil", true);
            }
            if (
                PlayerPrefsX.GetBool("NoRecoil")
                && GUILayout.Button("NoRecoil: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("NoRecoil", false);
            }

            if (
               !PlayerPrefsX.GetBool("MagicBullet")
               && GUILayout.Button("MagicBullet: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
           )
            {
                PlayerPrefsX.SetBool("MagicBullet", true);
            }
            if (
                PlayerPrefsX.GetBool("MagicBullet")
                && GUILayout.Button("MagicBullet: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) })
            )
            {
                PlayerPrefsX.SetBool("MagicBullet", false);
            }
            
            
            foreach (string gunName in WeaponNames)
            {
                for (int i = 0; i <= 5; i++)
                {
                    if (i == 1)
                    {
                        if (GUILayout.Button("Spawn Common " + gunName.ToString().Substring(16).Replace("_", " "), new GUILayoutOption[0]))
                        {
                            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            }
                            Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                            PickupableSpawner.CreateWeaponDrop($"{gunName}{i}", localPlayerPosition);
                        }
                    }
                    if (i == 2)
                    {
                        if (GUILayout.Button("Spawn Uncommon " + gunName.ToString().Substring(16).Replace("_", " "), new GUILayoutOption[0]))
                        {
                            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            }
                            Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                            PickupableSpawner.CreateWeaponDrop($"{gunName}{i}", localPlayerPosition);
                        }
                    }
                    if (i == 3)
                    {
                        if (GUILayout.Button("Spawn  Rare " + gunName.ToString().Substring(16).Replace("_", " "), new GUILayoutOption[0]))
                        {
                            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            }
                            Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                            PickupableSpawner.CreateWeaponDrop($"{gunName}{i}", localPlayerPosition);
                        }
                    }
                    if (i == 4)
                    {
                        if (GUILayout.Button("Spawn Epic " + gunName.ToString().Substring(16).Replace("_", " "), new GUILayoutOption[0]))
                        {
                            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            }
                            Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                            PickupableSpawner.CreateWeaponDrop($"{gunName}{i}", localPlayerPosition);
                        }
                    }
                    if (i == 5)
                    {
                        if (GUILayout.Button("Spawn Legendary " + gunName.ToString().Substring(16).Replace("_", " "), new GUILayoutOption[0]))
                        {
                            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                            {
                                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                            }
                            Vector3 localPlayerPosition = PlayerController.LFNGIIPNIDN.transform.position;
                            PickupableSpawner.CreateWeaponDrop($"{gunName}{i}", localPlayerPosition);
                        }
                    }

                }
            }
        }
        public void PlayerList()
        {
            GUILayout.Label("PlayerName: " + PhotonNetwork.NickName);
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = RecteUtils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            PhotonNetwork.NickName = GUILayout.TextField(PhotonNetwork.NickName, sty, new GUILayoutOption[0]);
            FirebaseManager.Instance.PGBJEGGCGKK.Nickname = PhotonNetwork.NickName;
            GUILayout.BeginHorizontal();
            if (!PlayerPrefsX.GetBool("RandomName") && GUILayout.Button("Random Usernames: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[0]))
            {

                PlayerPrefsX.SetBool("RandomName", true);
            }
            if (PlayerPrefsX.GetBool("RandomName") && GUILayout.Button("Random Usernames: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[0]))
            {

                PlayerPrefsX.SetBool("RandomName", false);

            }
            if (!PlayerPrefsX.GetBool("AntiDiscord") && GUILayout.Button("Anti-Discord: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[0]))
            {

                PlayerPrefsX.SetBool("AntiDiscord", true);
            }
            if (PlayerPrefsX.GetBool("AntiDiscord") && GUILayout.Button("Anti-Discord: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[0]))
            {

                PlayerPrefsX.SetBool("AntiDiscord", false);

            }
            GUILayout.EndHorizontal();
            new List<Player>();
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (!player.IsMasterClient)
                {
                    GUILayout.Button(string.Concat(new object[]
                            {
                                player.NickName,
                                "\n<b><color=#a6da95>[ID: ",
                                player.ActorNumber,
                                "]</color></b>"
                            }), new GUILayoutOption[] { GUILayout.Height(50f) });

                }


                if (player.IsMasterClient)
                {
                    GUILayout.Button(string.Concat(new object[]
                            {
                                "<b><color=#ed8796>[Host]</color></b> \n",
                                player.NickName,
                                "\n<b><color=#a6da95>[ID: ",
                                player.ActorNumber,
                                "]</color></b>"
                            }
                        ), new GUILayoutOption[] { GUILayout.Height(50f) });
                }

            }
        }
        public void Misc()
        {
           
            if (GUILayout.Button("Kill All", new GUILayoutOption[0]))
            {
                foreach (PlayerController pc in EntityFinding.Players)
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

            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = RecteUtils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            GUILayout.Label("Skin ID:");
            ids = GUILayout.TextField(ids, sty, new GUILayoutOption[0]);

            if (GUILayout.Button("Change Skin", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                MiscCheats.ChangeSkin(ids);
            }


            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Give Scifi Hammer", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                MiscCheats.ChangePickaxe();
            }
            if (GUILayout.Button("Unlock All Emotes", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                MiscCheats.UnlockAll();
            }
            GUILayout.EndHorizontal();
            if (!PlayerPrefsX.GetBool("AddAllOnStart") && GUILayout.Button("Unlock All On Start: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("AddAllOnStart", true);
            }
            if (PlayerPrefsX.GetBool("AddAllOnStart") && GUILayout.Button("Unlock All On Start: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {

                PlayerPrefsX.SetBool("AddAllOnStart", false);
            }

            /*
            if (!PlayerPrefsX.GetBool("AutoPickup") && GUILayout.Button("AutoPickup: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f)))
            {
                PlayerPrefsX.SetBool("AutoPickup", true);
            }
            if (PlayerPrefsX.GetBool("AutoPickup") && GUILayout.Button("AutoPickup: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f)))
            {

                PlayerPrefsX.SetBool("AutoPickup", false);
            }*/
            GUILayout.BeginHorizontal();
            if (!PlayerPrefsX.GetBool("SpeedHack") && GUILayout.Button("Speed Hack: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("SpeedHack", true);
            }
            if (PlayerPrefsX.GetBool("SpeedHack") && GUILayout.Button("Speed Hack: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {

                PlayerPrefsX.SetBool("SpeedHack", false);
            }
            if (!PlayerPrefsX.GetBool("Flight") && GUILayout.Button("Flight: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("Flight", true);
            }
            if (PlayerPrefsX.GetBool("Flight") && GUILayout.Button("Flight: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("Flight", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("SpeedHack"))
            {
                GUILayout.Label("Speed Boost: "+Mathf.Round(PlayerPrefs.GetFloat("SpeedBoost")).ToString());
                PlayerPrefs.SetFloat("SpeedBoost", GUILayout.HorizontalSlider(PlayerPrefs.GetFloat("SpeedBoost", 1f), 1f, 50f, new GUILayoutOption[0]));
            }
            if (PlayerPrefsX.GetBool("Flight"))
            {
                GUILayout.Label("Fly Speed:", new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label(PlayerPrefs.GetFloat("FlightSpeed").ToString(), new GUILayoutOption[0]);
                PlayerPrefs.SetFloat("FlightSpeed", GUILayout.HorizontalSlider(Mathf.Round(PlayerPrefs.GetFloat("FlightSpeed", 10f)), 10f, 75, new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }));
                if (GUILayout.Button("Default", new GUILayoutOption[]
                {
                    GUILayout.Width(100f),
                    GUILayout.Height(30f)
                }))
                {
                    PlayerPrefs.SetFloat("FlightSpeed", 10f);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Destroy Player Object", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            }
            if (GUILayout.Button("Destroy All Player Object", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                foreach (Player p in PhotonNetwork.PlayerList)
                {
                    if (p != PhotonNetwork.LocalPlayer)
                    {
                        PhotonNetwork.DestroyPlayerObjects(p);
                    }
                }

            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Destroy All Buildings", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                foreach (BuildingNetworkController builds in UnityEngine.Object.FindObjectsOfType<BuildingNetworkController>())
                {
                    builds.KillAllBuildings(true);
                }
            }
            if (PlayerPrefsX.GetBool("BuildSpam") && GUILayout.Button("Build Spam: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("BuildSpam", false);
            }
            if (!PlayerPrefsX.GetBool("BuildSpam") && GUILayout.Button("Build Spam: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("BuildSpam", true);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
                !PlayerPrefsX.GetBool("UnlimitedBuilds")
                && GUILayout.Button("Unlimited Builds: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("UnlimitedBuilds", true);
            }
            if (
                PlayerPrefsX.GetBool("UnlimitedBuilds")
                && GUILayout.Button("Unlimited Builds: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("UnlimitedBuilds", false);
            }
            if (
                !PlayerPrefsX.GetBool("BreakAllToggle")
                && GUILayout.Button("Destroy All Toggle: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("BreakAllToggle", true);
            }
            if (
                PlayerPrefsX.GetBool("BreakAllToggle")
                && GUILayout.Button("Destroy All Toggle: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("BreakAllToggle", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
           



            if (
                !PlayerPrefsX.GetBool("customScale")
                && GUILayout.Button("Player Scale: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f)})
            )
            {
                PlayerPrefsX.SetBool("customScale", true);
            }
            if (
                PlayerPrefsX.GetBool("customScale")
                && GUILayout.Button("Player Scale: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f)})
            )
            {
                PlayerPrefsX.SetBool("customScale", false);
            }
            GUILayout.EndHorizontal();/*
            if (PlayerPrefsX.GetBool("SpinBot"))
            {
                GUILayout.Label("Spinbot Speed: " + PlayerPrefs.GetFloat("SpinBotSpeed"));
                PlayerPrefs.SetFloat("SpinBotSpeed", GUILayout.HorizontalSlider((float)Math.Round((double)PlayerPrefs.GetFloat("SpinBotSpeed", 10f), 2), .1f, 10f));
            }*/
            if (PlayerPrefsX.GetBool("customScale"))
            {
                GUILayout.Label("Scale: " + PlayerPrefs.GetFloat("LocalScale"));
                PlayerPrefs.SetFloat("LocalScale", GUILayout.HorizontalSlider((float)Math.Round((double)PlayerPrefs.GetFloat("LocalScale", 1f), 2), .1f, 10f));
            }
            GUILayout.BeginHorizontal();
            if (

                !PlayerPrefsX.GetBool("GodMode")
                && GUILayout.Button("God Mode: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("GodMode", true);
            }
            if (
                PlayerPrefsX.GetBool("GodMode")
                && GUILayout.Button("God Mode: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("GodMode", false);
            }
            if (
                !PlayerPrefsX.GetBool("JumpHeight")
                && GUILayout.Button("JumpHeight: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("JumpHeight", true);
            }
            if (
                PlayerPrefsX.GetBool("JumpHeight")
                && GUILayout.Button("JumpHeight: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("JumpHeight", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (
            !PlayerPrefsX.GetBool("SpoofID")
            && GUILayout.Button("SpoofID: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
        )
            {
                PlayerPrefsX.SetBool("SpoofID", true);
            }
            if (
                PlayerPrefsX.GetBool("SpoofID")
                && GUILayout.Button("SpoofID: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("SpoofID", false);
            }


            if (
                !PlayerPrefsX.GetBool("HitboxExpansion")
                && GUILayout.Button("Hitbox Expander: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                PlayerPrefsX.SetBool("HitboxExpansion", true);
            }
            if (
                PlayerPrefsX.GetBool("HitboxExpansion")
                && GUILayout.Button("Hitbox Expander: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) })
            )
            {
                if (PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    foreach (PlayerController pc in EntityFinding.pcs.Values)
                    {
                        if (!pc.IsMine())
                        {
                            pc.KAIBGDBBIBO.transform.localScale = new Vector3(1f, 1f, 1f);
                        }
                    }
                }
                PlayerPrefsX.SetBool("HitboxExpansion", false);
            }
            GUILayout.EndHorizontal();
            if (PlayerPrefsX.GetBool("HitboxExpansion"))
            {
                GUILayout.Label("Hitbox Size: " + PlayerPrefs.GetFloat("HitboxExpand").ToString());
                PlayerPrefs.SetFloat("HitboxExpand", GUILayout.HorizontalSlider((float)Math.Round((double)PlayerPrefs.GetFloat("HitboxExpand"), 2), .95f, 25f));
            }
            if (PlayerPrefsX.GetBool("JumpHeight"))
            {
                GUILayout.Label("Jump Height: " + PlayerPrefs.GetFloat("JH").ToString());
                PlayerPrefs.SetFloat("JH", GUILayout.HorizontalSlider((float)Math.Round((double)PlayerPrefs.GetFloat("JH"), 2), 5.5f, 50f));
            }

            GUILayout.BeginHorizontal();
            if (!PlayerPrefsX.GetBool("BHop") && GUILayout.Button("BHop: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("BHop", true);
            }
            if (PlayerPrefsX.GetBool("BHop") && GUILayout.Button("BHop: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {

                PlayerPrefsX.SetBool("BHop", false);
            }
            if (!PlayerPrefsX.GetBool("AreBuildingsOneHit") && GUILayout.Button("1 Tap Builds: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("AreBuildingsOneHit", true);
            }
            if (PlayerPrefsX.GetBool("AreBuildingsOneHit") && GUILayout.Button("1 Tap Builds: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {

                PlayerPrefsX.SetBool("AreBuildingsOneHit", false);
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!PlayerPrefsX.GetBool("WeaponTrail") && GUILayout.Button("Loot Trail: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PlayerPrefsX.SetBool("WeaponTrail", true);
            }
            if (PlayerPrefsX.GetBool("WeaponTrail") && GUILayout.Button("Loot Trail: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {

                PlayerPrefsX.SetBool("WeaponTrail", false);
            }
            if (GUILayout.Button("Open All Chests", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                foreach (SupplyCrate c in EntityFinding.crates)
                {
                    c.OpenCrate(PlayerController.LFNGIIPNIDN);
                }
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Leave Game", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PhotonNetwork.Disconnect();
            }
            /*
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                GUILayout.Label("V3P Speed: " + v3p.speed.ToString());
                v3p.speed = GUILayout.HorizontalSlider(v3p., 10f, 50f, new GUILayoutOption[0]);
                GUILayout.Label("V3P Jump Height: " + v3p.speed.ToString());
                v3p.jumpHeight = GUILayout.HorizontalSlider(v3p.speed, 5f, 20f, new GUILayoutOption[0]);
                GUILayout.Label("Time Scale: " + Time.timeScale.ToString());
                Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 1f, 5f, new GUILayoutOption[0]);
            }*/




        }
        public void ConfigManager()
        {

            int num = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Length + 14;
            int num2 = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/").Length;
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/");
            ScrollPos8 = GUILayout.BeginScrollView(ScrollPos8, new GUILayoutOption[] { GUILayout.Height(curRect.height / 4) });

            for (int i = 0; i < files.Length; i++)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                if (files[i].Remove(0, num) == PlayerPrefs.GetString("loadedFileName"))
                {
                    GUILayout.Label("<b><color=#ed8796>" + files[i].Remove(0, num).Replace(".Recte", "") + "</color></b>");
                }
                if (files[i].Remove(0, num) != PlayerPrefs.GetString("loadedFileName"))
                {
                    GUILayout.Label("<color=#cad3f5>" + files[i].Remove(0, num).Replace(".Recte", "") + "</color>");
                }
                if (GUILayout.Button("Set As Config", new GUILayoutOption[] { GUILayout.Width(scrollWidth - (float)files[i].Length) }))
                {
                    PlayerPrefs.SetString("loadedFileName", files[i].Remove(0, num));
                    PlayerPrefs.SetString("newFileNameInfo", files[i]);
                }
                if (GUILayout.Button("Delete Config", new GUILayoutOption[] { GUILayout.Width(scrollWidth - (float)files[i].Length) }))
                {
                    File.Delete(files[i]);
                }
                GUILayout.EndHorizontal();

            }
            if (files.Length == 0)
            {
                GUILayout.Label("<b><color=#c6a0f6>No Configs To Show</color></b>");
            }


            GUILayout.EndScrollView();
            if (PlayerPrefs.GetInt("FileTooLong") == 1)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label("<size=30><color=#ed8796>Name Length Too Long!</color></size>", new GUILayoutOption[0]);
                LoadError++;
                if (LoadError >= 750)
                {
                    PlayerPrefs.SetInt("FileTooLong", 0);
                    LoadError = 0;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Label("<size=20><b><color=#f5a97f>Rename</color></b></size>", new GUILayoutOption[0]);
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = RecteUtils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            renameName = GUILayout.TextField(renameName, sty, new GUILayoutOption[0]);
            if (GUILayout.Button("Rename File", new GUILayoutOption[0]))
            {
                if (renameName.Length >= 63)
                {
                    PlayerPrefs.SetInt("FileTooLong", 1);
                }
                else
                {
                    string text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/" + renameName + ".Recte";
                    PlayerPrefs.SetString("loadedFileName", renameName + ".Recte");
                    File.Move(PlayerPrefs.GetString("newFileNameInfo"), text);
                }
            }
            if (PlayerPrefs.GetInt("WrongFile") == 1)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label("<size=30><color=#ed8796>Invalid File Type!</color></size>", new GUILayoutOption[0]);
                LoadError++;
                if (LoadError >= 750)
                {
                    PlayerPrefs.SetInt("WrongFile", 0);
                    LoadError = 0;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            if (GUILayout.Button("Save Config", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {


                string[] array = new string[]
                {
                    //Render Toggles
                    PlayerPrefsX.GetBool("RainbowOverlay").ToString(),
                    PlayerPrefs.GetFloat("RainbowSat").ToString(),
                    PlayerPrefsX.GetBool("ShowSelfESP").ToString(),
                    PlayerPrefsX.GetBool("MaxESPDist").ToString(),
                    PlayerPrefs.GetFloat("MaxESPDistance").ToString(),
                    PlayerPrefsX.GetBool("StringESP").ToString(),
                    PlayerPrefsX.GetBool("CornerBox").ToString(),
                    PlayerPrefsX.GetBool("BoxFilledESP").ToString(),
                    PlayerPrefsX.GetBool("BoxOutlineESP").ToString(),
                    PlayerPrefsX.GetBool("Tracers").ToString(),
                    PlayerPrefsX.GetBool("HealthShieldESP").ToString(),
                    PlayerPrefs.GetInt("HPBoxLoc").ToString(),
                    PlayerPrefs.GetInt("HPBoxOffset").ToString(),
                    PlayerPrefsX.GetBool("ShieldBoxCheck").ToString(),
                    PlayerPrefsX.GetBool("Skeleton").ToString(),
                    PlayerPrefsX.GetBool("Chams").ToString(),
                    PlayerPrefsX.GetBool("RecteESP").ToString(),
                    PlayerPrefsX.GetBool("TargetLine").ToString(),
                    PlayerPrefsX.GetBool("HeadESP").ToString(),
                    PlayerPrefs.GetInt("HeadESPShape").ToString(),
                    PlayerPrefsX.GetBool("ChestChams").ToString(),
                    PlayerPrefsX.GetBool("WeaponChams").ToString(),
                    PlayerPrefsX.GetBool("AmmoChams").ToString(),
                    PlayerPrefsX.GetBool("MatsChams").ToString(), //23

                    //Render Colors
                    PlayerPrefs.GetString("ChamsColor"),
                    PlayerPrefs.GetString("TracerColor"),
                    PlayerPrefs.GetString("CornerBoxColor"),
                    PlayerPrefs.GetString("BoxOutlineESPColor"),
                    PlayerPrefs.GetString("BoxFilledESPColor"),
                    PlayerPrefs.GetString("SkeletonColor"),
                    PlayerPrefs.GetString("HeadESPColor"),
                    PlayerPrefs.GetString("ChestChamsColor"),
                    PlayerPrefs.GetString("ItemChamsWeaponColor"),
                    PlayerPrefs.GetString("ItemChamsAmmoColor"),
                    PlayerPrefs.GetString("ItemChamsBuildsColor"), //34

                    //Weapon
                    PlayerPrefsX.GetBool("Aimbot").ToString(),
                    PlayerPrefsX.GetBool("DrawFOVCircle").ToString(),
                    PlayerPrefsX.GetBool("VisibilityCheck").ToString(),
                    PlayerPrefsX.GetBool("NoKeyAimbot").ToString(),
                    PlayerPrefs.GetInt("AimbotButton").ToString(),
                    PlayerPrefs.GetFloat("AimbotFOV").ToString(), // 40
                    PlayerPrefs.GetString("FOVCircleColor"),
                    PlayerPrefs.GetInt("SideCount").ToString(),
                    PlayerPrefs.GetInt("FOVCircleType").ToString(),
                    PlayerPrefs.GetInt("CurBone").ToString(),
                    Recte_1v1lol.Cheats.AimbotCheats.curBone.ToString(), //45
                    PlayerPrefs.GetInt("AimbotSort").ToString(),
                    PlayerPrefsX.GetBool("RapidFire").ToString(),
                    PlayerPrefsX.GetBool("InfiniteAmmo").ToString(),
                    PlayerPrefsX.GetBool("NoRecoil").ToString(),
                    PlayerPrefsX.GetBool("MagicBullet").ToString(),

                    //Misc
                    PlayerPrefsX.GetBool("AddAllOnStart").ToString(),
                    PlayerPrefsX.GetBool("SpeedHack").ToString(),
                    PlayerPrefsX.GetBool("Flight").ToString(),
                    PlayerPrefs.GetFloat("SpeedBoost").ToString(),
                    PlayerPrefs.GetFloat("FlightSpeed").ToString(),
                    PlayerPrefsX.GetBool("BuildSpam").ToString(),
                    PlayerPrefsX.GetBool("UnlimitedBuilds").ToString(),
                    PlayerPrefsX.GetBool("BreakAllToggle").ToString(),
                    PlayerPrefsX.GetBool("customScale").ToString(),
                    PlayerPrefs.GetFloat("LocalScale").ToString(),
                    PlayerPrefsX.GetBool("GodMode").ToString(),
                    PlayerPrefsX.GetBool("JumpHeight").ToString(),
                    PlayerPrefsX.GetBool("SpoofID").ToString(),
                    PlayerPrefsX.GetBool("HitboxExpansion").ToString(),
                    PlayerPrefs.GetFloat("HitboxExpand").ToString(),
                    PlayerPrefs.GetFloat("JH").ToString(),
                    PlayerPrefsX.GetBool("BHop").ToString(),
                    PlayerPrefsX.GetBool("AreBuildingsOneHit").ToString(),
                    PlayerPrefsX.GetBool("WeaponTrail").ToString(),

                    //PlayerList
                    PlayerPrefsX.GetBool("RandomName").ToString(),
                    PlayerPrefsX.GetBool("AntiDiscord").ToString(),

                    //Client
                    PlayerPrefs.GetInt("MenuStyle").ToString(),
                    PlayerPrefsX.GetBool("Watermark").ToString(),
                    PlayerPrefs.GetInt("WatermarkType").ToString(),

                    //KeyBinds
                    PlayerPrefs.GetString("MenuToggleKeyCode"),
                    PlayerPrefs.GetString("KillAllKeyCode"),
                    PlayerPrefs.GetString("DestroyAllBuildsKeyCode"),
                    PlayerPrefs.GetString("SpeedHackKeyCode"),
                    PlayerPrefs.GetString("FlightKeyCode"),
                    PlayerPrefs.GetString("GodModeKeyCode"),
                    PlayerPrefs.GetString("BreakAllToggleKeyCode"),



            };

                RecteUtils.Save(array, "/Config/", "Config - " + RecteUtils.CreateRandomStringForDir(6) + ".Recte");
            }
            if (GUILayout.Button("Load Config", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                if (!PlayerPrefs.GetString("loadedFileName").EndsWith(".Recte"))
                {
                    PlayerPrefs.SetInt("WrongFile", 1);
                }
                else
                {
                    string[] array = RecteUtils.Load(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/" + PlayerPrefs.GetString("loadedFileName"));

                    //Rendering
                    if (PlayerPrefsX.GetBool("LoadRenders", true))
                    {
                        
                        PlayerPrefsX.SetBool("RainbowOverlay", bool.Parse(array[0]));
                        PlayerPrefs.SetFloat("RainbowSat", float.Parse(array[1]));
                        PlayerPrefsX.SetBool("ShowSelfESP", bool.Parse(array[2]));
                        PlayerPrefsX.SetBool("MaxESPDist", bool.Parse(array[3]));
                        PlayerPrefs.SetFloat("MaxESPDistance", float.Parse(array[4]));
                        PlayerPrefsX.SetBool("StringESP", bool.Parse(array[5]));
                        PlayerPrefsX.SetBool("CornerBox", bool.Parse(array[6]));
                        PlayerPrefsX.SetBool("BoxFilledESP", bool.Parse(array[7]));
                        PlayerPrefsX.SetBool("BoxOutlineESP", bool.Parse(array[8]));
                        PlayerPrefsX.SetBool("Tracers", bool.Parse(array[9])) ;
                        PlayerPrefsX.SetBool("HealthShieldESP", bool.Parse(array[10]));
                        PlayerPrefs.SetInt("HPBoxLoc", int.Parse(array[11]));
                        PlayerPrefs.SetInt("HPBoxOffset", int.Parse(array[12]));
                        PlayerPrefsX.SetBool("ShieldBoxCheck", bool.Parse(array[13]));
                        PlayerPrefsX.SetBool("Skeleton", bool.Parse(array[14]));
                        PlayerPrefsX.SetBool("Chams", bool.Parse(array[15]));
                        PlayerPrefsX.SetBool("RecteESP", bool.Parse(array[16]));
                        PlayerPrefsX.SetBool("TargetLine", bool.Parse(array[17]));
                        PlayerPrefsX.SetBool("HeadESP", bool.Parse(array[18]));
                        PlayerPrefs.SetInt("HeadESPShape", int.Parse(array[19]));
                        PlayerPrefsX.SetBool("ChestChams", bool.Parse(array[20]));
                        PlayerPrefsX.SetBool("WeaponChams", bool.Parse(array[21]));
                        PlayerPrefsX.SetBool("AmmoChams", bool.Parse(array[22]));
                        PlayerPrefsX.SetBool("MatsChams", bool.Parse(array[23]));


                        PlayerPrefs.SetString("ChamsColor", array[24]);
                        PlayerPrefs.SetString("TracerColor", array[25]);
                        PlayerPrefs.SetString("CornerBoxColor", array[26]);
                        PlayerPrefs.SetString("BoxOutlineESPColor", array[27]);
                        PlayerPrefs.SetString("BoxFilledESPColor", array[28]);
                        PlayerPrefs.SetString("SkeletonColor", array[29]);
                        PlayerPrefs.SetString("HeadESPColor", array[30]);
                        PlayerPrefs.SetString("ChestChamsColor", array[31]);
                        PlayerPrefs.SetString("ItemChamsWeaponColor", array[32]);
                        PlayerPrefs.SetString("ItemChamsAmmoColor", array[33]);
                        PlayerPrefs.SetString("ItemChamsBuildsColor", array[34]);
                    }
                    //Weapon
                    if (PlayerPrefsX.GetBool("LoadWeapons", true))
                    {
                        PlayerPrefsX.SetBool("Aimbot", bool.Parse(array[35]));
                        PlayerPrefsX.SetBool("DrawFOVCircle", bool.Parse(array[36]));
                        PlayerPrefsX.SetBool("VisibilityCheck", bool.Parse(array[37]));
                        PlayerPrefsX.SetBool("NoKeyAimbot", bool.Parse(array[38]));
                        PlayerPrefs.SetInt("AimbotButton", int.Parse(array[39]));
                        PlayerPrefs.SetFloat("AimbotFOV", float.Parse(array[40]));
                        PlayerPrefs.SetString("FOVCircleColor", array[41]);
                        PlayerPrefs.SetInt("SideCount", int.Parse(array[42]));
                        PlayerPrefs.SetInt("FOVCircleType", int.Parse(array[43]));
                        PlayerPrefs.SetInt("CurBone", int.Parse(array[44]));
                        Recte_1v1lol.Cheats.AimbotCheats.curBone = (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), array[45]);
                        PlayerPrefs.SetInt("AimbotSourt", int.Parse(array[46]));
                        PlayerPrefsX.SetBool("RapidFire", bool.Parse(array[47]));
                        PlayerPrefsX.SetBool("InfiniteAmmo", bool.Parse(array[48]));
                        PlayerPrefsX.SetBool("NoRecoil", bool.Parse(array[49]));
                        PlayerPrefsX.SetBool("MagicBullet", bool.Parse(array[50])); // 50
                    }


                    //Misc
                    if (PlayerPrefsX.GetBool("LoadMisc", true))
                    {
                        /*layerPrefsX.GetBool("AddAllOnStart").ToString(),
                    PlayerPrefsX.GetBool("SpeedHack").ToString(),
                    PlayerPrefsX.GetBool("Flight").ToString(),
                    PlayerPrefs.GetFloat("SpeedBoost").ToString(),
                    PlayerPrefs.GetFloat("FlightSpeed").ToString(),
                    PlayerPrefsX.GetBool("BuildSpam").ToString(),
                    PlayerPrefsX.GetBool("UnlimitedBuilds").ToString(),
                    PlayerPrefsX.GetBool("BreakAllToggle").ToString(),
                    PlayerPrefsX.GetBool("customScale").ToString(),
                    PlayerPrefs.GetFloat("LocalScale").ToString(),
                    PlayerPrefsX.GetBool("GodMode").ToString(),
                    PlayerPrefsX.GetBool("JumpHeight").ToString(),
                    PlayerPrefsX.GetBool("SpoofID").ToString(),
                    PlayerPrefsX.GetBool("HitboxExpansion").ToString(),
                    PlayerPrefs.GetFloat("HitboxExpand").ToString(),
                    PlayerPrefs.GetFloat("JH").ToString(),
                    PlayerPrefsX.GetBool("BHop").ToString(),
                    PlayerPrefsX.GetBool("AreBuildingsOneHit").ToString(),
                    PlayerPrefsX.GetBool("WeaponTrail").ToString(),*/
                        PlayerPrefsX.SetBool("AddAllOnStart", bool.Parse(array[51]));
                        PlayerPrefsX.SetBool("SpeedHack", bool.Parse(array[52]));
                        PlayerPrefsX.SetBool("Flight", bool.Parse(array[53]));
                        PlayerPrefs.SetFloat("SpeedBoost", float.Parse(array[54]));
                        PlayerPrefs.SetFloat("FlightSpeed", float.Parse(array[55]));
                        PlayerPrefsX.SetBool("BuildSpam", bool.Parse(array[56]));
                        PlayerPrefsX.SetBool("UnlimitedBuilds", bool.Parse(array[57]));
                        PlayerPrefsX.SetBool("BreakAllToggle", bool.Parse(array[58]));
                        PlayerPrefsX.SetBool("customScale", bool.Parse(array[59]));
                        PlayerPrefs.SetFloat("LocalScale", float.Parse(array[60]));
                        PlayerPrefsX.SetBool("GodMode", bool.Parse(array[61]));
                        PlayerPrefsX.SetBool("JumpHeight", bool.Parse(array[62]));
                        PlayerPrefsX.SetBool("SpoofID", bool.Parse(array[63]));
                        PlayerPrefsX.SetBool("HitboxExpansion", bool.Parse(array[64]));
                        PlayerPrefs.SetFloat("HitboxExpand", float.Parse(array[65]));
                        PlayerPrefs.SetFloat("JH", float.Parse(array[66]));
                        PlayerPrefsX.SetBool("BHop", bool.Parse(array[67]));
                        PlayerPrefsX.SetBool("AreBuildingsOneHit", bool.Parse(array[68]));
                        PlayerPrefsX.SetBool("WeaponTrail", bool.Parse(array[69]));
                    }


                    //PlayerList
                    if (PlayerPrefsX.GetBool("LoadPList", true))
                    {
                        PlayerPrefsX.SetBool("RandomName", bool.Parse(array[70]));
                        PlayerPrefsX.SetBool("AntiDiscord", bool.Parse(array[71]));
                    }

                    //Client Menu
                    if (PlayerPrefsX.GetBool("LoadClient", true))
                    {
                        PlayerPrefs.SetInt("MenuStyle", int.Parse(array[72]));
                        PlayerPrefsX.SetBool("Watermark", bool.Parse(array[73]));
                        PlayerPrefs.SetInt("MenuStyle", int.Parse(array[74]));
                    }
                    if (PlayerPrefsX.GetBool("LoadKeybinds"))
                    {
                        PlayerPrefs.SetString("MenuToggleKeyCode", array[75]);
                        PlayerPrefs.SetString("KillAllKeyCode", array[76]);
                        PlayerPrefs.SetString("DestroyAllBuildsKeyCode", array[77]);
                        PlayerPrefs.SetString("SpeedHackKeyCode", array[78]);
                        PlayerPrefs.SetString("FlightKeyCode", array[79]);
                        PlayerPrefs.SetString("GodModeKeyCode", array[80]);
                        PlayerPrefs.SetString("BreakAllToggleKeyCode", array[81]);
                    }
                }
            }
            GUILayout.EndHorizontal();
            if (!PlayerPrefsX.GetBool("LoadRenders") && GUILayout.Button("Load Render: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadRenders", true);
            }
            if (PlayerPrefsX.GetBool("LoadRenders") && GUILayout.Button("Load Render: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadRenders", false);
            }
            if (!PlayerPrefsX.GetBool("LoadWeapons") && GUILayout.Button("Load Weapon: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadWeapons", true);
            }
            if (PlayerPrefsX.GetBool("LoadWeapons") && GUILayout.Button("Load Weapon: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadWeapons", false);
            }
            if (!PlayerPrefsX.GetBool("LoadMisc") && GUILayout.Button("Load Misc: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadMisc", true);
            }
            if (PlayerPrefsX.GetBool("LoadMisc") && GUILayout.Button("Load Misc: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadMisc", false);
            }
            if (!PlayerPrefsX.GetBool("LoadPList") && GUILayout.Button("Load Player List: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadPList", true);
            }
            if (PlayerPrefsX.GetBool("LoadPList") && GUILayout.Button("Load PlayerList: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadPList", false);
            }
            if (!PlayerPrefsX.GetBool("LoadClient") && GUILayout.Button("Load Client: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadClient", true);
            }
            if (PlayerPrefsX.GetBool("LoadClient") && GUILayout.Button("Load Client: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadClient", false);
            }
            if (!PlayerPrefsX.GetBool("LoadKeybinds") && GUILayout.Button("Load Keybinds: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadKeybinds", true);
            }
            if (PlayerPrefsX.GetBool("LoadKeybinds") && GUILayout.Button("Load Keybinds: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("LoadKeybinds", false);
            }
        }
        public void ClientMenu()
        {
            if (PlayerPrefs.GetInt("MenuStyle") == 0 && GUILayout.Button("Window Style: Vertical", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("MenuStyle", 1);
            }
            if (PlayerPrefs.GetInt("MenuStyle") == 1 && GUILayout.Button("Window Style: CS:GO", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefs.SetInt("MenuStyle", 0);
            }
            if (!PlayerPrefsX.GetBool("Watermark") && GUILayout.Button("Watermark: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("Watermark", true);
            }
            if (PlayerPrefsX.GetBool("Watermark") && GUILayout.Button("Watermark: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("Watermark", false);
            }
            if (PlayerPrefsX.GetBool("Watermark"))
            {
                if (PlayerPrefs.GetInt("WatermarkType") == 0 && GUILayout.Button("Watermark: Gamesense.vip", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 1);
                }
                if (PlayerPrefs.GetInt("WatermarkType") == 1 && GUILayout.Button("Watermark: Neverlose.cc", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 2);
                }
                if (PlayerPrefs.GetInt("WatermarkType") == 2 && GUILayout.Button("Watermark: AIMWARE.net", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 3);
                }
                if (PlayerPrefs.GetInt("WatermarkType") == 3 && GUILayout.Button("Watermark: Prodigyhook.xyz", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 4);
                }
                if (PlayerPrefs.GetInt("WatermarkType") == 4 && GUILayout.Button("Watermark: Exibition", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 5);
                }
                if (PlayerPrefs.GetInt("WatermarkType") == 5 && GUILayout.Button("Watermark: Dortware", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 6);
                }
                if (PlayerPrefs.GetInt("WatermarkType") == 6 && GUILayout.Button("Watermark: Adapt", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    PlayerPrefs.SetInt("WatermarkType", 0);
                }
            }
        }
        private void KeyCodes()
        {
            if (GUILayout.Button((!Keybinds.listenForKey0) ? ("Menu Toggle Keybind: " + PlayerPrefs.GetString("MenuToggleKeyCode", "RightShift")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey0 = true;
                Keybinds.KeyToList = "MenuToggleKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey) ? ("Kill All Keybind: " + PlayerPrefs.GetString("KillAllKeyCode", "K")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey = true;
                Keybinds.KeyToList = "KillAllKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey1) ? ("Destroy All Builds Keybind: " + PlayerPrefs.GetString("DestroyAllBuildsKeyCode", "I")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey1 = true;
                Keybinds.KeyToList = "DestroyAllBuildsKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey2) ? ("Speed Hack Keybind " + PlayerPrefs.GetString("SpeedHackKeyCode", "B")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey2 = true;
                Keybinds.KeyToList = "SpeedHackKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey3) ? ("Flight Keybind: " + PlayerPrefs.GetString("FlightKeyCode", "F")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey3 = true;
                Keybinds.KeyToList = "FlightKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey4) ? ("God Mode Keybind: " + PlayerPrefs.GetString("GodModeKeyCode", "H")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey4 = true;
                Keybinds.KeyToList = "GodModeKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey5) ? ("Break All Toggle Keybind: " + PlayerPrefs.GetString("BreakAllToggleKeyCode", "L")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey5 = true;
                Keybinds.KeyToList = "BreakAllToggleKeyCode";
            }
        }
        public static void Gamesense()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(284f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(284f, 28f);

            string watermarkText = $"{GetRainbowWaveText("Recte")} | {PhotonNetwork.NickName} | {DateTime.Now.Hour}:{DateTime.Now.Minute} | {PhotonNetwork.GetPing()}ms";

            RecteUtils.RectFilled(new Vector2(vector.x, vector.y), new Vector2(268f, 22f), RecteUtils.GetColorFromString("373737"), false);
            RecteUtils.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 13, 0);
            
            sat = .35f;
            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);
            Color lc = RecteUtils.GetColorFromString("232323");
            RecteUtils.DrawNonESPLine(new Vector2(vector.x - 2f, vector.y), new Vector2(vector2.x + 2f, vector2.y), color, 2.25f); // Top
            RecteUtils.DrawNonESPLine(vector2, vector4, lc, 2.25f); // Right Side
            RecteUtils.DrawNonESPLine(vector3, vector, lc, 2.25f); // Left Side
            RecteUtils.DrawNonESPLine(new Vector2(vector4.x + 2f, vector4.y), new Vector2(vector3.x - 2f, vector3.y), lc, 2.25f); // Bottom
        }

        public static void Neverlose()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(284f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(284f, 28f);

            string waterMarkText = $"<b>{GetRainbowWaveText("Recte")}<color=#5b6078> | </color></b>{RecteUtils.Version}<b><color=#5b6078> | </color></b>{PhotonNetwork.GetPing()}ms<b><color=#5b6078> | </color></b>{(int)((float)((int)(1f / Time.unscaledDeltaTime)))} fps<b><color=#5b6078> | </color></b>{DateTime.Now.Hour}:{DateTime.Now.Minute}";

            RecteUtils.RectFilled(new Vector2(vector.x, vector.y), new Vector2(268f, 22f), RecteUtils.GetColorFromString("24273a"), false);
            RecteUtils.DrawText(new Vector2(42f, 12f), waterMarkText, Color.black, false, 13, FontStyle.Normal, 0);

            //Color bg = Color.HSVToRGB(hue, sat, bri);
            Color lc = RecteUtils.GetColorFromString("181926"); 
            RecteUtils.DrawNonESPLine(new Vector2(vector.x - 2f, vector.y), new Vector2(vector2.x + 2f, vector2.y), lc, 3f); // Top
            RecteUtils.DrawNonESPLine(vector2, vector4, lc, 3f); // Right Side
            RecteUtils.DrawNonESPLine(vector3, vector, lc, 3f); // Left Side
            RecteUtils.DrawNonESPLine(new Vector2(vector4.x + 2f, vector4.y), new Vector2(vector3.x - 2f, vector3.y), lc, 3f); // Bottom
           
        }

        public static void Aimware()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(284f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(284f, 28f);

            string waterMarkText = $"{GetRainbowWaveText("Recte")} | Delay: {PhotonNetwork.GetPing()}ms | FPS: {(int)((float)((int)(1f / Time.unscaledDeltaTime)))}";

            RecteUtils.RectFilled(new Vector2(vector.x, vector.y), new Vector2(268f, 22f), RecteUtils.GetColorFromString("00000060"), false);
            RecteUtils.DrawText(new Vector2(42f, 12f), waterMarkText, Color.black, false, 13, FontStyle.Normal, 0);
        }

        public static void ProdHook()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(186f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(186f, 28f);   
            //284 - 268
            // 8 Each Side
            string watermarkText = $"<b>{GetRainbowWaveText("Recte")} | {PhotonNetwork.GetPing()}ms | fps: {(int)((float)((int)(1f / Time.unscaledDeltaTime)))}</b>";

            RecteUtils.RectFilled(new Vector2(vector.x, vector.y), new Vector2(170, 22f), RecteUtils.GetColorFromString("00000050"), false);
            RecteUtils.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 13, FontStyle.Bold, 0);
            
            sat = .35f;
            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);
            Color lc = RecteUtils.GetColorFromString("232323");
            RecteUtils.DrawNonESPLine(new Vector2(vector.x - 2f, vector.y), new Vector2(vector2.x + 2f, vector2.y), color, 2.25f); // Top
            RecteUtils.DrawNonESPLine(new Vector2(vector4.x + 2f, vector4.y), new Vector2(vector3.x - 2f, vector3.y), color, 2.25f); // Bottom
        }

        public static void Exhibition()
        {
            hue += 0.0007f;
            if (hue >= 1f)
            {
                hue = 0f;
            }
            sat = 0.35f;
            bri = 1f;

            // Calculate the RGB wave colors

            // Create a rainbow wave string for "Recte"

            string watermarkText = $"{GetRainbowWaveText("Recte")} | {DateTime.Now.Hour}:{DateTime.Now.Minute} | {(int)((1f / Time.unscaledDeltaTime))} FPS | {PhotonNetwork.GetPing()}ms";

            RecteUtils.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 16, FontStyle.Bold, 0);
        }
        public static string GetReversedRainbowWaveText(string text)
        {
            string rainbowText = "";

            for (int i = 0; i < text.Length; i++)
            {
                // Calculate the hue for the current character based on its position and the overall hue.
                float characterHue = hue + (i / (float)text.Length);

                // Ensure that characterHue remains within [0, 1] by wrapping it around.
                if (characterHue < 0f)
                {
                    characterHue += 1f;
                }
                else if (characterHue >= 1f)
                {
                    characterHue -= 1f;
                }

                // Use rich text to apply rainbow colors to each character.
                Color characterColor = Color.HSVToRGB(characterHue, .35f, 1f);
                rainbowText += $"<color=#{ColorUtility.ToHtmlStringRGB(characterColor)}>{text[i]}</color>";
            }

            return rainbowText;
        }

        public static string GetRainbowWaveText(string text)
        {
            string rainbowText = "";

            for (int i = 0; i < text.Length; i++)
            {
                // Calculate the hue for the current character based on its position and the overall hue.
                float characterHue = hue - (i / (float)text.Length);

                // Ensure that characterHue remains within [0, 1] by wrapping it around.
                if (characterHue < 0f)
                {
                    characterHue += 1f;
                }

                // Use rich text to apply rainbow colors to each character.
                Color characterColor = Color.HSVToRGB(characterHue, .35f, 1f);
                rainbowText += $"<color=#{ColorUtility.ToHtmlStringRGB(characterColor)}>{text[i]}</color>";
            }

            return rainbowText;
        }
        public static void Dortware()
        {

            
            sat = .35f;
            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);
            string watermarkText = $"{GetRainbowWaveText("Recte")} {RecteUtils.Version}";
            RecteUtils.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 16, FontStyle.Bold, 0);
        }

        public static void Adapt()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(186f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(186f, 28f);

            
            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);

            string watermarkText = $"{GetRainbowWaveText("Recte")} | {RecteUtils.Version} | {PhotonNetwork.NickName}";

            RecteUtils.RectFilled(new Vector2(vector.x, vector.y), new Vector2(170 + (1.5f*watermarkText.Length)+10f, 22f), RecteUtils.GetColorFromString("00000050"), false);
            RecteUtils.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 16, FontStyle.Bold, 0);
        }
        
        public static void Watermark()
        {
            
            if (PlayerPrefs.GetInt("WatermarkType") == 0)
            {
                Gamesense();
            }
            if (PlayerPrefs.GetInt("WatermarkType") == 1)
            {
                Neverlose();
            }
            if (PlayerPrefs.GetInt("WatermarkType") == 2)
            {
                Aimware();
            }
            if (PlayerPrefs.GetInt("WatermarkType") == 3)
            {
                ProdHook();
            }
            if (PlayerPrefs.GetInt("WatermarkType") == 4)
            {
                Exhibition();
            }
            if (PlayerPrefs.GetInt("WatermarkType") == 5)
            {
                Dortware();
            }
            if (PlayerPrefs.GetInt("WatermarkType") == 6)
            {
                Adapt();
            }

        }
    }
}
