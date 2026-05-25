using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float temperature = 35f;
    public int budget = 100;
    public int yearlyMaintenance = 0;

    void Awake()
    {
        instance = this;
    }

    public bool ApplyEffect(float tempChange, int cost, int maintenance)
    {
        if (budget >= cost)
        {
            temperature += tempChange;
            budget -= cost;
            yearlyMaintenance += maintenance;

            Debug.Log("Temperature: " + temperature);
            Debug.Log("Budget: " + budget);
            Debug.Log("Maintenance: " + yearlyMaintenance + "/year");

            return true;
        }
        else
        {
            Debug.Log("Not enough budget!");
            return false;
        }
    }
}