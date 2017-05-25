

using UnityEngine;
[CreateAssetMenu(fileName = "GameState", menuName = "Singletons/GameState")]
public class GameStateSingleton : ScriptableSingleton<GameStateSingleton>
{
    /*
    intro scene to game scene
    game scene to intro scene
    game scene to credit scene
    */
    public enum GameState
    {
        INTRO = 0,
        RUNNING = 1,
        EXIT = 2,
    }
}
