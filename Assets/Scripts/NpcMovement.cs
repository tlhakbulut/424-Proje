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

    private DeskPointController deskPointController; //Controls if an NPC should wait for the other or not
    private VotePlaceController votePlaceController1; //If there is someone in the first place, controls NPC behaviour
    private VotePlaceController votePlaceController2; //If there is someone in the second place, controls NPC behaviour
    private ChestPointController chestPointController;

    public Transform playerTransform;
    public Transform deskTransform;
    public Transform deskToVote1Transform;
    public Transform deskToVote2Transform;
    public Transform chestTransform;
    public Transform endTransform;

    private Transform targetTransform;
    private Transform votingPlace; //It can be first, second voting place and none
    private Transform votePlaceTransform1;
    private Transform votePlaceTransform2;
    private Transform votePlaceStop1;
    private Transform votePlaceStop2;

    private bool votingProcessEnded;
    private bool arrivedAtDesk;
    private bool arrivedAtVoteStop;
    private bool arrivedAtChest;
    private bool putVote;
    private bool signedPaper;
    private bool movingToDesk;
    private bool movingToVotePlace1;
    private bool movingToVotePlace2;

    private bool adjustedCount; //to increase/decrease the count of people in the classroom

    private float rotationSpeed = 8f;
    private float votingPeriod;
    private Vector3 targetRotation;

    private void Awake()
    {
        votingProcessEnded = false;
        arrivedAtDesk = false;
        arrivedAtVoteStop = false;
        putVote = false;
        arrivedAtChest = false;
        signedPaper = false;
        votingPlace = null;

        movingToDesk = false;
        movingToVotePlace1 = false;
        movingToVotePlace2 = false;

        adjustedCount = false;

        ObtainVotingRoad();

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

        deskPointController = deskTransform.GetComponent<DeskPointController>();
        votePlaceController1 = votePlaceTransform1.GetComponent<VotePlaceController>();
        votePlaceController2 = votePlaceTransform2.GetComponent<VotePlaceController>();
        chestPointController = chestTransform.GetComponent<ChestPointController>();
    }

    private void Update()
    {
        Debug.Log("Classroomdaki kisi sayisi:" + deskPointController.currentCount);

        if (deskPointController.isEmpty) //If the desk is empty, NPC moves to desk
            movingToDesk = true;

        if (deskPointController.currentCount <= 1 && !arrivedAtDesk && movingToDesk) //First step is arriving at desk
        {
            MoveTowardsDesk();
            MakeClassroomBusy();
        }
        
        //Player and the person will talk, then player will control the person's information and ask her/him to go and vote
        else if (arrivedAtDesk)//The other processes are after reaching the desk
        {
            Debug.Log("WaitToVote");
            WaitToVote(); //If there is anyone who is voting and no place is empty, NPC should wait
            if (votingPlace != null && !votingProcessEnded) //If at least one place is available to vote, person will do the further process
            {
                Debug.Log("Voting Destinationa Gidiliyor");
                MoveTowardsVotingDestination(); //The place will depend if both are empty or none or just one
                Vote(); //Voting animation(?) and the voice 
                //Maybe votingProcessEnded variable should be set to true after a specific amount of time
            }
            if (!arrivedAtChest && votingProcessEnded)
            {
                Debug.Log("Voting Process Ended");
                MoveTowardsChest(); //
                PutVoteInChest();
            }
            if (deskPointController.isEmpty && putVote && arrivedAtChest && !signedPaper)
            {
                GoToSignPaper();
            }
            if (putVote && arrivedAtChest && signedPaper)
            {
                GetOutOfTheClassroom();
            }
        }
    }

    private void MoveTowardsDesk()
    {
        //Debug.Log("MoveTowardsDesk");
        targetTransform = deskTransform;
        Move(deskTransform);
        RotateToTarget();
        deskPointController.isEmpty = false; //No one can move to the desk unless this NPC leaves there
        movingToDesk = true;
    }

    private void MoveTowardsVotingDestination()
    {
        Debug.Log("MoveTowardsVotingDestination");

        deskPointController.isEmpty = true;

        if (votingPlace == votePlaceTransform1)
        {
            votePlaceController1.isEmpty = false;
            movingToVotePlace1 = true;

            if(!arrivedAtVoteStop)
            {
                targetTransform = votePlaceStop1;
            }
            else
                targetTransform = votingPlace;
        }
        else if (votingPlace == votePlaceTransform2)
        {
            votePlaceController2.isEmpty = false;
            movingToVotePlace2 = true;
            
            if (!arrivedAtVoteStop)
            {
                targetTransform = votePlaceStop2;
            }
            else
                targetTransform = votingPlace;
        }

        if (movingToVotePlace1 || movingToVotePlace2)
        {
            Move(targetTransform); //votingPlace idi burasi
            RotateToTarget();
        }
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

        if (chestPointController.isEmpty) //If the chest is available, NPC moves towards it
        {
            chestPointController.isEmpty = false;

            if (votingPlace == votePlaceTransform1)
            {
                votePlaceController1.isEmpty = true;

                if (arrivedAtVoteStop)
                    targetTransform = votePlaceStop1;
                else
                    targetTransform = chestTransform;
            }
            else
            {
                votePlaceController2.isEmpty = true;

                if (arrivedAtVoteStop)
                    targetTransform = votePlaceStop2;
                else
                    targetTransform = chestTransform;
            }   
            
            //targetTransform = chestTransform;
            Move(targetTransform);
            RotateToTarget();
        }
    }

    private void PutVoteInChest()
    {
        Debug.Log("PutVoteInChest");
        //Animation to put person's vote in the chest
        if (transform.position == chestTransform.position)
        {
            Invoke("SetPutVoteTrue", 3f);
        }
    }

    void SetPutVoteTrue()
    {
        putVote = true;
        arrivedAtChest = true;
    }

    void GoToSignPaper()
    {
        chestPointController.isEmpty = true;
        deskPointController.isEmpty = false;

        targetTransform = deskTransform;
        Move(targetTransform);
        RotateToTarget();

        if (Input.GetKey(KeyCode.K))
        {
            signedPaper = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        Debug.Log("MoveTowardsPlayer");

        Move(playerTransform);
    }

    private void OnWalkStarted()
    {
        //Debug.Log("OnWalkStarted");

        animator.SetBool("isWalking", true);
    }

    private void OnWalkFinished()
    {
        //Debug.Log("OnWalkFinished");
        animator.SetBool("isWalking", false);

        //Invoke("RotateToTarget", 2f); //PARAMETER NEEDED
    }

    private void HandleWalking() //Tackles the animations depending on arrival at the target position
    {
        //Debug.Log("HandleWalking");
        //Debug.Log("Transform:" + transform.position + " and Target Transform:" + targetTransform.position);
        if (AreTheyInSamePosition(transform, targetTransform))
        {
            //Debug.Log("They are in same position");
            OnWalkFinished();

            if (targetTransform == deskTransform && !votingProcessEnded)
                arrivedAtDesk = true;
            else if (targetTransform == votePlaceStop1 || targetTransform == votePlaceStop2) //This bool is used 2 times
                arrivedAtVoteStop = !arrivedAtVoteStop;
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
        if (votePlaceController1.isEmpty) //Goes to the place which is empty
        {
            votingPlace = votePlaceTransform1;
            Debug.Log("Vote yeri 1");
        }
        else if (votePlaceController2.isEmpty)
        {
            votingPlace = votePlaceTransform2;
            Debug.Log("Vote yeri 2");
        }
    }

    //Returns if the two given transforms have the same x and z position values
    private bool AreTheyInSamePosition(Transform transform1, Transform transform2)
    {
        if (transform1.position.x - transform2.position.x <= 0.1f && transform1.position.z == transform2.position.z)
            return true;

        //Debug.Log("ayni konumda degiller");
        return false;
    }

    private void Move(Transform targetTransform) //Moves to the current target position and handles the animation of walking
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        HandleWalking();
    }

    void ObtainVotingRoad()
    {
        foreach (Transform place in deskToVote1Transform)
        {
            if (place.name == "NearVoting1")
                votePlaceStop1 = place;
            else
                votePlaceTransform1 = place;
        }

        foreach (Transform place in deskToVote2Transform)
        {
            if (place.name == "NearVoting2")
                votePlaceStop2 = place;
            else
                votePlaceTransform2 = place;
        }
    }

    void GetOutOfTheClassroom()
    {
        targetTransform = endTransform;
        Move(targetTransform);
        RotateToTarget();
        Invoke("MakeClassroomAvailable", 3f);
    }

    void MakeClassroomAvailable()
    {
        if (adjustedCount) //to execute it just once
        {
            deskPointController.currentCount -= 1;
            adjustedCount = false;
            deskPointController.isEmpty = true;
        }
    }

    void MakeClassroomBusy()
    {
        if (!adjustedCount) //to execute it just once
        {
            deskPointController.currentCount += 1;
            adjustedCount = true;
        }
    }
}
