using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartiSecimi : MonoBehaviour
{
    [HideInInspector]
    public bool partiA = false;
    [HideInInspector]
    public bool partiB = false;
    public void A_PartiSec()
    {
        Debug.Log("parti A secildi");
        partiA = true;
    }
    public void B_PartiSec()
    {
        Debug.Log("parti B secildi");
        partiB = true;
    }
}
