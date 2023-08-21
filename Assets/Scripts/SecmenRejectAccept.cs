using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecmenRejectAccept : MonoBehaviour
{
    [HideInInspector]
    public int isRejected = 0;
    [HideInInspector]
    public bool isPolice_MissionEnded = false;
    [HideInInspector]
    public bool isPartyA_MissionEnded = false;
    [HideInInspector]
    public bool isPartyA_MissionAccepted = false;
    [HideInInspector]
    //public int rejectedPartyA = 0;
   // public int rejectedPartyAFalse = 0;
    //public int rejectedPartyB = 0;
    private int rejectedPartyBFalse = 0;

    [HideInInspector]
    public bool isPartyB_MissionEnded = false;
    [HideInInspector]
    public bool isPartyB_MissionAccepted = false;


    VoterPointCount voterPointCount;

    // Start is called before the first frame update

    private void Awake()
    {
        voterPointCount = GetComponentInParent<VoterPointCount>();
    }


    public void SecmenReject()
    {
        
        Debug.Log("REJECTED");
        Debug.Log("name: " + gameObject.name);
        isRejected += 1;
        if(gameObject.tag == "A")
        {
            Debug.Log("A Rejected");
            voterPointCount.rejectedPartyA++;
            voterPointCount.rejectedPartyAFalse++;
            //rejectedPartyAFalse++;
            //rejectedPartyA++;
        }
        if(gameObject.tag == "A_Fraud")
        {
            Debug.Log("A Fraud Rejected");
            voterPointCount.rejectedPartyA++;
            voterPointCount.rejectedPartyA_Fraud++;
            //rejectedPartyA++;
        }
        if (gameObject.tag == "B")
        {
            Debug.Log("B Rejected");
            voterPointCount.rejectedPartyB++;
            voterPointCount.rejectedPartyBFalse++;
            //rejectedPartyBFalse++;
            //rejectedPartyB++;
        }
        if (gameObject.tag == "B_Fraud")
        {
            Debug.Log("B Fraud Rejected");
            voterPointCount.rejectedPartyB++;
            voterPointCount.rejectedPartyB_Fraud++;
            //rejectedPartyB++;
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
        Debug.Log("Quest A accepted");
    }

    public void PartAQuestEnded()
    {
        isPartyA_MissionEnded = true;
        //Debug.Log("Quest completed A: " + rejectedPartyBFalse);
        //Debug.Log("Quest completed A: " + rejectedPartyB);
        Debug.Log("Quest completed A: " + voterPointCount.rejectedPartyBFalse);
        Debug.Log("Quest completed A: " + voterPointCount.rejectedPartyB);
        //Debug.Log("B: " + rejectedPartyAFalse);
        //Debug.Log("B: " + rejectedPartyA);
        Debug.Log("B: " + voterPointCount.rejectedPartyAFalse);
        Debug.Log("B: " + voterPointCount.rejectedPartyA);
    }

    public void PartyAQuestFailed()
    {
        isPartyA_MissionEnded = false;
        Debug.Log("Quest failed");
    }

    public void AcceptPartyBQuest()
    {
        isPartyB_MissionAccepted = true;
        Debug.Log("Quest B accepted");
    }

    public void PartBQuestEnded()
    {
        isPartyB_MissionEnded = true;
        //Debug.Log("Quest completed B: " + rejectedPartyAFalse);
       // Debug.Log("Quest completed B: " + rejectedPartyA);
        Debug.Log("Quest completed A: " + voterPointCount.rejectedPartyAFalse);
        Debug.Log("Quest completed A: " + voterPointCount.rejectedPartyA);
        Debug.Log("B: " + voterPointCount.rejectedPartyBFalse);
        Debug.Log("B: " + voterPointCount.rejectedPartyB);
        //Debug.Log("A: " + rejectedPartyBFalse);
       // Debug.Log("A: " + rejectedPartyB);
    }

    public void PartyBQuestFailed()
    {
        isPartyB_MissionEnded = false;
        Debug.Log("Quest failed");
    }

    public void PoliceQuestEnded()
    {
        isPolice_MissionEnded = true;
    }


}
