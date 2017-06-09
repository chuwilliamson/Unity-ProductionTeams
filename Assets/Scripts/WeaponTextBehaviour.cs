using UnityEngine;
using UnityEngine.UI;

public class WeaponTextBehaviour : MonoBehaviour
{
    Text text;
    public GameObject landmine;
    public GameObject turret;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetText(string value)
    {
        landmine.SetActive(false);
        turret.SetActive(false);
    
        if (value.ToLower().Contains("landmine"))
            landmine.SetActive(true);
        else
            turret.SetActive(true);

        Debug.Log(value);
    }

}
