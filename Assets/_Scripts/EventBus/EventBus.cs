using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utility.DebugTool;
using UnityEngine;
using static Core.Utility.DebugTool.HtmlDebugColor;

namespace Core.GameEventSystem
{
    [Serializable]
    public sealed class EventBus
    {
#if UNITY_DEBUG
        [SerializeField] private DebugLogger _debugger = new();
#endif
        private Dictionary<string, List<CallbackWithPriority>> _signalCallbacks = new();

#if UNITY_DEBUG
        /// <summary>
        /// Print all events data to the console.
        /// </summary>
        [EasyButtons.Button]
        public void GetAllData()
        {
            _debugger.Log(null, "EVENT BUS SIGNALS!!");
            foreach (var pair in _signalCallbacks)
            {
                foreach (CallbackWithPriority callback in _signalCallbacks[pair.Key])
                {
                    if (callback.Callback is Delegate d)
                    {
                        _debugger.Log(
                            null, $" SIGNAL: {pair.Key.Color(Cyan)} -- CALLBACK: {d.Method.Name.Color(Cyan)}");
                    }
                    else
                    {
                        _debugger.Log(
                            null, $" SIGNAL: {pair.Key.Color(Cyan)} -- CALLBACK: {callback.Callback.Color(Cyan)}");
                    }
                }
            }
        }

#endif
        /// <summary>
        /// Subscribe event to signal.
        /// </summary>
        /// <param name="callback">Event that will be called when signal T invoke </param>
        /// <param name="priority">Priority of the callback</param>
        /// <typeparam name="T">Signal type</typeparam>
        public void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(new CallbackWithPriority(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<CallbackWithPriority> { new(priority, callback) });
            }
#if UNITY_DEBUG
            _debugger.Log(null, $"Action {callback.Method.Name.Color(Green)} was subscribed " +
                                $"to signal {typeof(T).Name.Color(Green)}");
#endif
            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
        }

        /// <summary>
        /// Invoke all callbacks belonging to signal T
        /// </summary>
        /// <param name="signal">Signal to pass</param>
        /// <typeparam name="T">Signal type</typeparam>
        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
#if UNITY_DEBUG
                _debugger.Log(null, $"Signal {key.Color(Green)} was Invoked");
#endif
                foreach (CallbackWithPriority obj in _signalCallbacks[key])
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }
            else
            {
                Debug.LogWarning(
                    $"No any listeners to {key.Color(Green)} signal! (possible missing eventBus instance)");
            }
        }

        /// <summary>
        /// Unsubscribe event from signal T
        /// </summary>
        /// <param name="callback">Event to unsubscribe</param>
        /// <typeparam name="T">Signal type</typeparam>
        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                CallbackWithPriority callbackToDelete =
                    _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
#if UNITY_DEBUG
                    _debugger.Log(null, $"Action {callback.Method.Name.Color(Red)} was unsubscribed " +
                                        $"to signal {typeof(T).Name.Color(Red)}");
#endif
                }
            }
            else
            {
                Debug.LogError($"Trying to unsubscribe for not existing key {key.Color(Red)}!");
            }
        }

        /// <summary>
        /// Unsubscribe all events from signal T
        /// </summary>
        /// <typeparam name="T">Signal type</typeparam>
        public void UnsubscribeAll<T>()
        {
            string key = typeof(T).Name;

            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks.Remove(key);
#if UNITY_DEBUG
                _debugger.Log(null, $"Signal {key.Color(Red)} was absolutely unsubscribed!".Color(Red));
#endif
            }
            else
            {
                Debug.LogError($"Trying to unsubscribe for not existing key {key.Color(Red)}!");
            }
        }
    }
}