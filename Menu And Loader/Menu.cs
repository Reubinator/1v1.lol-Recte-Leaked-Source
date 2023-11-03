using JustPlay.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using Recte.Cheats;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using System.Net;

namespace Recte
{
    public class Menu : MonoBehaviour
    {
        public static float hue;
        public static float sat;
        public static float bri;
        public static Vector2 ScrollPos8;
        public static string renameName;
        public static string version;
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
        public float waveSpeed = .1f;
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
        public void Start()
        {
            //PlayerPrefs.DeleteAll();

            TitleRect = new Rect((float)((double)Screen.width / 4.0), 70f, 250f, 50f);
            rect2 = new Rect((float)((double)Screen.width / 4.0), 70f, 250f, 50f);
            rect1 = new Rect((float)((double)Screen.width / 2.64), 70f, 431f, 50f);
            mainMenu = new Rect((float)((double)Screen.width / 2.64), 70f, 865f, 25f);
            base.Invoke("AddTypes", 1f);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/");
            }
            if (PlayerPrefsX.GetBool("AddAllOnStart", false))
            {
                
            }
            base.StartCoroutine(GetVersion());
            UiManager uiManager = UnityEngine.Object.FindObjectOfType<UiManager>();
            uiManager.ShowToast(new DefaultedLocalizedString(new LocalizedString("", ""), "Recte <3"), 10f);
        }
        public static IEnumerator GetVersion()
        {
            UnityWebRequest www = UnityWebRequest.Get("https://e-z.tools/p/raw/kz95llamb6");
            yield return www.SendWebRequest();
            version = www.downloadHandler.text;
            yield break;
        }
        private void AddTypes()
        {
            if (base.GetComponent<qLogger>() == null)
            {
                base.gameObject.AddComponent<qLogger>();
            }
        }

        public void OnGUI()
        {
            
            if (!string.Equals(version, Utils.Version, StringComparison.OrdinalIgnoreCase) && version != string.Empty)
            {
                Render.DrawText(Utils.CenterOfScreen(), "Client Needs Update! Get It At .gg/DZZ8cXTjG6", Color.white, Color.black, true, 12, FontStyle.Bold, 1);
            }
            GUI.backgroundColor = Utils.GetColorFromString("24273a");
            if (Vars.MenuStyle == 0)
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
                        $"<b>Recte {Utils.Version} Beta</b>",
                        new GUILayoutOption[0]
                    );
                }
            }
            if (Vars.MenuStyle == 1)
            {
                if (Vars.open && PlayerPrefs.GetInt("Banned") != 1)
                {

                    TitleRect = GUILayout.Window(888, TitleRect, new GUI.WindowFunction(MenuWindow1), "<b>Menus:</b>", new GUILayoutOption[0]);
                    rect2 = GUILayout.Window(889, rect2, new GUI.WindowFunction(MenuWindow2), $"<b>Recte {Utils.Version} Beta</b>", new GUILayoutOption[0]);
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
            if (Vars.Watermark)
            {
                Watermark();
            }
        }
        public static void Watermark()
        {
            GUI.contentColor = Utils.GetColorFromString("cad3f5");
            if (Vars.WatermarkType == 0)
            {
                Gamesense();
            }
            if (Vars.WatermarkType == 1)
            {
                Neverlose();
            }
            if (Vars.WatermarkType == 2)
            {
                Aimware();
            }
            if (Vars.WatermarkType == 3)
            {
                ProdHook();
            }
            if (Vars.WatermarkType == 4)
            {
                Exhibition();
            }
            if (Vars.WatermarkType == 5)
            {
                Dortware();
            }
            if (Vars.WatermarkType == 6)
            {
                Adapt();
            }
            if (Vars.WatermarkType == 7)
            {

            }
        }

        private void Update()
        {
            hue += waveSpeed * Time.deltaTime;
            if (hue >= 1f)
            {
                hue -= 1f;
            }
            KeyControls();
        }

        public void KeyControls()
        {
            if (Input.GetKeyUp((KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GUIToggleKeyCode", "RightShift"))))
            {
                Vars.open = !Vars.open;
            }
            if (Input.GetKeyUp(KeyCode.F11))
            {
                Screen.fullScreen = true;
            }
        }
        private void MenuWindow(int wID)
        {
            GUI.backgroundColor = Utils.GetColorFromString("6e738d");
            GUI.contentColor = Utils.GetColorFromString("cad3f5");
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);

            if (GUILayout.Button("<b>Visuals</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 0;
            }
            if (GUILayout.Button("<b>Combat</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 1;
            }
            if (GUILayout.Button("<b>Misc</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 2;
            }
            if (GUILayout.Button("<b>Player List</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 3;
            }
            if (GUILayout.Button("<b>Console</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 4;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<b>Client</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 5;
            }
            if (GUILayout.Button("<b>Keybinds</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 6;
            }
            if (GUILayout.Button("<b>Config</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 7;
            }
            GUILayout.EndHorizontal();
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUILayoutOption[] { GUILayout.Height(500f) });
            if (Vars.selectedTab == 0)
            {
                Visuals();
            }
            if (Vars.selectedTab == 1)
            {
                Weapons();
            }
            if (Vars.selectedTab == 2)
            {
                Misc();
            }
            if (Vars.selectedTab == 3)
            {
                PlayerList();
            }
            if (Vars.selectedTab == 4)
            {
                qLogger.instance.LoggingPart();
            }
            if (Vars.selectedTab == 5)
            {
                ClientMenu();
            }
            if (Vars.selectedTab == 6)
            {
                KeyCodes();
            }
            if (Vars.selectedTab == 7)
            {
                ConfigManager();
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();
        }
        public void MenuWindow1(int wID)
        {
            scrollPos1 = GUILayout.BeginScrollView(scrollPos1, new GUILayoutOption[] { GUILayout.Height(300f) });
            GUI.backgroundColor = Utils.GetColorFromString("6e738d");
            GUI.contentColor = Utils.GetColorFromString("cad3f5");

            if (GUILayout.Button("<b>Visuals</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 0;
            }
            if (GUILayout.Button("<b>Combat</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 1;
            }
            if (GUILayout.Button("<b>Misc</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 2;
            }
            if (GUILayout.Button("<b>Player List</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 3;
            }
            if (GUILayout.Button("<b>Console</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 4;
            }
            if (GUILayout.Button("<b>Client</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 5;
            }
            if (GUILayout.Button("<b>Keybinds</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 6;
            }
            if (GUILayout.Button("<b>Config</b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.selectedTab = 7;
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();

        }
        public void MenuWindow2(int ID)
        {
            GUI.backgroundColor = Utils.GetColorFromString("6e738d");
            GUI.contentColor = Utils.GetColorFromString("cad3f5");
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUILayoutOption[]
            {
            GUILayout.Width(500f),
            GUILayout.Height(300f)
            });
            if (Vars.selectedTab == 0)
            {
                Visuals();
            }
            if (Vars.selectedTab == 1)
            {
                Weapons();
            }
            if (Vars.selectedTab == 2)
            {
                Misc();
            }
            if (Vars.selectedTab == 3)
            {
                PlayerList();
            }
            if (Vars.selectedTab == 4)
            {
                qLogger.instance.LoggingPart();
            }
            if (Vars.selectedTab == 5)
            {
                ClientMenu();
            }
            if (Vars.selectedTab == 6)
            {
                KeyCodes();
            }
            if (Vars.selectedTab == 7)
            {
                ConfigManager();
            }
            GUILayout.EndScrollView();
            GUI.DragWindow();

        }
        public void Visuals()
        {
            
            GUILayout.BeginHorizontal();



            if (!Vars.RGBOverlay && GUILayout.Button("RGB Player Overlay: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.RGBOverlay = true;
            }
            if (Vars.RGBOverlay && GUILayout.Button("RGB Player Overlay: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.RGBOverlay = false;
            }
            /*
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

            if (Vars.RGBOverlay)
            {
                GUILayout.Label("Rainbow Saturation: " + Vars.RGBOverlaySaturation.ToString());
                Vars.RGBOverlaySaturation = GUILayout.HorizontalSlider((float)Math.Round((double)Vars.RGBOverlaySaturation, 3), .01f, 1f, new GUILayoutOption[0]);
            }

            GUILayout.BeginHorizontal();
            if (!Vars.showSelf && GUILayout.Button("Show Self: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.showSelf = true;
            }
            if (Vars.showSelf && GUILayout.Button("Show Self: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.showSelf = false;
            }
            if (!Vars.MaxESPDist && GUILayout.Button("Max ESP Distance: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.MaxESPDist = true;
            }
            if (Vars.MaxESPDist && GUILayout.Button("Max ESP Distance: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.MaxESPDist = false;
            }
            GUILayout.EndHorizontal();
            if (Vars.MaxESPDist)
            {
                GUILayout.Label("Max ESP Distance: " + Vars.MaxESPDistance);
                Vars.MaxESPDistance = Mathf.Round(GUILayout.HorizontalSlider((float)Math.Round((double)Vars.MaxESPDistance, 3), 25f, 400f));
            }
            GUILayout.BeginHorizontal();
            if (!Vars.Nametags && GUILayout.Button("Nametags: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Nametags = true;
            }
            if (Vars.Nametags && GUILayout.Button("Nametags: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Nametags = false;
            }
            if (!Vars.CornerBox && GUILayout.Button("Corner Box: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.CornerBox = true;
            }
            if (Vars.CornerBox && GUILayout.Button("Corner Box: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.CornerBox = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.FilledBox && GUILayout.Button("Filled Box: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.FilledBox = true;
            }
            if (Vars.FilledBox && GUILayout.Button("Filled Box: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.FilledBox = false;
            }

            if (!Vars.OutlineBox && GUILayout.Button("Outline Box: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.OutlineBox = true;
            }
            if (Vars.OutlineBox && GUILayout.Button("Outline Box: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.OutlineBox = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.Tracers && GUILayout.Button("Tracers: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Tracers = true;
            }
            if (Vars.Tracers && GUILayout.Button("Tracers: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Tracers = false;
            }
            if (!Vars.HealthBar && GUILayout.Button("Health Bars: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HealthBar = true;
            }
            if (Vars.HealthBar && GUILayout.Button("Health Bars: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HealthBar = false;
            }
            GUILayout.EndHorizontal();
            if (Vars.HealthBar)
            {
                if (Vars.HPBoxLocation == 0 && GUILayout.Button("Healthbox Position: Top", new GUILayoutOption[0]))
                {
                    Vars.HPBoxLocation = 1;
                }
                if (Vars.HPBoxLocation == 1 && GUILayout.Button("Healthbox Position: Bottom", new GUILayoutOption[0]))
                {
                    Vars.HPBoxLocation = 0;
                }
                if (Vars.BoxOffset == 0 && GUILayout.Button("Healthbox Offset: Center", new GUILayoutOption[0]))
                {
                    Vars.BoxOffset = 1;
                }
                if (Vars.BoxOffset == 1 && GUILayout.Button("Healthbox Offset: Sides", new GUILayoutOption[0]))
                {
                    Vars.BoxOffset = 0;
                }
                if (!Vars.ShieldBoxCheck && GUILayout.Button("Shield Check: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[0]))
                {
                    Vars.ShieldBoxCheck = true;
                }
                if (Vars.ShieldBoxCheck && GUILayout.Button("Shield Check: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[0]))
                {
                    Vars.ShieldBoxCheck = false;
                }
            }
            GUILayout.BeginHorizontal();

            if (!Vars.Skeleton && GUILayout.Button("Skeleton: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Skeleton = true;
            }
            if (Vars.Skeleton && GUILayout.Button("Skeleton: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Skeleton = false;
            }
            if (!Vars.Chams && GUILayout.Button("Chams: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Chams = true;
            }
            if (Vars.Chams && GUILayout.Button("Chams: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.Chams = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.RecteESP && GUILayout.Button("Recte ESP: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.RecteESP = true;
            }
            if (Vars.RecteESP && GUILayout.Button("Recte ESP: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.RecteESP = false;
            }
            if (!Vars.TargetLine && GUILayout.Button("Target Lins: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.TargetLine = true;
            }
            if (Vars.TargetLine && GUILayout.Button("Target Line: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.TargetLine = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.HeadESP && GUILayout.Button("HeadESP: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HeadESP = true;
            }
            if (Vars.HeadESP && GUILayout.Button("HeadESP: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HeadESP = false;
            }
            if (Vars.HeadESPShape == 0 && GUILayout.Button("Head ESP Shade: Circle", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HeadESPShape = 1;
            }
            if (Vars.HeadESPShape == 1 && GUILayout.Button("Head ESP Shape: Square", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HeadESPShape = 2;
            }
            if (Vars.HeadESPShape == 2 && GUILayout.Button("Head ESP Shade: Corner Box", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.HeadESPShape = 0;
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.ChestChams && GUILayout.Button("Chest Chams: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.ChestChams = true;
            }
            if (Vars.ChestChams && GUILayout.Button("Chest Chams: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.ChestChams = false;
            }
            if (!Vars.WeaponChams && GUILayout.Button("Weapon Chams: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.WeaponChams = true;
            }
            if (Vars.WeaponChams && GUILayout.Button("Weapon Chams: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.WeaponChams = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.AmmoChams && GUILayout.Button("Ammo Chams: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.AmmoChams = true;
            }
            if (Vars.AmmoChams && GUILayout.Button("Ammo Chams: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.AmmoChams = false;
            }

            if (!Vars.MatsChams && GUILayout.Button("Mats Chams: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.MatsChams = true;
            }
            if (Vars.MatsChams && GUILayout.Button("Mats Chams: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(scrollWidth) }))
            {
                Vars.MatsChams = false;
            }
            GUILayout.EndHorizontal();
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = Utils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;

            GUILayout.Label($"<b><color=#{Vars.ChamsColor}>Chams Color: </color></b>");
            Vars.ChamsColor = GUILayout.TextField(Vars.ChamsColor, sty, new GUILayoutOption[0]);


            GUILayout.Label($"<b><color=#{Vars.TracerColor}>Tracers Color: </color></b>");
            Vars.TracerColor = GUILayout.TextField(Vars.TracerColor, sty, new GUILayoutOption[0]);


            GUILayout.Label($"<b><color=#{Vars.CornerBoxColor}>Corner Box Color: </color></b>");
            Vars.CornerBoxColor = GUILayout.TextField(Vars.CornerBoxColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.BoxOutlineESPColor}>Box Outline ESP Color: </color></b>");
            Vars.BoxOutlineESPColor = GUILayout.TextField(Vars.BoxOutlineESPColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.BoxFilledESPColor}>Box Filled ESP Color: </color></b>");
            Vars.BoxFilledESPColor = GUILayout.TextField(Vars.BoxFilledESPColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.SkeletonColor}>Skeleton Color: </color></b>");
            Vars.SkeletonColor = GUILayout.TextField(Vars.SkeletonColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.HeadESPColor}>Head ESP Color: </color></b>");
            Vars.HeadESPColor = GUILayout.TextField(Vars.HeadESPColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.ChestChamsColor}>Chest Chams Color: </color></b>");
            Vars.ChestChamsColor = GUILayout.TextField(Vars.ChestChamsColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.ItemChamsWeaponColor}>Weapons Chams Color: </color></b>");
            Vars.ItemChamsWeaponColor = GUILayout.TextField(Vars.ItemChamsWeaponColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.ItemChamsAmmoColor}>Ammo Chams Color: </color></b>");
            Vars.ItemChamsAmmoColor = GUILayout.TextField(Vars.ItemChamsAmmoColor, sty, new GUILayoutOption[0]);

            GUILayout.Label($"<b><color=#{Vars.ItemChamsBuildsColor}>Mats Chams Color: </color></b>");
            Vars.ItemChamsBuildsColor = GUILayout.TextField(Vars.ItemChamsBuildsColor, sty, new GUILayoutOption[0]);

            //GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("SkyboxColor1", "\"87cefa")}>Skybox Color1: </color></b>");
            //PlayerPrefs.SetString("SkyboxColor1", GUILayout.TextField(PlayerPrefs.GetString("SkyboxColor1", "87cefa"), sty, new GUILayoutOption[0]));

            //GUILayout.Label($"<b><color=#{PlayerPrefs.GetString("SkyboxColor2", "\"ff69b4")}>Skybox Color2: </color></b>");
            //PlayerPrefs.SetString("SkyboxColor2", GUILayout.TextField(PlayerPrefs.GetString("SkyboxColor2", "ff69b4"), sty, new GUILayoutOption[0]));
        }

        public void Weapons()
        {

            if (!Vars.LockOn && GUILayout.Button("LockOn: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LockOn = true;
            }
            if (Vars.LockOn && GUILayout.Button("LockOn: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LockOn = false;
            }
            if (!Vars.SilentAim && GUILayout.Button("Silent Aim: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SilentAim = true;
            }
            if (Vars.SilentAim && GUILayout.Button("Silent Aim: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SilentAim = false;
            }
            if (!Vars.DrawFOV && GUILayout.Button("Draw FOV Shape: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.DrawFOV = true;
            }
            if (Vars.DrawFOV && GUILayout.Button("Draw FOV Shape: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.DrawFOV = false;
            }
            if (!Vars.VisibilityCheck && GUILayout.Button("Visibility Check: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.VisibilityCheck = true;
            }
            if (Vars.VisibilityCheck && GUILayout.Button("Visibility Check: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.VisibilityCheck = false;
            }
            if (!Vars.UseAimbotKey && GUILayout.Button("Use Aimbot Key: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.UseAimbotKey = true;
            }
            if (Vars.UseAimbotKey && GUILayout.Button("Use Aimbot Key: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.UseAimbotKey = false;
            }
            if (Vars.AimbotKey == 0 && GUILayout.Button("Mouse Bind: Left Mouse", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimbotKey = 1;
            }
            if (Vars.AimbotKey == 1 && GUILayout.Button("Mouse Bind: Right Mouse", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimbotKey = 0;
            }
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = Utils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            GUILayout.Label("Fov: " + Math.Round(Vars.FOVSize));
            Vars.FOVSize = GUILayout.HorizontalSlider(Vars.FOVSize, 20f, 750f, new GUILayoutOption[0]);
            GUILayout.Label($"<b><color=#{Vars.FOVCircleColor}>FOV Shape Color: </color></b>");
            Vars.FOVCircleColor = GUILayout.TextField(Vars.FOVCircleColor, sty, new GUILayoutOption[0]);
            if (Vars.FOVShape == 0 && GUILayout.Button("FOV Shape: Triangle", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 4;
                Vars.FOVShape = 1;
            }
            if (Vars.FOVShape == 1 && GUILayout.Button("FOV Shape: Square", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 5;
                Vars.FOVShape = 2;
            }
            if (Vars.FOVShape == 2 && GUILayout.Button("FOV Shape: Pentagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 6;
                Vars.FOVShape = 3;
            }
            if (Vars.FOVShape == 3 && GUILayout.Button("FOV Shape: Hexagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 7;
                Vars.FOVShape = 4;
            }
            if (Vars.FOVShape == 4 && GUILayout.Button("FOV Shape: Heptagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 8;
                Vars.FOVShape = 5;
            }
            if (Vars.FOVShape == 5 && GUILayout.Button("FOV Shape: Octogon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 9;
                Vars.FOVShape = 6;
            }
            if (Vars.FOVShape == 6 && GUILayout.Button("FOV Shape: Nonagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 10;
                Vars.FOVShape = 7;
            }
            if (Vars.FOVShape == 7 && GUILayout.Button("FOV Shape: Decagon", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.FOVShape = 8;
            }
            if (Vars.FOVShape == 8 && GUILayout.Button("FOV Shape: Circle", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.SideCount = 3;
                Vars.FOVShape = 0;
            }
            //curBone
            if (Vars.AimBoneInt == 0 && GUILayout.Button("Aimbot Bone: Head", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimBoneInt = 1;
            }
            if (Vars.AimBoneInt == 1 && GUILayout.Button("Aimbot Bone: Jaw", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimBoneInt = 2;
            }
            if (Vars.AimBoneInt == 2 && GUILayout.Button("Aimbot Bone: Neck", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimBoneInt = 3;
            }
            if (Vars.AimBoneInt == 3 && GUILayout.Button("Aimbot Bone: Upper Chest", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimBoneInt = 4;
            }
            if (Vars.AimBoneInt == 4 && GUILayout.Button("Aimbot Bone: Chest", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimBoneInt = 5;
            }
            if (Vars.AimBoneInt == 5 && GUILayout.Button("Aimbot Bone: Hips", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimBoneInt = 0;
            }
            if (Vars.AimbotSort == 0 && GUILayout.Button("Aimbot Sort By: Crosshair Distance", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimbotSort = 1;
            }
            if (Vars.AimbotSort == 1 && GUILayout.Button("Aimbot Sort By: Player Distance", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimbotSort = 2;
            }
            if (Vars.AimbotSort == 2 && GUILayout.Button("Aimbot Sort By: Health", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.AimbotSort = 0;
            }
            if (!Vars.RapidFire && GUILayout.Button("RapidFire: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.RapidFire = true;
            }
            if (Vars.RapidFire && GUILayout.Button("RapidFire: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.RapidFire = false;
            }
            if (!Vars.InfiniteAmmo && GUILayout.Button("InfiniteAmmo: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.InfiniteAmmo = true;
            }
            if (Vars.InfiniteAmmo && GUILayout.Button("InfiniteAmmo: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.InfiniteAmmo = false;
            }
            if (!Vars.NoRecoil && GUILayout.Button("NoRecoil: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.NoRecoil = true;
            }
            if (Vars.NoRecoil && GUILayout.Button("NoRecoil: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.NoRecoil = false;
            }

            if (!Vars.MagicBullet && GUILayout.Button("MagicBullet: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.MagicBullet = true;
            }
            if (Vars.MagicBullet && GUILayout.Button("MagicBullet: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.MagicBullet = false;
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
            sty.normal.textColor = Utils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            GUILayout.Label("Skin ID:");
            ids = GUILayout.TextField(ids, sty, new GUILayoutOption[0]);
            if (GUILayout.Button("Change Skin", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                //MiscCheats.ChangeSkin(ids);
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Give Scifi Hammer", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                //MiscCheats.ChangePickaxe();
            }
            if (GUILayout.Button("Unlock All Emotes", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                //MiscCheats.UnlockAll();
            }
            GUILayout.EndHorizontal();
            if (!PlayerPrefsX.GetBool("AddAllOnStart", false) && GUILayout.Button("Unlock All On Start: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                PlayerPrefsX.SetBool("AddAllOnStart", true);
            }
            if (PlayerPrefsX.GetBool("AddAllOnStart", false) && GUILayout.Button("Unlock All On Start: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {

                PlayerPrefsX.SetBool("AddAllOnStart", false);
            }
            GUILayout.BeginHorizontal();
            if (!Vars.SpeedHack && GUILayout.Button("Speed Hack: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.SpeedHack = true;
            }
            if (Vars.SpeedHack && GUILayout.Button("Speed Hack: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.SpeedHack = false;
            }
            if (!Vars.Flight && GUILayout.Button("Flight: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.Flight = true;
            }
            if (Vars.Flight && GUILayout.Button("Flight: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.Flight = false;
            }
            GUILayout.EndHorizontal();
            if (Vars.SpeedHack)
            {
                GUILayout.Label("Speed Boost: " + Mathf.Round(Vars.SpeedBoost).ToString());
                Vars.SpeedBoost = GUILayout.HorizontalSlider(Vars.SpeedBoost, 1f, 50f, new GUILayoutOption[0]);
            }
            if (Vars.Flight)
            {
                GUILayout.Label("Fly Speed:" + Vars.FlightSpeed.ToString(), new GUILayoutOption[0]);
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                Vars.FlightSpeed = GUILayout.HorizontalSlider(Mathf.Round(Vars.FlightSpeed), 10f, 75, new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) });
                if (GUILayout.Button("Default", new GUILayoutOption[]
                {
                    GUILayout.Width(100f),
                    GUILayout.Height(30f)
                }))
                {
                    Vars.FlightSpeed = 10f;
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
                    if (p != PhotonNetwork.LocalPlayer && p.CustomProperties["ImLiterallyAboutToNutWithRecte"] != "t")
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
            if (!Vars.BuildSpam && GUILayout.Button("Build Spam: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.BuildSpam = true;
            }
            if (Vars.BuildSpam && GUILayout.Button("Build Spam: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.BuildSpam = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.UnlimitedBuilds && GUILayout.Button("Unlimited Builds: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.UnlimitedBuilds = true;
            }
            if (Vars.UnlimitedBuilds && GUILayout.Button("Unlimited Builds: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.UnlimitedBuilds = false;
            }
            if (!Vars.BreakAll && GUILayout.Button("Destroy All Toggle: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.BreakAll = true;
            }
            if (Vars.BreakAll && GUILayout.Button("Destroy All Toggle: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.BreakAll = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.RigSpam && GUILayout.Button("RigSpam: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.RigSpam = true;
            }
            if (Vars.RigSpam && GUILayout.Button("RigSpam: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.RigSpam = false;
            }
            if (!Vars.PlayerScale && GUILayout.Button("Player Scale: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.PlayerScale = true;
            }
            if (Vars.PlayerScale && GUILayout.Button("Player Scale: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                if (PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.transform != null)
                {
                    PlayerController.LFNGIIPNIDN.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                Vars.PlayerScale = false;
            }
            GUILayout.EndHorizontal();
            if (Vars.PlayerScale)
            {
                GUILayout.Label("Scale: " + Vars.LocalScale);
                Vars.LocalScale = GUILayout.HorizontalSlider((float)Math.Round((double)Vars.LocalScale, 2), .1f, 10f);
            }
            GUILayout.BeginHorizontal();
            if (!Vars.GodMode && GUILayout.Button("God Mode: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.GodMode = true;
            }
            if (Vars.GodMode && GUILayout.Button("God Mode: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.GodMode = false;
            }
            if (!Vars.JumpHeight && GUILayout.Button("JumpHeight: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.JumpHeight = true;
            }
            if (Vars.JumpHeight && GUILayout.Button("JumpHeight: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                if (PhotonNetwork.InRoom && PlayerController.LFNGIIPNIDN.KBAOKNMAPBD != null)
                {
                    PlayerController.LFNGIIPNIDN.KBAOKNMAPBD.jumpHeight = 5.5f;
                }
                Vars.JumpHeight = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.SpoofID && GUILayout.Button("SpoofID: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.SpoofID = true;
            }
            if (Vars.SpoofID && GUILayout.Button("SpoofID: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.SpoofID = false;
            }
            if (!Vars.HitboxExpansion && GUILayout.Button("Hitbox Expander: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.HitboxExpansion = true;
            }
            if (Vars.HitboxExpansion && GUILayout.Button("Hitbox Expander: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
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
                Vars.HitboxExpansion = false;
            }
            GUILayout.EndHorizontal();
            if (Vars.HitboxExpansion)
            {
                GUILayout.Label("Hitbox Size: " + Vars.HitboxSize.ToString());
                Vars.HitboxSize = GUILayout.HorizontalSlider((float)Math.Round((double)Vars.HitboxSize, 2), .95f, 25f);
            }
            if (Vars.JumpHeight)
            {
                GUILayout.Label("Jump Height: " + Vars.JH.ToString());
                Vars.JH = GUILayout.HorizontalSlider((float)Math.Round((double)Vars.JH, 2), 5.5f, 50f);
            }

            GUILayout.BeginHorizontal();
            if (!Vars.Bhop && GUILayout.Button("BHop: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.Bhop = true;
            }
            if (Vars.Bhop && GUILayout.Button("BHop: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.Bhop = false;
            }
            if (!Vars.OneHitBuilds && GUILayout.Button("1 Tap Builds: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.OneHitBuilds = true;
            }
            if (Vars.OneHitBuilds && GUILayout.Button("1 Tap Builds: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                if (PhotonNetwork.InRoom && PlayerBuildingManager.Instance != null)
                {
                    PlayerBuildingManager.IsOneHitBuildings = false;
                }
                Vars.OneHitBuilds = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (!Vars.WeaponTrail && GUILayout.Button("Loot Trail: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.WeaponTrail = true;
            }
            if (Vars.WeaponTrail && GUILayout.Button("Loot Trail: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f), GUILayout.Width(curWidth) }))
            {
                Vars.WeaponTrail = false;
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
        }

        public void PlayerList()
        {
            GUILayout.Label("PlayerName: " + PhotonNetwork.NickName);
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = Utils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            PhotonNetwork.NickName = GUILayout.TextField(PhotonNetwork.NickName, sty, new GUILayoutOption[0]);
            FirebaseManager.Instance.PGBJEGGCGKK.Nickname = PhotonNetwork.NickName;
            GUILayout.BeginHorizontal();
            if (!Vars.RandomNames && GUILayout.Button("Random Usernames: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[0]))
            {
                Vars.RandomNames = true;
            }
            if (Vars.RandomNames && GUILayout.Button("Random Usernames: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[0]))
            {
                Vars.RandomNames = false;

            }
            if (!Vars.AntiDiscord && GUILayout.Button("Anti-Discord: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[0]))
            {
                Vars.AntiDiscord = true;
            }
            if (Vars.AntiDiscord && GUILayout.Button("Anti-Discord: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[0]))
            {
                Vars.AntiDiscord = false;

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
                    }), new GUILayoutOption[] { GUILayout.Height(50f) });
                }

            }
        }

        public void ClientMenu()
        {
            if (Vars.MenuStyle == 0 && GUILayout.Button("Window Style: Vertical", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.MenuStyle = 1;
            }
            if (Vars.MenuStyle == 1 && GUILayout.Button("Window Style: CS:GO", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.MenuStyle = 0;
            }
            if (!Vars.Watermark && GUILayout.Button("Watermark: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.Watermark = true;
            }
            if (Vars.Watermark && GUILayout.Button("Watermark: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.Watermark = false;
            }
            if (Vars.Watermark)
            {
                if (Vars.WatermarkType == 0 && GUILayout.Button("Watermark: Gamesense.vip", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 1;
                }
                if (Vars.WatermarkType == 1 && GUILayout.Button("Watermark: Neverlose.cc", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 2;
                }
                if (Vars.WatermarkType == 2 && GUILayout.Button("Watermark: AIMWARE.net", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 3;
                }
                if (Vars.WatermarkType == 3 && GUILayout.Button("Watermark: Prodigyhook.xyz", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 4;
                }
                if (Vars.WatermarkType == 4 && GUILayout.Button("Watermark: Exibition", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 5;
                }
                if (Vars.WatermarkType == 5 && GUILayout.Button("Watermark: Dortware", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 6;
                }
                if (Vars.WatermarkType == 6 && GUILayout.Button("Watermark: Adapt", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 0;
                }

                if (Vars.WatermarkType == 7 && GUILayout.Button("Watermark: Monoware", new GUILayoutOption[] { GUILayout.Height(30f) }))
                {
                    Vars.WatermarkType = 0;
                }
            }
            if (GUILayout.Button("Unload", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Loader.Init();
            }
        }
        private void KeyCodes()
        {
            if (GUILayout.Button((!Keybinds.listenForKey0) ? ("Menu Toggle Keybind: " + PlayerPrefs.GetString("GUIToggleKeyCode", "RightShift")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey0 = true;
                Keybinds.KeyToList = "GUIToggleKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey) ? ("Kill All Keybind: " + PlayerPrefs.GetString("KllAllKeyCode", "K")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey = true;
                Keybinds.KeyToList = "KllAllKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey1) ? ("Destroy All Builds Keybind: " + PlayerPrefs.GetString("DestroyAllKeyCode", "I")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey1 = true;
                Keybinds.KeyToList = "DestroyAllKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey2) ? ("Speed Hack Keybind " + PlayerPrefs.GetString("SpeedKeyCode", "B")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey2 = true;
                Keybinds.KeyToList = "SpeedKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey3) ? ("Flight Keybind: " + PlayerPrefs.GetString("FlyKeyCode", "F")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey3 = true;
                Keybinds.KeyToList = "FlyKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey4) ? ("God Mode Keybind: " + PlayerPrefs.GetString("ImmunityKeyCode", "H")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey4 = true;
                Keybinds.KeyToList = "ImmunityKeyCode";
            }
            if (GUILayout.Button((!Keybinds.listenForKey5) ? ("Break All Toggle Keybind: " + PlayerPrefs.GetString("BreakAllTogKeyCode", "L")) : "Listening...", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Keybinds.listenForKey5 = true;
                Keybinds.KeyToList = "BreakAllTogKeyCode";
            }
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
                if (files[i].Remove(0, num) == Vars.loadedFileName)
                {
                    GUILayout.Label("<b><color=#ed8796>" + files[i].Remove(0, num).Replace(".Recte", "") + "</color></b>");
                }
                if (files[i].Remove(0, num) != Vars.loadedFileName)
                {
                    GUILayout.Label("<color=#cad3f5>" + files[i].Remove(0, num).Replace(".Recte", "") + "</color>");
                }
                if (GUILayout.Button("Set As Config", new GUILayoutOption[] { GUILayout.Width(scrollWidth - (float)files[i].Length) }))
                {
                    Vars.loadedFileName = files[i].Remove(0, num);
                    Vars.newFileNameInfo = files[i];
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
            if (Vars.FileTooLong == 1)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label("<size=30><color=#ed8796>Name Length Too Long!</color></size>", new GUILayoutOption[0]);
                LoadError++;
                if (LoadError >= 750)
                {
                    Vars.FileTooLong = 0;
                    LoadError = 0;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Label("<size=20><b><color=#f5a97f>Rename</color></b></size>", new GUILayoutOption[0]);
            GUIStyle sty = new GUIStyle();
            sty.alignment = TextAnchor.MiddleCenter;
            sty.fontStyle = FontStyle.Bold;
            sty.normal.textColor = Utils.GetColorFromString("cad3f5");

            Texture2D txt = new Texture2D(1, 1);
            sty.normal.background = txt;
            renameName = GUILayout.TextField(renameName, sty, new GUILayoutOption[0]);
            if (GUILayout.Button("Rename File", new GUILayoutOption[0]))
            {
                if (renameName.Length >= 63)
                {
                    Vars.FileTooLong = 1;
                }
                else
                {
                    string text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/" + renameName + ".Recte";
                    Vars.loadedFileName = renameName + ".Recte";
                    File.Move(Vars.newFileNameInfo, text);
                }
            }
            if (Vars.WrongFile == 1)
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.Label("<size=30><color=#ed8796>Invalid File Type!</color></size>", new GUILayoutOption[0]);
                LoadError++;
                if (LoadError >= 750)
                {
                    Vars.WrongFile = 0;
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
                    Vars.RGBOverlay.ToString(),
                    Vars.RGBOverlaySaturation.ToString(),
                    Vars.showSelf.ToString(),
                    Vars.MaxESPDist.ToString(),
                    Vars.MaxESPDistance.ToString(),
                    Vars.Nametags.ToString(),
                    Vars.CornerBox.ToString(),
                    Vars.FilledBox.ToString(),
                    Vars.OutlineBox.ToString(),
                    Vars.Tracers.ToString(),
                    Vars.HealthBar.ToString(),
                    Vars.HPBoxLocation.ToString(),
                    Vars.BoxOffset.ToString(),
                    Vars.ShieldBoxCheck.ToString(),
                    Vars.Skeleton.ToString(),
                    Vars.Chams.ToString(),
                    Vars.RecteESP.ToString(),
                    Vars.TargetLine.ToString(),
                    Vars.HeadESP.ToString(),
                    Vars.HeadESPShape.ToString(),
                    Vars.ChestChams.ToString(),
                    Vars.WeaponChams.ToString(),
                    Vars.AmmoChams.ToString(),
                    Vars.MatsChams.ToString(), //23

                    //Render Colors
                    Vars.ChamsColor,
                    Vars.TracerColor,
                    Vars.CornerBoxColor,
                    Vars.BoxOutlineESPColor,
                    Vars.BoxFilledESPColor,
                    Vars.SkeletonColor,
                    Vars.HeadESPColor,
                    Vars.ChestChamsColor,
                    Vars.ItemChamsWeaponColor,
                    Vars.ItemChamsAmmoColor,
                    Vars.ItemChamsBuildsColor, // 34

                    //Weapon
                    Vars.SilentAim.ToString(),
                    Vars.DrawFOV.ToString(),
                    Vars.VisibilityCheck.ToString(),
                    Vars.UseAimbotKey.ToString(),
                    Vars.AimbotKey.ToString(),
                    Vars.FOVSize.ToString(), // 40
                    Vars.FOVCircleColor,
                    Vars.SideCount.ToString(),
                    Vars.FOVShape.ToString(),
                    Vars.AimBoneInt.ToString(),
                    Vars.AimBone[Vars.AimBoneInt].ToString(), // 45
                    Vars.AimbotSort.ToString(),
                    Vars.RapidFire.ToString(),
                    Vars.InfiniteAmmo.ToString(),
                    Vars.NoRecoil.ToString(),
                    Vars.MagicBullet.ToString(), // 50

                    //Misc
                    PlayerPrefsX.GetBool("AddAllOnStart", false).ToString(),
                    Vars.SpeedHack.ToString(),
                    Vars.Flight.ToString(),
                    Vars.SpeedBoost.ToString(),
                    Vars.FlightSpeed.ToString(),
                    Vars.BuildSpam.ToString(),
                    Vars.UnlimitedBuilds.ToString(),
                    Vars.BreakAll.ToString(),
                    Vars.PlayerScale.ToString(),
                    Vars.LocalScale.ToString(),
                    Vars.GodMode.ToString(),
                    Vars.JumpHeight.ToString(),
                    Vars.SpoofID.ToString(),
                    Vars.HitboxExpansion.ToString(),
                    Vars.HitboxSize.ToString(),
                    Vars.JH.ToString(),
                    Vars.Bhop.ToString(),
                    Vars.OneHitBuilds.ToString(),
                    Vars.WeaponTrail.ToString(),
                    Vars.RigSpam.ToString(),

                    //PlayerList
                    Vars.RandomNames.ToString(),
                    Vars.AntiDiscord.ToString(),

                    //Client
                    Vars.MenuStyle.ToString(),
                    Vars.Watermark.ToString(),
                    Vars.WatermarkType.ToString(),

                    //KeyBinds
                    PlayerPrefs.GetString("GUIToggleKeyCode", "RightShift"),
                    PlayerPrefs.GetString("KllAllKeyCode", "K"),
                    PlayerPrefs.GetString("DestroyAllKeyCode", "I"),
                    PlayerPrefs.GetString("SpeedKeyCode", "B"),
                    PlayerPrefs.GetString("FlyKeyCode", "F"),
                    PlayerPrefs.GetString("ImmunityKeyCode", "H"),
                    PlayerPrefs.GetString("BreakAllTogKeyCode", "L")
                };
                Utils.Save(array, "/Config/", "Config - " + Utils.CreateRandomStringForDir(6) + ".Recte");
            }
            if (GUILayout.Button("Load Config", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                if (!Vars.loadedFileName.EndsWith(".Recte"))
                {
                    Vars.WrongFile = 1;
                }
                else
                {
                    string[] array = Utils.Load(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/Config/" + Vars.loadedFileName);

                    //Rendering
                    if (Vars.LoadRenders)
                    {

                        Vars.RGBOverlay = bool.Parse(array[0]);
                        Vars.RGBOverlaySaturation = float.Parse(array[1]);
                        Vars.showSelf = bool.Parse(array[2]);
                        Vars.MaxESPDist = bool.Parse(array[3]);
                        Vars.MaxESPDistance = float.Parse(array[4]);
                        Vars.Nametags = bool.Parse(array[5]);
                        Vars.CornerBox = bool.Parse(array[6]);
                        Vars.FilledBox = bool.Parse(array[7]);
                        Vars.OutlineBox = bool.Parse(array[8]);
                        Vars.Tracers = bool.Parse(array[9]);
                        Vars.HealthBar = bool.Parse(array[10]);
                        Vars.HPBoxLocation = int.Parse(array[11]);
                        Vars.BoxOffset = int.Parse(array[12]);
                        Vars.ShieldBoxCheck = bool.Parse(array[13]);
                        Vars.Skeleton = bool.Parse(array[14]);
                        Vars.Chams = bool.Parse(array[15]);
                        Vars.RecteESP = bool.Parse(array[16]);
                        Vars.TargetLine = bool.Parse(array[17]);
                        Vars.HeadESP = bool.Parse(array[18]);
                        Vars.HeadESPShape = int.Parse(array[19]);
                        Vars.ChestChams = bool.Parse(array[20]);
                        Vars.WeaponChams = bool.Parse(array[21]);
                        Vars.AmmoChams = bool.Parse(array[22]);
                        Vars.MatsChams = bool.Parse(array[23]);


                        Vars.ChamsColor = array[24];
                        Vars.TracerColor = array[25];
                        Vars.CornerBoxColor = array[26];
                        Vars.BoxOutlineESPColor = array[27];
                        Vars.BoxFilledESPColor = array[28];
                        Vars.SkeletonColor = array[29];
                        Vars.HeadESPColor = array[30];
                        Vars.ChestChamsColor = array[31];
                        Vars.ItemChamsWeaponColor = array[32];
                        Vars.ItemChamsAmmoColor = array[33];
                        Vars.ItemChamsBuildsColor = array[34];
                    }
                    //Weapon
                    if (Vars.LoadWeapons)
                    {
                        Vars.SilentAim = bool.Parse(array[35]);
                        Vars.DrawFOV = bool.Parse(array[36]);
                        Vars.VisibilityCheck = bool.Parse(array[37]);
                        Vars.UseAimbotKey = bool.Parse(array[38]);
                        Vars.AimbotKey = int.Parse(array[39]);
                        Vars.FOVSize = float.Parse(array[40]);
                        Vars.FOVCircleColor = array[41];
                        Vars.SideCount = int.Parse(array[42]);
                        Vars.FOVShape = int.Parse(array[43]);
                        Vars.AimBoneInt = int.Parse(array[44]);
                        Vars.AimBone[Vars.AimBoneInt] = (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), array[45]);
                        Vars.AimbotSort = int.Parse(array[46]);
                        Vars.RapidFire = bool.Parse(array[47]);
                        Vars.InfiniteAmmo = bool.Parse(array[48]);
                        Vars.NoRecoil = bool.Parse(array[49]);
                        Vars.MagicBullet = bool.Parse(array[50]); // 50
                    }


                    //Misc
                    if (Vars.LoadMisc)
                    {
                        PlayerPrefsX.SetBool("AddAllOnStart", bool.Parse(array[51]));
                        Vars.SpeedHack = bool.Parse(array[52]);
                        Vars.Flight = bool.Parse(array[53]);
                        Vars.SpeedBoost = float.Parse(array[54]);
                        Vars.FlightSpeed = float.Parse(array[55]);
                        Vars.BuildSpam = bool.Parse(array[56]);
                        Vars.UnlimitedBuilds = bool.Parse(array[57]);
                        Vars.BreakAll = bool.Parse(array[58]);
                        Vars.PlayerScale = bool.Parse(array[59]);
                        Vars.LocalScale = float.Parse(array[60]);
                        Vars.GodMode = bool.Parse(array[61]);
                        Vars.JumpHeight = bool.Parse(array[62]);
                        Vars.SpoofID = bool.Parse(array[63]);
                        Vars.HitboxExpansion = bool.Parse(array[64]);
                        Vars.HitboxSize = float.Parse(array[65]);
                        Vars.JH = float.Parse(array[66]);
                        Vars.Bhop = bool.Parse(array[67]);
                        Vars.OneHitBuilds = bool.Parse(array[68]);
                        Vars.WeaponTrail = bool.Parse(array[69]);
                        Vars.RigSpam = bool.Parse(array[70]);
                    }


                    //PlayerList
                    if (Vars.LoadPList)
                    {
                        Vars.RandomNames = bool.Parse(array[71]);
                        Vars.AntiDiscord = bool.Parse(array[72]);
                    }

                    //Client Menu
                    if (Vars.LoadClient)
                    {
                        try
                        {
                            Vars.MenuStyle = int.Parse(array[73]);
                            Vars.Watermark = bool.Parse(array[74]);
                            Vars.WatermarkType = int.Parse(array[75]);
                        }
                        catch (Exception ex)
                        {
                            print(ex);
                        }
                    }
                    if (Vars.LoadKeybinds)
                    {
                        PlayerPrefs.SetString("GUIToggleKeyCode", array[76]);
                        PlayerPrefs.SetString("KllAllKeyCode", array[77]);
                        PlayerPrefs.SetString("DestroyAllKeyCode", array[78]);
                        PlayerPrefs.SetString("SpeedKeyCode", array[79]);
                        PlayerPrefs.SetString("FlyKeyCode", array[80]);
                        PlayerPrefs.SetString("ImmunityKeyCode", array[81]);
                        PlayerPrefs.SetString("BreakAllTogKeyCode", array[82]);
                    }
                }
            }
            GUILayout.EndHorizontal();
            if (!Vars.LoadRenders && GUILayout.Button("Load Render: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadRenders = true;
            }
            if (Vars.LoadRenders && GUILayout.Button("Load Render: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadRenders = false;
            }
            if (!Vars.LoadWeapons && GUILayout.Button("Load Weapon: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadWeapons = true;
            }
            if (Vars.LoadWeapons && GUILayout.Button("Load Weapon: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadWeapons = false;
            }
            if (!Vars.LoadMisc && GUILayout.Button("Load Misc: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadMisc = true;
            }
            if (Vars.LoadMisc && GUILayout.Button("Load Misc: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadMisc = false;
            }
            if (!Vars.LoadPList && GUILayout.Button("Load Player List: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadPList = true;
            }
            if (Vars.LoadPList && GUILayout.Button("Load PlayerList: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadPList = false;
            }
            if (!Vars.LoadClient && GUILayout.Button("Load Client: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadClient = true;
            }
            if (Vars.LoadClient && GUILayout.Button("Load Client: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadClient = false;
            }
            if (!Vars.LoadKeybinds && GUILayout.Button("Load Keybinds: <b><color=#ed8796>Disabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadKeybinds = true;
            }
            if (Vars.LoadKeybinds && GUILayout.Button("Load Keybinds: <b><color=#a6da95>Enabled</color></b>", new GUILayoutOption[] { GUILayout.Height(30f) }))
            {
                Vars.LoadKeybinds = false;
            }
        }

        public static void Gamesense()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(284f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(284f, 28f);

            string watermarkText = $"{GetRainbowWaveText("Recte")}<color=#cad3f5> | {PhotonNetwork.NickName} | {DateTime.Now.Hour}:{DateTime.Now.Minute} | {PhotonNetwork.GetPing()}ms</color>";

            Render.RectFilled(new Vector2(vector.x, vector.y), new Vector2(268f, 22f), Utils.GetColorFromString("373737"), false);
            Render.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 13, FontStyle.Normal, 0);

            sat = .35f;
            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);
            Color lc = Utils.GetColorFromString("232323");
            Render.DrawNonESPLine(new Vector2(vector.x - 2f, vector.y), new Vector2(vector2.x + 2f, vector2.y), color, 2.25f); // Top
            Render.DrawNonESPLine(vector2, vector4, lc, 2.25f); // Right Side
            Render.DrawNonESPLine(vector3, vector, lc, 2.25f); // Left Side
            Render.DrawNonESPLine(new Vector2(vector4.x + 2f, vector4.y), new Vector2(vector3.x - 2f, vector3.y), lc, 2.25f); // Bottom
        }

        public static void Neverlose()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(284f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(284f, 28f);

            string waterMarkText = $"<b>{GetRainbowWaveText("Recte")}<color=#5b6078> | </color></b>{Utils.Version}<b><color=#5b6078> | </color></b>{PhotonNetwork.GetPing()}ms<b><color=#5b6078> | </color></b>{(int)((float)((int)(1f / Time.unscaledDeltaTime)))} fps<b><color=#5b6078> | </color></b>{DateTime.Now.Hour}:{DateTime.Now.Minute}";

            Render.RectFilled(new Vector2(vector.x, vector.y), new Vector2(268f, 22f), Utils.GetColorFromString("24273a"), false);
            Render.DrawText(new Vector2(42f, 12f), waterMarkText, Color.black, false, 13, FontStyle.Normal, 0);

            //Color bg = Color.HSVToRGB(hue, sat, bri);
            Color lc = Utils.GetColorFromString("181926");
            Render.DrawNonESPLine(new Vector2(vector.x - 2f, vector.y), new Vector2(vector2.x + 2f, vector2.y), lc, 3f); // Top
            Render.DrawNonESPLine(vector2, vector4, lc, 3f); // Right Side
            Render.DrawNonESPLine(vector3, vector, lc, 3f); // Left Side
            Render.DrawNonESPLine(new Vector2(vector4.x + 2f, vector4.y), new Vector2(vector3.x - 2f, vector3.y), lc, 3f); // Bottom

        }

        public static void Aimware()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(284f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(284f, 28f);

            string waterMarkText = $"{GetRainbowWaveText("Recte")}<color=#cad3f5> | Delay: {PhotonNetwork.GetPing()}ms | FPS: {(int)((float)((int)(1f / Time.unscaledDeltaTime)))}</color>";

            Render.RectFilled(new Vector2(vector.x, vector.y), new Vector2(268f, 22f), Utils.GetColorFromString("00000060"), false);
            Render.DrawText(new Vector2(42f, 12f), waterMarkText, Color.black, false, 13, FontStyle.Normal, 0);
        }

        public static void ProdHook()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(186f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(186f, 28f);
            //284 - 268
            // 8 Each Side
            string watermarkText = $"<b>{GetRainbowWaveText("Recte")}<color=#cad3f5> | {PhotonNetwork.GetPing()}ms | fps: {(int)((float)((int)(1f / Time.unscaledDeltaTime)))}</color></b>";

            Render.RectFilled(new Vector2(vector.x, vector.y), new Vector2(170, 22f), Utils.GetColorFromString("00000050"), false);
            Render.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 13, FontStyle.Bold, 0);

            sat = .35f;
            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);
            Color lc = Utils.GetColorFromString("232323");
            Render.DrawNonESPLine(new Vector2(vector.x - 2f, vector.y), new Vector2(vector2.x + 2f, vector2.y), color, 2.25f); // Top
            Render.DrawNonESPLine(new Vector2(vector4.x + 2f, vector4.y), new Vector2(vector3.x - 2f, vector3.y), color, 2.25f); // Bottom
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

            string watermarkText = $"{GetRainbowWaveText("Recte")}<color=#cad3f5> | {DateTime.Now.Hour}:{DateTime.Now.Minute} | {(int)((1f / Time.unscaledDeltaTime))} FPS | {PhotonNetwork.GetPing()}ms</color>";

            Render.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 16, FontStyle.Bold, 0);
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
            string watermarkText = $"{GetRainbowWaveText("Recte")} {Utils.Version}";
            Render.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 16, FontStyle.Bold, 0);
        }

        public static void Adapt()
        {
            Vector2 vector = new Vector2(16f, 8f);
            Vector2 vector2 = new Vector2(186f, 8f);
            Vector2 vector3 = new Vector2(16f, 28f);
            Vector2 vector4 = new Vector2(186f, 28f);


            bri = 1f;
            Color color = Color.HSVToRGB(hue, sat, bri);

            string watermarkText = $"{GetRainbowWaveText("Recte")}<color=#cad3f5> | {Utils.Version} | {PhotonNetwork.NickName}</color>";

            Render.RectFilled(new Vector2(vector.x, vector.y), new Vector2(170 + (1.5f * watermarkText.Length) + 10f, 22f), Utils.GetColorFromString("00000050"), false);
            Render.DrawText(new Vector2(28f, 12f), watermarkText, Color.black, false, 16, FontStyle.Bold, 0);
        }
    }
}
