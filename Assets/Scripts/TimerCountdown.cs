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
        slider.value = 1;
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
                LevelManager.Instance.GameOver();
            }
            else
            {
                slider.value = currentTime/timeLimit;
            }
        }
    }
}