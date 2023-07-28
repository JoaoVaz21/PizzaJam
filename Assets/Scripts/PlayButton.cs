using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour, IPointerDownHandler
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(() => { StartLevel("Level1"); });
    }

    public void StartLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.PlaySFX(0);
    }
}