
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private bool gameOver = false;
    public Transform target;
    public float smoothSpeed = Mathf.Pow(Mathf.Epsilon,Mathf.Epsilon);
    private Vector3 offset = new Vector3(0f,4f,-4f);

    private void Start()
    {
        transform.position = new Vector3(0,10,-50);
    }
    private void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector3 desiredPosition = new Vector3(0, 0.3f, target.position.z) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 5f*Time.deltaTime);
            transform.position = smoothedPosition;
            //transform.LookAt(new Vector3(0,3,target.position.z+999));
        }

        else
        {
            transform.LookAt(target);

            /*
            bool rotating = false;
            //offset.z = -offset.z;
            //değiştirilecek
            //Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, target.position.z) + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            //transform.position = smoothedPosition;
            if (rotating ==false)
            {
                //transform.position = new Vector3(transform.position.x, offset.y, 5);
            }
            transform.RotateAround(target.position, target.up, 20 * Time.deltaTime);
            rotating = true;
        }
        */
        }
    }
    public void LookAtPlayer()
    {
        Debug.Log("Gameover was called");
        gameOver = true;
    }
}
