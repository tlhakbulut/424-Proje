using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    PlayerCam playerCam;

    float xRotation;
    float yRotation;

    bool lookAtDesk;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        playerCam = GetComponent<PlayerCam>();

        lookAtDesk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lookAtDesk)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -37f, 16f);
            yRotation = Mathf.Clamp(yRotation, -170f, 10f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {
            
        }
    }

    void Ondisable()
    {
        playerCam.enabled = true;
    }
}
