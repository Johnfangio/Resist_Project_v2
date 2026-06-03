using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float mouseSensitivity = 2f;

    [Header("Interaction")]
    public Transform cameraTransform;
    public TMP_Text interactText;

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

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
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

        if (Physics.Raycast(ray, out hit, 3f))
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