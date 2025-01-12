using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioListener audioListener; // Reference to the AudioListener
    private bool isMuted = false; // Tracks the current mute state
    private bool isCooldown = false; // Tracks if the button is in cooldown

    private void Start()
    {
        // Ensure the AudioListener is enabled at the start
        audioListener.enabled = true;
        isMuted = false;

    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Chemie");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("CampusZus");
    }

    public void Mute()
    {
        if (isCooldown) return; // Prevent action if cooldown is active

        // Toggle the mute state
        isMuted = !isMuted;

        // Enable or disable the AudioListener based on the new state
        AudioListener.pause = isMuted;

        Debug.Log($"Audio is now {(isMuted ? "muted" : "unmuted")}.");

        // Start the cooldown coroutine
        StartCoroutine(Cooldown(1f)); // 1-second delay
    }

    private IEnumerator Cooldown(float delay)
    {
        isCooldown = true; // Activate cooldown
        yield return new WaitForSeconds(delay); // Wait for the specified time
        isCooldown = false; // Deactivate cooldown
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        SceneManager.LoadScene("Chemie");
    }
}
