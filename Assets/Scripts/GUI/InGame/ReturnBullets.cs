using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBullets : MonoBehaviour
{
    [SerializeField] private CannonShooter _cannonShooter;

    public void OnClick()
    {
        if (_cannonShooter.CheckShooting())
        {
            _cannonShooter.EndShoot();
            foreach (var bullet in _cannonShooter.GetBullets())
            {
                bullet.GetComponent<BulletMover>().Die();
            }
        }
    }
}
