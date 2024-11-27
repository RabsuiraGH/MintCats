using Core;
using Core.Character.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCameraDebug : MonoBehaviour
{
    [SerializeField] private PlayerCamera _camera;
    [SerializeField] private PlayerCameraConfig _cameraConfig;

    [Header("UI Elements")]
    [SerializeField] private Toggle _showHide;

    [SerializeField] private RectTransform _panel;

    [SerializeField] private TMP_InputField _cameraSmoothTimeInput;
    [SerializeField] private TMP_InputField _upAndDownRotationSpeedInput;
    [SerializeField] private TMP_InputField _leftAndRightRotationSpeedInput;
    [SerializeField] private TMP_InputField _minimumPivotInput;
    [SerializeField] private TMP_InputField _maximumPivotInput;
    [SerializeField] private Toggle _inverseYToggle;
    [SerializeField] private Toggle _inverseXToggle;
    [SerializeField] private TMP_InputField _sensitiveYInput;
    [SerializeField] private TMP_InputField _sensitiveXInput;
    [SerializeField] private TMP_InputField _defaultCameraPositionInput;

    [SerializeField] private Button _applyButton;

    private void Awake()
    {
        _showHide.onValueChanged.AddListener(Toggle);

        _applyButton.onClick.AddListener(ApplyChanges);

        InitializeUI();
    }

    private void Toggle(bool value)
    {
        _panel.gameObject.SetActive(value);
    }

    private void InitializeUI()
    {
        _cameraSmoothTimeInput.text = _cameraConfig.CameraSmoothTime.ToString("F2");
        _upAndDownRotationSpeedInput.text = _cameraConfig.UpAndDownRotationSpeed.ToString("F0");
        _leftAndRightRotationSpeedInput.text = _cameraConfig.LeftAndRightRotationSpeed.ToString("F0");
        _minimumPivotInput.text = _cameraConfig.MinimumPivot.ToString("F0");
        _maximumPivotInput.text = _cameraConfig.MaximumPivot.ToString("F0");
        _inverseYToggle.isOn = _cameraConfig.InverseY;
        _inverseXToggle.isOn = _cameraConfig.InverseX;
        _sensitiveYInput.text = _cameraConfig.SensitiveY.ToString("F2");
        _sensitiveXInput.text = _cameraConfig.SensitiveX.ToString("F2");
        _defaultCameraPositionInput.text = _cameraConfig.DefaultCameraPosition.ToString("F2");
    }

    private void ApplyChanges()
    {
        if (float.TryParse(_cameraSmoothTimeInput.text, out float smoothTime))
            _cameraConfig.CameraSmoothTime = smoothTime;

        if (float.TryParse(_upAndDownRotationSpeedInput.text, out float upDownSpeed))
            _cameraConfig.UpAndDownRotationSpeed = upDownSpeed;

        if (float.TryParse(_leftAndRightRotationSpeedInput.text, out float leftRightSpeed))
            _cameraConfig.LeftAndRightRotationSpeed = leftRightSpeed;

        if (float.TryParse(_minimumPivotInput.text, out float minPivot))
            _cameraConfig.MinimumPivot = minPivot;

        if (float.TryParse(_maximumPivotInput.text, out float maxPivot))
            _cameraConfig.MaximumPivot = maxPivot;

        _cameraConfig.InverseX = _inverseXToggle.isOn;
        _cameraConfig.InverseY = _inverseYToggle.isOn;

        if (float.TryParse(_sensitiveYInput.text, out float sensitiveY))
            _cameraConfig.SensitiveY = sensitiveY;

        if (float.TryParse(_sensitiveXInput.text, out float sensitiveX))
            _cameraConfig.SensitiveX = sensitiveX;

        if (float.TryParse(_defaultCameraPositionInput.text, out float defaultCameraPosition))
            _cameraConfig.DefaultCameraPosition = defaultCameraPosition;

        _camera.ApplyConfig(_cameraConfig);
        InitializeUI();
    }
}