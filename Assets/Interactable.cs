using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Effect")]
    public float temperatureChange = -2f;
    public int cost = 10;
    public int maintenance = 5;

    [Header("Visual to activate")]
    public GameObject objectToActivate;

    [Header("Information")]
    public string solutionName = "Tree";

    [TextArea(2, 5)]
    public string description = "Trees provide shade, reduce surface heat and improve the comfort of public spaces during extreme heat.";

    private bool hasBeenUsed = false;

    public bool HasBeenUsed()
    {
        return hasBeenUsed;
    }

    public string GetInteractionText()
    {
        if (hasBeenUsed)
        {
            return solutionName + " already planted.";
        }

        return
            "Press E to plant: " + solutionName + "\n\n" +
            "Temperature effect: " + temperatureChange + "°C\n" +
            "Cost: " + cost + "\n" +
            "Maintenance: " + maintenance + "/year\n\n" +
            description;
    }

    public void Interact()
    {
        if (hasBeenUsed)
        {
            Debug.Log(solutionName + " has already been used.");
            return;
        }

        Debug.Log("Interact() started on: " + gameObject.name);

        bool success = true;

        if (GameManager.instance != null)
        {
            success = GameManager.instance.ApplyEffect(temperatureChange, cost, maintenance);
        }
        else
        {
            Debug.LogWarning("No GameManager.instance found. Tree will still be activated, but budget/temperature will not update.");
        }

        if (!success)
        {
            Debug.Log("Not enough budget for " + solutionName);
            return;
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log("Activated visual object: " + objectToActivate.name);
        }
        else
        {
            Debug.LogError("Object To Activate is missing on: " + gameObject.name);
            return;
        }

        hasBeenUsed = true;

        Debug.Log("Activated: " + solutionName);
    }
}