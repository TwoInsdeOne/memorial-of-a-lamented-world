using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlAim : MonoBehaviour
{
    public Transform aim;
    public PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = new PlayerActions();
        playerActions.Aiming.Enable();
        playerActions.Aiming.Aim.started += Aim;
    }

    // Update is called once per frame
    void Update()
    {
        float sign = aim.parent.localScale.x;
        Vector2 direction = playerActions.Aiming.Aim.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x)*sign;
            aim.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * angle);
            
        }
        aim.localScale = new Vector3(sign, 1, 1);


    }
    public void Aim(InputAction.CallbackContext context)
    {
        
    }
}
