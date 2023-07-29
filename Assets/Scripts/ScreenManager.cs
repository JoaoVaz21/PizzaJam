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
    [SerializeField]
    private GameObject victoryScreenBackgroundImage;
    [SerializeField]
    private GameObject gameOverScreenBackgroundImage;

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
        gameOverScreenBackgroundImage.SetActive(true);
        retryPanel.SetActive(true);
        SoundManager.PlaySFX(3);
    }

    private void OnVictory()
    {
        victoryScreenBackgroundImage.SetActive(true);
        victoryPanel.SetActive(true);
    }
}