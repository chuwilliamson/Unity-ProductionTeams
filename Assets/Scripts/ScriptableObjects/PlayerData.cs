using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableSingleton<PlayerData>, IDamageable
{
    public Stats stats;

    public class OnPlayerDataChanged : UnityEvent<int> { };

    public OnPlayerDataChanged onExperienceChanged = new OnPlayerDataChanged();
    public OnPlayerDataChanged onGoldChanged = new OnPlayerDataChanged();

    public int Gold
    {
        get { return stats["gold"].Value; }
    }

    public int Experience
    {
        get
        {
            return stats["experience"].Value; 
        }
        
    }

    public void GainExperience(int amount)
    {
        stats["experience"].Value += amount;
        onExperienceChanged.Invoke(stats["experience"].Value);

    }

    public void GainGold(int amount)
    {
        stats["gold"].Value += amount;
        onGoldChanged.Invoke(stats["gold"].Value);
    }

    public void SpendGold(int amount)
    {
        stats["gold"].Value -= amount;
        onGoldChanged.Invoke(stats["gold"].Value);
        Debug.Log("spend gold " + amount + "gold is now: " + stats["gold"].Value);
        
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerData))]
    public class PlayerDataInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mytarget = target as PlayerData;
            if (mytarget == null) return;
            foreach (var stat in mytarget.stats)
            {
                EditorGUILayout.LabelField(stat.Name, stat.Value.ToString());
            }
            if (mytarget.stats.Count < 1)
                EditorGUILayout.LabelField("none");
        }
    }
#endif

    public void TakeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
}

