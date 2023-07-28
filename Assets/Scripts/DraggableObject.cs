using DG.Tweening;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector2 _offset;
    private Vector3 _startPosition;
    private bool _animating = false;
    private bool _canSnap = false;
    private bool _snapped = false;
    private GameObject _snappingGameObject;

    private void OnMouseDown()
    {
        if (!_animating && !_snapped)
        {
            SoundManager.PlaySFX(1);
            _startPosition = transform.position;
            _offset = (Vector2)transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseDrag()
    {
        if (!_snapped)
            transform.position = Vector2.Lerp(transform.position, GetMouseWorldPos() + _offset, 0.8f);
    }

    private void OnMouseUp()
    {
        if (!_snapped)
        {
            if (_canSnap)
            {
                _snapped = true;
                SoundManager.PlaySFX(1);
                transform.DOMove(_snappingGameObject.transform.position, 0.1f).OnComplete(() =>
                {
                    LevelManager.Instance.CompleteObjective();
                });
            }
            else
            {
                _animating = true;
                transform.DOMove(_startPosition, 1).OnComplete(() =>
                {
                    SoundManager.PlaySFX(1);
                    _animating = false;
                });
            }
        }
    }

    private Vector2 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetSnappingPoint(GameObject gameObject)
    {
        _snappingGameObject = gameObject;
        _canSnap = true;
    }

    public void UnsetSnappingPoint(GameObject gameObject)
    {
        if (gameObject == _snappingGameObject)
        {
            _canSnap = false;
        }
    }
}