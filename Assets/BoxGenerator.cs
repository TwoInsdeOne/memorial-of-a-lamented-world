using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public GameObject thing;
    public float interval;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            GameObject t = Instantiate(thing);
            t.transform.parent = transform;
            t.transform.position = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y), 0);
            timer = interval;
        }
    }
}
