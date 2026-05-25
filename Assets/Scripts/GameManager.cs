using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Starting values")]
    public float startTemperature = 35f;
    public int startBudget = 100;
    public int startMaintenance = 0;

    [Header("Current values")]
    public float temperature;
    public int budget;
    public int yearlyMaintenance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        temperature = startTemperature;
        budget = startBudget;
        yearlyMaintenance = startMaintenance;

        Debug.Log("GameManager reset.");
        Debug.Log("Temperature: " + temperature);
        Debug.Log("Budget: " + budget);
        Debug.Log("Maintenance: " + yearlyMaintenance + "/year");
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