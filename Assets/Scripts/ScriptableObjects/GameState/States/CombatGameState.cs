
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "CombatGameState", menuName = "GameState/States/CombatState")]
public class CombatGameState : GameState
{
    public State Next;
    public override void OnEnter(GameStateBehaviour game)
    {
        game.SetText(this.name);
        SceneManager.LoadScene(2);
    }
    public override void UpdateState(GameStateBehaviour game)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ToState(game, Next);
    }
}
