using System.Linq;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableSingleton<PlayerData>
{
    public enum PlayerDataStats
    {
        Experience,
        Gold,
        Kills,
    }
    public class OnPlayerDataChanged : UnityEvent<Stat> { }
    public OnPlayerDataChanged onExperienceChanged = new OnPlayerDataChanged();
    public OnPlayerDataChanged onGoldChanged = new OnPlayerDataChanged();
    public OnPlayerDataChanged onKillsChanged = new OnPlayerDataChanged();
    public OnPlayerDataChanged onStatsChanged = new OnPlayerDataChanged();

    public Stats stats;

    public int Gold
    {
        get { return stats["gold"].Value; }
    }

    public int Experience
    {
        get { return stats["experience"].Value; }
    }

    public int Kills
    {
        get { return stats["kills"].Value; }
    }

    private void OnEnable()
    {
        if(stats)
            return;
        stats = Resources.Load<Stats>("PLayerStats");
        stats = Instantiate(stats);
        Debug.Log("enabled");
    }

    public void ForceRefresh()
    {
        foreach (var stat in stats)
            onStatsChanged.Invoke(stat);
    }
    public void GainKills()
    {
        stats["kills"].Value++;
        onKillsChanged.Invoke(stats["kills"]);
        onStatsChanged.Invoke(stats["kills"]);

    }
    public void GainExperience(int amount)
    {
        stats["experience"].Value += amount;
        onExperienceChanged.Invoke(stats["experience"]);
        onStatsChanged.Invoke(stats["experience"]);
    }

    public void GainGold(int amount)
    {
        stats["gold"].Value += amount;
        onGoldChanged.Invoke(stats["gold"]);
        onStatsChanged.Invoke(stats["gold"]);
    }

    public void SpendGold(int amount)
    {
        stats["gold"].Value -= amount;
        onGoldChanged.Invoke(stats["gold"]);
        onStatsChanged.Invoke(stats["gold"]);
        Debug.Log("spend gold: " + amount + " gold is now: " + stats["gold"].Value);
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerData))]
    public class PlayerDataInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mytarget = target as PlayerData;
            if(mytarget == null) return;
            foreach(var stat in mytarget.stats)
                EditorGUILayout.LabelField(stat.Name, stat.Value.ToString());
            if(mytarget.stats.Count < 1)
                EditorGUILayout.LabelField("none");
            Repaint();
        }
    }
#endif
}