using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonClicked : MonoBehaviour
{
    public class ButtonClicked : UnityEvent<string> { }

    [SerializeField]
    string message = "";
    public static ButtonClicked OnButtonClicked;
    private void Awake()
    {
        OnButtonClicked = new ButtonClicked();
    }

    public void onClicked(string value)
    {
        OnButtonClicked.Invoke(message);
    }
    
}
