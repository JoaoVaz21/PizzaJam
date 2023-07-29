using System;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryPanel;
    [SerializeField]
    private Button nextLevelBtn;
    [SerializeField]
    private GameObject retryPanel;
    [SerializeField]
    private Button retryBtn;

    // Start is called before the first frame update
    private void Start()
    {
        LevelManager.Instance.Victory+=OnVictory;
        LevelManager.Instance.GameOver+=OnGameOver;
        nextLevelBtn.onClick.AddListener(() => { LevelManager.Instance.ChangeScene(); });
        retryBtn.onClick.AddListener(() => { LevelManager.Instance.ChangeScene(); });
    }

    private void OnGameOver()
    {
        retryPanel.SetActive(true);
    }

    private void OnVictory()
    {
        victoryPanel.SetActive(true);
    }
}