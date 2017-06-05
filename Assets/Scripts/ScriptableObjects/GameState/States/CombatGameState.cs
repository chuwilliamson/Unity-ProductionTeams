
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "CombatGameState", menuName = "GameState/States/CombatState")]
public class CombatGameState : GameState
{
    public State Next;
    public override void OnEnter(GameStateBehaviour game)
    {
        Debug.Log("hello");
        game.SetText(this.name);
        PlayerData.Instance.ForceRefresh();

    }
    public override void UpdateState(GameStateBehaviour game)
    {
        
    }
}
