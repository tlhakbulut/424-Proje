using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskPointController : MonoBehaviour
{
    public bool isEmpty; //If there is a person in front of the desk or not
    public int currentCount;

    void Awake()
    {
        isEmpty = true;
        currentCount = 0;
    }
}
