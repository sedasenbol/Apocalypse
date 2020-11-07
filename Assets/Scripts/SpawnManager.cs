using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab = null;
    [SerializeField]
    private GameObject blockContainer = null;
    [SerializeField]
    private GameObject gatePrefab = null;
    [SerializeField]
    private GameObject gateContainer = null;
    [SerializeField]
    private GameObject wallOfFirePrefab = null;
    [SerializeField]
    private GameObject wallOfFireContainer = null;
    [SerializeField]
    private GameObject meteorContainer = null;
    [SerializeField]
    private GameObject meteorPrefab = null;
    private readonly float enemyRandomizer = 1.0f;
    private readonly float wallOfFireRandomizer = 1.0f;
    private readonly float blockRandomizer = 1.0f;
    private readonly float meteorRandomizer = 1.0f;
    private readonly float xPosition = 2.5f;
    [SerializeField]
    private GameObject demonContainer = null;
    [SerializeField]
    private GameObject demonPrefab = null;
    private bool stopSpawning = false;
    private GameObject player;
    private float blockSpawningSwitch;
    private float gateCount = 0f;
    private float gateLength = 20f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnWallOfFireRoutine());
        StartCoroutine(SpawnBlockRoutine2());
        StartCoroutine(SpawnMeteorRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        blockSpawningSwitch = UnityEngine.Random.Range(0f, 10f);
        SpawnGateRoutine();
        if (stopSpawning)
        {
            StopAllCoroutines();
        }
    }
    public void StopSpawning()
    {
        stopSpawning = true;
    }

    private void SpawnGateRoutine()
    {
        if (player.transform.position.z >= gateCount * gateLength - 1  && stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(10, 0, gateCount * gateLength + 120);
            GameObject newGate = Instantiate(gatePrefab, posToSpawn, Quaternion.Euler(0, -269.966f, 0));
            if (newGate != null)
            {
                newGate.transform.parent = gateContainer.transform;
            }
            gateCount++;
        }
    }


    private IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(enemyRandomizer);
            Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition, 0, player.transform.position.z + 200);
            GameObject newDemon = Instantiate(demonPrefab, posToSpawn, Quaternion.Euler(0, 180, 0));
            if (newDemon != null)
            {
                newDemon.transform.parent = demonContainer.transform;
            }
        }

    }
    private IEnumerator SpawnWallOfFireRoutine()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(wallOfFireRandomizer);
            Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition + 1.5f, 5, player.transform.position.z + 200);
            GameObject newWallOfFire = Instantiate(wallOfFirePrefab, posToSpawn, Quaternion.Euler(0, 0, 90));
            if (newWallOfFire != null)
            {
                newWallOfFire.transform.parent = wallOfFireContainer.transform;
            }
        }
    }

    private IEnumerator SpawnMeteorRoutine()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(meteorRandomizer);
            Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition, 4.19f, player.transform.position.z + 200);
            GameObject newMeteor = Instantiate(meteorPrefab, posToSpawn, Quaternion.Euler(0, 0, 0));
            if (newMeteor != null)
            {
                newMeteor.transform.parent = meteorContainer.transform;
            }
        }
    }

    private IEnumerator SpawnBlockRoutine2()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(blockRandomizer);
            if (blockSpawningSwitch < 5f)
            {
                Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition, 0.2f, player.transform.position.z + 200);
                GameObject newBlock = Instantiate(blockPrefab, posToSpawn, Quaternion.Euler(0, 0, 0));
                if (newBlock != null)
                {
                    newBlock.transform.parent = blockContainer.transform;
                }
            }
            /*else
            {
                Vector3 posToSpawn1 = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition, 0.2f, player.transform.position.z + 200);
                Vector3 posToSpawn2 = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition, -0.2f, player.transform.position.z + 200);
                GameObject newBlock1 = Instantiate(blockPrefab, posToSpawn1, Quaternion.Euler(0, 0, 0));
                GameObject newBlock2 = Instantiate(blockPrefab, posToSpawn2, Quaternion.Euler(0, 0, 0));

                if (newBlock1 != null)
                {
                    newBlock1.transform.parent = blockContainer.transform;
                }
                if (newBlock2 != null)
                {
                    newBlock2.transform.parent = blockContainer.transform;
                }

            }*/

        }
    }
    private IEnumerator SpawnBlockRoutine()
    {
        while (stopSpawning == false)
        {
            yield return new WaitForSeconds(blockRandomizer);
            if (blockSpawningSwitch < 5f)
            {
                Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition + 5f, -4.89f, player.transform.position.z + 200);
                GameObject newBlock = Instantiate(blockPrefab, posToSpawn, Quaternion.Euler(0, 90, 0));
                if (newBlock != null)
                {
                    newBlock.transform.parent = blockContainer.transform;
                }
            }
            else
            {
                Vector3 posToSpawn1 = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition + 5f, -4.89f, player.transform.position.z + 200);
                Vector3 posToSpawn2 = new Vector3(UnityEngine.Random.Range(-1, 2) * xPosition + 5f, -4.89f, player.transform.position.z + 200);
                GameObject newBlock1 = Instantiate(blockPrefab, posToSpawn1, Quaternion.Euler(0, 90, 0));
                GameObject newBlock2 = Instantiate(blockPrefab, posToSpawn2, Quaternion.Euler(0, 90, 0));

                if (newBlock1 != null)
                {
                    newBlock1.transform.parent = blockContainer.transform;
                }
                if (newBlock2 != null)
                {
                    newBlock2.transform.parent = blockContainer.transform;
                }

            }

        }
    }
}
