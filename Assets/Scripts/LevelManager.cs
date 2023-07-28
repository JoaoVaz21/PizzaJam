using System;
using UnityEngine;

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

    private int _currentObjectiveCount = 0;

    public event Action StartLevel;
    public event Action EndLevel;

    // Start is called before the first frame update
    private void Start()
    {
        //TODO do animations
        StartLevel?.Invoke();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void CompleteObjective()
    {
        //Maybe add validations (?)
        _currentObjectiveCount++;
        if (totalObjectives==_currentObjectiveCount)
        {
            //TODO Finish animations and switch level;
            EndLevel?.Invoke();
        }
    }

    public void GameOver()
    {
        //TODO validations
        //TODO show end animation, reset level
        Debug.Log("Game over - time");
    }
}