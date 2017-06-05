using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherBaseBehaviour : MonoBehaviour, IDamageable
{
    public Stat HealthStat;

	// Use this for initialization
	void Start ()
	{
	    HealthStat = Instantiate(HealthStat);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        var mod = ScriptableObject.CreateInstance<StatModifier>();
        mod.stat = HealthStat;
        mod.value = -amount;
        mod.type = Modifier.ModType.ADD;
        HealthStat.Apply(mod);
    }
}
