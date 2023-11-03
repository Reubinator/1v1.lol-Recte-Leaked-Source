using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte_1v1lol.Utils
{
    internal class Vars
    {
        //Menu
        public static bool open;
        public static int selectedTab = 1;
        public static int MenuStyle;

        //Visuals
        public static bool RGBOverlay = false;
        public static float RGBOverlaySaturation = .35f;
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
        public static int HPBoxLocation = 1;
        public static int BoxOffset = 1;
        public static bool Skeleton = false;
        public static bool Chams = false;
        public static bool RecteESP = false;
        public static bool TargetLine = false;
        public static bool HeadESP = false;
        public static int HeadESPShape = 1;
        public static bool ChestChams = false;
        public static bool WeaponChams = false;
        public static bool AmmoChams = false;
        public static bool MatsChams = false;

        //Combat
        public static bool Aimbot = false;
        public static bool LockOn = false;
        public static bool SilentAim = false;
        public static bool MagicBullet = false;
        public static bool DrawFOV = false;
        public static bool FOVLogic = false;
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
        public static bool VisibilityCheck = false;
        public static bool UseAimbotKey = false;
        public static int MouseBind = 0;
        public static int FOVShape = 0;
        public static float FOVSize = 75f;
        public static HumanBodyBones AimBone = HumanBodyBones.Head;
        public static int AimbotSort = 0;
        public static bool InfiniteAmmo = false;
        public static bool NoRecoil = false;

        //Player List
        public static bool RandomNames = false;
    }
}
