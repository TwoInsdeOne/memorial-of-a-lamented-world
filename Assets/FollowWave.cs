using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWave : MonoBehaviour
{
    private WaterSurface surface;
    private WaterSurfaceMesh mesh;
    private float x;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        mesh = transform.parent.GetComponent<WaterSurfaceMesh>();
        surface = transform.parent.GetComponent<WaterSurface>();
        x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(x, surface.GetSurfaceY(x), 0);
        if (mesh.populated)
        {
            mesh.wavePoints[id] = transform.position;
        }
        
    }
}
