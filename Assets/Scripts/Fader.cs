using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        LevelManager.Instance.EndLevel+=OnEndLevel;
    }

    private void OnEndLevel()
    {
        animator.SetTrigger("FadeOutIn");
    }

    private void BetweenFadeOutInAnimation()
    {
        LevelManager.Instance.ChangeToEndScreen();
    }

    private void FadeOutComplete()
    {
        LevelManager.Instance.ChangeScene();
    }

    private void FinishedFadeIn()
    {
        LevelManager.Instance.OnFadeInCompleted();
    }
}