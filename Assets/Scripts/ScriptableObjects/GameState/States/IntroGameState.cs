using UnityEngine;

[CreateAssetMenu(fileName = "IntroGameState", menuName = "GameState/States/Intro")]
public class IntroGameState : GameState
{
    public State Next;

    public override void UpdateState(GameStateBehaviour game)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ToState(game, Next);
    }
}