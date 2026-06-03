using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Values")]
    public float temperature = 35f;
    public int budget = 100;
    public float targetTemperature = 22f;

    [Header("UI")]
    public TMP_Text temperatureText;
    public TMP_Text budgetText;

    [Header("End Screens")]
    public GameObject winPanel;
    public GameObject losePanel;

    private int activatedStations = 0;
    private bool gameEnded = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

            CheckGameState();
        }
        else
        {
            Debug.Log("Not enough budget!");
        }
    }

    public void StationActivated()
    {
        activatedStations++;
        CheckGameState();
    }

    void CheckGameState()
    {
        if (gameEnded)
            return;

        // Win
        if (temperature <= targetTemperature)
        {
            WinGame();
            return;
        }

        // Lose
        if (activatedStations >= 4 && temperature > targetTemperature)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        gameEnded = true;

        if (winPanel != null)
            winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        Debug.Log("You Win!");
    }

    void LoseGame()
    {
        gameEnded = true;

        if (losePanel != null)
            losePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;

        Debug.Log("You Lose!");
    }

    void UpdateUI()
    {
        temperatureText.text = $"Temperature: {temperature:0}°C";
        budgetText.text = $"Budget: ${budget}";
    }
}

