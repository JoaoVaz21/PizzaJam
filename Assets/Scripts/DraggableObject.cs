using DG.Tweening;
using System;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector2 _offset;
    private Vector3 _startPosition;
    private bool _animating = false;
    private bool _canSnap = false;
    private bool _snapped = false;
    private bool _canDrag = true;
    private GameObject _snappingGameObject;
    [SerializeField]
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem.Stop();
    }

    private void Start()
    {
        LevelManager.Instance.Victory+=OnVictory;
        LevelManager.Instance.GameOver+=OnGameOver;
    }

    private void OnGameOver()
    {
        _canDrag = false;
        transform.position = _startPosition;
    }

    private void OnVictory()
    {
        this.gameObject.SetActive(false);
    }

    private bool CanDrag()
    {
        return _canDrag && !_animating && !_snapped;
    }

    private void OnMouseDown()
    {
        if (CanDrag())
        {
            SoundManager.PlaySFX(1);
            _startPosition = transform.position;
            _offset = (Vector2)transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseDrag()
    {
        if (!_snapped && _canDrag)
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
                _particleSystem.Stop();
                transform.DOMove(_snappingGameObject.transform.position, 0.1f).OnComplete(() =>
                {
                    transform.rotation = _snappingGameObject.transform.rotation;
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
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();
    }

    public void UnsetSnappingPoint(GameObject gameObject)
    {
        if (gameObject == _snappingGameObject)
        {
            _canSnap = false;
            _particleSystem.Stop();
        }
    }
}