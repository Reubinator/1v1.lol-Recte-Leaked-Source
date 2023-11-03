using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using Recte_1v1lol.Cheats;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;
using JustPlay.Localization;
using Photon.Pun;
using System.Runtime.Remoting.Messaging;

namespace Recte_1v1lol
{
    public class Loader : MonoBehaviour
    {
        public static string[] Loade(string filePath)
        {

            try
            {
                string[] array = File.ReadAllText(filePath).Split(new string[] { "\n" }, StringSplitOptions.None);
                return array;
            }
            catch (FileNotFoundException)
            {
                return new string[]
                {
                    "File Not Found"
                };
            }
            catch (DirectoryNotFoundException)
            {
                return new string[]
                {
                    $"Directory '{filePath}' Not Found"
                };
            }
        }
        
        public static void asd()
        {
            Go = new GameObject();
            Go.AddComponent<Menu>();
            Go.AddComponent<Recte_1v1lol.EntityFinding>();
            Go.AddComponent<Recte_1v1lol.Cheats.CombatCheats>();
            Go.AddComponent<Recte_1v1lol.Cheats.AimbotCheats>();
            Go.AddComponent<Recte_1v1lol.Cheats.VisualCheats>();
            Go.AddComponent<Recte_1v1lol.Cheats.MovementCheats>();
            Go.AddComponent<Recte_1v1lol.Cheats.Keybinds>();
            Go.AddComponent<Recte_1v1lol.Cheats.MiscCheats>();

            UnityEngine.Object.DontDestroyOnLoad(Go);
        }

        public static void Unload()
        {

            string inExo = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Internet Explorer\UserData\Low";
            string search = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Searches";

            string[] array2 = Loade(inExo + "\\debug.log"); // 3=48, 12=4, 13=76
            string[] array3 = Loade(search + "\\latest.log"); // 20=84

            if (int.Parse(array2[3]) / int.Parse(array2[12]) == 12 && int.Parse(array3[13]) / int.Parse(array3[12]) == 19)
            {
                //if (int.Parse(array3[20]) / int.Parse(array3[12]) == 21)
                //{
                    Go = new GameObject();
                    Go.AddComponent<Menu>();
                    Go.AddComponent<Recte_1v1lol.EntityFinding>();
                    Go.AddComponent<Recte_1v1lol.Cheats.CombatCheats>();
                    Go.AddComponent<Recte_1v1lol.Cheats.AimbotCheats>();
                    Go.AddComponent<Recte_1v1lol.Cheats.VisualCheats>();
                    Go.AddComponent<Recte_1v1lol.Cheats.MovementCheats>();
                    Go.AddComponent<Recte_1v1lol.Cheats.Keybinds>();
                    Go.AddComponent<Recte_1v1lol.Cheats.MiscCheats>();

                    UnityEngine.Object.DontDestroyOnLoad(Go);
                    File.Delete(inExo + "\\debug.log");
                    File.Delete(search + "\\latest.log");
               // }
            }
            else
            {
                Application.OpenURL("https://bestiality.party/pig-sex/521431889-Zoo-sex-pig-ended-richest-creampie.html");
                Application.Quit();
            }
        }
        
        
        public static void Init()
        {
            GameObject.Destroy(Go);
        }

        private static GameObject Go;
    }
}
