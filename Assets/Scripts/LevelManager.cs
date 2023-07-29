using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region singleton
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get => _instance;
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else
            {
                Destroy(value);
            }
        }
    }

    public void Awake()
    {
        Instance = this;
    }

    #endregion singleton

    [SerializeField]
    private int totalObjectives;
    private bool _levelWon = false;
    [SerializeField]
    private GameObject[] imagesToHideOnLevelComplete;
    [SerializeField]
    private GameObject[] ImagesToShowOnLevelComplete;

    private int _currentObjectiveCount = 0;

    public event Action StartLevel;
    public event Action EndLevel;
    public event Action Victory;
    public event Action GameOver;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnFadeInCompleted()
    {
        StartLevel?.Invoke();
    }

    public void CompleteObjective()
    {
        //Maybe add validations (?)
        _currentObjectiveCount++;
        if (totalObjectives==_currentObjectiveCount)
        {
            Debug.Log("LevelWon");
            _levelWon = true;

            //TODO Finish animations and switch level;
            EndLevel?.Invoke();
        }
    }

    public void SetGameOver()
    {
        //TODO validations
        //TODO show end animation, reset level
        _levelWon = false;
        EndLevel?.Invoke();
        Debug.Log("Game over - time");
    }

    public void ChangeToEndScreen()
    {
        if (_levelWon)
        {
            StartCoroutine(DelayVictoryAnnouncement());
            foreach (var gameObject in imagesToHideOnLevelComplete)
            {
                gameObject.SetActive(false);
            }
            foreach (var gameObject in ImagesToShowOnLevelComplete)
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            GameOver?.Invoke();
        }
    }

    private IEnumerator DelayVictoryAnnouncement()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManager.PlaySFX(2);
        yield return new WaitForSeconds(1.2f);
        Victory?.Invoke();
    }

    public void ChangeScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_levelWon ? activeScene.buildIndex+1 : activeScene.buildIndex);
    }
}