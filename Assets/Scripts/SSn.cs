using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSn : MonoBehaviour
{
    // Start is called before the first frame update

    public int ssn = 0;
    void Start()
    {
        ssn = Random.Range(10000000, 99999999);
        PlayerPrefs.SetInt("ssn"+ gameObject.name, ssn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
