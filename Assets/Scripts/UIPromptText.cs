using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPromptText : MonoBehaviour
{
    private Text text;
    public float duration = 1f;
    private void Start()
    {
        text = GetComponent<Text>();
        text.CrossFadeAlpha(0, 0, true);
    }
    public void FlashText(string value)
    {
        text.text = value;
        text.CrossFadeAlpha(1, 0, true);
        text.CrossFadeAlpha(0, duration, true);
    }
}
