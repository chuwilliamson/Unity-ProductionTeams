using UnityEngine;

[CreateAssetMenu(fileName = "ExitGameState", menuName = "GameState/States/Exit")]
public class ExitGameState : GameState
{
    public State Next;

    public override void UpdateState(GameStateBehaviour game)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ToState(game, Next);
    }
}