using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Camera : MonoBehaviour
{
    [SerializeField] public Transform Target;
    [SerializeField] public float Height = 45f;
    [SerializeField] public float Distance = 20f;
    [SerializeField] public float Angle = 0f;
    [SerializeField] public float ZoomSpeed = 50f;
    private float zoom;

    private void LateUpdate()
    {
        if (!Target)
        {
            return;
        }

        // Build World Position vector
        Vector3 worldPosition = (Vector3.forward * -Distance) + (Vector3.up * Height);

        //  Build Rotated vector
        Vector3 rotatedVector = Quaternion.AngleAxis(Angle, Vector3.up) * worldPosition;

        // Move Position
        HandleZoom();
        Vector3 flatTargetPosition = Target.position;
        flatTargetPosition.y = zoom;
        Vector3 finalPosition = flatTargetPosition + rotatedVector;
//        Debug.DrawLine(Target.position, finalPosition, Color.blue);

        transform.position = finalPosition;
        transform.LookAt(flatTargetPosition);
    }

    private void HandleZoom()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            zoom -= ZoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            zoom += ZoomSpeed * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= ZoomSpeed * Time.deltaTime * 10f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += ZoomSpeed * Time.deltaTime * 10f;
        }
        zoom = Mathf.Clamp(zoom, 0f, 100f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        if (Target)
        {
            Gizmos.DrawLine(transform.position, Target.position);
            Gizmos.DrawSphere(Target.position, 1.5f);
        }
        Gizmos.DrawSphere(transform.position, 1.5f);
    }
}
