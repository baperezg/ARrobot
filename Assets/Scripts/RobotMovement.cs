using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 0.5f; // Units per second

    private bool moveForward = false;
    private bool moveBackward = false;

    [Header("Rotation Settings")]
    public float rotationStep = 15f; // Degrees per button press

    private Matrix4x4 initialMatrix;
    private void Start()
    {
        initialMatrix = transform.worldToLocalMatrix;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (moveForward)
            direction = -transform.forward;   // Local forward
        else if (moveBackward)
            direction = transform.forward;  // Local backward

        // Apply movement with deltaTime
        transform.position += direction * speed * Time.deltaTime;
    }

    // These methods will be linked to UI Buttons
    public void StartForward()
    {
        moveForward = true;
        moveBackward = false;
    }

    public void StartBackward()
    {
        moveBackward = true;
        moveForward = false;
    }

    public void StopMovement()
    {
        moveForward = false;
        moveBackward = false;
    }

    // --- Rotation Controls ---
    public void RotateClockwise()
    {
        transform.Rotate(0f, rotationStep, 0f, Space.Self);
    }

    public void RotateCounterClockwise()
    {
        transform.Rotate(0f, -rotationStep, 0f, Space.Self);
    }

    // --- Reset Transformations ---
    public void ResetTransformations()
    {
        if (initialMatrix != Matrix4x4.zero)
        {
            // Extract position
            Vector3 position = initialMatrix.GetColumn(3);

            // Extract rotation
            Quaternion rotation = Quaternion.LookRotation(
                initialMatrix.GetColumn(2), // Forward (Z axis)
                initialMatrix.GetColumn(1)  // Up (Y axis)
            );

            // Extract scale
            Vector3 scale = new Vector3(
                initialMatrix.GetColumn(0).magnitude,
                initialMatrix.GetColumn(1).magnitude,
                initialMatrix.GetColumn(2).magnitude
            );

            // Apply back to transform
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }
    }
}
