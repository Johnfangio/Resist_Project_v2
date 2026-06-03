using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel;
    public PlayerController playerController;

    private bool gameStarted = false;

    void Start()
    {
        introPanel.SetActive(true);

        playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;

        introPanel.SetActive(false);

        playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}