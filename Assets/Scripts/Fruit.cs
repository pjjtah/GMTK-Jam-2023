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
    public AudioSource audioS;
    public Slicer slicer;

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
            float sca = 2.5f;
            switch (fruitNum)
            {
                case 1:
                    splPos.y = splPos.y - 0.4f;
                    v = new Color(1f, 0.51f, 0.47f);
                    break;
                case 2:
                    splPos.y = splPos.y - 0.2f;
                    v = new Color(0.83f, 1f, 0.81f);
                    sca = 2f;
                    break;
                case 3:
                    splPos.y = splPos.y - 0.1f;
                    v = new Color(1f, 0.79f, 0.43f);
                    sca = 2f;
                    break;
                case 4:
                    splPos.y = splPos.y - 0.02f;
                    v = new Color(1f, 0.35f, 0.35f);
                    sca = 1f;
                    break;
                case 5:
                    splPos.y = splPos.y - 0.02f;
                    v = new Color(0f, 0.76f, 0.2f);
                    sca = 1.5f;
                    break;
            }

            audioS.Play();

            GameObject spl = Instantiate(splatter, splPos,Quaternion.Euler(new Vector3(90f, -other.transform.eulerAngles.y, other.transform.eulerAngles.z)));
            spr = spl.GetComponent<SpriteRenderer>();
            spl.transform.localScale = new Vector3(sca, sca, sca);
            spr.color = v;
            Destroy(spl, 2f);
            slicer.fruits.Remove(gameObject);
            slicer.FruitDestroyed();
            Destroy(spr, 1.1f);
        }

    }

    void clampMovement()
    {
        Vector3 position = transform.position;

        float distance = transform.position.z - Camera.main.transform.position.z;

        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;

        position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
        transform.position = position;
    }
}
