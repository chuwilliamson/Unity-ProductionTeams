using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "IntroGameState", menuName = "GameState/States/Intro")]
public class IntroGameState : GameState
{
    public State Next;
    public override void OnEnter(GameStateBehaviour game)
    {
        game.SetText("Intro Game State");
        SceneManager.LoadScene(1);
    }
    public override void UpdateState(GameStateBehaviour game)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ToState(game, Next);
    }
}