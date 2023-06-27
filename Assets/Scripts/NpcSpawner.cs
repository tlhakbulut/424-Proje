using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public Transform player;

    public float spawnInterval = 6f;

    private void Start()
    {
        InvokeRepeating("SpawnNPC", 0f, spawnInterval);
    }

    private void SpawnNPC()
    {
        Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
    }
}
