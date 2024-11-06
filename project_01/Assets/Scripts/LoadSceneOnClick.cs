using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneOnClick : MonoBehaviour
{
    public string sceneName = "Chemie";  // Naam van de scène die geladen moet worden

    // Functie om de scène te laden
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

