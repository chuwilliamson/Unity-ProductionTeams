using UnityEngine;
using UnityEngine.Events;

public class MotherBaseBehaviour : MonoBehaviour, IDamageable
{
    public Stat HealthStat;
    public MotherBaseEvent OnDamaged = new MotherBaseEvent();
    public MotherBaseEvent OnDead = new MotherBaseEvent();

    public void TakeDamage(int amount)
    {
        var mod = ScriptableObject.CreateInstance<StatModifier>();
        mod.stat = HealthStat;
        //negative to do damage
        mod.value = -amount;
        mod.type = Modifier.ModType.ADD;
        HealthStat.Apply(mod);
        OnDamaged.Invoke(HealthStat);
        if (HealthStat.Value < 1)
            OnDead.Invoke(HealthStat);
    }
    // Use this for initialization

    void Start()
    {
        HealthStat = Instantiate(HealthStat);
        //invoke event to update ui
        OnDamaged.Invoke(HealthStat);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public class MotherBaseEvent : UnityEvent<Stat>
    {
    }
}