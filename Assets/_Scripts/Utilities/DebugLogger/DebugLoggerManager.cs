using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utility.DebugTool
{
    internal class DebugLoggerManager : MonoBehaviour
    {
        public static DebugLoggerManager Instance { get; private set; }
        public static event Action OnInitialized;

        [SerializeField] private DebugInitialState _initialState = DebugInitialState.Initial;
        private Dictionary<DebugLogger, bool> _debugLoggersInitialState = new();

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            OnInitialized?.Invoke();
        }

        [EasyButtons.Button]
        private void DisableAllDebuggers()
        {
            foreach (KeyValuePair<DebugLogger, bool> debuggerState in _debugLoggersInitialState)
            {
                debuggerState.Key.EnableDebug = false;
            }
        }

        [EasyButtons.Button]
        private void EnableAllDebuggers()
        {
            foreach (KeyValuePair<DebugLogger, bool> debuggerState in _debugLoggersInitialState)
            {
                debuggerState.Key.EnableDebug = true;
            }
        }

        [EasyButtons.Button]
        private void ResetAllDebuggers()
        {
            foreach (KeyValuePair<DebugLogger, bool> debuggerState in _debugLoggersInitialState)
            {
                debuggerState.Key.EnableDebug = debuggerState.Value;
            }
        }

        public void RegistryLogger(DebugLogger logger)
        {
            _debugLoggersInitialState.Add(logger, logger.EnableDebug);

            if (_initialState is DebugInitialState.AllDisabled)
            {
                logger.EnableDebug = false;
            }
            else if (_initialState is DebugInitialState.AllEnabled)
            {
                logger.EnableDebug = true;
            }
        }

        public void UnregistryLogger(DebugLogger logger)
        {
            _debugLoggersInitialState.Remove(logger);
        }
    }
}