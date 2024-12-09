using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private bool followZ = true;

    [SerializeField] private float followSpeed = 5f;

    private Vector3 targetPosition;
    
    private bool enableTargetTracking = false;

    public void Initialize(Transform target, bool x = false, bool y = false, bool z = false)
    {
        this.target = target;
        followX = x; followY = y; followZ = z;
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("No hay target asignado para seguir.");
            return;
        }

        if (enableTargetTracking)
        {
            targetPosition = transform.position;

            if (followX)
                targetPosition.x = target.position.x;

            if (followY)
                targetPosition.y = target.position.y;

            if (followZ)
                targetPosition.z = target.position.z;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
