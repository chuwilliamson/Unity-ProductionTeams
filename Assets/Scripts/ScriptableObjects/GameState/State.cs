using UnityEngine;

public abstract class State : ScriptableObject, IGameState
{
    public virtual void UpdateState(GameStateBehaviour game) {}
    public virtual void ToState(GameStateBehaviour game, IGameState state) { }
    public virtual void OnEnter(GameStateBehaviour game) { }
    public virtual void OnExit(GameStateBehaviour game) { }
}