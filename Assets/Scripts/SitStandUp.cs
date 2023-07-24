using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitStandUp : MonoBehaviour
{
    public Transform target; //The target that makes possible to change the camera aspect when getting close to it
    public GameObject deskCamHolder;

    private PlayerMovement playerMovement;
    private Transform playerTransform;
    private Vector3 playerPos;
    private DeskCam deskCam;

    private bool isSitting = false;

    void Start()
    {
        playerTransform = gameObject.transform.parent.transform;
        playerPos = playerTransform.position;
        playerMovement = gameObject.transform.parent.GetComponent<PlayerMovement>();
        
        deskCam = deskCamHolder.GetComponent<DeskCam>();
    }

    void Update()
    {
        playerTransform = gameObject.transform.parent.transform;
        playerPos = playerTransform.position;
        SittingBehaviour();
    }

    void SittingBehaviour()
    {
        if (Vector3.Distance(playerPos, target.position) <= 4.2f)
        {
            if (!isSitting && Input.GetKeyDown(KeyCode.E))
            {
                isSitting = true;
                Sit();
            }

            else if (isSitting && Input.GetKeyDown(KeyCode.E))
            {
                isSitting = false;
                StandUp();
            }
        }
    }

    void StandUp()
    {
        gameObject.transform.parent.transform.Translate(Vector3.forward*-2f + Vector3.right, Space.Self);
        playerMovement.enabled = true;

        deskCam.enabled = false;
    }

    void Sit()
    {
        playerMovement.enabled = false;
        playerTransform.position = Vector3.Lerp(playerPos, target.transform.position + Vector3.up*1.2f, 5f);
        Vector3 newRotation = Vector3.RotateTowards(playerTransform.rotation.eulerAngles, target.transform.rotation.eulerAngles, Time.deltaTime * 5f, 0f);
        playerTransform.rotation = Quaternion.Euler(newRotation);

        deskCam.enabled = true;
    }
}
