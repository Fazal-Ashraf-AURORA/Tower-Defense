using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool canControl;
    [SerializeField] private Vector3 levelCenterPoint;
    [SerializeField] private float maxDistanceFromCenter;

    [Header("Movement Details")]
    [SerializeField] private float movementSpeed = 120f;
    [SerializeField] private float mouseMovementSpeed = 5f;

    [Header("Edge Movement Details")]
    [SerializeField] private float edgeThreshold = 10f;
    [SerializeField] private float edgeMovementSpeed = 10f;
    private float screenWidth;
    private float screenHeight;

    [Header("Rotation details")]
    [SerializeField] private Transform focusPoint;
    [SerializeField] private float maxFocusPointDisctance = 15f;
    [SerializeField] private float rotationSpeed = 200f;
    [Space]
    private float pitch;
    [SerializeField] private float maxPitch = 85f;
    [SerializeField] private float minPitch = 5f;

    [Header("Zoom details")]
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 15f;

    private float smoothTime = 0.1f;
    private Vector3 movementVelocity = Vector3.zero;
    private Vector3 mouseMovementVelocity = Vector3.zero;
    private Vector3 edgeMovementVelocity = Vector3.zero;
    private Vector3 zoomVelocity = Vector3.zero;
    private Vector3 lastMousePosition;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Update()
    {
        if(!canControl)
            return;
        HandleMovement();
        HandleMouseMovement();
        //HandleEdgeMovement();
        HandleZoom();
        HandleRotation();

        focusPoint.position = transform.position + (transform.forward * GetFocusPointDistance());
    }

    

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomDirection = transform.forward * scroll * zoomSpeed;
        Vector3 targetPosition = transform.position + zoomDirection;

        if(transform.position.y < minZoom && scroll > 0)
            return;
        if(transform.position.y > maxZoom && scroll < 0)
            return;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref zoomVelocity, .5f);
    }

    private float GetFocusPointDistance()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxFocusPointDisctance))
            return hitInfo.distance;
        return maxFocusPointDisctance;
    }

    private void HandleRotation()
    {
        if(Input.GetMouseButton(1)) // Right mouse button for rotation
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            pitch = Mathf.Clamp(pitch - verticalRotation, minPitch, maxPitch);
            
            transform.RotateAround(focusPoint.position, Vector3.up, horizontalRotation);
            transform.RotateAround(focusPoint.position, transform.right, pitch - transform.eulerAngles.x);

            transform.LookAt(focusPoint);
        }
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButton(1))
            return; // there is a bug i have to fix it

        Vector3 targetPosition = transform.position;

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        if (horizontalInput > 0)
            targetPosition += transform.right * movementSpeed * Time.deltaTime;
        if (horizontalInput < 0)
            targetPosition -= transform.right * movementSpeed * Time.deltaTime;

        if (verticalInput > 0)
            targetPosition += flatForward * movementSpeed * Time.deltaTime;
        if (verticalInput < 0)
            targetPosition -= flatForward * movementSpeed * Time.deltaTime;

        if (Vector3.Distance(levelCenterPoint, targetPosition) > maxDistanceFromCenter)
        {
            targetPosition = levelCenterPoint + (targetPosition - levelCenterPoint).normalized * maxDistanceFromCenter;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref movementVelocity, smoothTime);
    }

    private void HandleMouseMovement()
    {
        if(Input.GetMouseButtonDown(2))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 positionDifference = Input.mousePosition - lastMousePosition;
            Vector3 moveRight = transform.right * (-positionDifference.x) * mouseMovementSpeed * Time.deltaTime;
            Vector3 moveForward = transform.forward * (-positionDifference.y) * mouseMovementSpeed * Time.deltaTime;

            moveRight.y = 0;
            moveForward.y = 0;

            Vector3 movement = moveForward + moveRight;
            Vector3 targetPosition = transform.position + movement;

            if(Vector3.Distance(levelCenterPoint, targetPosition) > maxDistanceFromCenter)
            {
                targetPosition = levelCenterPoint + (targetPosition - levelCenterPoint).normalized * maxDistanceFromCenter;
            }

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref mouseMovementVelocity, smoothTime);
            lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleEdgeMovement()
    {
        Vector3 targetPosition = transform.position;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 flatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        if (mousePosition.x > screenWidth - edgeThreshold)
            targetPosition += transform.right * edgeMovementSpeed * Time.deltaTime;

        if (mousePosition.x < edgeThreshold)
            targetPosition -= transform.right * edgeMovementSpeed * Time.deltaTime;

        if (mousePosition.y > screenHeight - edgeThreshold)
            targetPosition += flatForward * edgeMovementSpeed * Time.deltaTime;

        if (mousePosition.y < edgeThreshold)
            targetPosition -= flatForward * edgeMovementSpeed * Time.deltaTime;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref edgeMovementVelocity, smoothTime);
    }

    public void EnableCameraControls(bool enable) => canControl = enable;

    public float AdjustPitchValue(float value) => pitch = value;
}
