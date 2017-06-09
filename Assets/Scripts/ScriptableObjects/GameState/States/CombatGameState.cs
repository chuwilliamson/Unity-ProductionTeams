﻿using UnityEngine;
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
        game.Text.gameObject.SetActive(false);
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
        if (timer >= goldTimer)
        {
            timer = 0;
            PlayerData.Instance.GainGold(GoldGainPerSecond);
        }

        timer += Time.deltaTime;
        
        if (game.WinCondition)
        {
            game.Text.gameObject.SetActive(true);
            game.SetText("You win. r To restart");
            game.Text.transform.localPosition = Vector3.zero;
            Time.timeScale = .1f;
        }
            
        if(game.LoseCondition)
        {
            game.SetText("You lose. Press Any Key To Restart");
            Time.timeScale = .1f;
            if (Input.anyKey)
                PlayerData.Instance.ResetGame();
          
            
            
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ToState(game, Previous);
            PlayerData.Instance.ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            ToState(game, Next);
    }
}