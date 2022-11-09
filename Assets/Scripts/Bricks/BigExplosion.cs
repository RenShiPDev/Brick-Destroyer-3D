using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigExplosion : MonoBehaviour
{
    [SerializeField] private BrickHealth _brickHealth;
    private void OnEnable()
    {
        _brickHealth.DieEvent.AddListener(DestroyAllBricks);
    }

    private void OnDisable()
    {
        _brickHealth.DieEvent.RemoveListener(DestroyAllBricks);
    }

    private void DestroyAllBricks()
    {
        _brickHealth.DieEvent.RemoveListener(DestroyAllBricks);
        var bricks = FindObjectsOfType<BrickDie>();

        foreach (var brick in bricks)
        {
            BrickHealth brickHealth = brick.GetComponent<BrickHealth>();

            brickHealth.enabled = true;
            int health = brickHealth.GetHealth();
            brickHealth.GetDamage(health + 1);
        }
    }
}
