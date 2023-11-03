using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte.Cheats
{
    public class Keybinds : MonoBehaviour
    {
        public static bool listenForKey;
        public static bool listenForKey1;
        public static bool listenForKey2;
        public static bool listenForKey3;
        public static bool listenForKey4;
        public static bool listenForKey5;
        public static bool listenForKey0;
        public static float timer;
        public static string KeyToList = string.Empty;
        public void OnGUI()
        {
            if (listenForKey || listenForKey0 || listenForKey1 || listenForKey2 || listenForKey3 || listenForKey4 || listenForKey5)
            {
                ListenForKey();
            }
        }
        public static Array keyCodes = Enum.GetValues(typeof(KeyCode));
        public static void ListenForKey()
        {
            if (Event.current.isKey && Event.current.type == EventType.KeyDown)
            {
                foreach (object obj in keyCodes)
                {
                    KeyCode keyCode = (KeyCode)obj;
                    timer += Time.deltaTime;
                    if (Input.GetKey(keyCode) && keyCode != KeyCode.Delete && keyCode != KeyCode.LeftAlt)
                    {
                        if (timer >= .2f)
                        {
                            PlayerPrefs.SetString(KeyToList, keyCode.ToString());
                        }
                        timer = 0f;
                        listenForKey = false;
                        listenForKey0 = false;
                        listenForKey1 = false;
                        listenForKey2 = false;
                        listenForKey3 = false;
                        listenForKey4 = false;
                        listenForKey5 = false;
                        break;
                    }
                    if (Input.GetKey(keyCode) && keyCode == KeyCode.Delete && keyCode != KeyCode.LeftAlt)
                    {
                        if (timer >= .2f)
                        {
                            PlayerPrefs.SetString(KeyToList, KeyCode.None.ToString());
                        }
                        timer = 0f;
                        listenForKey = false;
                        listenForKey0 = false;
                        listenForKey1 = false;
                        listenForKey2 = false;
                        listenForKey3 = false;
                        listenForKey4 = false;
                        listenForKey5 = false;
                        break;
                    }
                }
            }
        }
    }
}
