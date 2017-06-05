using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "Stats/Stats")]
public class Stats : ScriptableObject, IEnumerable<Stat>
{
    public readonly Dictionary<string, Stat> Items = new Dictionary<string, Stat>();
    public readonly Dictionary<int, Modifier> Modifiers = new Dictionary<int, Modifier>();

    public Stat[] StatsArray;

    public Stat this[string element]
    {
        get
        {
            var item = Items.ContainsKey(element) ? Items[element] : null;
            if (item == null)
                Debug.Log("tried to fetch " + element);
            return item;
        }

        set { Items[element] = value; }
    }

    public int Count
    {
        get { return StatsArray.Length; }
    }

    public IEnumerator<Stat> GetEnumerator()
    {
        return Items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Items.Values.GetEnumerator();
    }


    void OnEnable()
    {
        if (StatsArray == null) return;

        foreach (var stat in StatsArray)
        {
            var s = Instantiate(stat);
            AddStat(s);
        }
    }

    public string AddModifier(int id, Modifier mod)
    {
        Modifiers.Add(id, mod);
        var result = string.Format(
            "Add modifier {0} {1} {2}",
            Modifiers[id].stat,
            Modifiers[id].type,
            Modifiers[id].value);

        Items[mod.stat.Name].Apply(mod);

        return result;
    }

    public string RemoveModifier(int id)
    {
        var statname = Modifiers[id].stat.Name;
        var result = string.Format("Remove modifier {0} {1} {2}", statname, Modifiers[id].type,
            Modifiers[id].value);
        Items[Modifiers[id].stat.Name].Remove(Modifiers[id]);
        Modifiers.Remove(id);

        return result;
    }

    public void AddStat(Stat s)
    {
        Assert.IsFalse(s.name.Contains("Clone"), "name has clone..this will not work well with the dictionaries");
        Items.Add(s.Name, s);
    }

    public void ClearModifiers()
    {
        var keys = Modifiers.Keys.ToArray();
        foreach (var key in keys)
            RemoveModifier(key);

        Modifiers.Clear();
    }
}