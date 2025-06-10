using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public EnemyDrop drop;
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DeathAnimation();
        }
    }
    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }
    public void DeathAnimation()
    {
        drop.Drop();
        ani.SetTrigger("destroy");
        Destroy(gameObject, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletControler>().damage);
        }
    }
}
