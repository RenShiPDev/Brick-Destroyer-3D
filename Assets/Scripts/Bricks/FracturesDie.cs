using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturesDie : MonoBehaviour
{
    private Transform _previousParent;
    private Vector3 _startScale;
    private Vector3 _startLocalPos;

    private float _lifeTime = 1;

    public void Initialize(Transform previousParent)
    {
        _previousParent = previousParent;
    }

    private void OnEnable()
    {
        if (_startLocalPos != Vector3.zero)
        {
            _lifeTime = 1;
            transform.localPosition = _startLocalPos;
            transform.localScale = _startScale;
        }
        else
        {
            _startScale = transform.localScale;
            _startLocalPos = transform.localPosition;
        }
    }

    private void OnDisable()
    {
        transform.localPosition = _startLocalPos;
        transform.localScale = _startScale;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        transform.localScale = _startScale * _lifeTime;

        if(_lifeTime < 0)
            gameObject.SetActive(false);
    }
}
