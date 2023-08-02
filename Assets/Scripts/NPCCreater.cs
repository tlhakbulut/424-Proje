using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCreater : MonoBehaviour
{
    public GameObject Remy;
    public GameObject Boss;
    public GameObject Leonard;

    int counter = 3;

    void Update()
    {   
        while (counter >= 0)
        {
            Instantiate(Remy, new Vector3(-0.144389153f,-2.50729036f,-9.20677948f), Quaternion.identity);
            counter -= 1;
        }
        
    }
}
