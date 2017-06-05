using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMotherBaseTextPrompt : UITextBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<MotherBaseBehaviour>().OnDamaged.AddListener(UpdateText);
    }
    
    protected override void UpdateText(Stat stat)
    {
        displayText = stat.Name + " : " + stat.Value;
        base.UpdateText(stat);
    }
}
