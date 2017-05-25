using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/CombatKnife")]
public class CombatKnife : Weapon
{
    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
    }
}