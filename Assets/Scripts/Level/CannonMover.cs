using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMover : MonoBehaviour
{
    [SerializeField] private GameObject _cannonLimit;
    [SerializeField] private GameObject _cannon;
    [SerializeField] private CannonShooter _cannonShooter;

    private Vector3 _startCannonPos;
    private Vector3 _newCannonPos;

    private bool _isFirstBulletSet = false;

    private void Start()
    {
        _startCannonPos = _cannon.transform.position;
    }

    public void OnCollision(Vector3 position)
    {
        if (!_isFirstBulletSet)
        {
            _newCannonPos = position;
            if(Mathf.Abs(_newCannonPos.x) > Mathf.Abs(_cannonLimit.transform.position.x))
                _newCannonPos.x = _newCannonPos.x < 0 ? _cannonLimit.transform.position.x : -_cannonLimit.transform.position.x;

            _newCannonPos = new Vector3(_newCannonPos.x, _startCannonPos.y, _newCannonPos.z);
            _isFirstBulletSet = true;
        }
    }

    public void MoveToNewPosition()
    {
        if (_isFirstBulletSet)
        {
            _cannon.transform.position = _newCannonPos;
            _isFirstBulletSet = false;
        }
    }
}
