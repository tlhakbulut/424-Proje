using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameObject[] voterObjects; // An array to hold the 12 voter objects
    private int currentVoterIndex = 0; // Index of the next voter object to activate

    private void Start()
    {
       // ActivateNextGroup(); // Activate the first group of voter objects at the start
    }

    private void Update()
    {
        // Check for user input to activate the next group of voter objects
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivateNextGroup();
        }
    }

    private void ActivateNextGroup()
    {
        // Deactivate the previous group of voter objects (if any)
        DeactivateCurrentGroup();

        // Activate the next group of voter objects (up to 4)
        for (int i = currentVoterIndex; i < Mathf.Min(currentVoterIndex + 4, voterObjects.Length); i++)
        {
            voterObjects[i].SetActive(true);
        }

        // Update the currentVoterIndex for the next group activation
        currentVoterIndex += 4;

        // Check if all voter objects are activated
        if (currentVoterIndex >= voterObjects.Length)
        {
            // Handle the case where all voter objects are activated
            Debug.Log("All voter objects activated!");
        }
    }

    private void DeactivateCurrentGroup()
    {
        // Deactivate the currently active voter objects
        for (int i = currentVoterIndex - 4; i < currentVoterIndex; i++)
        {
            if (i >= 0 && i < voterObjects.Length)
            {
                voterObjects[i].SetActive(false);
            }
        }
    }
}
