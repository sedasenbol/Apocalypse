using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject demon;
    private SpawnManager spawnManager;
    [SerializeField]
    private GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GameOver()
    {
        if (spawnManager != null)
        {
            Debug.Log("1");
            spawnManager.StopSpawning();
        }
        if (demon != null)
        {
            Debug.Log("2");
            demon.GetComponent<Demon>().StopMoving();
        }
        if (mainCamera != null)
        {
            Debug.Log("3");
            mainCamera.GetComponent<CameraFollow>().LookAtPlayer();
        }
    }
}
