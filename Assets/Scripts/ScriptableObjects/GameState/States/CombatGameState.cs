
using System.Collections;
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
        game.StartCoroutine(LoadLvl(game));
    }

    IEnumerator LoadLvl(GameStateBehaviour game)
    {
        AsyncOperation loadlvl = SceneManager.LoadSceneAsync(2);
        loadlvl.allowSceneActivation = false;
        while(!loadlvl.isDone)
        {
            Debug.Log(loadlvl.progress * 100);
            game.SetSlider(loadlvl.progress * 100);
            if (loadlvl.progress >= .9f)
            {
                game.SetSlider(100);
                game.SetText("Press Any Key to Start");
                if (Input.anyKey)
                {
                    loadlvl.allowSceneActivation = true;
                    game.SetText("");
                    game.Text.gameObject.SetActive(false);
                    game.Slider.gameObject.SetActive(false);


                }
            }
            yield return null;
        }

        PlayerData.Instance.ForceRefresh();
        yield return null;
    }
    public override void UpdateState(GameStateBehaviour game)
    {

    }
}
