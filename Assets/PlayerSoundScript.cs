using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    public AudioSource footStepsSound, sprintSound;
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
            if(Input.GetKey(KeyCode.LeftShift)){
                footStepsSound.enabled = false;
                sprintSound.enabled = true;
            }
            else{
                footStepsSound.enabled = true;
                sprintSound.enabled = false;
            }
        }
        else{
            footStepsSound.enabled = false;
            sprintSound.enabled = false;
        }
    }
}
