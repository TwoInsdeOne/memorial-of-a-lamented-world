using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Amulet,
        Gun,
        Armor,
        Artefact,
        Symbol,
        Key
    }
    public ItemType type;
    public string itemName;
    public Sprite icon;
    public string description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
