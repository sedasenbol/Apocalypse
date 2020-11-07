using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    bool rolled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rolled == false)
        {
            StartCoroutine(Roll());
        }
    }
    private IEnumerator Roll()
    {
        //Vector3 movement = new Vector3(-1, 0, 0) * 500;
        //rb.AddForce(movement);
        rolled = true;
        for (int i = 1; i < 361; i=i+10)
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
