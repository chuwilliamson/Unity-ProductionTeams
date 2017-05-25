using UnityEngine;

public class GameStateBehaviour : MonoBehaviour
{
    public State Current;
    private void Start()
    {
        Current = GameState.Instance.Current;
        UnityEngine.Assertions.Assert.IsNotNull(Current, "no");
    }
    private void Update()
    {
        
        //Current.UpdateState(this);
    }
}
