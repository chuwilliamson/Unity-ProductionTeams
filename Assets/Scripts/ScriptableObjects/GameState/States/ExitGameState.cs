using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "ExitGameState", menuName = "GameState/States/Exit")]
public class ExitGameState : GameState
{
    public State Next;
    public override void OnEnter(GameStateBehaviour game)
    {
        
        gsb = game;
        game.SetText("game over");
        SceneManager.LoadScene(3);
        game.StartCoroutine(Countdown());

    }

    public override void UpdateState(GameStateBehaviour game)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToState(game, Next);
        }
    }

    public override void OnExit(GameStateBehaviour game)
    {
        game.StopAllCoroutines();
    }

    private GameStateBehaviour gsb;
    IEnumerator Countdown()
    {
        var timer = 3f;
        while(timer > 0)
        {
            gsb.SetText(((int)timer).ToString());
            yield return timer -= Time.deltaTime;
        }
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}