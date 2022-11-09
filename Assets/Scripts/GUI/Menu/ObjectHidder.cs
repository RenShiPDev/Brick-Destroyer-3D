using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectHidder : MonoBehaviour
{
    public UnityEvent VisibleHandler;

    [SerializeField] private bool _isHidden;
    [SerializeField] private float _scalingSpeed = 10;

    private Vector3 _startScale;
    private bool _inPosition;

    private void OnEnable()
    {
        _startScale = transform.localScale;

        if (_isHidden)
            transform.localScale = Vector3.zero;

        _inPosition = true;
    }

    private void Update()
    {
        if(!_inPosition)
            if (_isHidden)
                MoveObject(Vector3.zero);
            else
                MoveObject(_startScale);
    }

    public void ChangeVisibility()
    {
        _isHidden = !_isHidden;
        _inPosition = false;
    }

    private void MoveObject(Vector3 targetScale)
    {
        transform.localScale = Vector3.Slerp(transform.localScale, targetScale, _scalingSpeed*Time.deltaTime);
        if ((targetScale - transform.localScale).magnitude <= 0.1f)
        {
            transform.localScale = targetScale;
            _inPosition = true;
            VisibleHandler?.Invoke();
        }
    }
}
