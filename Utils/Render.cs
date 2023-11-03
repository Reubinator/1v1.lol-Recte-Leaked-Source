using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Recte
{
    public class Render
    {
        private static GUIStyle style = new GUIStyle();
        private static GUIStyle style1 = new GUIStyle();

        private static Texture2D drawingTex;
        private static Color lastTexColour;

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
                Render.RectFilled(Head.x - Width / 2f - 1f, Head.y - 1f, (float)(num + 2), 3f, Color.black);
                Render.RectFilled(Head.x - Width / 2f - 1f, Head.y - 1f, 3f, (float)(num2 + 2), Color.black);
                Render.RectFilled(Head.x + Width / 2f - (float)num - 1f, Head.y - 1f, (float)(num + 2), 3f, Color.black);
                Render.RectFilled(Head.x + Width / 2f - 1f, Head.y - 1f, 3f, (float)(num2 + 2), Color.black);
                Render.RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - 4f, (float)(num + 2), 3f, Color.black);
                Render.RectFilled(Head.x - Width / 2f - 1f, Head.y + Height - (float)num2 - 4f, 3f, (float)(num2 + 2), Color.black);
                Render.RectFilled(Head.x + Width / 2f - (float)num - 1f, Head.y + Height - 4f, (float)(num + 2), 3f, Color.black);
                Render.RectFilled(Head.x + Width / 2f - 1f, Head.y + Height - (float)num2 - 4f, 3f, (float)(num2 + 3), Color.black);
            }
            Render.RectFilled(Head.x - Width / 2f, Head.y, (float)num, 1f, color);
            Render.RectFilled(Head.x - Width / 2f, Head.y, 1f, (float)num2, color);
            Render.RectFilled(Head.x + Width / 2f - (float)num, Head.y, (float)num, 1f, color);
            Render.RectFilled(Head.x + Width / 2f, Head.y, 1f, (float)num2, color);
            Render.RectFilled(Head.x - Width / 2f, Head.y + Height - 3f, (float)num, 1f, color);
            Render.RectFilled(Head.x - Width / 2f, Head.y + Height - (float)num2 - 3f, 1f, (float)num2, color);
            Render.RectFilled(Head.x + Width / 2f - (float)num, Head.y + Height - 3f, (float)num, 1f, color);
            Render.RectFilled(Head.x + Width / 2f, Head.y + Height - (float)num2 - 3f, 1f, (float)(num2 + 1), color);
        }
        internal static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (!drawingTex)
            {
                drawingTex = new Texture2D(1, 1);
            }
            if (color != lastTexColour)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();
                lastTexColour = color;
            }
            GUI.DrawTexture(new Rect(x, y, width, height), drawingTex);
        }
        public static void RectFilled(Vector2 v2, Vector2 size, Color color, bool center = true)
        {
            if (center)
            {
                v2 = v2 - size / 2f;
            }
            if (!drawingTex)
            {
                drawingTex = new Texture2D(1, 1);
            }
            if (color != lastTexColour)
            {
                drawingTex.SetPixel(0, 0, color);
                drawingTex.Apply();
                lastTexColour = color;
            }
            GUI.DrawTexture(new Rect(v2.x, v2.y, size.x, size.y), drawingTex);
        }
        public static void DrawBox(Vector2 position, Vector2 size, float thickness, Color color, bool centered = true)
        {
            Color = color;
            DrawBox(position, size, thickness, centered);
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
        public static Texture2D lineTex;
        private static Dictionary<int, RingArray> ringDict = new Dictionary<int, RingArray>();

        public static void DrawCircle(Vector2 position, float radius, int numSides, Color color, bool centered = true, float thickness = 1f)
        {
            RingArray ringArray;
            if (ringDict.ContainsKey(numSides))
            {
                ringArray = ringDict[numSides];
            }
            else
            {
                ringArray = (ringDict[numSides] = new RingArray(numSides));
            }
            Vector2 vector = (centered ? position : (position + Vector2.one * radius));
            for (int i = 0; i < numSides - 1; i++)
            {
                DrawNonESPLine(vector + ringArray.Positions[i] * radius, vector + ringArray.Positions[i + 1] * radius, color, thickness);
            }
            DrawNonESPLine(vector + ringArray.Positions[0] * radius, vector + ringArray.Positions[ringArray.Positions.Length - 1] * radius, color, thickness);
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
