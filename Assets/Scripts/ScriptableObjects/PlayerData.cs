using System.Linq;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableSingleton<PlayerData>
{
    public OnPlayerDataChanged onExperienceChanged = new OnPlayerDataChanged();

    public OnPlayerDataChanged onGoldChanged = new OnPlayerDataChanged();
    public Stats stats;

    public int Gold
    {
        get { return stats["gold"].Value; }
    }

    public int Experience
    {
        get { return stats["experience"].Value; }
    }

    private void OnEnable()
    {
        stats = stats ? Instantiate(stats) : Resources.FindObjectsOfTypeAll<Stats>().FirstOrDefault();
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
        Debug.Log("spend gold: " + amount + " gold is now: " + stats["gold"].Value);
    }

    public class OnPlayerDataChanged : UnityEvent<int> { }

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
                EditorGUILayout.LabelField(stat.Name, stat.Value.ToString());
            if (mytarget.stats.Count < 1)
                EditorGUILayout.LabelField("none");
        }
    }
#endif
}