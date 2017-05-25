public interface IGameState
{
    void UpdateState(GameStateBehaviour game);
    void ToState(GameStateBehaviour game, IGameState state);
    void OnEnter(GameStateBehaviour game);
    void OnExit(GameStateBehaviour game);
}