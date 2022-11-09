using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountCanvasMover : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;

    private void Update()
    {
        transform.position = _targetTransform.position;
    }
}
