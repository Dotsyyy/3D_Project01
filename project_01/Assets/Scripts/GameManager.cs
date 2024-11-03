using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] menus;   // Menus: 0: menuA, 1: menuB, 2: menuC, 3: menuDLC
    public GameObject[] buttons; // Buttons: 0: buttonA, 1: buttonB, 2: buttonC, 3: buttonDLC
    public GameObject[] starts;  // Start buttons: 0: startA, 1: startB, 2: startC, 3: startDLC




    // Handles button presses
    public void OnCampusPress(int index)
    {
        // Activate the selected menu and start button
        menus[index].SetActive(true);
        starts[index].SetActive(true);

        // Deactivate the pressed button
        buttons[index].SetActive(false);

        // Loop through and manage other menus/buttons
        for (int i = 0; i < menus.Length; i++)
        {
            if (i != index) // Skip the pressed index
            {
                menus[i].SetActive(false);   // Deactivate other menus
                starts[i].SetActive(false);  // Deactivate other start buttons
                buttons[i].SetActive(true);  // Reactivate other buttons
            }
        }
    }
}
