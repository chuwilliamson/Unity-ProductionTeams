using System;
using UnityEngine;

[Serializable]
public abstract class Item : ScriptableObject, IExecutable
{
    protected bool _initialized;
    protected int _itemID;
    public string _itemName;

    /// <summary>
    ///     the GameObject responsible for this instance object
    /// </summary>
    protected GameObject _owner;

    public string DisplayName;
    public Sprite ItemSprite;

    public abstract void Execute();

    public virtual void SetOwner(CharacterBehaviour cb)
    {
        _owner = cb.gameObject;
    }

    /// <summary>
    ///     initialize this object with an owner
    /// </summary>
    /// <param name="obj"></param>
    public virtual void Initialize(GameObject obj)
    {
        if (_initialized)
        {
            Debug.LogWarning("attempting to initialize an item that has already been initialized");
            return;
        }
        _itemName = GetType().ToString();
        _itemID = GetHashCode();
        if (DisplayName == "")
            DisplayName = _itemName;
        _owner = obj;
    }
}

[Serializable]
public abstract class Equipment : Item, IEquippable
{
    public virtual void Equip()
    {
    }

    public virtual void UnEquip()
    {
    }

    void OnEnable()
    {
        _itemName = GetType().ToString();
    }
}

[Serializable]
public abstract class Potion : Equipment, IConsumable
{
    public abstract void Consume(GameObject owner);
}

[Serializable]
public abstract class Weapon : Equipment
{
    public int Damage;
}

[Serializable]
public abstract class Armor : Equipment
{
    public int ArmorRating;
}

#region Interfaces

public interface IExecutable
{
    void Execute();
}

public interface IShootable
{
    void Shoot(GameObject owner);
}

public interface IConsumable
{
    void Consume(GameObject owner);
}

public interface IEquippable
{
    void Equip();
    void UnEquip();
}

public interface ISwingable
{
    void Swing();
}

#endregion