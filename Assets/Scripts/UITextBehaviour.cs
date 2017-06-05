using UnityEngine;
using UnityEngine.UI;

public class UITextBehaviour : MonoBehaviour
{
    public Stat _stat;
    private Text text;
    [SerializeField]
    protected string displayText;

    protected virtual void Awake()
    {
        text = GetComponent<Text>();
        PlayerData.Instance.onStatsChanged.AddListener(UpdateText);
    }

    protected virtual void UpdateText(Stat stat)
    {
        if(stat.Name == _stat.Name)
            text.text = displayText;
    }
}