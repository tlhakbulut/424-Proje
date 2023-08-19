using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform teleportPosition;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    public void Teleport()
    {
        player.transform.position = teleportPosition.position;
        player.transform.localScale = new Vector3(1, 2.5f, 1f);
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.playerHeight = 5f;
    }

    
}
