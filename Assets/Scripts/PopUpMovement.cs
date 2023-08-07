using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMovement : MonoBehaviour
{
    public GameObject countdownText;
    private float timeLeft = 5f;

    private void Update()
    {
        // Check if the countdown has not reached 0 yet and the text hasn't been shown
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            // Convert the time left to an integer value (e.g., 4.8 becomes 4)
            int secondsLeft = Mathf.CeilToInt(timeLeft);
            countdownText.SetActive(true);
        }
        else 
        {
            countdownText.SetActive(false);
        }
    }
}
