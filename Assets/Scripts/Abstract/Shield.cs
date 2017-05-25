using UnityEngine;

public abstract class Shield : Weapon, IBlockable
{
    public float ShieldGrowth;

    public abstract void Block(GameObject blockedObject);
    public abstract void StopBlock();
}


public interface IBlockable
{
    void Block(GameObject blockedObject);

    void StopBlock();
}