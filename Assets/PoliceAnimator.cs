using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAnimator : MonoBehaviour
{
    Animator animatorController;
    float timeCounter = 0f;

    void Awake()
    {
        animatorController = GetComponent<Animator>();
    }

    void Update()
    {
        if (timeCounter >= 15f)
        {
            timeCounter = 0f;
            animatorController.SetBool("changeAnimation", true);
        }
        else if (timeCounter - 5f <= Time.deltaTime)
        {
            timeCounter += Time.deltaTime;
            animatorController.SetBool("changeAnimation", false);
        }
        else
        {
            timeCounter += Time.deltaTime;
        }
    }
}