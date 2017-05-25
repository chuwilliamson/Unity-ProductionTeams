using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSingleton", menuName = "GameState/Singleton")]
public class GameState : State
{
    protected static GameState _instance;
    
    public static GameState Instance
    {
        get
        {
            if(!_instance)
                _instance = Resources.FindObjectsOfTypeAll<GameState>().FirstOrDefault();
            if(!_instance)
                _instance = CreateInstance<GameState>();
            return _instance;
        }
    }

    public State Current;
    /*
    intro scene to game scene
    game scene to intro scene
    game scene to credit scene
    */

    public override void ToState(GameStateBehaviour game, IGameState state)
    {
        Debug.Log("move to state" + game.Current + state);
        game.Current.OnExit(game);
        game.Current = (State)state;
        game.Current.OnEnter(game);
    }
}