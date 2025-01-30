using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private CameraController cameraController;

    [SerializeField] private Vector3 inMenuPosition;
    [SerializeField] private Quaternion inMenuRotation;
    [Space]
    [SerializeField] private Vector3 inGamePosition;
    [SerializeField] private Quaternion inGameRotation;

    [Header("ScreenShake Details")]
    [Range(0.01f, 0.5f)]
    [SerializeField] private float shakeMagnitude;
    [Range(0.1f, 3f)]
    [SerializeField] private float shakeDuration;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void Start()
    {
        SwitchToMenuView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToMenuView();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToGameView();

        if (Input.GetKeyDown(KeyCode.V))
            ScreenShake(shakeDuration, shakeMagnitude);
    }

    public void ScreenShake(float newDuration, float newMagnitude)
    {
        StartCoroutine(ScreenShakeFX(newDuration,newMagnitude));
    }
    private void SwitchToMenuView()
    {
        StartCoroutine(ChangePositionAndRotation(inMenuPosition, inMenuRotation));
        cameraController.AdjustPitchValue(inMenuRotation.eulerAngles.x);
    }

    private void SwitchToGameView()
    {
        StartCoroutine(ChangePositionAndRotation(inGamePosition, inGameRotation));
        cameraController.AdjustPitchValue(inGameRotation.eulerAngles.x);
    }

    private IEnumerator ChangePositionAndRotation(Vector3 targetPosition, Quaternion targetRotation, float duration = 3, float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        cameraController.EnableCameraControls(false);

        float time = 0;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;
        cameraController.EnableCameraControls(true);
    }

    private IEnumerator ScreenShakeFX(float duration, float magnitude)
    {
        Vector3 originalPosition = cameraController.transform.position;
        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            cameraController.transform.position = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraController.transform.position = originalPosition;
    }
}
