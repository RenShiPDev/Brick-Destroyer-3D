using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletMover : MonoBehaviour
{
    public UnityEvent RayCollisionHandler;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private bool _isDied;

    private CannonShooter _cannonShooter;
    private Transform _bulletPos;
    private Rigidbody _bulletRB;

    private Vector3 _targetPosition;

    private float _speed;
    private int _layerMask;
    private bool _isFantom;

    private void OnEnable()
    {
        _isDied = false;
        _bulletRB = GetComponent<Rigidbody>();
        GetComponent<Collider>().enabled = true;

        if (GetComponent<FantomBullet>() != null)
            _isFantom = true;
    }

    private void Start()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Bullet");
        _layerMask = ~_layerMask;
    }

    private void FixedUpdate()
    {
        if (!_isDied)
        {
            _bulletRB.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
            CheckCollisionByRaycast();

            float distance = (transform.position - _bulletPos.transform.position).magnitude;
            if (distance > 33)
                Die();
        }
        else
        {
            if (_isFantom)
            {
                gameObject.SetActive(false);
            }
            else
            {
                transform.LookAt(_bulletPos);
                float distance = (transform.position - _bulletPos.transform.position).magnitude;
                _bulletRB.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime * distance * 2);

                if ((transform.position - _bulletPos.transform.position).magnitude <= 0.1f)
                {
                    _isDied = false;
                    gameObject.SetActive(false);
                    _cannonShooter.PushBullet(gameObject);
                }
            }
        }
        _bulletRB.velocity = Vector3.zero;
    }

    private void CheckCollisionByRaycast()
    {
        Ray Ray = new Ray(transform.position, transform.forward);
        RaycastHit Hit;

        if (Physics.Raycast(Ray, out Hit, 0.15f, _layerMask))
        {
            Debug.DrawLine(Ray.origin, Hit.point, Color.blue);
            Vector3 newDirection = Vector3.Reflect(transform.forward, Hit.normal);
            newDirection.z = 0;

            if (Hit.collider.gameObject.TryGetComponent(out CannonMover cannonMover))
            {
                cannonMover.OnCollision(transform.position);
                Die();
            }

            if (Hit.collider.gameObject.TryGetComponent(out BrickHealth brickHealth))
            {
                brickHealth.GetDamage();
            }

            if (Hit.collider.gameObject.TryGetComponent(out MoveUpBonuse moveUpBonuse))
                if (transform.forward.y < 0)
                {
                    _targetPosition.y = -transform.forward.y;
                    transform.LookAt(_targetPosition);
                    RayCollisionHandler?.Invoke();
                }


            if (Hit.collider.gameObject.TryGetComponent(out Bonuse bonuse))
            {
                bonuse.ActivateBonuse();
            }
            else
            {
                _targetPosition = transform.position + newDirection;
                transform.LookAt(_targetPosition);
                RayCollisionHandler?.Invoke();
            }
        }
    }

    public void Die()
    {
        _bulletRB.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.LookAt(_bulletPos);
        _isDied = true;
    }

    public void InitBullet(float speed, Transform bulletPos, CannonShooter cannonShooter, Material material = null)
    {
        _speed = speed;
        _bulletPos = bulletPos;
        _cannonShooter = cannonShooter;
        _targetPosition = transform.forward;

        if (GetComponent<FantomBullet>() == null)
            _meshRenderer.material = material;
    }
}
