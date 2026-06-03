using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float mouseSensitivity = 1f;

    [Header("Interaction")]
    public Transform cameraTransform;
    public TMP_Text interactText;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
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

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * z + right * x;

        controller.Move(move * speed * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        cameraTransform.localRotation =
            Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f))
        {
            Interactable interactable =
                hit.collider.GetComponentInParent<Interactable>();

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

        if (Physics.Raycast(ray, out hit, 2f))
        {
            Interactable interactable =
                hit.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                if (interactText != null)
                {
                    if (!interactable.IsActivated)
                    {
                        interactText.text =
                            $"Press E\n\n" +
                            $"{interactable.itemName}\n" +
                            $"Cost: ${interactable.cost}\n" +
                            $"Cooling: {Mathf.Abs(interactable.temperatureChange)}°C";
                    }
                    else
                    {
                        interactText.text =
                            $"{interactable.itemName}\nActivated";
                    }

                    interactText.gameObject.SetActive(true);
                }

                return;
            }
        }

        if (interactText != null)
        {
            interactText.gameObject.SetActive(false);
        }
    }
}