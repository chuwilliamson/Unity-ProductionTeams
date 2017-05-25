using System;
using UnityEngine;

/// <summary>
///     The stat modifier class.
///     Scriptable Object base class for modifier scriptable objects.
/// </summary>
[CreateAssetMenu(fileName = "StatModifier", menuName = "Modifiers/StatModifiers", order = 1)]
public class StatModifier : Modifier
{
    public override void Initialize(GameObject go)
    {
        throw new NotImplementedException();
    }
}