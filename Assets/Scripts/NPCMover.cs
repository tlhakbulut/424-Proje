using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMover : MonoBehaviour
{
    //NOT: YANLIŞLIKLA DIALOGUEMANAGERDAKİ PLAYER YERİNE NORMAL PLAYERİ KOYDUM
    [SerializeField] float speed = 5f;

    public Transform spawnPointTransform;
    public Transform deskTransform;
    public Transform deskToVote1Transform;
    public Transform deskToVote2Transform;
    public Transform chestTransform;
    public Transform endTransform;

    private DeskPointController deskPointController; //Controls if an NPC should wait for the other or not
    private VotePlaceController votePlaceController1; //If there is someone in the first place, controls NPC behaviour
    private VotePlaceController votePlaceController2; //If there is someone in the second place, controls NPC behaviour
    private ChestPointController chestPointController;

    private Animator animator;
    private DialogueManager dialogueManager;

    //The points that NPC is moving towards
    private Transform targetTransform;
    private Transform votingPlace; //It can be first, second voting place and none
    private Transform votePlaceTransform1;
    private Transform votePlaceTransform2;
    private Transform votingPlaceStop;
    private Transform votePlaceStop1;
    private Transform votePlaceStop2;

    //The values to control the voting process of an NPC
    private bool arrivedAtDesk;
    private bool movingToDesk;
    private bool talkedToPlayer;
    private bool permittedToVote;
    private bool arrivedAtVoteStop;
    private bool arrivedAtVotePlace;
    private bool votingProcessEnded;
    private bool arrivedBackAtVoteStop;
    private bool movingToChest;
    private bool arrivedAtChest;
    private bool putVote;
    private bool movingBackToDesk;
    private bool arrivedBackAtDesk;
    private bool signedPaper;
    private bool finishedProcess;

    private float rotationSpeed = 8f;
    private Vector3 targetRotation;

    private void Awake()
    {
        SpawnNPC();
        InitializeValues();
        ObtainVotingRoad();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogueManager = GetComponent<DialogueManager>();
        targetTransform = dialogueManager.targetPosition;

        GetPointControllers();
    }

    private void Update()
    {
        //Debug.Log("desk su an bos:" + deskPointController.isEmpty);
        //Debug.Log("vote place 1 su an bos:" + votePlaceController1.isEmpty);
        //Debug.Log("vote place 2 su an bos:" + votePlaceController2.isEmpty);
        Debug.Log("sandik su an bos:" + chestPointController.isEmpty);
        //Debug.Log("Oy yerine vardi:" + arrivedAtVotePlace + " sandiga varmadi:" + !arrivedAtChest + " oy verme islemi bitti:" + votingProcessEnded + " sandiga gidiyo" + movingToChest);

        ManageValues(); //Changes the bool and other values according to the current situation of NPC

        if (movingToDesk && !arrivedAtDesk)
        {
            Debug.Log(gameObject.name + " masaya geliyor");
            MoveTowardsDesk();
        }
        else if (arrivedAtDesk && !talkedToPlayer)
        {
            Debug.Log(gameObject.name + " oyuncuyla konusuyor");
            TalkToPlayer(); //Let this method handle the bool value talkedToPlayer and permittedToVote
            //(for example, after the chat, press Enter and let talkedToPlayer be true)
        }
        else if (permittedToVote && !arrivedAtVotePlace && !votingProcessEnded)
        {
            Debug.Log(gameObject.name + " oy verme yerine gidiyor");
            MoveTowardsVotingDestination(); //make votingProcessEnded, arrivedAtVoteStop true after some seconds (or votinProcessEnd may be useful)
        }
        else if (talkedToPlayer && !permittedToVote)
        {
            Debug.Log(gameObject.name + " siniftan cikiyor");
            GetOutOfTheClassroom();
        }
        else if (permittedToVote && arrivedAtVotePlace && !votingProcessEnded)
        {
            Debug.Log(gameObject.name + " oy atiyor");
            Vote();
        }
        else if (arrivedAtVotePlace && !arrivedAtChest && votingProcessEnded && movingToChest)
        {
            Debug.Log(gameObject.name + " sandiga gidiyor");
            MoveTowardsChest();
        }
        else if (arrivedAtChest && !putVote)
        {
            Debug.Log(gameObject.name + " sandiga oy atiyor");
            PutVoteInChest();
        }
        else if (arrivedAtChest && putVote && !arrivedBackAtDesk && movingBackToDesk)
        {
            Debug.Log(gameObject.name + " imza atmak icin masaya geliyor");
            MoveTowardsDesk();
        }
        else if (arrivedBackAtDesk && !signedPaper)
        {
            Debug.Log(gameObject.name + " imza atiyor");
            SignPaper(); //Let this method handle the bool value signedPaper
        }
        else if (signedPaper)
        {
            Debug.Log(gameObject.name + " siniftan cikiyor");
            GetOutOfTheClassroom();
        }
    }

    //-----------------------------------Methods for Awake---------------------------------------
    //Initializes all the values suitable for starting
    private void SpawnNPC()
    {
        transform.position = spawnPointTransform.position;
    }

    private void InitializeValues()
    {
        arrivedAtDesk = false;
        movingToDesk = false;
        talkedToPlayer = false;
        permittedToVote = false;
        arrivedAtVoteStop = false;
        arrivedAtVotePlace = false;
        votingProcessEnded = false;
        arrivedBackAtVoteStop = false;
        movingToChest = false;
        arrivedAtChest = false;
        putVote = false;
        movingBackToDesk = false;
        arrivedBackAtDesk = false;
        signedPaper = false;
        finishedProcess = false;

        votingPlace = null;
        votingPlaceStop = null;
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
    //-------------------------------End of methods for Awake------------------------------------

    //--------------------------------------Methods for Start-------------------------------------
    //Obtains the scripts of the points to be able to control the electors' behaviours in the classroom
    private void GetPointControllers()
    {
        deskPointController = deskTransform.GetComponent<DeskPointController>();
        votePlaceController1 = votePlaceTransform1.GetComponent<VotePlaceController>();
        votePlaceController2 = votePlaceTransform2.GetComponent<VotePlaceController>();
        chestPointController = chestTransform.GetComponent<ChestPointController>();
    }
    //-------------------------------End of methods for Start------------------------------------

    //----------------------------------Methods for Update------------------------------------
    private void ManageValues()
    {
        //If the NPC is spawned, waiting there and there are 1 or less people in class, moves towards the empty desk
        if (IsAt(spawnPointTransform) && deskPointController.currentCount <= 1 && deskPointController.isEmpty)
        {
            //Debug.Log(gameObject.name + "1");
            movingToDesk = true;
            deskPointController.currentCount += 1;
            deskPointController.isEmpty = false;
        }
        //If the NPC arrived at the desk, makes arrivedAtDesk true so that NPC can talk to our player
        else if (IsAt(deskTransform) && movingToDesk)
        {
            //Debug.Log(gameObject.name + "2");
            movingToDesk = false;
            arrivedAtDesk = true;
            targetTransform = transform;
        }
        //If the NPC talked to the player, the voting place is decided
        else if (IsAt(deskTransform) && talkedToPlayer && permittedToVote && votingPlace == null)
        {
            Debug.Log(gameObject.name + "3");
            if (votePlaceController1.isEmpty)
            {
                votingPlace = votePlaceTransform1;
                targetTransform = votingPlace;
                votingPlaceStop = votePlaceStop1;
                votePlaceController1.isEmpty = false;
            }
            else
            {
                votingPlace = votePlaceTransform2;
                targetTransform = 
                votingPlaceStop = votePlaceStop2;
                votePlaceController2.isEmpty = false;
            }

            deskPointController.isEmpty = true;
        }
        //The NPC is not allowed to vote, so s/he leaves
        else if (IsAt(deskTransform) && talkedToPlayer && !permittedToVote && !finishedProcess)
        {
            finishedProcess = true;
            deskPointController.currentCount -= 1;
        }
        //If NPC arrived at the stop on his/her way to the voting place
        else if (votingPlace != null && IsAt(votingPlaceStop) && !arrivedAtVoteStop)
        {
            //Debug.Log(gameObject.name + "4");
            arrivedAtVoteStop = true;
        }
        //If the NPC arrived at voting place, is ready to move to chest after voting
        else if (votingPlace != null && IsAt(votingPlace) && !votingProcessEnded && !arrivedAtVotePlace && chestPointController.isEmpty)
        {
            Debug.Log(gameObject.name + "5");
            arrivedAtVotePlace = true;

            if (votingPlace == votePlaceTransform1)
            {
                votePlaceController1.isEmpty = true;
            }
            else
            {
                votePlaceController2.isEmpty = true;
            }
            
            movingToChest = true;
        }
        //NPC's first point on the way to the chest
        else if (votingProcessEnded && IsAt(votingPlaceStop) && !arrivedBackAtVoteStop)
        {
            Debug.Log(gameObject.name + "5.2");
            arrivedBackAtVoteStop = true;
            chestPointController.isEmpty = false;
        }
        //NPC arrives at the chest in order to vote
        else if (votingProcessEnded && arrivedBackAtVoteStop && IsAt(chestTransform) && !arrivedAtChest)
        {
            Debug.Log(gameObject.name + "6");
            arrivedAtChest = true;
        }
        else if (IsAt(chestTransform) && deskPointController.isEmpty && arrivedAtChest)
        {
            deskPointController.isEmpty = false;
            movingBackToDesk = true;
        }
        else if (IsAt(deskTransform) && arrivedAtChest && !arrivedBackAtDesk)
        {
            Debug.Log(gameObject.name + "7");
            arrivedBackAtDesk = true;
        }
        else if (signedPaper && !finishedProcess)
        {
            deskPointController.isEmpty = true;
            finishedProcess = true;
            Invoke("DecreasePeopleCount", 2f);
        }
        //Signing is already controlled by the method SignPaper
    }
    
    private void MoveTowardsDesk()
    {
        //Debug.Log(gameObject.name + "MoveTowardsDesk");
        targetTransform = deskTransform;
        Move(deskTransform);
        RotateToTarget();
    }

    private void TalkToPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            talkedToPlayer = true;
            permittedToVote = true; //For now
        }
    }

    private void MoveTowardsVotingDestination()
    {
        Debug.Log("MoveTowardsVotingDestination");
        //If the place that the NPC will vote is decided
        if (votingPlace != null)
        {
            Debug.Log("voting place null degil");
            if (!arrivedAtVoteStop)
            {
                targetTransform = votingPlaceStop;
                Move(votingPlaceStop);
                RotateToTarget();
            }
            else
            {
                targetTransform = votingPlace;
                Move(votingPlace);
                RotateToTarget();
            }
        }
    }

    private void Vote()
    {
        float votingPeriod = Random.Range(4f, 12f);
        Invoke("EndVotingProcess", votingPeriod);
        Invoke("PlayVotingSound", (votingPeriod-2f));
    }

    private void EndVotingProcess()
    {
        //Debug.Log(gameObject.name + " end voting process");
        votingProcessEnded = true;

        chestPointController.isEmpty = false;
    }

    //Voting sound is played
    private void PlayVotingSound()
    {

    }

    private void MoveTowardsChest()
    {
        if (!arrivedBackAtVoteStop)
        {
            targetTransform = votingPlaceStop;
            Move(targetTransform);
            RotateToTarget();
        }
        else
        {
            targetTransform = chestTransform;
            Move(targetTransform);
            RotateToTarget();
        }
    }

    private void PutVoteInChest()
    {
        Debug.Log("PutVoteInChest");
        //Animation to put person's vote in the chest
        Invoke("SetPutVoteTrue", 3f);
    }

    private void SetPutVoteTrue()
    {
        putVote = true;
        chestPointController.isEmpty = true;
    }

    //For now, signs the paper when we press I
    private void SignPaper()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            signedPaper = true;
        }
    }

    void GetOutOfTheClassroom()
    {
        targetTransform = endTransform;
        Move(targetTransform);
        RotateToTarget();
    }

    private void DecreasePeopleCount()
    {
        deskPointController.currentCount -= 1;
    }

    private void Move(Transform targetTransform) //Moves to the current target position and handles the animation of walking
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
        HandleWalking();
    }

    private void HandleWalking() //Tackles the animations depending on arrival at the target position
    {
        //Debug.Log("HandleWalking");
        //Debug.Log("Transform:" + transform.position + " and Target Transform:" + targetTransform.position);
        if (IsAt(targetTransform))
        {
            //Debug.Log("They are in same position");
            OnWalkFinished();
        }
        else
        {
            OnWalkStarted();
        } 
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

    private void RotateToTarget() //Looks at the current target and rotates towards it
    {
        //animator.SetBool("isTurning", true);

        Vector3 directionToTarget = targetTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
    }

    //If the current position of the NPC is same as the given place
    private bool IsAt(Transform place)
    {
        Debug.Log(place.name + "'de mi? " + (transform.position == place.position));
        return transform.position == place.position;
    }
}