using UnityEngine;
using UnityEngine.UI;
public class GameStateBehaviour : MonoBehaviour
{
    
    public IGameState Current;
    public GameState fsm;
    public Text Text;

    private void Start()
    {
        DontDestroyOnLoad(this);
        Current = fsm.Start;
        UnityEngine.Assertions.Assert.IsNotNull(Current, "no");
    }
    private void Update()
    {
        Current.UpdateState(this);
    }

    public void SetText(string value)
    {
        Text.text = value;
    }
}
