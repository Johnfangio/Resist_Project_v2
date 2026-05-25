using UnityEngine;

public class CameraLookDrag : MonoBehaviour
{
    public float sensitivity = 0.2f;
    public float minPitch = -30f;
    public float maxPitch = 30f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        Vector3 currentRotation = transform.eulerAngles;
        yaw = currentRotation.y;
        pitch = currentRotation.x;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * 10f;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * 10f;

            yaw += mouseX;
            pitch -= mouseY;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}