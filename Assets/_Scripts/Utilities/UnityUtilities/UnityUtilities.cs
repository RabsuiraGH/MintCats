using System;
using UnityEngine;

namespace Core.Utility.UnityUtility
{
    public static class UnityUtilities
    {
        public static T GetComponentForce<T>(this GameObject gameObject)
        {
            T component;

            component = gameObject.GetComponent<T>();
            if (component != null) return component;
            component = gameObject.GetComponentInChildren<T>();
            if (component != null) return component;
            component = gameObject.GetComponentInParent<T>();
            if (component != null) return component;

            Debug.LogError($"No component {typeof(T)}", gameObject);

            return default(T);
        }
    }
}