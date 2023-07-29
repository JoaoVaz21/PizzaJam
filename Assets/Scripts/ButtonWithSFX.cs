using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonWithSFX : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.PlaySFX(0);
    }
}