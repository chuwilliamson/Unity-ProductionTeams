using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextBehaviour : MonoBehaviour
{
    private Text text;

    public string start;
    
    private void Start()
    {
        text = GetComponent<Text>();
        if(start == "")
            start = "";
        
        PlayerData.Instance.onGoldChanged.AddListener(UpdateText);
        PlayerData.Instance.onGoldChanged.Invoke(PlayerData.Instance.Gold);
        PlayerData.Instance.onExperienceChanged.Invoke(PlayerData.Instance.Experience);
    }

    private void UpdateText(int value)
    {
        text.text = start + ":" + value;
    }

}
