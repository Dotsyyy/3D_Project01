using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public float delay = 0.5f;      // Time in seconds before the sound can be played again
    private bool canPlaySound = true; // Flag to control sound playback

    public void PlaySound()
    {
        if (canPlaySound && audioSource != null)
        {
            audioSource.Play();     // Play the sound
            canPlaySound = false;  // Disable sound playback temporarily
            StartCoroutine(ResetSoundDelay()); // Start delay coroutine
        }
    }

    private System.Collections.IEnumerator ResetSoundDelay()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        canPlaySound = true;                   // Re-enable sound playback
    }
}
