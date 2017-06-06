using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "IntroGameState", menuName = "GameState/States/Intro")]
public class IntroGameState : GameState
{
    public State Next;
    public AsyncOperation combatSceneLoad;
    public override void OnEnter(GameStateBehaviour game)
    {
        base.OnEnter(game);
        UIButtonClicked.OnButtonClicked.AddListener(
            delegate(string val)
            {
                if(val == "startgame") ToState(game, Next);
            });
    }

    

    public override void OnExit(GameStateBehaviour game)
    {
        Debug.Log("exit intro");
        SceneManager.LoadScene(2);
    }

    IEnumerator LoadLvl(GameStateBehaviour game)
    {
        if(combatSceneLoad == null)
            combatSceneLoad = SceneManager.LoadSceneAsync(2);
        combatSceneLoad.allowSceneActivation = false;

        while(!combatSceneLoad.isDone)
        {
            game.SetSlider(combatSceneLoad.progress * 100);
            if(combatSceneLoad.progress >= .9f)
            {
                game.SetSlider(100);
                game.SetText("Press Any Key to Start");
                if(Input.GetKeyDown(KeyCode.E))
                    ToState(game, Next);
            }
            yield return null;
        }

        PlayerData.Instance.ForceRefresh();
        yield return null;
    }
}