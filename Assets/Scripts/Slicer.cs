using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    public Player player;
    public int level;
    public GameObject slice;
    public List<GameObject> fruits = new List<GameObject>();
    public GameObject[] fruitPrefabs;
    public float timer;
    public float sliceInterval;
    public float sliceTime;
    public int maxSlices;

    public int slicesLeft;

    public bool Reversed;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer + sliceInterval && fruits.Count > 0 && slicesLeft > 0)
        {
            GameObject fruit = fruits[Random.Range(0, fruits.Count)];
            Vector3 pp = fruit.transform.position;
            pp.y = -3.3f;
            if (Reversed)
            {
                pp.x += Random.Range(-0.5f, -0.5f);
                pp.z += Random.Range(-0.5f, -0.5f);
            }
            for(int i = 0; i < maxSlices; i++)
            {
                GameObject s = Instantiate(slice, pp, Quaternion.Euler(0, Random.Range(0, 360), 0));
                Slice sl = s.GetComponent<Slice>();
                sl.sliceTime = sliceTime;
                sl.spawnTime = Time.time;
            }

        
            timer = Time.time;
            slicesLeft -= 1;
        }
    }

    public void SpawnFruits(int[] amounts)
    {
        int fr = 0;
        foreach(int i in amounts)
        {
            for(int j = 0; j<i; j++)
            {
                Vector3 randomPos = new Vector3(
                    Random.Range(-3.5f, 7.1f),
                    -2.53f,
                    Random.Range(-3.9f, 1.7f));
                GameObject fi = Instantiate(fruitPrefabs[fr], randomPos, Random.rotation);
                fi.GetComponent<Fruit>().slicer = this;
                fruits.Add(fi);
            }
            fr += 1;
        }
    }

    public void FruitDestroyed()
    {
        if (!Reversed)
        {
            player.LoseHP();
        }
    }

    public void EndSlicer()
    {
        foreach(GameObject f in fruits)
        {
            Destroy(f);
        }
        Destroy(this);
    }
}
