using UnityEngine;
using UnityEngine.UI;
public class GameStateBehaviour : MonoBehaviour
{
    
    public IGameState Current;
    public GameState fsm;
    public Text Text;
    public string currentState;
    private void Start()
    {
        DontDestroyOnLoad(this);
        Current = fsm.Start;
        UnityEngine.Assertions.Assert.IsNotNull(Current, "no");
        Current.OnEnter(this);
        
    }
    private void Update()
    {
        Current.UpdateState(this);
        currentState = Current.ToString();
    }

    public void SetText(string value)
    {
        Text.text = value;
    }
}
