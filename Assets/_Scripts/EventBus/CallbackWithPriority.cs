namespace Core.GameEventSystem
{
    /// <summary>
    /// Wrapper around events
    /// </summary>
    internal class CallbackWithPriority
    {
        public readonly object Callback;
        public readonly int Priority;

        public CallbackWithPriority(int priority, object callback)
        {
            Priority = priority;
            Callback = callback;
        }
    }
}