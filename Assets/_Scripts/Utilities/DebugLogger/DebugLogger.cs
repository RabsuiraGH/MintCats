using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Utility.DebugTool
{
    [Serializable]
    public class DebugLogger
    {
        [field: SerializeField] public bool EnableDebug { get; set; } = true;

        [SerializeField] private int _defaultFontSize = 12;

        public DebugLogger()
        {
            if (DebugLoggerManager.Instance == null)
                DebugLoggerManager.OnInitialized += Registry;
            else
                Registry();
        }

        ~DebugLogger()
        {
            DebugLoggerManager.Instance.UnregistryLogger(this);
        }

        private void Registry()
        {
            DebugLoggerManager.Instance.RegistryLogger(this);
        }


        private void DoLog(System.Action<string, Object> logFunction, string prefix, Object myObj, int size,
                           params object[] message)
        {
#if UNITY_EDITOR
            if (!EnableDebug)
            {
                return;
            }

            string name = (myObj ? myObj.name : "NullObject").Color("lightblue");
            logFunction($"{prefix}[{name}]: {string.Join("; ", message).Size(size)}\n ", myObj);
#endif
        }

#region FULL LOG

        public void Log(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.Log, "", myObj, size, message);
        }

        public void LogError(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.LogError, "<!>".Color("red"), myObj, size, message);
        }

        public void LogWarning(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.LogWarning, "⚠️".Color("yellow"), myObj, size, message);
        }

        public void LogSuccess(Object myObj, int size, params object[] message)
        {
            DoLog(Debug.Log, "☻".Color("green"), myObj, size, message);
        }

#endregion FULL LOG

#region SHORT LOG

        public void Log(Object myObj, params object[] message)
        {
            DoLog(Debug.Log, "", myObj, _defaultFontSize, message);
        }

        public void LogError(Object myObj, params object[] message)
        {
            DoLog(Debug.LogError, "<!>".Color("red"), myObj, _defaultFontSize, message);
        }

        public void LogWarning(Object myObj, params object[] message)
        {
            DoLog(Debug.LogWarning, "⚠️".Color("yellow"), myObj, _defaultFontSize, message);
        }

        public void LogSuccess(Object myObj, params object[] message)
        {
            DoLog(Debug.Log, "☻".Color("green"), myObj, _defaultFontSize, message);
        }

#endregion SHORT LOG
    }
}