using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Stats/Stat")]
public class Stat : ScriptableObject
{
    [SerializeField] int _baseValue;

    public string Name;
    public int Value;

    void OnEnable()
    {
        name = name.Contains("(Clone)") ? name.Replace("(Clone)", string.Empty) : name;
        Name = name.ToLower();
        Value = _baseValue;
    }

    public void Apply(Modifier mod)
    {
        if (mod.type == Modifier.ModType.ADD)
            Value += mod.value;

        if (mod.type == Modifier.ModType.MULT)
            Value += _baseValue * mod.value / 10;
    }

    public void Remove(Modifier mod)
    {
        if (mod.type == Modifier.ModType.ADD)
            Value -= mod.value;
        if (mod.type == Modifier.ModType.MULT)
            Value -= _baseValue * mod.value / 10;
    }
}