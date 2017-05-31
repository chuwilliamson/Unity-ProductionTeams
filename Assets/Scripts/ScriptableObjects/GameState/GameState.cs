using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStateSingleton", menuName = "GameState/Singleton")]
public class GameState : State
{
    //assigned in inspector
    [SerializeField]
    private State start;

    public IGameState Start
    {
        get
        {
            if (!start) start = Resources.FindObjectsOfTypeAll<InitGameState>().FirstOrDefault();
            if (!start) start = CreateInstance<InitGameState>();
            return start;
        }
        set { start = (State)value; }
    }

    public override void ToState(GameStateBehaviour game, IGameState state)
    {
        Debug.Log("move to state::" + game.Current + "->" + state);
        game.Current.OnExit(game);
        game.Current = state;
        game.Current.OnEnter(game);
    }
}