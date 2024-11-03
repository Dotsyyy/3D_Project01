using UnityEngine;

public class CubeSlider : MonoBehaviour
{
    public Transform targetPosition;  // The position where the cube will slide to
    public float speed = 2f;          // Speed of the slide
    private bool shouldSlide = false; // To track if the cube should start sliding
    private bool movingToTarget = true; // To track direction of sliding (true -> towards target, false -> back to start)

    private Vector3 startPosition;    // Cube's initial position
    private Vector3 destination;      // The current destination (either start or target)

    void Start()
    {
        // Store the starting position of the cube
        startPosition = transform.position;
        destination = targetPosition.position; // Start by sliding towards the target position
    }

    void Update()
    {
        // If the slide should happen, move the cube
        if (shouldSlide)
        {
            // Smoothly move the cube towards the current destination (either target or start position)
            transform.position = Vector3.Lerp(transform.position, destination, speed * Time.deltaTime);

            // Stop sliding if it reaches the destination (target or start)
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                shouldSlide = false;

                // If we reached the target, prepare to go back to the start position, and vice versa
                if (movingToTarget)
                {
                    destination = startPosition;
                    movingToTarget = false;
                }
                else
                {
                    destination = targetPosition.position;
                    movingToTarget = true;
                }
            }
        }
    }

    // This function will be linked to the button
    public void StartSliding()
    {
        shouldSlide = true;  // Start sliding whenever the button is pressed
    }
}
