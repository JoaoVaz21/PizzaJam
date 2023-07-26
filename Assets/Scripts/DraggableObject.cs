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
                transform.DOMove(_snappingGameObject.transform.position, 0.1f).OnComplete(() =>
                {
                    // TODO tell level manager that an objective was completed
                });
            }
            else
            {
                _animating = true;
                transform.DOMove(_startPosition, 1).OnComplete(() =>
                {
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