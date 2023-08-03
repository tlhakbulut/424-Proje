using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDrag : MonoBehaviour
{
    // Reference to the Text UI object
    public GameObject popUpText;
    private void Start()
    {
        popUpText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DragTutorial"))
        {
            // Activate the pop-up text
            Debug.Log("inside");
            popUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DragTutorial"))
        {
            // Deactivate the pop-up text
            Debug.Log("outside");
            popUpText.SetActive(false);
        }
    }
}
