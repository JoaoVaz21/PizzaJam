using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private Slider slider;
    private float currentTime = 0;

    // Start is called before the first frame update
    private void Start()
    {
        LevelManager.Instance.StartLevel+=StartCountdown;
        LevelManager.Instance.EndLevel+=OnEndLevel;
        slider.value = 1;
    }

    private void OnEndLevel()
    {
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void StartCountdown()
    {
        currentTime = timeLimit;
        StartCoroutine(CountTime());
    }

    private IEnumerator CountTime()
    {
        while (currentTime>=0)
        {
            yield return new WaitForEndOfFrame();
            currentTime-= Time.deltaTime;
            if (currentTime<0)
            {
                LevelManager.Instance.SetGameOver();
            }
            else
            {
                slider.value = currentTime/timeLimit;
            }
        }
    }
}