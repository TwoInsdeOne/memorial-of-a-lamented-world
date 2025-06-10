using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int hpAmount;
    public float epAmount;
    public Animator ani;
    private bool collected;
    public void Collect()
    {
        ani.SetTrigger("collected");
        collected = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !collected)
        {
            collision.gameObject.GetComponent<PlayerEnergy>().AddEnergy(epAmount);
            collision.gameObject.GetComponent<PlayerHealth>().Heal(hpAmount);
            Collect();
        }
    }
}
