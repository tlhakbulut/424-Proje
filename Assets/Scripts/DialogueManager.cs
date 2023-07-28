using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;

    float distance;
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;

    //public Vector3 targetPosition;
    public Transform targetPosition;
    private bool shouldMove = false;


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);// only activate the ui when talking to npcs
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance <= 2.5f)
        {

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.E) && isTalking == false)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(KeyCode.E) && isTalking == true)
            {
                EndDialogue();
            }

            if (curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                //Debug.Log("333333333");
                playerResponse.text = npc.playerDialogue[0];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("3A");
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                //Debug.Log("4444444444");
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("4A");
                    npcDialogueBox.text = npc.dialogue[2];
                    shouldMove = true;
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                //Debug.Log("55555555");
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("5A");
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }

        }
        else
        {
            if (isTalking == true)
            {
                EndDialogue();
            }
        }
    }


    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true); //add dialogue ui to scene
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
        Debug.Log("target x: "+targetPosition.position.x);
        Debug.Log("target y: " + targetPosition.position.y);
        Debug.Log("target z: " + targetPosition.position.z);
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);

    }

    void Update()
    {

        if (shouldMove)
        {
            Debug.Log("MOVE");
            Vector3 direction = targetPosition.position - this.transform.position;
            float distance = direction.magnitude;
            dialogueUI.SetActive(false);

            if (distance > 0.1f)
            {
                Debug.Log("WALKING");
                direction.Normalize();
                this.transform.position += direction * 10f * Time.deltaTime;
            }
            else
            {
                StartCoroutine(WaitAndResumeMovement());

            }
        }

    }
    IEnumerator WaitAndResumeMovement()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        // come back to desk
        shouldMove = false;
    }
}
