using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using UnityEngine.UIElements;
namespace Recte_1v1lol
{
    public class RecteUtils
    {
        private static GUIStyle style = new GUIStyle();
        private static GUIStyle style1 = new GUIStyle();

        public static int tabSystem(int min, int max, int value, int direction)
        {
            if (value < min)
            {
                return max;
            }
            if (value > max)
            {
                return min;
            }

            int newValue = value + direction;

            if (newValue < min)
            {
                return max;
            }
            if (newValue > max)
            {
                return min;
            }
            return newValue;
        }

        public static Transform GetBone(Transform[] bones, string name)
        {
            foreach (Transform transform in bones)
            {
                if (transform.name == name)
                {
                    return transform;
                }
            }
            return null;
        }
        public static float EaseInOutQuart(float start, float end, float value)
        {
            value /= 0.5f;
            end -= start;
            if (value < 1f)
            {
                return end * 0.5f * value * value * value * value + start;
            }
            value -= 2f;
            return -end * 0.5f * (value * value * value * value - 2f) + start;
        }
        public static void DrawRoundedTex(Vector2 pos, Vector2 size, Texture2D tex, int radius)
        {
            GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), tex, ScaleMode.StretchToFill, true, 0f, GUI.color, 0f, radius);
        }
        public static void DrawRoundedTex(Vector2 pos, Vector2 size, Color color, int radius)
        {
            if (!RecteUtils.drawingTex)
            {
                RecteUtils.drawingTex = new Texture2D(1, 1);
            }
            if (color != RecteUtils.lastTexColour)
            {
                RecteUtils.drawingTex.SetPixel(0, 0, color);
                RecteUtils.drawingTex.Apply();
                RecteUtils.lastTexColour = color;
            }
            GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), RecteUtils.drawingTex, ScaleMode.StretchToFill, true, 0f, GUI.color, 0f, radius);
        }
        public static Texture2D RoundedTex(Color color, int radius)
        {
            Texture2D tex = new Texture2D(64, 64);
            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    if (Vector2.Distance(new Vector2(x, y), new Vector2(32, 32)) < radius) tex.SetPixel(x, y, color);
                    else tex.SetPixel(x, y, Color.clear);
                }
            }
            tex.Apply();
            return tex;
        }
        public static Color hpColor(float hp)
        {
            Color c = new Color();
            int N = 4;
            float M = 1.5f;
            float healthCalc = (float)(((float)hp / (float)100)) * 100f;
            float N_root = (float)Mathf.Pow((healthCalc / 100f), (1f / M));
            float N_power = (float)Mathf.Pow((healthCalc / 100f), N);

            if (healthCalc < 50)
            {
                c = Color.Lerp(Color.red, Color.yellow, (float)N_root);
            }
            else if (healthCalc >= 50)
            {
                c = Color.Lerp(Color.yellow, Color.green, (float)N_power);
            }
            return c;
        }

        public static void DrawText(Vector2 ScreenPos, string text, Color outLineColor, bool center = false, int fontSize = 12, FontStyle fontStyle = FontStyle.Bold, int int_1 = 1)
        {
            style.fontSize = fontSize;
            style.richText = true;
            style.fontStyle = fontStyle;
            style1.fontSize = fontSize;
            style1.richText = true;
            style1.normal.textColor = outLineColor;
            style1.fontStyle = fontStyle;
            GUIContent guicontent = new GUIContent(text);
            GUIContent guicontent2 = new GUIContent(text);
            if (center)
            {
                ScreenPos.x -= style.CalcSize(guicontent).x / 2f;
            }
            switch (int_1)
            {
                case 0:
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                case 1:
                    GUI.Label(new Rect(ScreenPos.x + 1f, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                case 2:
                    GUI.Label(new Rect(ScreenPos.x + 1f, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x - 1f, ScreenPos.y - 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                case 3:
                    GUI.Label(new Rect(ScreenPos.x + 1f, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x - 1f, ScreenPos.y - 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y - 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                default:
                    return;
            }
        }
        public static void DrawText(Vector2 ScreenPos, string text, Color textColor, Color outLineColor, bool center = false, int fontSize = 12, FontStyle fontStyle = FontStyle.Bold, int int_1 = 1)
        {
            style.fontSize = fontSize;
            style.richText = true;
            style.fontStyle = fontStyle;
            style.normal.textColor = textColor;
            style1.fontSize = fontSize;
            style1.richText = true;
            style1.normal.textColor = outLineColor;
            style1.fontStyle = fontStyle;
            GUIContent guicontent = new GUIContent(text);
            GUIContent guicontent2 = new GUIContent(text);
            if (center)
            {
                ScreenPos.x -= style.CalcSize(guicontent).x / 2f;
            }
            switch (int_1)
            {
                case 0:
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                case 1:
                    GUI.Label(new Rect(ScreenPos.x + 1f, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                case 2:
                    GUI.Label(new Rect(ScreenPos.x + 1f, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x - 1f, ScreenPos.y - 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                case 3:
                    GUI.Label(new Rect(ScreenPos.x + 1f, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x - 1f, ScreenPos.y - 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y - 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y + 1f, 300f, 25f), guicontent2, style1);
                    GUI.Label(new Rect(ScreenPos.x, ScreenPos.y, 300f, 25f), guicontent, style);
                    return;
                default:
                    return;
            }
        }
        public static void CornerBox(Vector2 Head, float Width, float Height, float thickness, Color color, bool outline)
        {
            int num = (int)(Width / 4f);
            int num2 = num;
            if (outline)
            {
                RecteUtils.RectFilled(Head.x - Width / 2f - 1f, Head.y - 1f, (float)(num + 2), 3f, Color.black);
                RecteUtils.RectFilled(Head.x - Width / 2f - 1f, Head.y - 1f, 3f, (float)(num2 + 2), Color.black);
                RecteUtils.RectFilled(Head.x + Width / 2f - (float)num - 1f, Head.y - 1f, (float)(num + 2), 3f, Color.black);
                RecteUtils.RectFilled(Head.x + Width / 2f - 1f, Head.y - 1f, 3f, (float)(num2 + 2), Color.black);
                RecteUtils.RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 4f, (float)(num + 2), 3f, Color.black);
                RecteUtils.RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - (float)num2 - 4f, 3f, (float)(num2 + 2), Color.black);
                RecteUtils.RectFilled(Head.x + Width / 2f - (float)num - 1f, Head.y + Height - 4f, (float)(num + 2), 3f, Color.black);
                RecteUtils.RectFilled(Head.x + Width / 2f - 1f, Head.y + Height - (float)num2 - 4f, 3f, (float)(num2 + 3), Color.black);
            }
            RecteUtils.RectFilled(Head.x - Width / 2f, Head.y, (float)num, 1f, color);
            RecteUtils.RectFilled(Head.x - Width / 2f, Head.y, 1f, (float)num2, color);
            RecteUtils.RectFilled(Head.x + Width / 2f - (float)num, Head.y, (float)num, 1f, color);
            RecteUtils.RectFilled(Head.x + Width / 2f, Head.y, 1f, (float)num2, color);
            RecteUtils.RectFilled(Head.x - Width / 2f, Head.y + Height - 3f, (float)num, 1f, color);
            RecteUtils.RectFilled(Head.x - Width / 2f, Head.y + Height - (float)num2 - 3f, 1f, (float)num2, color);
            RecteUtils.RectFilled(Head.x + Width / 2f - (float)num, Head.y + Height - 3f, (float)num, 1f, color);
            RecteUtils.RectFilled(Head.x + Width / 2f, Head.y + Height - (float)num2 - 3f, 1f, (float)(num2 + 1), color);
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
        private static Texture2D drawingTex;
        private static Color lastTexColour;
        // Token: 0x06000017 RID: 23 RVA: 0x0000277C File Offset: 0x0000097C
        internal static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (!RecteUtils.drawingTex)
            {
                RecteUtils.drawingTex = new Texture2D(1, 1);
            }
            if (color != RecteUtils.lastTexColour)
            {
                RecteUtils.drawingTex.SetPixel(0, 0, color);
                RecteUtils.drawingTex.Apply();
                RecteUtils.lastTexColour = color;
            }
            GUI.DrawTexture(new Rect(x, y, width, height), RecteUtils.drawingTex);
        }
        public static void RectFilled(Vector2 v2, Vector2 size, Color color, bool center = true)
        {
            if (center)
            {
                v2 = v2 - size / 2f;
            }
            if (!RecteUtils.drawingTex)
            {
                RecteUtils.drawingTex = new Texture2D(1, 1);
            }
            if (color != RecteUtils.lastTexColour)
            {
                RecteUtils.drawingTex.SetPixel(0, 0, color);
                RecteUtils.drawingTex.Apply();
                RecteUtils.lastTexColour = color;
            }
            GUI.DrawTexture(new Rect(v2.x, v2.y, size.x, size.y), RecteUtils.drawingTex);
        }
        public static void DrawBox(Vector2 position, Vector2 size, float thickness, Color color, bool centered = true)
        {
            RecteUtils.Color = color;
            RecteUtils.DrawBox(position, size, thickness, centered);
        }
        public static void DrawBox(Vector2 position, Vector2 size, float thickness, bool centered = true)
        {
            if (centered)
            {
                position = position - size / 2f;
            }
            GUI.DrawTexture(new Rect(position.x, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y, thickness, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x + size.x, position.y, thickness, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y + size.y, size.x + thickness, thickness), Texture2D.whiteTexture);
        }
        public static void DrawCross(Vector2 position, Vector2 size, float thickness)
        {
            GUI.DrawTexture(new Rect(position.x - size.x / 2f, position.y, size.x, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(position.x, position.y - size.y / 2f, thickness, size.y), Texture2D.whiteTexture);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000028AE File Offset: 0x00000AAE
        public static void DrawCross(Vector2 position, Vector2 size, float thickness, Color color)
        {
            RecteUtils.Color = color;
            RecteUtils.DrawCross(position, size, thickness);
        }
        public static Color Color
        {
            get
            {
                return GUI.color;
            }
            set
            {
                GUI.color = value;
            }
        }
        public static int HexToDec(string Hex)
        {
            return Convert.ToInt32(Hex, 16);
        }

        public static float HexToFloatNormalized(string Hex)
        {
            return (float)RecteUtils.HexToDec(Hex) / 255f;
        }
        public static Color GetColorFromString(string HexCode)
        {
            if (HexCode.StartsWith("#"))
            {
                HexCode.Replace("#", "");
            }
            if (HexCode.Length == 6)
            {
                float num = RecteUtils.HexToFloatNormalized(HexCode.Substring(0, 2));
                float num2 = RecteUtils.HexToFloatNormalized(HexCode.Substring(2, 2));
                float num3 = RecteUtils.HexToFloatNormalized(HexCode.Substring(4, 2));
                return new Color(num, num2, num3);
            }
            if (HexCode.Length == 8)
            {
                float num = RecteUtils.HexToFloatNormalized(HexCode.Substring(0, 2));
                float num2 = RecteUtils.HexToFloatNormalized(HexCode.Substring(2, 2));
                float num3 = RecteUtils.HexToFloatNormalized(HexCode.Substring(4, 2));
                float num4 = RecteUtils.HexToFloatNormalized(HexCode.Substring(6, 2));
                return new Color(num, num2, num3, num4);
            }
            return new Color();
        }

        public static Vector2 CenterOfScreen()
        {
            return new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
        }

        
        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);
        public static void DrawString(Vector2 position, string label, bool centered = true)
        {
            var content = new GUIContent(label);
            var size = StringStyle.CalcSize(content);

            var upperLeft = centered ? position - size / 2f : position;
            GUI.Label(new Rect(upperLeft, size), content.ToString());
        }

        public static Texture2D lineTex;
        private static Dictionary<int, RecteUtils.RingArray> ringDict = new Dictionary<int, RecteUtils.RingArray>();

        public static void DrawCircle(Vector2 position, float radius, int numSides, Color color, bool centered = true, float thickness = 1f)
        {
            RecteUtils.RingArray ringArray;
            if (RecteUtils.ringDict.ContainsKey(numSides))
            {
                ringArray = RecteUtils.ringDict[numSides];
            }
            else
            {
                ringArray = (RecteUtils.ringDict[numSides] = new RecteUtils.RingArray(numSides));
            }
            Vector2 vector = (centered ? position : (position + Vector2.one * radius));
            for (int i = 0; i < numSides - 1; i++)
            {
                RecteUtils.DrawNonESPLine(vector + ringArray.Positions[i] * radius, vector + ringArray.Positions[i + 1] * radius, color, thickness);
            }
            RecteUtils.DrawNonESPLine(vector + ringArray.Positions[0] * radius, vector + ringArray.Positions[ringArray.Positions.Length - 1] * radius, color, thickness);
        }

        public static void DrawCircle(Color color_3, Vector2 vector2_0, float float_0)
        {
            GL.PushMatrix();
            material_0.SetPass(0);
            GL.Begin(1);
            GL.Color(color_3);
            GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
            for (float num = 0f; num < 6.2831855f; num += 0.05f)
            {
                GL.Vertex(new Vector3(Mathf.Cos(num) * float_0 + vector2_0.x, Mathf.Sin(num) * float_0 + vector2_0.y));
                GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * float_0 + vector2_0.x, Mathf.Sin(num + 0.05f) * float_0 + vector2_0.y));
            }
            GL.End();
            GL.PopMatrix();
        }
        public static bool isOnScreen(Vector3 vector3_0)
        {
            return vector3_0.y > 0.01f && vector3_0.y < (float)Screen.height - 5f && vector3_0.z > 0.01f;
        }

        public static void DrawLine(Vector2 to, Vector2 from, Color color_3, float width)
        {
            Color color = GUI.color;
            Vector2 vector = from - to;
            float num = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
            {
                num += 180f;
            }
            int num2 = (int)Mathf.Ceil(width / 2f);
            RotateAroundPivot(num, to);
            GUI.color = color_3;
            GUI.DrawTexture(new Rect(to.x, to.y - (float)num2, vector.magnitude, width), Texture2D.whiteTexture, 0);
            RotateAroundPivot(-num, to);
            GUI.color = color;
        }
        public static void RotateAroundPivot(float num, Vector2 v2)
        {
            Matrix4x4 matrix = GUI.matrix;
            GUI.matrix = Matrix4x4.identity;
            GUI.matrix = Matrix4x4.TRS(v2, Quaternion.identity, Vector3.one);
            Vector2 vector = EspMath(v2);
            GUI.matrix = Matrix4x4.TRS(v2, Quaternion.Euler(0f, 0f, num), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one) * matrix;
        }
        public static void DrawNonESPLine(Vector2 to, Vector2 from, Color color_3, float width)
        {
            Color color = GUI.color;
            Vector2 vector = from - to;
            float num = 57.29578f * Mathf.Atan(vector.y / vector.x);
            if (vector.x < 0f)
            {
                num += 180f;
            }
            int num2 = (int)Mathf.Ceil(width / 2f);
            LineMath(num, to);
            GUI.color = color_3;
            GUI.DrawTexture(new Rect(to.x, to.y - (float)num2, vector.magnitude, width), Texture2D.whiteTexture, 0);
            LineMath(-num, to);
            GUI.color = color;
        }
        public static void LineMath(float float_0, Vector2 vector2_0)
        {
            Matrix4x4 matrix = GUI.matrix;
            GUI.matrix = Matrix4x4.identity;
            Vector2 vector = MatrixMath(vector2_0);
            GUI.matrix = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, float_0), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one) * matrix;
        }
        public static Vector2 MatrixMath(Vector2 vector2_0)
        {
            GUI.matrix = Matrix4x4.TRS(vector2_0, Quaternion.identity, Vector3.one);
            return Loc(vector2_0);
        }

        public static Vector2 Loc(Vector2 vector2_0)
        {
            return new Vector2(vector2_0.x, vector2_0.y);
        }

        public static Vector2 EspMath(Vector3 v2)
        {
            return new Vector2(v2.x, (float)Screen.height - v2.y);
        }
        public static Vector2 EspMath(Vector2 v2)
        {
            return new Vector2(v2.x, (float)Screen.height - v2.y);
        }
        private static Material material_0 = new Material(Shader.Find("Hidden/Internal-Colored"))
        {
            hideFlags = HideFlags.HideAndDontSave
        };

        public static string Version = "v1.1";
        private class RingArray
        {
            public Vector2[] Positions { get; private set; }

            public RingArray(int numSegments)
            {
                this.Positions = new Vector2[numSegments];
                float num = 360f / (float)numSegments;
                for (int i = 0; i < numSegments; i++)
                {
                    float num2 = 0.017453292f * num * (float)i;
                    this.Positions[i] = new Vector2(Mathf.Sin(num2), Mathf.Cos(num2));
                }
            }
        }
    }
}
