using UnityEngine;

[CreateAssetMenu(menuName = "Items/Potions/HealthPotions")]
public class HealthPotion : Potion
{
    public bool consumed;
    public Modifier modifier;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        modifier = Instantiate(modifier);
        modifier.Initialize(obj);
    }

    public override void Execute()
    {
        Consume(_owner);
    }

    public override void Consume(GameObject owner)
    {
        if (consumed) return;
        consumed = true;
    }
}