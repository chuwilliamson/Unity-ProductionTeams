using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Items/BackPack")]
public class BackPack : ScriptableObject
{
    [SerializeField] public List<Item> Items;
}