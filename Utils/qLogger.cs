using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Recte_1v1lol
{
    // Token: 0x0200045E RID: 1118
    public class qLogger : MonoBehaviour
    {
        // Token: 0x06001EF8 RID: 7928 RVA: 0x0002F2AB File Offset: 0x0002D4AB
        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            this.isShow = this.openAwake;
            qLogger.instance = this;
        }

        // Token: 0x06001EF9 RID: 7929 RVA: 0x0002F2CA File Offset: 0x0002D4CA
        public static void CreateGO()
        {
            qLogger.CreateGO(true);
        }

        // Token: 0x06001EFA RID: 7930 RVA: 0x0002F2D3 File Offset: 0x0002D4D3
        public static void CreateGO(bool isStackTrace)
        {
            if (qLogger.instance == null)
            {
                qLogger component = new GameObject("qLogger").GetComponent<qLogger>();
                component.showStackTrace = isStackTrace;
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    qLogger.instance.isShow = true;
                }
            }
        }

        // Token: 0x06001EFB RID: 7931 RVA: 0x0002F311 File Offset: 0x0002D511
        public static void DestroyGO()
        {
            if (qLogger.instance != null)
            {
                UnityEngine.Object.Destroy(qLogger.instance.gameObject);
            }
        }

        // Token: 0x06001EFC RID: 7932 RVA: 0x0002F32F File Offset: 0x0002D52F
        public static void OnOpenLogger()
        {
            if (qLogger.instance != null)
            {
                qLogger.instance.isShow = true;
            }
        }

        // Token: 0x06001EFD RID: 7933 RVA: 0x0002F349 File Offset: 0x0002D549
        public static void OnCloseLogger()
        {
            if (qLogger.instance != null)
            {
                qLogger.instance.isShow = false;
            }
        }

        

        // Token: 0x06001F00 RID: 7936 RVA: 0x0002F37E File Offset: 0x0002D57E
        private void Update()
        {
            if (Input.GetKeyDown(this.toogleKey))
            {
                this.isShow = !this.isShow;
            }
            this.UpdateScroll();
            if (logList.Count >= 100)
            {
                logList.Clear();
            }
        }

        // Token: 0x06001F01 RID: 7937 RVA: 0x00121F94 File Offset: 0x00120194
        private void UpdateScroll()
        {
            if (Input.touchCount != 1)
            {
                return;
            }
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                this.scrollPos += touch.deltaPosition;
            }
        }

        // Token: 0x06001F02 RID: 7938 RVA: 0x0002F3A2 File Offset: 0x0002D5A2
        private void OnGUI()
        {
            if (!this.isShow)
            {
                return;
            }
            this.rect = GUILayout.Window(152, this.rect, new GUI.WindowFunction(this.ConsoleWindow), "<b>Console</b>", new GUILayoutOption[0]);
        }

        // Token: 0x06001F03 RID: 7939 RVA: 0x0002F3DA File Offset: 0x0002D5DA
        private void ConsoleWindow(int windowID)
        {
            this.LoggingPart();
        }

        // Token: 0x06001F04 RID: 7940 RVA: 0x00121FD8 File Offset: 0x001201D8
        public static void HandleLog(string message, string stackTrace, LogType type)
        {
            qLogger.LoggerClass loggerClass = new qLogger.LoggerClass();
            switch (type)
            {
                case LogType.Error:
                    loggerClass.message = "<color=orange>" + message + "</color>";
                    loggerClass.stackTrace = "<color=orange>" + stackTrace + "</color>";
                    break;
                case LogType.Assert:
                    loggerClass.message = "<color=red>" + message + "</color>";
                    loggerClass.stackTrace = "<color=red>" + stackTrace + "</color>";
                    break;
                case LogType.Warning:
                    loggerClass.message = "<color=yellow>" + message + "</color>";
                    loggerClass.stackTrace = "<color=yellow>" + stackTrace + "</color>";
                    break;
                case LogType.Log:
                    loggerClass.message = "<color=white>" + message + "</color>";
                    loggerClass.stackTrace = "<color=white>" + stackTrace + "</color>";
                    break;
                case LogType.Exception:
                    loggerClass.message = "<color=yellow>" + message + "</color>";
                    loggerClass.stackTrace = "<color=yellow>" + stackTrace + "</color>";
                    break;
            }
            loggerClass.time = string.Concat(new string[]
            {
            DateTime.Now.Hour.ToString(),
            ":",
            DateTime.Now.Minute.ToString(),
            ":",
            DateTime.Now.Second.ToString(),
            ".",
            DateTime.Now.Millisecond.ToString()
            });
            loggerClass.logType = type;
            logList.Add(loggerClass);
        }

        // Token: 0x06001F06 RID: 7942 RVA: 0x00122190 File Offset: 0x00120390
        public void Save()
        {
            if (!Directory.Exists(Application.dataPath + "/Debug"))
            {
                Directory.CreateDirectory(Application.dataPath + "/Debug");
            }
            foreach (char c in logList.ToString())
            {
                File.WriteAllText(Application.dataPath + "/Debug/Debug.Recte", c.ToString());
            }
        }

        // Token: 0x06001F07 RID: 7943 RVA: 0x00122208 File Offset: 0x00120408
        public void LoggingPart()
        {
            GUI.contentColor = Color.white;
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUILayoutOption[0]);
            for (int i = 0; i < logList.Count; i++)
            {
                if ((isInfo || logList[i].logType != LogType.Log) && (isWarring || logList[i].logType != LogType.Warning) && (isError || logList[i].logType != LogType.Error) && (isAssert || logList[i].logType != LogType.Assert) && (isException || logList[i].logType != LogType.Exception))
                {
                    GUILayout.Label(((!isTime) ? " " : (logList[i].time + " ")) + logList[i].message + ((!isStackTrace) ? string.Empty : ("\n" + logList[i].stackTrace)), new GUILayoutOption[0]);
                }
            }
            GUILayout.EndScrollView();
            Toolbar();
        }

        // Token: 0x06001F08 RID: 7944 RVA: 0x0012236C File Offset: 0x0012056C
        private void Toolbar()
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            if (GUILayout.Button("Clear", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                logList.Clear();
            }
            if (GUILayout.Button((!this.isInfo) ? "<color=red>Info</color>" : "Info", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                this.isInfo = !this.isInfo;
            }
            if (GUILayout.Button((!this.isWarring) ? "<color=red>Warning</color>" : "Warning", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                this.isWarring = !this.isWarring;
            }
            if (GUILayout.Button((!this.isError) ? "<color=red>Error</color>" : "Error", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                this.isError = !this.isError;
            }
            if (GUILayout.Button((!this.isException) ? "<color=red>Exception</color>" : "Exception", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                this.isException = !this.isException;
            }
            if (GUILayout.Button((!this.isAssert) ? "<color=red>Assert</color>" : "Assert", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                this.isAssert = !this.isAssert;
            }
            if (GUILayout.Button((!this.isStackTrace) ? "<color=red>StackTrace</color>" : "StackTrace", new GUILayoutOption[]
            {

            GUILayout.Height(35f)
            }))
            {
                this.isStackTrace = !this.isStackTrace;
            }
            GUILayout.EndHorizontal();
        }

        // Token: 0x06001F09 RID: 7945 RVA: 0x0002F419 File Offset: 0x0002D619
        private void Start()
        {
            this.rect = new Rect(20f, 20f, (float)(Screen.width - 40), (float)(Screen.height - 40));
        }

        // Token: 0x04001DD6 RID: 7638
        public KeyCode toogleKey = KeyCode.Delete;

        // Token: 0x04001DD7 RID: 7639
        public bool openAwake;

        // Token: 0x04001DD8 RID: 7640
        public bool showStackTrace = true;

        // Token: 0x04001DD9 RID: 7641
        private static List<qLogger.LoggerClass> logList = new List<qLogger.LoggerClass>();

        // Token: 0x04001DDA RID: 7642
        private bool isInfo = true;

        // Token: 0x04001DDB RID: 7643
        private bool isWarring = true;

        // Token: 0x04001DDC RID: 7644
        private bool isError = true;

        // Token: 0x04001DDD RID: 7645
        private bool isStackTrace;

        // Token: 0x04001DDE RID: 7646
        private bool isTime;

        // Token: 0x04001DDF RID: 7647
        private bool isShow;

        // Token: 0x04001DE0 RID: 7648
        private Vector2 scrollPos;

        // Token: 0x04001DE1 RID: 7649
        public static qLogger instance;

        // Token: 0x04001DE2 RID: 7650
        public bool isCollapsed;

        // Token: 0x04001DE3 RID: 7651
        private Rect rect;

        // Token: 0x04001DE4 RID: 7652
        private bool isAssert;

        // Token: 0x04001DE5 RID: 7653
        private bool isException;

        // Token: 0x0200045F RID: 1119
        public class LoggerClass
        {
            // Token: 0x04001DE6 RID: 7654
            public string time;

            // Token: 0x04001DE7 RID: 7655
            public string message;

            // Token: 0x04001DE8 RID: 7656
            public string stackTrace;

            // Token: 0x04001DE9 RID: 7657
            public LogType logType;
        }
    }
}