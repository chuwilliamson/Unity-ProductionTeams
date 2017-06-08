using UnityEngine;
using UnityEngine.Events;

public class SimpleDamageBehaviour : MonoBehaviour, IDamageable
{
    public Stat HealthStat;
    public DamageEvent OnDamaged = new DamageEvent();
    public DeathEvent OnDead = new DeathEvent();

    public virtual void TakeDamage(int amount)
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
        OnDead.AddListener(onDied);
    }

    protected virtual void onDied(Stat s)
    {
        Destroy(gameObject);
    }
    public class DamageEvent : UnityEvent<Stat>
    {
    }

    public class DeathEvent : UnityEvent<Stat>
    {
    }
}