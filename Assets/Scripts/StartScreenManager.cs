using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameUI;

    void Start()
    {
        if (startScreen != null)
        {
            startScreen.SetActive(true);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(false);
        }
    }

    public void StartSimulation()
    {
        if (startScreen != null)
        {
            startScreen.SetActive(false);
        }

        if (gameUI != null)
        {
            gameUI.SetActive(true);
        }
    }
}