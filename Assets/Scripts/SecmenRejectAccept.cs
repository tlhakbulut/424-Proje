using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecmenRejectAccept : MonoBehaviour
{
    [HideInInspector]
    public int isRejected = 0;
    [HideInInspector]
    public bool isPartyA_MissionEnded = false;
    [HideInInspector]
    public bool isPartyA_MissionAccepted = false;
    [HideInInspector]
    public int rejectedPartyA = 0;
    public int rejectedPartyAFalse = 0;
    public int rejectedPartyB = 0;
    public int rejectedPartyBFalse = 0;

    [HideInInspector]
    public bool isPartyB_MissionEnded = false;
    [HideInInspector]
    public bool isPartyB_MissionAccepted = false;


    // Start is called before the first frame update
    public void SecmenReject()
    {
        isRejected += 1;
        if(gameObject.tag == "A")
        {
            rejectedPartyAFalse++;
            rejectedPartyA++;
        }
        if(gameObject.tag == "A_Fraud")
        {
            rejectedPartyA++;
        }
        if (gameObject.tag == "B")
        {
            rejectedPartyBFalse++;
            rejectedPartyB++;
        }
        if (gameObject.tag == "B_Fraud")
        {
            rejectedPartyB++;
        }

        //Debug.Log("Rejected no: " + rejectedPartyA);
    }

    public void SecmenAccept()
    {
        isRejected -= 1;
    }
    public void AcceptPartyAQuest()
    {
        isPartyA_MissionAccepted = true;
        Debug.Log("Quest accepted");
    }

    public void PartAQuestEnded()
    {
        isPartyA_MissionEnded = true;
        Debug.Log("Quest completed A: " + rejectedPartyBFalse);
        Debug.Log("Quest completed A: " + rejectedPartyB);
        Debug.Log("B: " + rejectedPartyAFalse);
        Debug.Log("B: " + rejectedPartyA);
    }

    public void PartyAQuestFailed()
    {
        isPartyA_MissionEnded = false;
        Debug.Log("Quest failed");
    }

    public void AcceptPartyBQuest()
    {
        isPartyB_MissionAccepted = true;
        Debug.Log("Quest accepted");
    }

    public void PartBQuestEnded()
    {
        isPartyB_MissionEnded = true;
        Debug.Log("Quest completed B: " + rejectedPartyAFalse);
        Debug.Log("Quest completed B: " + rejectedPartyA);
        Debug.Log("A: " + rejectedPartyBFalse);
        Debug.Log("A: " + rejectedPartyB);
    }

    public void PartyBQuestFailed()
    {
        isPartyB_MissionEnded = false;
        Debug.Log("Quest failed");
    }


}
