using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "CombatGameState", menuName = "GameState/States/CombatState")]
public class CombatGameState : GameState
{
    public int GoldGainPerSecond = 1;
    public float goldTimer = 1;
    public State Next;
    public State Previous;
    public float timer;

    public override void OnEnter(GameStateBehaviour game)
    {
        base.OnEnter(game);
        if(SceneManager.GetActiveScene().buildIndex != 2)
            SceneManager.LoadScene(2);
        game.SetText(name);
        PlayerData.Instance.ForceRefresh();
        Time.timeScale = 1f;
        
    }

    public override void OnExit(GameStateBehaviour game)
    {
        base.OnExit(game);
        Time.timeScale = 1f;
    }

    public override void UpdateState(GameStateBehaviour game)
    {
        if(SceneManager.GetActiveScene().buildIndex != 2)
            return;
        if (timer >= goldTimer)
        {
            timer = 0;
            PlayerData.Instance.GainGold(GoldGainPerSecond);
        }

        timer += Time.deltaTime;
        var wincondition = game.WinCondition;
        var losecondition = game.LoseCondition;
        if (wincondition)
            game.SetText("You win. r To restart");
        if (losecondition)
        {
            game.SetText("You lose. r To restart");
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ToState(game, Previous);
            PlayerData.Instance.ResetGame();
        }
            

        if (Input.GetKeyDown(KeyCode.Escape))
            ToState(game, Next);
    }
}