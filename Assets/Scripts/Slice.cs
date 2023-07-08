using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{
    public float sliceTime;
    public float spawnTime;
    public bool slashed;
    private Renderer r;
    Animator animator;
    public GameObject line;

    public AudioSource audioS;
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
        slashed = false;
        r = gameObject.GetComponentInChildren<Renderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!slashed)
        {
            r.material.color = new Color(Time.time - (spawnTime + sliceTime / Time.time), 0.2f, 0.2f, Time.time - (spawnTime + sliceTime / Time.time));
            if (Time.time > spawnTime + sliceTime)
            {
                slashed = true;
                animator.Play("katana_slice");
                audioS.clip = clips[Random.Range(0, 1)];
                audioS.Play();

                Destroy(line);
                Destroy(gameObject, 0.3f);
            }
        }

    }
}
