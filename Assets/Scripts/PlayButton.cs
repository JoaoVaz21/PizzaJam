using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : ButtonWithSFX
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() => { StartLevel("Level1"); });
    }

    public void StartLevel(string name)
    {
        SoundManager.PlayMusic(1);
        SceneManager.LoadScene(name);
    }
}