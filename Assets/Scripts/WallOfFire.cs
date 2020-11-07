using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOfFire : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DestroyThisObject();
    }
    private void DestroyThisObject()
    {
        if (transform.position.z < player.transform.position.z - 20f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }
}
