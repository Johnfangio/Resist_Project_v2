using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float temperature = 35f;
    public int budget = 100;

    [Header("UI")]
    public TMP_Text temperatureText;
    public TMP_Text budgetText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void ApplyEffect(float tempChange, int cost)
    {
        if (budget >= cost)
        {
            temperature += tempChange;
            budget -= cost;

            UpdateUI();

            Debug.Log("Temperature: " + temperature);
            Debug.Log("Budget: " + budget);
        }
        else
        {
            Debug.Log("Not enough budget!");
        }
    }

    void UpdateUI()
    {
        temperatureText.text = $"Temperature: {temperature:0}°C";
        budgetText.text = $"Budget: ${budget}";
    }
}

