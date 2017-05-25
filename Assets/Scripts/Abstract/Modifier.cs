using UnityEngine;

public abstract class Modifier : ScriptableObject
{
    public enum ModType
    {
        ADD,
        MULT
    }

    /// <summary>
    ///     The effected stat type to modify.
    /// </summary>
    public Stat stat;

    public ModType type;

    public int value;

    /// <summary>
    ///     The initialize function.
    /// </summary>
    /// <param name="go">
    ///     The go.
    /// </param>
    public abstract void Initialize(GameObject go);
}