using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitNum;
    public Collider c;
    public Rigidbody r;
    public MeshCollider first_half;
    public MeshCollider second_half;
    public Renderer first_r;
    public Renderer second_r;
    public bool sliced = false;
    float sliceTime;
    public GameObject splatter;
    private SpriteRenderer spr;

    // Start is called before the first frame update
    void Start()
    {
        sliced = false;
        r.AddForce(new Vector3(Random.Range(1f, 10f), Random.Range(1f, 10f), Random.Range(1f, 10f)));
    }

    // Update is called once per frame
    void Update()
    {
        if (sliced)
        {
            if (Time.time < sliceTime + 1f)
            {
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1f - (Time.time - sliceTime));
                foreach(Material m in first_r.materials)
                {
                    m.color = new Color(first_r.material.color.r, first_r.material.color.g, first_r.material.color.b, 1f - (Time.time - sliceTime));
                }
                foreach (Material m in second_r.materials)
                {
                    m.color = new Color(first_r.material.color.r, first_r.material.color.g, first_r.material.color.b, 1f - (Time.time - sliceTime));
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!sliced)
        {
            c.enabled = false;
            first_half.enabled = true;
            second_half.enabled = true;
            Rigidbody f = first_half.gameObject.AddComponent<Rigidbody>();
            Rigidbody s = second_half.gameObject.AddComponent<Rigidbody>();
            f.AddForce(transform.right * 150f);
            s.AddForce(-transform.right * 150f);
            sliced = true;
            sliceTime = Time.time;
            Quaternion q = Quaternion.LookRotation(other.transform.position);
            Vector3 splPos = transform.position;
            Color v = new Color(0.83f, 1f, 0.81f);
            switch (fruitNum)
            {
                case 1:
                    splPos.y = splPos.y - 0.4f;
                    v = new Color(1f, 0.51f, 0.47f);
                    break;
                case 2:
                    splPos.y = splPos.y - 0.2f;
                    v = new Color(0.83f, 1f, 0.81f);
                    break;
                case 3:
                    splPos.y = splPos.y - 0.1f;
                    v = new Color(1f, 0.79f, 0.43f);
                    break;
            }



            GameObject spl = Instantiate(splatter, splPos,Quaternion.Euler(new Vector3(90f, -other.transform.eulerAngles.y, other.transform.eulerAngles.z)));
            spr = spl.GetComponent<SpriteRenderer>();
            spr.color = v;
            Destroy(spr, 1.1f);
        }

    }


}
