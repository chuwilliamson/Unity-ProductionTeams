using System.Collections.Generic;
using UnityEngine;

public class BackpackRemovalBehaviour : MonoBehaviour
{
    public GameObject ItemPrefab;
    public List<Item> items;
    public float timer = 5f;

    void Update()
    {
        if (timer <= 0)
        {
            Remove(items[items.Count - 1]);
            timer = 5;
            return;
        }

        timer -= Time.deltaTime;
    }

    public void Remove(Item item)
    {
        if (items.Count <= 0)
            return;

        items.Remove(item);
        
        ItemPrefab.GetComponent<ItemBehaviour>().Create(item);
    }
}