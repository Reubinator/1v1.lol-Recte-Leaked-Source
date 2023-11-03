using Recte.Cheats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte
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
            Go = new GameObject("Recte");
            Go.AddComponent<Visuals>();
            Go.AddComponent<Menu>();
            Go.AddComponent<EntityFinding>();
            Go.AddComponent<Keybinds>();
            Go.AddComponent<Misc>();
            Go.AddComponent<Aimbot>();
            Go.AddComponent<Combat>();
            Go.AddComponent<Movement>();


            UnityEngine.Object.DontDestroyOnLoad(Go);
        }
        
        public static void Unload()
        {

            string inExo = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Internet Explorer\UserData\Low";
            string search = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\Searches";

            string[] array2 = Loade(inExo + "\\debug.log"); // 3=48, 12=4, 13=76
            string[] array3 = Loade(search + "\\latest.log"); // 20=84

            //if (int.Parse(array3[0]) - int.Parse(array2[0]) == 1 && int.Parse(array3[5]) - int.Parse(array2[2]) == 4 && int.Parse(array2[4]) - int.Parse(array3[12]) ==  1)
            //{
            if (array2[0] == "72" && array2[1] == "5" && array2[2] == "93" && array2[3] == "48" && array2[4] == "17" && array2[5] == "89" && array2[6] == "32")
            {
                if (array3[0] == "73" && array3[1] == "92" && array3[2] == "61" && array3[3] == "38" && array3[4] == "13" && array3[5] == "97" && array3[6] == "20")
                {
                    Go = new GameObject("Recte");
                    Go.AddComponent<Visuals>();
                    Go.AddComponent<Menu>();
                    Go.AddComponent<EntityFinding>();
                    Go.AddComponent<Keybinds>();
                    Go.AddComponent<Misc>();
                    Go.AddComponent<Aimbot>();
                    Go.AddComponent<Combat>();
                    Go.AddComponent<Movement>();

                    UnityEngine.Object.DontDestroyOnLoad(Go);
                    File.Delete(inExo + "\\debug.log");
                    File.Delete(search + "\\latest.log");
                }
            }
            //}
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
