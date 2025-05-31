using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovements : MonoBehaviour
{
    public PlayerActions playerActions;
    private Rigidbody2D rb;
    public Transform visuals;
    public Vector2 direction;
    public float speed;
    public float springStrength;
    public CapsuleCollider2D capsuleCollider;
    public float targetAngle;
    private LightAndShadowController lightAndShadowController;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new PlayerActions();
        playerActions.Movements.Enable();
        rb = GetComponent<Rigidbody2D>();
        lightAndShadowController = GetComponent<LightAndShadowController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        direction = playerActions.Movements.Move.ReadValue<Vector2>().normalized;
        rb.AddForce(direction*speed, ForceMode2D.Impulse);
        if(direction.x < 0)
        {
            visuals.localScale = new Vector3(-1, 1, 0);
            capsuleCollider.offset = new Vector2 (0.37f, 0);
            targetAngle = 20;
            lightAndShadowController.flipped = true;
        }else if(direction.x > 0)
        {
            visuals.localScale = new Vector3(1, 1, 0);
            capsuleCollider.offset = new Vector2(-0.37f, 0);
            targetAngle = -20;
            lightAndShadowController.flipped = false;
        } else
        {
            targetAngle = 0;
        }
        float angleError = Mathf.DeltaAngle(rb.rotation, targetAngle);
        float torque = angleError * springStrength - rb.angularVelocity;
        rb.AddTorque(torque);
    }
    public int Flipped()
    {
        return Mathf.FloorToInt(visuals.localScale.x);
    }
}
