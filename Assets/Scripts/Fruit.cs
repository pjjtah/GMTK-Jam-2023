using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Collider c;
    public Rigidbody r;
    public MeshCollider first_half;
    public MeshCollider second_half;
    public Renderer first_r;
    public Renderer second_r;
    public bool sliced = false;
    float sliceTime;


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
        }

    }


}
