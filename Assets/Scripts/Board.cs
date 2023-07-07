using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    bool noTarget = true;
    Quaternion qTo;
    public float speed = 1.25f;
    public float rotateSpeed = 3.0f;
    public float timer = 0.0f;
    public float amount = 30f;
    // Start is called before the first frame update
    public void Start()
    {
        qTo = Quaternion.Euler(new Vector3(Random.Range(-amount, amount), 0f, Random.Range(-amount, amount)));

    }

    public void Update()
    {

        timer += Time.deltaTime;

        if (noTarget == true)
        {//when not targeting hero	 
            if (timer > 2)
            { // timer resets at 2, allowing .5 s to do the rotating
                qTo = Quaternion.Euler(new Vector3(Random.Range(-amount, amount), 0f, Random.Range(-amount, amount)));
                timer = 0.0f;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotateSpeed);
        }
    }
}
