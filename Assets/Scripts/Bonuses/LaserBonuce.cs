using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBonuce : MonoBehaviour
{
    private List<BrickHealth> _onLineObjects = new List<BrickHealth>();

    private void Start()
    {
        FindLineObjects();
    }

    public void DamageBricks()
    {
        foreach (var brick in _onLineObjects)
            if (brick.gameObject.activeSelf && brick.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                if (brick.gameObject.TryGetComponent(out Bonuse bonuse))
                    bonuse.ActivateBonuse();

                brick.GetDamage();
            }
    }

    private void FindLineObjects()
    {
        Vector3 directionVector = transform.forward;
        FindBrickByRaycast(directionVector);
        FindBrickByRaycast(-directionVector);
    }

    private void FindBrickByRaycast(Vector3 direction)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity);

        foreach (RaycastHit hit in hits)
            if (hit.collider.gameObject.TryGetComponent(out BrickHealth brick))
                _onLineObjects.Add(brick);
    }
}
