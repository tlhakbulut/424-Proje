using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPointController : MonoBehaviour
{
    public bool isEmpty; //If there is a person in front of the desk or not

    void Awake()
    {
        isEmpty = true;
    }
}
