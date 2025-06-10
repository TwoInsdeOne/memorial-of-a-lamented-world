using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int maxHealth;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = (health + 0.00001f) / maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }
    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(collision.gameObject.GetComponent<BulletControler>().damage);
        }
    }
}
