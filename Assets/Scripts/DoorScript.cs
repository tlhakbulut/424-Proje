using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float interactionDistance;
    public GameObject intText;
    public string doorOpenAnim, doorCloseAnim;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, interactionDistance))
        {
            if(hit.collider.gameObject.tag == "door")
            {
                // door hinge which is parent of door
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnim = doorParent.GetComponent<Animator>();
                intText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //if door anim state is open state
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnim))
                    {
                        doorAnim.ResetTrigger("open");
                        doorAnim.SetTrigger("close");
                    }
                    if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnim))
                    {
                        doorAnim.ResetTrigger("close");
                        doorAnim.SetTrigger("open");
                    }
                }
            }
            else
            {
                intText.SetActive(false);
            }
        }
        else
        {
            intText.SetActive(false);
        }

    }

}
