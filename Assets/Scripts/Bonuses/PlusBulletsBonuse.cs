using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBulletsBonuse : MonoBehaviour
{
    private CannonShooter _cannonShooter;

    public void OnEnable()
    {
        _cannonShooter = FindObjectOfType<CannonShooter>();
    }

    public void AddBullet()
    {
        _cannonShooter.AddBullets();
    }
}
