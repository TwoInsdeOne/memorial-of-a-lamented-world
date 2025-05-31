using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BertuanBrain : MonoBehaviour
{
    public AIPath aiPath;
    private Rigidbody2D rb;
    public LightAndShadowController ls;
    public float firstTurnDuration;
    public float secondTurnDuration;
    private int currentTurn;
    private float timer;
    private Transform player;
    private float maxSpeed;
    public SpriteRenderer coreSprite;
    public SpriteRenderer shellSprite;
    public Transform visual;
    // Start is called before the first frame update
    void Start()
    {
        currentTurn = 1;
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
        player = GameObject.Find("Caladrius").transform;
        maxSpeed = aiPath.maxSpeed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(currentTurn == 1 && timer > firstTurnDuration)
        {
            aiPath.maxSpeed = 0;
            currentTurn = 2;
            timer = 0;
        }else if(currentTurn == 2 && timer > secondTurnDuration)
        {
            aiPath.maxSpeed = maxSpeed;
            currentTurn = 1;
            timer = 0;
        }
        aiPath.destination = player.position;
        float facing = transform.position.x - player.position.x;
        if(facing > 0)
        {
            ls.flipped = true;
            visual.localScale = new Vector3(-1, 1, 0);
        }else if(facing < 0)
        {
            ls.flipped = false;
            visual.localScale = new Vector3(1, 1, 0);
        }
    }
}
