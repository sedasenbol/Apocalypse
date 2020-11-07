using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class Player : MonoBehaviour
{
    private readonly float epsilon = 2f * Mathf.Exp(-8);//exp -8 di
    private float correctValue;
    private float motionInverseSpeed = 100f;
    private CharacterController controller;
    private Animator anim;
    private readonly float xPosition = 2.5f;
    private float moveForwardSpeed = 16f;
    private GameManager gameManager;
    private readonly float jumpingHeight = 12f;
    private float yVelocity = 0f;
    private bool canDoubleJump = false;
    private readonly float gravity = 1f;
    private bool isFallling = false;
    private float yVelocityDoubleJump = 0f;
    private bool jumpAttempt = false;
    private Rigidbody rb;
    private bool isAlive = true;
    private bool isleaning = false;
    [SerializeField]
    private GameObject meteorExplosionPrefab;
    private bool collidingWithBlock = false;
    private int score = 0;
    private UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        transform.position = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            AddScore();
            XAxisCheck();
            YAxisCheck();
            //MoveLeftRight();
            MoveLeft();
            MoveRight();
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpAttempt = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                StartCoroutine(LeanBack());
            }
        }
    }
    private void AddScore()
    {
        score = (int) transform.position.z * 10;
        if (uIManager != null)
        {
            uIManager.ShowScore(score);
        }
    }
    private void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
        }
    }
    private bool IsEqual(float a, float b)
    {
        if (a >= b - epsilon && a <= b + epsilon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CorrectXPosition()
    {
        if (IsEqual(transform.position.x, 0f))
        {
            correctValue = 0f;
        }
        else if (IsEqual(transform.position.x, xPosition))
        {
            correctValue = xPosition;
        }
        else if (IsEqual(transform.position.x, -xPosition))
        {
            correctValue = -xPosition;
        }
        else
        {
            return;
        }
        transform.position = new Vector3(correctValue, transform.position.y, transform.position.z);
    }

    private void Move()
    {
        Vector3 velocity = new Vector3(0, 0, moveForwardSpeed);
        if (controller.isGrounded == true)
        {
            if (jumpAttempt == true)
            {
                yVelocity = jumpingHeight;
                canDoubleJump = true;
                jumpAttempt = false;
            }
        }
        else
        {
            if (jumpAttempt == true && canDoubleJump == true)
            {
                yVelocityDoubleJump = jumpingHeight;
                canDoubleJump = false;
                jumpAttempt = false;
            }
            yVelocity -= gravity;
        }

        velocity.y = Mathf.Max(yVelocity, yVelocityDoubleJump);
        controller.Move(velocity * 2f * Time.deltaTime);
        //rb.velocity = velocity * 2f * Time.deltaTime;
        yVelocityDoubleJump -= gravity;
    }


    private IEnumerator SlowMotionX(Vector3 motionStep, float motionInverseSpeed)
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentPosition + motionStep * motionInverseSpeed;
        while (IsEqual(transform.position.x, targetPosition.x) == false && isAlive)
        {
            controller.Move(motionStep);
            yield return new WaitForEndOfFrame();
        }
        CorrectXPosition();
    }
    private IEnumerator SlowMotionY(Vector3 motionStep, float motionInverseSpeed)
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = currentPosition + motionStep * motionInverseSpeed;
        while (IsEqual(transform.position.y, targetPosition.y) == false)
        {
            controller.Move(motionStep);
            yield return new WaitForEndOfFrame();
        }
    }
    private void MoveLeftRight()
    {
        if (isAlive && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float x = transform.position.x;
            if (x >= -0.002f && x <= 0.002f)
            {
                //Vector3.Lerp(transform.position,new Vector3(-2.5f,transform.position.y,transform.position.z),Time.deltaTime*moveForwardSpeed);
                StartCoroutine(SlowMotionX(new Vector3((-2.5f - transform.position.x) / motionInverseSpeed,0f, Mathf.Abs(-2.5f - transform.position.x) / motionInverseSpeed), motionInverseSpeed));
            }
            else if (x > 0.002f && x <= 2.6f)
            {
                //Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), Time.deltaTime * moveForwardSpeed);
                StartCoroutine(SlowMotionX(new Vector3((0f - transform.position.x) / motionInverseSpeed, 0f, Mathf.Abs(0f - transform.position.x) / motionInverseSpeed) ,  motionInverseSpeed));
            }
        }
        else if (isAlive && Input.GetKeyDown(KeyCode.RightArrow))
        {
            float x = transform.position.x;
            if (x >= -0.002f && x <= 0.002f)
            {
                //Vector3.Lerp(transform.position, new Vector3(2.5f, transform.position.y, transform.position.z), Time.deltaTime * moveForwardSpeed);
                StartCoroutine(SlowMotionX(new Vector3((2.5f - transform.position.x) / motionInverseSpeed, 0f, Mathf.Abs(2.5f-transform.position.x) / motionInverseSpeed), motionInverseSpeed));
            }
            else if (x < -0.002f && x >= -2.6f)
            {
                //Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), Time.deltaTime * moveForwardSpeed);
                StartCoroutine(SlowMotionX(new Vector3((0f - transform.position.x) / motionInverseSpeed, 0f, Mathf.Abs(0f - transform.position.x) / motionInverseSpeed) , motionInverseSpeed));
            }
        }
    }

    private void MoveLeft()
    {
        motionInverseSpeed = 10f;
        if (isAlive && Input.GetKeyDown(KeyCode.LeftArrow) && (transform.position.x == 2.5f || transform.position.x == 0f))
        {
            Vector3 motionStep = new Vector3(-xPosition / motionInverseSpeed, 0, xPosition / motionInverseSpeed);
            StartCoroutine(SlowMotionX(motionStep, motionInverseSpeed));
        }
    }
    private void MoveRight()
    {
        motionInverseSpeed = 10f;
        if (isAlive && Input.GetKeyDown(KeyCode.RightArrow) && (transform.position.x == -2.5f || transform.position.x == 0f))
        {
            Vector3 motionStep = new Vector3(xPosition / motionInverseSpeed, 0, xPosition / motionInverseSpeed);
            StartCoroutine(SlowMotionX(motionStep, motionInverseSpeed));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Obstacle") || other.CompareTag("Demon"))&& isAlive)
        {
            Debug.Log(other.name);
            PlayerDies();
        }
        else if (other.CompareTag("Block")&& isAlive)
        {
            collidingWithBlock = true;
            Debug.Log("Collision with block at y=" + transform.position.y);
            if (/*controller.isGrounded == false && (transform.position.y >= 0.30f &&*/ transform.position.y <= 1.78f)
            {
                Debug.Log(other.name);
                PlayerDies();
            }
            else if (transform.position.y > 1.78f && isAlive)
            {
                Debug.Log("grounded?"+controller.isGrounded+"y position:"+transform.position.y);
            }
        }
        else if (other.CompareTag("Meteor") && isAlive)
        {
            Debug.Log("meteorla çarpıştı @y=");
            Debug.Log(transform.position.y);
            GameObject newMeteorExplosion = Instantiate(meteorExplosionPrefab,  other.GetComponent<Transform>().position, Quaternion.Euler(0, 0, 0));
            PlayerDies();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            collidingWithBlock = false;
        }
    }

    //private void StayStill()

    private void PlayerDies()
    {
        anim.Play("Dead");
        Debug.Log("Died");
        moveForwardSpeed = 0f;
        if (controller.isGrounded == false && collidingWithBlock==true)
        {
            controller.Move(new Vector3(0,0.26076f-transform.position.y,0));
        }
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        if (uIManager != null)
        {
            uIManager.GameOverText();
        }
        isAlive = false;
        StartCoroutine(SlowMotionY(new Vector3(0, (0.26076f-transform.position.y)/motionInverseSpeed, 0),motionInverseSpeed));
        controller.center = new Vector3(0,0.5f,0);
        controller.height = 0.6f;
        StartCoroutine(LieBack());
        Application.Quit();
    }

    private IEnumerator LieBack()
    {
        float leanBackAngle = -90f;
        for (int i = 1; i < -leanBackAngle+1; i++)
        {
            var rotationVector = new Vector3(-i, 0, 0);
            controller.transform.rotation = Quaternion.Euler(rotationVector);
            yield return new WaitForEndOfFrame();
        }
    }

    private void XAxisCheck()
    {
        float x = transform.position.x;
        if (x > xPosition)
        {
            controller.Move(new Vector3(xPosition, transform.position.y, transform.position.z));
            StopCoroutine("SlowMotionX");
        }
        else if (x < -xPosition)
        {
            controller.Move(new Vector3(-xPosition, transform.position.y, transform.position.z));
            StopCoroutine("SlowMotionX");
        }
        else if((IsEqual(x,0) && x!=0))
        {
           controller.Move(new Vector3(0, transform.position.y, transform.position.z));
        }
        else if(IsEqual(x,xPosition) && x != xPosition)
        {
            controller.Move(new Vector3(xPosition, transform.position.y, transform.position.z));
        }
        else if(IsEqual(x,-xPosition)&&x!=-xPosition)
        {
            controller.Move( new Vector3(-xPosition, transform.position.y, transform.position.z));
        }
    }
    private void YAxisCheck()
    {
        if (isFallling == false && transform.position.y < 0.2 && isleaning==false)
        {
            Debug.LogError("player is not supposed to fall");
            Debug.LogError(controller.isGrounded);
            //transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
        }
    }

    public float pushPower = 2.0F;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }

    private IEnumerator LeanBack()
    {
        isleaning = true;
        float leanBackAngle = -70f;
        controller.center = new Vector3(0, 0.5f, 0);
        controller.height = 1;
        var rotationVector = transform.rotation.eulerAngles;
        for (int i = 15; i < -leanBackAngle; i=i+1)
        {
            rotationVector = new Vector3(-i, 0, 0);
            controller.transform.rotation = Quaternion.Euler(rotationVector);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(leanForward());
    }

    private IEnumerator leanForward()
    {
        float leanBackAngle = -70f;
        var rotationVector = transform.rotation.eulerAngles;
        for (int j = 1; j < -leanBackAngle; j=j+2)
        {
            rotationVector = new Vector3(leanBackAngle + j + 1, 0, 0);
            controller.transform.rotation = Quaternion.Euler(rotationVector);
            if (j == -leanBackAngle - 1)
            {
                isleaning = false;
            }
            controller.center = new Vector3(0, 0.5f+0.5f*(j+1)/-leanBackAngle, 0);
            controller.height = 1+1.1f*(j+1)/-leanBackAngle;
            yield return new WaitForEndOfFrame();
        }
    }
}
