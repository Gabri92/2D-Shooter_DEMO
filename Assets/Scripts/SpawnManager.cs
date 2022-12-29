using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Variables
    public GameObject enemy;
    public GameObject player;
    private Vector3 spawnPos;
    private float minPos = 5;
    private float maxPos = 10;
    private float yPos = -2.37f;
    private float spawnStartTime = 1.5f;
    private float spawnRate = 2;
    private float playerPos = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnStartTime, spawnRate);
    }

    // Spawn method
    void Spawn()
    {
        // TODO: Troppo facile falsare il gioco con questo spawn manager
        // Da aggiungere per spawn nemici
        if(player.transform.position.x > playerPos)
        {
            spawnPos = new Vector3(Random.Range(-maxPos, -minPos), yPos, 0);
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
        else if (player.transform.position.x < -playerPos)
        {
            spawnPos = new Vector3(Random.Range(minPos, maxPos), yPos, 0);
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
        else if (player.transform.position.x >= - playerPos && player.transform.position.x <= playerPos)
        {
            int randomVar = Random.Range(0, 2);
            if (randomVar == 0)
            {
                spawnPos = new Vector3(Random.Range(-minPos, -maxPos), yPos, 0);
            }
            else
            {
                spawnPos = new Vector3(Random.Range(minPos, maxPos), yPos, 0);
            }
            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}
