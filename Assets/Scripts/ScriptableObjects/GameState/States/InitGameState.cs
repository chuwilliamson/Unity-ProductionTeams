using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "InitGameState", menuName = "GameState/States/Init")]
public class InitGameState : GameState
{
    public State Next;
    public override void OnEnter(GameStateBehaviour game)
    {
        base.OnEnter(game);
    
    }
    public override void UpdateState(GameStateBehaviour game)
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        Debug.Log("move to " + Next.name);
        ToState(game, Next);
    }

    public override void OnExit(GameStateBehaviour game)
    {
        base.OnExit(game);
        SceneManager.LoadScene(1);
    }
}