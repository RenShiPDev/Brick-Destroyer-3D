using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomBullet : MonoBehaviour
{
    [SerializeField] private BulletMover _bulletMover;

    [SerializeField] private int _health;

    public void OnEnable()
    {
        _bulletMover.RayCollisionHandler.AddListener(OnBulletCollision);
    }

    public void OnDisable()
    {
        _bulletMover.RayCollisionHandler.RemoveListener(OnBulletCollision);
    }

    public void OnBulletCollision()
    {
        _health--;

        if (_health <= 0)
            gameObject.SetActive(false);
    }
}
