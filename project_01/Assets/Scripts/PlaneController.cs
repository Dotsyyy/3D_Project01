using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the "A" key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("MOVE");
            StartPlaneDescent();
            
        }
    }

    public void StartPlaneDescent()
    {
        // Trigger the descent animation
        animator.SetBool("Move", true);
    }

    // This method can be called at the end of the animation using an Animation Event
    public void OnDescentComplete()
    {
        // Load the next scene
        SceneManager.LoadScene("Chemie");
    }
}
