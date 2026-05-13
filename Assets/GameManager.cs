using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float temperature = 35f;
    public int budget = 100;

    void Awake()
    {
        instance = this;
    }

    public void ApplyEffect(float tempChange, int cost)
    {
        if (budget >= cost)
        {
            temperature += tempChange;
            budget -= cost;

            Debug.Log("Temperature: " + temperature);
            Debug.Log("Budget: " + budget);
        }
        else
        {
            Debug.Log("Not enough budget!");
        }
    }
}

