using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public bool timer;
    public float interval;
    // Start is called before the first frame update
    void Start()
    {
        if (timer)
        {
            Destroy(gameObject, interval);
        }
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
