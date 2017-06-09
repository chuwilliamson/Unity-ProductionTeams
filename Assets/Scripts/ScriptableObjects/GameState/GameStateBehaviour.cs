using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GameStateBehaviour : MonoBehaviour
{
    public IGameState Current;
    public GameState fsm;
    public Text Text;
    public Slider Slider;
    public string currentState;
    
    private void Start()
    {
        Current = fsm.Start;
        UnityEngine.Assertions.Assert.IsNotNull(Current, "no");
        Current.OnEnter(this);
        PlayerData.Instance.ForceRefresh();
    }

    private void Update()
    {
        Current.UpdateState(this);
        currentState = Current.ToString();
    }

    public void SetText(string value)
    {
        if(Text != null)
        Text.text = value;
    }

    public void SetSlider(float value)
    {
        if (Slider == null)
        {
            Debug.LogWarning("slider not set");
            return;
        }
            
        Slider.value = value;
    }
    
    public bool LoseCondition
    {
        get { return PlayerData.Instance.Health < 1; }
    }

    [SerializeField]
    private int sum;
    public bool WinCondition
    {
        get
        {
            var spawners = FindObjectsOfType<EnemyTowerSpawningBehaviour>();
            sum = spawners.Sum(s => s.maxEnemies);
            return PlayerData.Instance.BossKills >= 3 && PlayerData.Instance.Kills >= sum;
        }
    }
}
