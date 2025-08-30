using System.Collections;
using TMPro;
using UnityEngine;

public class WinConditionScript : MonoBehaviour
{
    /*
    1.
    Title: Configurable TIMER / STOPWATCH Unity Tutorial
    Author: BMo
    Date: 13 August 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=u_n3NEi223E

    2.
    Title: "Sophies Heroic Bloodbath - Assignment 4 - Exam - Playable Game"
    Author: Miguel Marindanise, Fatima Zahraa Bham, Yongama Ntloko 
    Date: 16 August 2025
    Code version: 1
    */

    public GameObject levelCompletionScreen;

    [Header("Collectable Objects")]
    public GameObject counterTextObject;
    public GameObject[] collectable;

    [Header("Objective Prompts")]
    public TextMeshProUGUI objectiveText;
    [TextArea(2, 3)] public string currentObjective = "scan the objects.";
    [TextArea(2, 3)] public string objectiveUponCompletion = "completed. leave.";

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public float currentTimer;
    public float timerLimit;
    public bool countDown;
    public bool hasLimit;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectiveText.gameObject.GetComponent<TextMeshProUGUI>().text = currentObjective;
        counterTextObject.gameObject.GetComponent<TextMeshProUGUI>().text = collectable.Length.ToString();

        if (levelCompletionScreen != null)
        {
            levelCompletionScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.Rotate(Vector3.up * 55 * Time.deltaTime);

        StartTimer();

        collectable = GameObject.FindGameObjectsWithTag("Interactable");
        int CollectablesInLevel = collectable.Length;

        objectiveText.gameObject.GetComponent<TextMeshProUGUI>().text = currentObjective;
        counterTextObject.gameObject.GetComponent<TextMeshProUGUI>().text = collectable.Length.ToString();

        /*
        if (CollectablesInLevel > 0)
        {
            //maybe innitiated once player leaves certain area
            StartTimer();
        }
        */

        if (CollectablesInLevel <= 0)
        {
            objectiveText.gameObject.GetComponent<TextMeshProUGUI>().text = objectiveUponCompletion;
            counterTextObject.gameObject.GetComponent<TextMeshProUGUI>().text = "--".ToString();

            if (levelCompletionScreen != null)
            {
                Time.timeScale = 0;
                levelCompletionScreen.SetActive(true);

                gameObject.GetComponent<PauseMenuScript>().enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    void StartTimer()
    {
        timerText.enabled = true;

        currentTimer = countDown ? currentTimer -= Time.deltaTime : currentTimer += Time.deltaTime;

        if (hasLimit && ((countDown && currentTimer <= timerLimit || (!countDown && currentTimer >= timerLimit))))
        {
            currentTimer = timerLimit;
            timerText.color = Color.red;
            enabled = false;
        }

        timerText.text = currentTimer.ToString("00");

        if (currentTimer == timerLimit)
        {
            FindAnyObjectByType<SceneManagerScript>().Restart();
        }

        if (currentTimer <= 20)
        {
            timerText.color = Color.yellow;
        }

        if (currentTimer <= 10)
        {
            timerText.color = Color.red;
        }
    }
}
