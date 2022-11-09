using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplosion : MonoBehaviour
{
    [SerializeField] private BrickHealth _brickHealth;
    private List<BrickHealth> _closestBricks = new List<BrickHealth>();

    private void OnEnable()
    {
        _brickHealth.DieEvent.AddListener(OnDie);
    }
    private void OnDisable()
    {
        _brickHealth.DieEvent.RemoveListener(OnDie);
    }

    private void Start()
    {
        FindClosestObjects();
    }

    private void OnDie()
    {
        foreach(var brick in _closestBricks)
        {
            brick.GetDamage(10);
        }
    }

    private void FindClosestObjects()
    {
        Vector3 directionVector = Vector3.up;
        for(int i = 0; i <= 8; i++)
        {
            directionVector = Quaternion.AngleAxis(45f*i, Vector3.forward) * directionVector;
            FindBrickByRaycast(directionVector);
        }
    }

    private void FindBrickByRaycast(Vector3 direction)
    {
        Ray Ray = new Ray(transform.position, direction);
        RaycastHit Hit;

        if (Physics.Raycast(Ray, out Hit, 1))
        {
            Debug.DrawLine(Ray.origin, Hit.point, Color.blue);

            if (Hit.collider.gameObject.TryGetComponent(out BrickHealth brick))
            {
                _closestBricks.Add(brick);
            }
        }
    }
}
