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
        if(SceneManager.GetActiveScene().buildIndex != 1)
            SceneManager.LoadScene(1);
        if(UIButtonClicked.OnButtonClicked == null)
            return;
        UIButtonClicked.OnButtonClicked.AddListener(
            delegate(string val)
            {
                if(val == "startgame") ToState(game, Next);
            });
         
    }

    public override void UpdateState(GameStateBehaviour game)
    {
        base.UpdateState(game);
        if (UIButtonClicked.OnButtonClicked == null)
        {
            try
            {
                UIButtonClicked.OnButtonClicked.AddListener(
                    delegate(string val)
                    {
                        if (val == "startgame") ToState(game, Next);
                    });
            }
            catch{}
        }
    
    }
}