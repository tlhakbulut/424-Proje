using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameObject[] voterObjects; 
    private int currentVoterIndex = 0; 
    private bool isActiveChild = false;
    private int activeChildCount = 0;
    public GameObject[] electionOfficers;
    private int electionOfficerCounter = 3;

    private void Start()
    {
       
    }

    private void Update()
    {
        

        for (int i = 0; i < transform.childCount; i++)
        {
            if(i == 0)
            {
                activeChildCount = 0;
            }
            GameObject child = transform.GetChild(i).gameObject;
            if (child.activeSelf)
            {
                activeChildCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && activeChildCount == 0)
        {
            ActivateNextGroup();
            ActivateNextElectionOfficer();
        }
    }

    private void ActivateNextGroup()
    {
        
        //DeactivateCurrentGroup();

        // Activate the next group of voter objects (up to 4)
        for (int i = currentVoterIndex; i < Mathf.Min(currentVoterIndex + 4, voterObjects.Length); i++)
        {
            voterObjects[i].SetActive(true);
        }

        currentVoterIndex += 4;

        if (currentVoterIndex >= voterObjects.Length)
        {
            Debug.Log("All voter objects activated!");
        }
    }

    private void ActivateNextElectionOfficer()
    {
        if(electionOfficerCounter == 3)
        {
            Debug.Log("hereee");
            electionOfficerCounter -= 1;
            electionOfficers[0].SetActive(false);
            electionOfficers[1].SetActive(true);

        }
        else if(electionOfficerCounter == 2)
        {
            Debug.Log("XXXXXXXXXXXXX");
            electionOfficerCounter -= 1;
            electionOfficers[1].SetActive(false);
            electionOfficers[2].SetActive(true);
        }
    }



}
