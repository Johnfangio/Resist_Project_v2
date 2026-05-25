using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    [Header("Detection")]
    public float interactionRadius = 20f;

    private Interactable currentInteractable;
    private Interactable lastInteractable;

    void Start()
    {
        Debug.Log("PlayerInteractor is running on: " + gameObject.name);

        if (interactionPanel != null)
        {
            Debug.Log("InteractionPanel is connected.");
            interactionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("InteractionPanel is NOT connected.");
        }

        if (interactionText != null)
        {
            Debug.Log("InteractionText is connected.");
        }
        else
        {
            Debug.LogError("InteractionText is NOT connected.");
        }
    }

    void Update()
    {
        FindClosestInteractable();

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

    void FindClosestInteractable()
    {
        currentInteractable = null;

        Interactable[] interactables = FindObjectsOfType<Interactable>();

        float closestDistance = Mathf.Infinity;
        Interactable closest = null;

        foreach (Interactable interactable in interactables)
        {
            if (interactable == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, interactable.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }

        if (closest != null && closestDistance <= interactionRadius)
        {
            currentInteractable = closest;

            if (lastInteractable != currentInteractable)
            {
                Debug.Log("Closest interactable: " + currentInteractable.gameObject.name + " | Distance: " + closestDistance);
                lastInteractable = currentInteractable;
            }
        }
        else
        {
            currentInteractable = null;
            lastInteractable = null;
        }
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