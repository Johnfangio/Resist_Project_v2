using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    [Header("Raycast Detection")]
    public Camera playerCamera;
    public float interactionDistance = 6f;

    private Interactable currentInteractable;
    private Interactable lastInteractable;

    void Start()
    {
        Debug.Log("PlayerInteractor is running on: " + gameObject.name);

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (playerCamera == null)
        {
            Debug.LogError("No player camera assigned and Camera.main was not found.");
        }

        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("InteractionPanel is NOT connected.");
        }

        if (interactionText == null)
        {
            Debug.LogError("InteractionText is NOT connected.");
        }
    }

    void Update()
    {
        FindInteractableInFront();

        if (currentInteractable != null)
        {
            ShowInteractionInfo();

            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                Debug.Log("Interaction input detected.");
                Debug.Log("Trying to interact with: " + currentInteractable.gameObject.name);

                currentInteractable.Interact();

                ShowInteractionInfo();
            }
        }
        else
        {
            HideInteractionInfo();
        }
    }

    void FindInteractableInFront()
    {
        currentInteractable = null;

        if (playerCamera == null)
        {
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, ~0, QueryTriggerInteraction.Collide))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable == null)
            {
                interactable = hit.collider.GetComponentInParent<Interactable>();
            }

            if (interactable != null && !interactable.HasBeenUsed())
            {
                currentInteractable = interactable;

                if (lastInteractable != currentInteractable)
                {
                    Debug.Log("Looking at interactable: " + currentInteractable.gameObject.name);
                    lastInteractable = currentInteractable;
                }

                return;
            }
        }

        lastInteractable = null;
    }

    void ShowInteractionInfo()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
        }

        if (interactionText != null && currentInteractable != null)
        {
            interactionText.text = currentInteractable.GetInteractionText();
        }
    }

    void HideInteractionInfo()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }

        if (interactionText != null)
        {
            interactionText.text = "";
        }
    }
}