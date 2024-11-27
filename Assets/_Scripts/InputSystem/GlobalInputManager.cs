using Core.GameEventSystem;
using Core.Utility.DebugTool;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Core.GameInputSystem
{
    public class GlobalInputManager : MonoBehaviour
    {
        [SerializeField] private EventBus _eventBus;
#if UNITY_DEBUG
        [SerializeField] private DebugLogger _debugger = new();
#endif
#if UNITY_DEBUG
        [SerializeField] private InputTypeDebug _startInputModeDebug;
#endif
        [SerializeField] private InputMaps _baseControls;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            //_eventBus.Subscribe<SwitchToUIInputSignal>(SwitchToUIInput);
            //_eventBus.Subscribe<SwitchToGameplayInputSignal>(SwithToGameplayInput);
        }

        [Inject]
        public void Construct(EventBus eventBus, InputMaps baseInput)
        {
            _eventBus = eventBus;
            _baseControls = baseInput;
        }

        private InputDevice currentDevice;

        private void OnEnable()
        {
            InputSystem.onActionChange += OnActionChange;
        }

        private void OnDisable()
        {
            InputSystem.onActionChange -= OnActionChange;
        }

        private void OnActionChange(object actionOrMap, InputActionChange change)
        {
            if (change == InputActionChange.ActionPerformed)
            {
                var action = (InputAction)actionOrMap;
                if (action.activeControl != null)
                {
                    currentDevice = action.activeControl.device;
                    Debug.Log($"Current device: {currentDevice.name}");
                }
            }
        }

#if UNITY_DEBUG

        [EasyButtons.Button]
        public void DEBUG_EnableControls(bool enable = true)
        {
            if (enable)
            {
                _baseControls.Enable();
            }
            else
            {
                _baseControls.Disable();
            }
        }

        private void Update()
        {
            /*_debugger.Log(
                this,
                $"Input State: Global: {_baseControls.Global.enabled.Color(Bool)} " +
                $" Gameplay: {_baseControls.Gameplay.enabled.Color(Bool)} " +
                $" UI: {_baseControls.UI.enabled.Color(Bool)}");*/
        }

        private enum InputTypeDebug
        {
            Gameplay,
            UI
        }

        [EasyButtons.Button]

        private void SwitchInputDebug(InputTypeDebug type)
        {
            switch (type)
            {
                case InputTypeDebug.Gameplay:
                    //SwithToGameplayInput(null);
                    break;

                case InputTypeDebug.UI:
                    //SwitchToUIInput(null);
                    break;
            }
        }

#endif
    }
}