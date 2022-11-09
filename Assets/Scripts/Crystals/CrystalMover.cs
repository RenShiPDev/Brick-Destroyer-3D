using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private GameObject _crystalImage;
    private CrystalText _crystalText;
    private Vector3 _targetPosition;
    private Vector3 _startScale;

    private float _startDistance;

    private void Start()
    {
        _crystalText = FindObjectOfType<CrystalText>();
        _crystalImage = _crystalText.gameObject;

        _targetPosition = _crystalImage.transform.position;
        _startScale = transform.localScale;
        _startDistance = (_targetPosition - transform.position).magnitude;
    }

    private void Update()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        float distance = (_targetPosition - transform.position).magnitude+0.1f;

        transform.Translate(direction * _movementSpeed*Time.deltaTime);
        transform.localScale = _startScale * (distance / _startDistance);

        if ((transform.position - _targetPosition).magnitude <= 0.1f)
        {
            _crystalText.UpdateText();
            gameObject.SetActive(false);
        }
    }
}
