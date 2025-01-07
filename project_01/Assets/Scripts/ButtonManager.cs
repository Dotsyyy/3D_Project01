using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioListener audioListener;

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
        audioListener.enabled = false;
    }
}
