using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Vector2 direction;
    public float speed;
    public ParticleSystem ps;
    public Animator animator;
    public CircleCollider2D circleCollider;
    public GameObject bulletExplosionFX;
    public int damage;
    public float energyCost;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Shot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shot()
    {
        Debug.Log(direction);
        rigidBody.AddForce(direction * speed, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject beFX = Instantiate(bulletExplosionFX);
        beFX.transform.position = transform.position;
        Destroy(rigidBody);
        Destroy(circleCollider);
        ParticleSystem.EmissionModule emi = ps.emission;
        emi.rateOverTime = 0;
        animator.SetTrigger("destroy");
        Destroy(gameObject, 1);

    }
}
