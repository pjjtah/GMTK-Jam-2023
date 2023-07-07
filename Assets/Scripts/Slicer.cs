using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    public int level;
    public GameObject slice;
    public GameObject[] fruits;
    public float timer;
    public float sliceInterval;
    public float sliceTime;
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        timer = 0f;
        sliceInterval = 3f;
        sliceTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer + sliceInterval)
        {
            GameObject fruit = fruits[Random.Range(0, fruits.Length)];

            GameObject s = Instantiate(slice, fruit.transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0));
            Slice sl = s.GetComponent<Slice>();
            sl.sliceTime = sliceTime;
            sl.spawnTime = Time.time;
        
            timer = Time.time;
        }
    }
}
