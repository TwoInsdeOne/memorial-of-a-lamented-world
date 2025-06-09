using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBubble : MonoBehaviour
{
    public float energyAmount;
    public Animator ani;
    public bool popped;

    public void Pop()
    {
        ani.SetTrigger("Pop");
        popped = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bubble Ceil")
        {
            Pop();
        }
    }
}
