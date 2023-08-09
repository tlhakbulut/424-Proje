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
    // Start is called before the first frame update
    public void SecmenReject()
    {
        isRejected += 1;
        rejectedPartyA++;
        Debug.Log("Rejected no: " + rejectedPartyA);
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
        Debug.Log("Quest completed");
    }
    public void PartyAQuestFailed()
    {
        isPartyA_MissionEnded = false;
        Debug.Log("Quest failed");
    }

}
