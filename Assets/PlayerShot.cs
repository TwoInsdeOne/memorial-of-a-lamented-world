using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShot : MonoBehaviour
{
    public PlayerActions playerActions;
    public List<GameObject> bullets;
    public int currentBullet;
    public Transform aim;
    public PlayerEnergy playerEnergy;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = new PlayerActions();
        playerActions.Shotting.Enable();
        playerActions.Shotting.Shot.started += Shot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shot(InputAction.CallbackContext context)
    {
        float cost = bullets[currentBullet].GetComponent<BulletControler>().energyCost;
        if (playerEnergy.energy > cost)
        {
            GameObject b = Instantiate(bullets[currentBullet]);
            b.transform.position = aim.position;
            Vector2 direction = new Vector2(aim.position.x - transform.position.x, aim.position.y - transform.position.y);
            b.GetComponent<BulletControler>().direction = direction.normalized;
            playerEnergy.TakeEnergy(cost);
        }
        
    }
    
}
