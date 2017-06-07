using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour {

    private void Start()
    {
        PlayerData.Instance.onKillsChanged.AddListener(DoEpic);
    }

    public void DoEpic(Stat s)
    {
        StartCoroutine(WaitAndEpic());
    }
    IEnumerator WaitAndEpic()
    {
        Time.timeScale = .2f;
        yield return new WaitForSeconds(epicTimes);
        Time.timeScale = 1f;
    }

    public float epicTimes = .2f;
}
