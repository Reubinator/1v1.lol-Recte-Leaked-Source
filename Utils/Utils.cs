using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte
{
    public class Utils
    {
        public static string Version = "v1.7";
        public static bool isOnScreen(Vector3 vector3_0)
        {
            return vector3_0.y > 0.01f && vector3_0.y < (float)Screen.height - 5f && vector3_0.z > 0.01f;
        }
        public static int HexToDec(string Hex)
        {
            return Convert.ToInt32(Hex, 16);
        }

        public static float HexToFloatNormalized(string Hex)
        {
            return (float)HexToDec(Hex) / 255f;
        }
        public static Color GetColorFromString(string HexCode)
        {
            if (HexCode.Contains("#"))
            {
                HexCode.Replace("#", "");
            }
            if (HexCode.Length == 6)
            {
                float num = HexToFloatNormalized(HexCode.Substring(0, 2));
                float num2 = HexToFloatNormalized(HexCode.Substring(2, 2));
                float num3 = HexToFloatNormalized(HexCode.Substring(4, 2));
                return new Color(num, num2, num3);
            }
            if (HexCode.Length == 8)
            {
                float num = HexToFloatNormalized(HexCode.Substring(0, 2));
                float num2 = HexToFloatNormalized(HexCode.Substring(2, 2));
                float num3 = HexToFloatNormalized(HexCode.Substring(4, 2));
                float num4 = HexToFloatNormalized(HexCode.Substring(6, 2));
                return new Color(num, num2, num3, num4);
            }
            return new Color();
        }

        public static Vector2 CenterOfScreen()
        {
            return new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
        }
        public static string[] Load(string filePath)
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
        public static void Save(string[] value, string path, string fileName)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/" + path))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte/" + path);
            }
            string text = string.Join("\n", value);
            File.WriteAllText(string.Concat(new string[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Recte",
                "/",
                path,
                "/",
                fileName
            }), text);
        }
        public static string CreateRandomStringForDir(int _length)
        {
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] array = new char[_length];
            for (int i = 0; i < _length; i++)
            {
                array[i] = text[UnityEngine.Random.Range(0, text.Length)];
            }
            return new string(array);
        }
    }
}
