using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    /*
    Title: "Sophies Heroic Bloodbath - Assignment 4 - Exam - Playable Game"
    Author: Miguel Marindanise, Fatima Zahraa Bham, Yongama Ntloko
    Date: 16 August 2025
    Code version: 1
    */

    public GameObject pauseScreen;
    private GameObject player;

    private bool gameIsPaused = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseScreen.SetActive(false);
        gameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            gameObject.GetComponent<PauseMenuScript>().enabled = false;
        }
    }

    public void OnPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed && gameIsPaused == false)
        {
            PauseGame();
        }
        else if (context.performed && gameIsPaused == true)
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);

        gameIsPaused = true;

        //disable player controls

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);

        gameIsPaused = false;

        //enable player controls

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
