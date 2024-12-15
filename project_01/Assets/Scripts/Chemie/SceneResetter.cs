using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    // Methode om de huidige scene opnieuw te laden
    public void ResetScene()
    {
        // Haal de actieve scene op
        Scene currentScene = SceneManager.GetActiveScene();

        // Herlaad de actieve scene
        SceneManager.LoadScene(currentScene.name);
    }
}
