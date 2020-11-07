using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private CharacterController controller;
    public Transform target;
    private float speed = 100f;
    public Vector3 offset;
    private GameObject player;
    private bool rolled = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (rolled == false)
        {
            StartCoroutine(Roll());
        }
    }
    void FixedUpdate()
    {
        controller.Move(new Vector3(0, 0, -1) * speed * Time.deltaTime);
        //rb.AddForce(new Vector3(0,0,5f));
    }
    private void Update()
    {
        //this.gameObject.transform.Translate(Vector3.back *  5f * Time.deltaTime);

        if (transform.position.z < player.transform.position.z - 20f)
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator Roll()
    {
        rolled = true;
        for (int i = 1; i < 361; i = i + 10)
        {
            var rotationVector = new Vector3(-i, 0, 0);
            transform.rotation = Quaternion.Euler(rotationVector);
            yield return new WaitForEndOfFrame();
            if (i == 351)
            {
                rolled = false;
            }

        }
    }

}
