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
    private Vector2 previousPosition;
    public Vector2 velocity;

    private float targetAngle;
    private float springStrength;

    public Transform aim;
    public Transform squareAim;
    public GameObject fireball;

    public bool seeingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        currentTurn = 1;
        rb = GetComponent<Rigidbody2D>();
        timer = 0;
        player = GameObject.Find("Caladrius").transform;
        maxSpeed = aiPath.maxSpeed;
        previousPosition = transform.position;
        springStrength = 13;

    }
    private void Update()
    {
        velocity = new Vector2 (aiPath.velocity.x, aiPath.velocity.y);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (currentTurn == 2)
        {
            squareAim.localEulerAngles = new Vector3(0, 0, timer * Mathf.Rad2Deg * 2);
            if(timer >= secondTurnDuration && seeingPlayer)
            {
                Shot();
                timer = 0;
            }
        } else if (aiPath.reachedDestination)
        {
            currentTurn = 2;
            timer = 0;
        } else if(timer == 0)
        {
            currentTurn = 1;
        }
        if (seeingPlayer)
        {
            aiPath.destination = player.position;
        }
        
        
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x);
        aim.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * angle);
        float facing = transform.position.x - player.position.x;
        if(facing > 0)
        {
            ls.flipped = true;
            visual.localScale = new Vector3(-1, 1, 0);
            targetAngle = 20;
        }else if(facing < 0)
        {
            ls.flipped = false;
            visual.localScale = new Vector3(1, 1, 0);
            targetAngle = -20;
        }

        float angleError = Mathf.DeltaAngle(rb.rotation, targetAngle);
        float torque = angleError * springStrength - rb.angularVelocity;
        rb.AddTorque(torque);
    }

    public void Shot()
    {
        GameObject fb = Instantiate(fireball);
        fb.transform.position = squareAim.position;
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        fb.GetComponent<BulletControler>().direction = direction.normalized ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            seeingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            seeingPlayer = false;
        }
    }
}
