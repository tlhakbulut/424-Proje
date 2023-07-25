using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoodController : MonoBehaviour
{
    public TextMeshProUGUI hoodText;
    private float currentTime;
    private int hour;

    void Start()
    {
        hour = 12;
        currentTime = 0f;
        UpdateHoodText();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        UpdateHoodText();
    }

    void UpdateHoodText()
    {
        int seconds = (int)currentTime; // Convert float to int to get the number of seconds

        if (seconds % 60 == 0 && seconds != 0)
        {
            hour += 1;
            seconds = 0;
        }

        if (seconds < 10)
            hoodText.text = "Time: " + hour.ToString() + ".0" + seconds.ToString(); // Update the UI TextMeshPro with the current time
        else
            hoodText.text = "Time: " + hour.ToString() + "." + seconds.ToString();
    }
}
