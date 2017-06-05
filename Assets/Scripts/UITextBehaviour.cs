using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextBehaviour : MonoBehaviour
{
    private Text text;

    public Stat _stat;
    private void Awake()
    {
        text = GetComponent<Text>();
        PlayerData.Instance.onStatsChanged.AddListener(UpdateText);
    }
    private void Start()
    {
        
    }

    private void UpdateText(Stat stat)
    {
        if(stat.Name == _stat.Name)
            text.text = stat.Name + ":" + stat.Value;
    }

}
