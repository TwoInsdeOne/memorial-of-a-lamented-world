using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGeneretor : MonoBehaviour
{
    public GameObject obj;
    public int prewarm;
    public Vector2 centerOfBox;
    public Vector2 dimensions;
    public bool autoGenerate;
    public float interval;
    private float timer;
    public bool doneGeneratingPreWarm;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < prewarm; i++)
        {
            Generate();
            doneGeneratingPreWarm = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doneGeneratingPreWarm && !GetComponent<WaterSurfaceMesh>().populated)
        {
            GetComponent<WaterSurfaceMesh>().PopulateWave();
        }
        if (!autoGenerate)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Generate();
            timer = interval;
        }
    }

    public void Generate()
    {
        GameObject o = Instantiate(obj);
        o.transform.parent = transform;
        float step = dimensions.x / prewarm; //dimensions.x is the lenght of the distribution of pixels, prewarm is the amount of pixels
        o.transform.localPosition = centerOfBox + new Vector2(-dimensions.x/2 + transform.childCount* step, 0);
    }
}
