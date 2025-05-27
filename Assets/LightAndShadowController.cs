using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAndShadowController : MonoBehaviour
{
    public Transform lightsource;
    public int current_frame;
    public SpriteRenderer coreSL;
    public SpriteRenderer bodySL;
    public List<Sprite> coreSL_list;
    public List<Sprite> bodySL_list;
    public float angleToLightSource;
    public PlayerMovements playerMovements;
    public Transform lights;
    public float maxDistanceForSL;
    // Start is called before the first frame update
    void Start()
    {
        coreSL_list = new List<Sprite>();

        string corename = "Sprites/caladriusCoreSL/caladrius_";
        string bodyname = "Sprites/caladriusBodySL/bodyL&S_";
        int i = 360;
        while(i >= 1)
        {
            coreSL_list.Add(Resources.Load<Sprite>(corename + string.Format("{0:D5}", i) ));
            bodySL_list.Add(Resources.Load<Sprite>(bodyname + string.Format("{0:D5}", i)));
            i--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lightsource = ClosestLight();
        float distance = (transform.position - lightsource.position).sqrMagnitude;
        float newAlpha = 1 - Mathf.Min(distance, maxDistanceForSL) / maxDistanceForSL;
        coreSL.color = new Color(1, 1, 1, newAlpha );
        bodySL.color = new Color(1, 1, 1, newAlpha);
        angleToLightSource = Mathf.Atan2(transform.position.y - lightsource.position.y, transform.position.x - lightsource.position.x);
        
        current_frame = ConvertAngle(angleToLightSource + Mathf.PI - Mathf.PI/2);
        if (playerMovements.Flipped() == -1)
        {
            current_frame = 359 - ConvertAngle(angleToLightSource + Mathf.PI - Mathf.PI / 2);
        }
        coreSL.sprite = coreSL_list[current_frame];
        bodySL.sprite = bodySL_list[current_frame];


    }
    public int ConvertAngle(float angle)
    {
        float angle2 = angle;
        if (angle2 < 0)
        {
            angle2 += Mathf.PI * 2;
        }
        if(angle2 > Mathf.PI * 2)
        {
            angle2 -= Mathf.PI * 2;
        }
        int angle3 = Mathf.FloorToInt(Mathf.Rad2Deg * angle2);
        if(angle3 == 360)
        {
            angle3 = 0;
        }
        return angle3;
    }
    public Transform ClosestLight()
    {
        int closest = 0;
        float distance = (transform.position - lights.GetChild(closest).position).sqrMagnitude;
        for (int i = 0; i < lights.childCount; i++)
        {
            if( (transform.position - lights.GetChild(i).position).sqrMagnitude < distance)
            {
                distance = (transform.position - lights.GetChild(i).position).sqrMagnitude;
                closest = i;
            }
        }
        return lights.GetChild(closest);
    }

}
