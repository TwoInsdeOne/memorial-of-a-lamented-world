using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public List<GameObject> items;
    public List<float> chances;

    public void Drop()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (Random.Range(0f, 1f) < chances[i])
            {
                GameObject item = Instantiate(items[i]);
                float angle = Random.Range(0f, Mathf.PI * 2);
                item.transform.position = transform.position + new Vector3(Mathf.Cos(angle) * 2f, Mathf.Sin(angle) * 2f, 0);
            }
        }
    }
}
