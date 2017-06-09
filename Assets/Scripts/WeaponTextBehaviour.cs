using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WeaponTextBehaviour : MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetText(string value)
    {
        text.text = value;
    }

}
