using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
    [SerializeField] private Transform visuals;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float vereticalRotationSpeed;

    private void Update()
    {
        AlignWithSlope();
    }

    private void AlignWithSlope()
    {
        if (visuals == null)
            return;

        if (Physics.Raycast(visuals.position, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, whatIsGround))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hitInfo.normal) * transform.rotation;

            visuals.rotation = Quaternion.Slerp(visuals.rotation, targetRotation,  Time.deltaTime * vereticalRotationSpeed);
        }
    }
}
