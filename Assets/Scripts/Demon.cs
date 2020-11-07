using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private readonly float moveForwardSpeed = 20f;
    private bool stopMoving = false;
    [SerializeField]
    private Transform target;
    public Vector3 offset;
    private CharacterController controller;
    private bool blockCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        anim.Play("Run");
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        //OnTheAir();
    }
    public void StopMoving()
    {
        Debug.Log("Entered");
        stopMoving = true;
        //anim.Play("Idle");
    }
    private void MoveForward()
    {
        if (stopMoving == false)
        {
            transform.Translate(new Vector3(0, 0, 1) * moveForwardSpeed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Rotating");
            //transform.rotation = Quaternion.RotateTowards(transform.rotation,player.transform.rotation,Time.deltaTime);
            transform.LookAt(player.transform.position);
        }

        if (transform.position.z < player.transform.position.z - 20f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            blockCollision = true;
            anim.Play("Jump");
            StartCoroutine("FixOnTheBlockPosition");
            anim.Play("Run");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block")
        {
            blockCollision = false;
            anim.Play("Jump");
            StopAllCoroutines();
            StopCoroutine("FixOnTheBlockPosition");
            StopCoroutine(FixOnTheBlockPosition());
            anim.Play("Run");
            transform.position = new Vector3(transform.position.x, 0.26076f, transform.position.z);
        }
    }
    private IEnumerator FixOnTheBlockPosition()
    {
        while (blockCollision)
        {
            transform.position = new Vector3(transform.position.x, 2.160327f, transform.position.z);
            yield return new WaitForEndOfFrame();
        }        
    }
    private void OnTheAir()
    {
        if (controller.isGrounded==false)
        {
            transform.position=new Vector3(transform.position.x, 0.26076f,transform.position.z);
            anim.Play("Run");
        }
    }
}
