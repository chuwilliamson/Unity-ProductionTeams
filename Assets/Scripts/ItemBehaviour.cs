using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public Item item;
    
    public void Create(Item value)
    {
        item = Instantiate(value);
        Instantiate(this);
    }
}
