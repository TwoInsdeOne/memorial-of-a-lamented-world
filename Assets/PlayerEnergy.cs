using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public float maxEnergy;
    public float energy;
    public Slider energyBar;
    public float energyRecoverRate;
    public float energyShow;
    public GameObject energyLightUpFX;
    
    // Start is called before the first frame update
    void Start()
    {
        energy = maxEnergy;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(energyShow > energy)
        {
            energyShow -= Mathf.Abs(energyShow - energy);
        }else if(energyBar.value < energy)
        {
            energyShow += Mathf.Abs(energyShow - energy);
        }
        if(Mathf.Abs(energyShow - energy) < 0.005f)
        {
            energyShow = energy;
        }
        if(energy < maxEnergy)
        {
            energy += energyRecoverRate * Time.deltaTime;
            if(energy> maxEnergy)
            {
                energy = maxEnergy;
            }
            energyBar.value = energyShow / maxEnergy;
        }
    }
    public void TakeEnergy(float amount)
    {
        energy -= amount;
        if(energy < 0) energy = 0;

    }
    public void AddEnergy(float amount)
    {
        GameObject elufx = Instantiate(energyLightUpFX);
        elufx.transform.parent = transform;
        elufx.transform.localPosition = Vector3.zero;
        energy += amount;
        if (energy > maxEnergy) energy = maxEnergy;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnergyBubble")
        {
            EnergyBubble eb = collision.gameObject.GetComponent<EnergyBubble>();
            if (!eb.popped)
            {
                AddEnergy(eb.energyAmount);
                eb.Pop();
            }
            
        }
    }
}
