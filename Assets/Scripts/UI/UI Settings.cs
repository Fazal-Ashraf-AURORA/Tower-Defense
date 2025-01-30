using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    private CameraController cameraController;

    #region Keyboard Sensitivity
    [Header("Keyboard Sensitivity")]
    [SerializeField] private Slider keyboardSensitivitySlider;
    [SerializeField] private TextMeshProUGUI keyboardSensitivityText;
    [SerializeField] private string keyboardSensitivityParameter = "KeyboardSenstivity";

    [SerializeField] private float minimumSensitivity = 60;
    [SerializeField] private float maximumSensitivity = 250;
    public void KeyboardSenstivity(float value)
    {
        float newSenstivity = Mathf.Lerp(minimumSensitivity, maximumSensitivity, value);
        cameraController.AdjustKeyboardSensitivity(newSenstivity);

        keyboardSensitivityText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    #endregion

    #region Mouse Sensitivity

    //For Mouse Movement
    [Header("Mouse Movement Sensitivity")]
    [SerializeField] private Slider mouseMovementSensitivitySlider;
    [SerializeField] private TextMeshProUGUI mouseMovementSensitivityText;
    [SerializeField] private string mouseMovementSensitivityParameter = "mouseMovementSenstivity";

    [SerializeField] private float minimumMouseMovementSensitivity = 60;
    [SerializeField] private float maximumMouseMovementSensitivity = 250;
    public void MouseMovementSenstivity(float value)
    {
        float newSenstivity = Mathf.Lerp(minimumMouseMovementSensitivity, maximumMouseMovementSensitivity, value);
        cameraController.AdjustMouseMovementSensitivity(newSenstivity);

        mouseMovementSensitivityText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    //For Mouse Rotation
    [Header("Mouse Rotation Sensitivity")]
    [SerializeField] private Slider mouseRotationSensitivitySlider;
    [SerializeField] private TextMeshProUGUI mouseRotationSensitivityText;
    [SerializeField] private string mouseRotationSensitivityParameter = "mouseRotationSenstivity";

    [SerializeField] private float minimumMouseRotationSensitivity = 60;
    [SerializeField] private float maximumMouseRotationSensitivity = 250;
    public void MouseRotationSenstivity(float value)
    {
        float newSenstivity = Mathf.Lerp(minimumMouseRotationSensitivity, maximumMouseRotationSensitivity, value);
        cameraController.AdjustMouseRotationSensitivity(newSenstivity);

        mouseRotationSensitivityText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    //For Mouse Zoom
    [Header("Mouse Zoom Sensitivity")]
    [SerializeField] private Slider mouseZoomSensitivitySlider;
    [SerializeField] private TextMeshProUGUI mouseZoomSensitivityText;
    [SerializeField] private string mouseZoomSensitivityParameter = "mouseZoomSenstivity";

    [SerializeField] private float minimumMouseZoomSensitivity = 60;
    [SerializeField] private float maximumMouseZoomSensitivity = 250;
    public void MouseZoomSenstivity(float value)
    {
        float newSenstivity = Mathf.Lerp(minimumMouseZoomSensitivity, maximumMouseZoomSensitivity, value);
        cameraController.AdjustMouseZoomSensitivity(newSenstivity);

        mouseZoomSensitivityText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    #endregion

    private void Awake()
    {
        cameraController = FindFirstObjectByType<CameraController>();
    }


    private void OnDisable()
    {
        PlayerPrefs.SetFloat(keyboardSensitivityParameter, keyboardSensitivitySlider.value);
        PlayerPrefs.SetFloat(mouseMovementSensitivityParameter, mouseMovementSensitivitySlider.value);
        PlayerPrefs.SetFloat(mouseRotationSensitivityParameter, mouseRotationSensitivitySlider.value);
        PlayerPrefs.SetFloat(mouseZoomSensitivityParameter, mouseZoomSensitivitySlider.value);
    }

    private void OnEnable()
    {
        LoadPlayerGameSettings();

        //keyboardSensitivitySlider.value = PlayerPrefs.GetFloat(keyboardSensitivityParameter, 0.5f);
        //mouseMovementSensitivitySlider.value = PlayerPrefs.GetFloat(mouseMovementSensitivityParameter, 0.5f);
        //mouseRotationSensitivitySlider.value = PlayerPrefs.GetFloat(mouseRotationSensitivityParameter, 0.5f);
        //mouseZoomSensitivitySlider.value = PlayerPrefs.GetFloat(mouseZoomSensitivityParameter, 0.5f);
    }

    public void LoadPlayerGameSettings()
    {
        keyboardSensitivitySlider.value = PlayerPrefs.GetFloat(keyboardSensitivityParameter, 0.5f);
        mouseMovementSensitivitySlider.value = PlayerPrefs.GetFloat(mouseMovementSensitivityParameter, 0.5f);
        mouseRotationSensitivitySlider.value = PlayerPrefs.GetFloat(mouseRotationSensitivityParameter, 0.5f);
        mouseZoomSensitivitySlider.value = PlayerPrefs.GetFloat(mouseZoomSensitivityParameter, 0.5f);
    }
}
