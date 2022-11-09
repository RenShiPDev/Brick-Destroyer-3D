using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickMover : MonoBehaviour
{
    public UnityEvent PositionChangedEvent;

    private LevelGenerator _levelGenerator;
    private GameOverMenu _gameOverMenu;
    private Transform _brickLimit;

    private Vector3 _startScale;

    private float _targetHeight;
    private float _moveSpeed = 3;


    private void OnEnable()
    {
        _startScale = transform.localScale;

        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _gameOverMenu = FindObjectOfType<GameOverMenu>();

        _brickLimit = FindObjectOfType<BrickLimitObject>().transform;

        _levelGenerator.ChangeHeightEvent.AddListener(ChangeHeight);
        _targetHeight = transform.localPosition.y;
    }

    private void OnDisable()
    {
        _levelGenerator.ChangeHeightEvent.RemoveListener(ChangeHeight);
    }

    private void Update()
    {
        Move();
        ChangeScale();
    }

    public void ChangeHeight()
    {
        Vector3 targetPos = new Vector3(transform.localPosition.x, _targetHeight, transform.localPosition.z);
        transform.localPosition = targetPos;

        _targetHeight--;
        PositionChangedEvent?.Invoke();
    }

    private void ChangeScale()
    {
        Vector3 targetScale = Vector3.Slerp(transform.localScale, _startScale, 3 * Time.deltaTime);
        transform.localScale = (transform.localScale - _startScale).magnitude > 0.1f ? targetScale : _startScale;
    }

    private void Move()
    {
        Vector3 targetPos = new Vector3(transform.localPosition.x, _targetHeight, transform.localPosition.z);
        if (Mathf.Abs(_targetHeight - transform.localPosition.y) > 0.02f)
        {
            targetPos = Vector3.Slerp(transform.localPosition, targetPos, _moveSpeed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x, targetPos.y, transform.localPosition.z);
        }
        else
            transform.localPosition = targetPos;

        if (transform.position.y <= _brickLimit.position.y)
            _gameOverMenu.GameOver();
    }
}
