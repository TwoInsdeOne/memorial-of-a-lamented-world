using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLock : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxBollider;
    public int hitPoints;
    public void OpenPadLock()
    {
        animator.SetTrigger("open");
        boxBollider.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            hitPoints -= collision.gameObject.GetComponent<BulletControler>().damage;
            if(hitPoints < 0)
            {
                OpenPadLock();
            }
        }
    }

}
