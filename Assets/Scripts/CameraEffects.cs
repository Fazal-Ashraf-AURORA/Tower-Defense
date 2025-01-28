using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private CameraController cameraController;

    [SerializeField] private Vector3 inMenuPosition;
    [SerializeField] private Quaternion inMenuRotation;
    [Space]
    [SerializeField] private Vector3 inGamePosition;
    [SerializeField] private Quaternion inGameRotation;

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
    }

    private void SwitchToGameView()
    {
        StartCoroutine(ChangePositionAndRotation(inGamePosition, inGameRotation));
        cameraController.AdjustPitchValue(inGameRotation.x);
    }

    private void SwitchToMenuView()
    {
        StartCoroutine(ChangePositionAndRotation(inMenuPosition, inMenuRotation));
        cameraController.AdjustPitchValue(inMenuRotation.x);
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
}
