using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Station Info")]
    public string itemName = "Tree";

    [Header("Effects")]
    public float temperatureChange = -2f;
    public int cost = 10;

    [Header("Tree")]
    public GameObject treeObject;

    private bool activated = false;

    public bool IsActivated => activated;

    public void Interact()
    {
        if (activated)
            return;

        if (GameManager.instance.budget < cost)
        {
            Debug.Log("Not enough budget!");
            return;
        }

        activated = true;

        if (treeObject != null)
        {
            treeObject.SetActive(true);
        }

        Debug.Log("Interacted with " + gameObject.name);

        GameManager.instance.ApplyEffect(
            temperatureChange,
            cost
        );

        GameManager.instance.StationActivated();
    }
}   