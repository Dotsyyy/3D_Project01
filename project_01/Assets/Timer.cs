using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;
    private float startTime;
    private bool isRunning;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Start the timer only if it isn't running
        if (!isRunning)
        {
            StartTimer();
        }
    }

    public void StartTimer()
    {
        if (!isRunning) // Avoid resetting startTime if timer is already running
        {
            startTime = Time.time;
            isRunning = true;
        }
        finalTimeText.gameObject.SetActive(false); // Hide final time text initially
    }

    public void StopTimer()
    {
        isRunning = false;
        DisplayFinalTime();
    }

    private void Update()
    {
        if (isRunning)
        {
            float currentTime = Time.time - startTime;
            int minutes = Mathf.FloorToInt(currentTime / 60F);
            int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
            int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    private void DisplayFinalTime()
    {
        float finalTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(finalTime / 60F);
        int seconds = Mathf.FloorToInt(finalTime - minutes * 60);
        int milliseconds = Mathf.FloorToInt((finalTime * 1000) % 1000);

        finalTimeText.text = string.Format("Final Time: {0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        finalTimeText.gameObject.SetActive(true); // Show final time text
    }

    public float GetCurrentTime()
    {
        return Time.time - startTime;
    }
}
