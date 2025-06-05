using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    private float angle;
    public Transform parentRef;
    // Start is called before the first frame update
    void Start()
    {
        if (parentRef == null)
        {
            parentRef = transform.parent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float sign = parentRef.localScale.x;
        transform.localScale = new Vector3(transform.localScale.x*sign, transform.localScale.y, 1);
        angle += Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = new Vector3(0, 0, angle*sign);
    }
}
