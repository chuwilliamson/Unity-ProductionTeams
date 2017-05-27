using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "InitGameState", menuName = "GameState/States/Init")]
public class InitGameState : GameState
{
    public State Next;
    
    public override void UpdateState(GameStateBehaviour game)
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ToState(game, Next);
    }
}