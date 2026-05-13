using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float temperatureChange = -2f;
    public int cost = 10;

    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);

        GameManager.instance.ApplyEffect(temperatureChange, cost);
    }
}