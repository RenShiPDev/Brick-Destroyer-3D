using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomBulletsBonuse : MonoBehaviour
{
    private Stack<GameObject> _bullets = new Stack<GameObject>();
    private GameObject _fantomBullet;
    private CannonShooter _cannonShooter;
    

    private int _health;

    public void OnEnable()
    {
        _cannonShooter = FindObjectOfType<CannonShooter>();
        _health = GetComponent<BrickHealth>().GetHealth();

        _fantomBullet = Resources.Load<GameObject>("Bullets/FantomBullet");

        for(int i = 0; i < _health; i++)
        {
            var clone = Instantiate(_fantomBullet);
            clone.SetActive(false);

            _bullets.Push(clone);
        }
    }

    public void ShootBullet()
    {
        var clone = _bullets.Pop();
        clone.transform.position = transform.position;

        Vector2 randDirectionV2 = new Vector2(Random.Range(-1f, 2f), Random.Range(-1f, 2f));

        clone.transform.LookAt(clone.transform.position + new Vector3(randDirectionV2.x, randDirectionV2.y, 0));
        clone.GetComponent<BulletMover>().InitBullet(6, _cannonShooter.transform, _cannonShooter);
        clone.SetActive(true);
    }

}
