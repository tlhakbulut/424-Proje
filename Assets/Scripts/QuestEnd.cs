using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class QuestEnd : MonoBehaviour
{

    [HideInInspector]
    public bool isPartyA_MissionEnded = false;
    [HideInInspector]
    public bool isPartyB_MissionEnded = false;
    [HideInInspector]
    public bool isPolice_MissonEnded = false;

    VoterPointCount voterPointCount;


    private void Awake()
    {
        voterPointCount = GetComponentInParent<VoterPointCount>();
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

    public void PoliceQuestEnded()
    {
        Debug.Log("police mission completed");
        isPolice_MissonEnded = true;
    }
    public void PoliceQuestFailed()
    {
        Debug.Log("police quest failed: " + isPolice_MissonEnded);
    }


}
