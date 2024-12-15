using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour
{
    // Methode om de huidige scene opnieuw te laden
    public void ResetScene()
    {
        SceneManager.LoadScene("Chemie");
    }
}
