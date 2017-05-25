using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackPackBehaviour : MonoBehaviour
{
    public BackPack backPack_config;

    [HideInInspector] public BackPack backPack_runtime;

    public OnBackPackAddItem onBackPackAddItem = new OnBackPackAddItem();

    public OnBackPackChange onBackPackChange = new OnBackPackChange();

    public List<Item> Items { get; set; }

    /// <summary>
    ///     on start initialize the backpack
    /// </summary>
    void Start()
    {
        Initialize(backPack_config);
    }

    /// <summary>
    ///     initialize the runtime backpack with a configuration
    ///     if we want to replace the backpack at runtime with another one we can see the UIController button callbacks
    /// </summary>
    /// <param name="config"></param>
    public void Initialize(BackPack config)
    {
        Items = new List<Item>();
        backPack_config = config ?? Resources.Load<BackPack>(@"Items\BackPackConfig");
        backPack_runtime = Instantiate(backPack_config);
        foreach (var item in backPack_config.Items)
        {
            var runtimeitem = Instantiate(item);
            runtimeitem.Initialize(null);
            AddToPack(runtimeitem);
        }
        onBackPackChange.Invoke(backPack_runtime);
    }

    public bool AddToPack(Item item)
    {
        Items.Add(item);
        backPack_runtime.Items = Items;
        onBackPackAddItem.Invoke(item);
        onBackPackChange.Invoke(backPack_runtime);
        return true;
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        backPack_runtime.Items = Items;
        onBackPackChange.Invoke(backPack_runtime);
    }

    public void RemoveAll()
    {
        Items.Clear();
        backPack_runtime.Items = Items;
        onBackPackChange.Invoke(backPack_runtime);
    }

    public void PrintItems()
    {
        Items.ForEach(item => Debug.Log(item.DisplayName));
    }

    public class OnBackPackChange : UnityEvent<BackPack>
    {
    }

    public class OnBackPackAddItem : UnityEvent<Item>
    {
    }
}