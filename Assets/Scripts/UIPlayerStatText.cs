using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerStatText : UITextBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        PlayerData.Instance.onStatsChanged.AddListener(UpdateText);
    }
    protected override void UpdateText(Stat stat)
    {
        if(stat.Name == _stat.Name)
            displayText = _stat.Name + " : " + stat.Value;
        base.UpdateText(stat);
    }
}
