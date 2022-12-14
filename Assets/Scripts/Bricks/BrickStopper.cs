using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickStopper : MonoBehaviour
{
    [SerializeField] private bool _isStatic;
    private bool _isBlocked;

    private void Start()
    {
        _isBlocked = _isStatic;
    }

    private void Update()
    {
        CheckBlockedBrick();
        if (GetComponent<BrickMover>() != null)
            GetComponent<BrickMover>().enabled = !_isBlocked;
    }

    private void CheckBlockedBrick()
    {
        var brick = FindBrickByRaycast(Vector3.down);
        _isBlocked = brick != null ? brick.CheckBlocked() : false;
    }

    private BrickStopper FindBrickByRaycast(Vector3 direction)
    {
        Ray Ray = new Ray(transform.position, direction);
        RaycastHit Hit;

        if (Physics.Raycast(Ray, out Hit, 0.15f) && Hit.collider.gameObject.TryGetComponent(out BrickStopper brick))
            return brick;

        return null;
    }

    public bool CheckBlocked()
    {
        return _isBlocked || _isStatic;
    }
}
