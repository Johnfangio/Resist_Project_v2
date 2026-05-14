using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float temperature = 35f;
    public int budget = 100;

    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI budgetText;

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
        }
        else
        {
            Debug.Log("Not enough budget!");
        }
    }

    void UpdateUI()
    {
        temperatureText.text = "Temperature: " + temperature.ToString("F1") + "°C";
        budgetText.text = "Budget: " + budget;
    }
}
