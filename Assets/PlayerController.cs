using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float mouseSensitivity = 0.5f;

    [Header("Interaction")]
    public Transform cameraTransform;
    public GameObject interactText;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        if (interactText != null)
        {
            interactText.SetActive(false);
        }
    }

    void Update()
    {
        Move();
        Look();
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 50f * Time.deltaTime;

        // Only rotate left/right
        transform.Rotate(Vector3.up * mouseX);
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                if (interactText != null)
                {
                    interactText.SetActive(true);
                }
                return;
            }
        }

        if (interactText != null)
        {
            interactText.SetActive(false);
        }
    }
}