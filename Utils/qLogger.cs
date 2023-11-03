using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Recte
{
    public class qLogger : MonoBehaviour
    {
        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            this.isShow = this.openAwake;
            qLogger.instance = this;
        }

        public static void CreateGO()
        {
            qLogger.CreateGO(true);
        }

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

        public static void DestroyGO()
        {
            if (qLogger.instance != null)
            {
                UnityEngine.Object.Destroy(qLogger.instance.gameObject);
            }
        }

        public static void OnOpenLogger()
        {
            if (qLogger.instance != null)
            {
                qLogger.instance.isShow = true;
            }
        }

        public static void OnCloseLogger()
        {
            if (qLogger.instance != null)
            {
                qLogger.instance.isShow = false;
            }
        }

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

        private void OnGUI()
        {
            if (!this.isShow)
            {
                return;
            }
            this.rect = GUILayout.Window(152, this.rect, new GUI.WindowFunction(this.ConsoleWindow), "<b>Console</b>", new GUILayoutOption[0]);
        }

        private void ConsoleWindow(int windowID)
        {
            this.LoggingPart();
        }

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

        private void Start()
        {
            this.rect = new Rect(20f, 20f, (float)(Screen.width - 40), (float)(Screen.height - 40));
        }

        public KeyCode toogleKey = KeyCode.Delete;

        public bool openAwake;

        public bool showStackTrace = true;

        private static List<qLogger.LoggerClass> logList = new List<qLogger.LoggerClass>();

        private bool isInfo = true;

        private bool isWarring = true;

        private bool isError = true;

        private bool isStackTrace;

        private bool isTime;

        private bool isShow;

        private Vector2 scrollPos;

        public static qLogger instance;

        public bool isCollapsed;

        private Rect rect;

        private bool isAssert;

        private bool isException;

        public class LoggerClass
        {
            public string time;

            public string message;

            public string stackTrace;

            public LogType logType;
        }
    }
}