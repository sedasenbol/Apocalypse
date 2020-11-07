using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        ///Physics.gravity = new Vector3(0, 0, 0);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DestroyThisObject();
    }
    private void DestroyThisObject()
    {
        if (transform.position.z < player.transform.position.z - 60f)
        {
            Destroy(this.gameObject);
        }
    }
}
