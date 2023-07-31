using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    //NOT: YANLIŞLIKLA DIALOGUEMANAGERDAKİ PLAYER YERİNE NORMAL PLAYERİ KOYDUM
    public float speed = 5f;
    private Transform player;
    public Transform spawnPoint;
    private Animator animator;
    private DialogueManager dialogueManager;

    public Transform playerTransform;
    public Transform deskTransform;
    public Transform votePlaceTransform1;
    public Transform votePlaceTransform2;
    public Transform chestTransform;

    private Transform targetTransform;
    private Transform votingPlace; //It can be first, second voting place and none

    private bool firstStandIsEmpty, secondStandIsEmpty;
    private bool votingProcessEnded;
    private bool arrivedAtDesk;

    private float rotationSpeed = 8f;
    private float votingPeriod;
    private Vector3 targetRotation;

    private void Awake()
    {
        firstStandIsEmpty = true; //These states should be held in a clasroom object.
        secondStandIsEmpty = true;
        votingProcessEnded = false;
        arrivedAtDesk = false;
        votingPlace = null;

        votingPeriod = Random.Range(5f, 15f);
    }

    private void Start()
    {
        Debug.Log("go");
        transform.position = spawnPoint.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        dialogueManager = GetComponent<DialogueManager>();
        targetTransform = dialogueManager.targetPosition;
    }

    private void Update()
    {
        if (!arrivedAtDesk) //First step is arriving at desk
            MoveTowardsDesk();
        
        //Player and the person will talk, then player will control the person's information and ask her/him to go and vote
        else //The other processes are after reaching the desk
        {
            Debug.Log("WaitToVote");
            WaitToVote();
            if (votingPlace != null && !votingProcessEnded) //If at least one place is available to vote, person will do the further process
            {
                MoveTowardsVotingDestination(votingPlace); //The place will depend if both are empty or none or just one
                Vote(); //Voting animation(?) and the voice 
                //Maybe votingProcessEnded variable should be set to true after a specific amount of time
            }
            if (votingProcessEnded)
            {
                MoveTowardsChest(); //
                PutVoteInChest();
            }
        }
    }

    private void MoveTowardsDesk()
    {
        Debug.Log("MoveTowardsDesk");
        targetTransform = deskTransform;
        Move(deskTransform);
        RotateToTarget();
    }

    private void MoveTowardsVotingDestination(Transform votingPlace)
    {
        Debug.Log("MoveTowardsVotingDestination");
        targetTransform = votingPlace;
        Move(votingPlace);
        RotateToTarget();
    }

    private void Vote()
    {
        Debug.Log("Vote");
        if (AreTheyInSamePosition(transform, targetTransform)) //For now, it's only when arrived at the destination
        {
            votingPeriod -= Time.deltaTime;
            //Play the sound of voting

            if (votingPeriod <= 0f)
                votingProcessEnded = true;
        }
    }

    private void MoveTowardsChest()
    {
        Debug.Log("MoveTowardsChest");
        targetTransform = chestTransform;
        Move(chestTransform);
        RotateToTarget();
    }

    private void PutVoteInChest()
    {
        Debug.Log("PutVoteInChest");
        //Animation to put person's vote in the chest
    }

    private void MoveTowardsPlayer()
    {
        Debug.Log("MoveTowardsPlayer");

        Move(playerTransform);
    }

    private void OnWalkStarted()
    {
        Debug.Log("OnWalkStarted");

        animator.SetBool("isWalking", true);
    }

    private void OnWalkFinished()
    {
        Debug.Log("OnWalkFinished");
        animator.SetBool("isWalking", false);

        //Invoke("RotateToTarget", 2f); //PARAMETER NEEDED
    }

    private void HandleWalking() //Tackles the animations depending on arrival at the target position
    {
        Debug.Log("HandleWalking");
        //Debug.Log("Transform:" + transform.position + " and Target Transform:" + targetTransform.position);
        if (AreTheyInSamePosition(transform, targetTransform))
        {
            Debug.Log("They are in same position");
            OnWalkFinished();

            if (targetTransform == deskTransform)
                arrivedAtDesk = true;
        }
        else
        {
            OnWalkStarted();
        } 
    }

    private void RotateToTarget() //Looks at the current target and rotates towards it
    {
        //animator.SetBool("isTurning", true);

        Vector3 directionToTarget = targetTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
    }

    private void WaitToVote() //The person who is gonna vote should wait for one of the stands to be empty
    {
        if (Input.GetKeyDown(KeyCode.H)) //For now, only when H is pressed, the person goes to vote
        {
            votingPlace = votePlaceTransform1;
        }
        
        // if (firstStandIsEmpty)
        //     votingPlace = votePlaceTransform1;
        // else if (secondStandIsEmpty)
        //     votingPlace = votePlaceTransform2;
        
        // votingPlace = null;
    }

    //Returns if the two given transforms have the same x and z position values
    private bool AreTheyInSamePosition(Transform transform1, Transform transform2)
    {
        if (transform1.position.x - transform2.position.x <= 0.1f && transform1.position.z == transform2.position.z)
            return true;

        Debug.Log("ayni konumda degiller");
        return false;
    }

    private void Move(Transform targetTransform) //Moves to the current target position and handles the animation of walking
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        HandleWalking();
    }

}
