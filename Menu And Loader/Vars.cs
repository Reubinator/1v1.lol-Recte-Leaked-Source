using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte
{
    internal class Vars
    {
        public static bool open = false;
        public static int selectedTab = 0;
        public static int MenuStyle = 0;
        public static bool Watermark = true;
        public static int WatermarkType = 0;

        public static string textColor = "cad3f5";
        public static Color tabSelect = Utils.GetColorFromString("5b6078");
        public static Color tabUnSelect = Utils.GetColorFromString("5b6078");
        public static Color tabHover = Utils.GetColorFromString("5b6078");

        public static Color ModuleSettings = Utils.GetColorFromString("181926");
        public static Color toggleBackground = Utils.GetColorFromString("24273a");
        public static Color toggleOn = Utils.GetColorFromString("a6da95");
        public static Color toggleBackgroundHover = Utils.GetColorFromString("d8ffc4");
        public static Color toggleOnHover = Utils.GetColorFromString("5b6078");

        public static Color SliderBg = Utils.GetColorFromString("363a4f");
        public static Color SliderFg = Utils.GetColorFromString("7dc4e4");
        public static Color SliderFgHover = Utils.GetColorFromString("494d64");


        //Visuals
        public static bool RGBOverlay = false;
        public static float RGBOverlaySaturation = .35f;
        public static bool showSelf = false;
        public static bool showSelfRecteESP = false;
        public static bool showSelfNametags = false;
        public static bool showSelfChams = false;
        public static bool showSelfCorner = false;
        public static bool showSelfOutline = false;
        public static bool showSelfFilled = false;
        public static bool showSelfSkeleton = false;
        public static bool showSelfHealth = false;
        public static bool showSelfTracers = false;
        public static bool showSelfHeadESP = false;
        public static bool MaxESPDist = false;
        public static float MaxESPDistance = 250f;
        public static bool Nametags = false;
        public static bool CornerBox = false;
        public static bool FilledBox = false;
        public static bool OutlineBox = false;
        public static bool Tracers = false;
        public static bool HealthBar = false;
        public static bool ShieldBoxCheck = false;
        public static int HPBoxLocation = 0;
        public static int BoxOffset = 0;
        public static bool Skeleton = false;
        public static bool Chams = false;
        public static bool RecteESP = false;
        public static bool TargetLine = false;
        public static bool HeadESP = false;
        public static int HeadESPShape = 0;
        public static bool ChestChams = false;
        public static bool WeaponChams = false;
        public static bool AmmoChams = false;
        public static bool MatsChams = false;

        //Visual Colors
        public static string ChamsColor = "8a2be280";
        public static string TracerColor = "87cefa";
        public static string CornerBoxColor = "880000";
        public static string BoxOutlineESPColor = "87c4a3";
        public static string BoxFilledESPColor = "30303070";
        public static string SkeletonColor = "ff69b4";
        public static string HeadESPColor = "9fa32e";
        public static string ChestChamsColor = "ffff6b";
        public static string ItemChamsWeaponColor = "87c4a3";
        public static string ItemChamsAmmoColor = "42f578";
        public static string ItemChamsBuildsColor = "4287f5";


        //Combat
        public static bool InfiniteAmmo = false;
        public static bool NoRecoil = true;
        public static bool RapidFire = false;
        public static bool HitboxExpansion = false;
        public static float HitboxSize = 1f;
        public static bool NoSpread = false;

        //Aimbot Settings
        public static int FOVShape = 8;
        public static int SideCount = 3;
        public static string[] fovShapeName = new string[]
        {
            "Triangle",
            "Square",
            "Pentagon",
            "Hexagon",
            "Heptagon",
            "Octogon",
            "Nonagon",
            "Decagon",
            "Circle"
        };

        public static int MouseBind = 1;
        public static string[] MouseBindName = new string[]
        {
            "Left",
            "Right"
        };
        public static float FOVSize = 100f;
        public static HumanBodyBones[] AimBone = new HumanBodyBones[]
        {
            HumanBodyBones.Head,
            HumanBodyBones.Jaw,
            HumanBodyBones.Neck,
            HumanBodyBones.Chest,
            HumanBodyBones.UpperChest,
            HumanBodyBones.Hips, // 5
            (HumanBodyBones)Enum.Parse(typeof(HumanBodyBones), UnityEngine.Random.Range(0, 5).ToString())
        };
        public static string[] AimboneNames = new string[]
        {
            "Head",
            "Jaw",
            "Neck",
            "Chest",
            "Stomach",
            "Pelvis", // 5
            "Random"
        };
        public static int AimBoneInt;
        public static int AimbotSort = 0;
        public static string[] SortMethods = new string[]
        {
            "Crosshair",
            "Distance",
            "Health"
        };
        public static int AimbotKey = 0;
        public static string FOVCircleColor = "880000";

        //Aimbot Bools
        public static bool VisibilityCheck = false;
        public static bool UseAimbotKey = true;
        public static bool DrawFOV = false;
        public static bool FOVLogic = true;

        //Aimbot Types
        public static bool LockOn = false;
        public static bool SilentAim = false;
        public static bool MagicBullet = false;

        //Player List
        public static bool RandomNames = false;
        public static bool AntiDiscord = false;

        //Misc
        public static bool SpeedHack = false;
        public static bool Flight = false;
        public static float SpeedBoost = 1f;
        public static float FlightSpeed = 10f;
        public static bool BuildSpam = false;
        public static bool UnlimitedBuilds = false;
        public static bool BreakAll = false;
        public static bool PlayerScale = false;
        public static float LocalScale = 1f;
        public static bool GodMode = true;
        public static bool JumpHeight = false;
        public static float JH = 1f;
        public static bool SpoofID = false;
        public static bool Bhop = false;
        public static bool OneHitBuilds = true;
        public static bool WeaponTrail = false;
        public static bool RigSpam = false;

        //Config Manager
        public static string loadedFileName = string.Empty;
        public static string newFileNameInfo = string.Empty;
        public static int FileTooLong = 0;
        public static int WrongFile = 0;

        //Config Bools
        public static bool LoadRenders = true;
        public static bool LoadWeapons = true;
        public static bool LoadMisc = true;
        public static bool LoadPList = true;
        public static bool LoadClient = true;
        public static bool LoadKeybinds = true;
    }
}
