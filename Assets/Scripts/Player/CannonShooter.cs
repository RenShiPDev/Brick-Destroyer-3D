using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonShooter : MonoBehaviour
{
    [SerializeField] private TrackDrawer _drawer;
    [SerializeField] private CannonRotater _rotater;
    [SerializeField] private CannonMover _cannonMover;
    [SerializeField] private BrickHealthMaximator _healthMaximator;
    [SerializeField] private LevelCompleteMenu _levelCompleteMenu;
    [SerializeField] private LevelGenerator _levelGenerator;

    [SerializeField] private GameObject _bulletPool;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _cannon;
    [SerializeField] private GameObject _limitObject;
    [SerializeField] private Text _countText;
    [SerializeField] private Transform _bulletPosition;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootSpeed;
    [SerializeField] private int _bulletCount;

    private Stack<GameObject> _bullets = new Stack<GameObject>();
    private List<GameObject> _spawnedBullets = new List<GameObject>();

    private Vector3 _shootDirectionV3;

    private float _timer;
    private int _shootCount;
    private bool _isShoot;
    private bool _isShootAccepted;

    private void OnDisable()
    {
        _countText.text = "";
    }
    private void Start()
    {
        for (int i = 0; i < _bulletCount; i++)
            InitializeBullet();

        SetCountText();
    }

    private void Update()
    {
        OnMouseTouch();

        if (!_isShootAccepted)
            EnableShoot();
    }

    private void FixedUpdate()
    {
        SetCountText();

        if (_isShoot)
            Shoot();
    }

    public void PushBullet(GameObject bullet)
    {
        _bullets.Push(bullet);
    }

    public bool CheckShooting()
    {
        return _isShoot || _bullets.Count != _bulletCount;
    }

    public void EndShoot()
    {
        _isShootAccepted = false;
        _isShoot = false;
        _shootCount = 0;
    }

    public List<GameObject> GetBullets()
    {
        return _spawnedBullets;
    }

    public void AddBullets()
    {
        _bulletCount++;
        InitializeBullet();
    }

    private void OnMouseTouch()
    {
        if (Input.GetMouseButtonUp(0) && !_isShoot && _bullets.Count == _bulletCount)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if(mouseHit.collider.gameObject.GetComponent<SafeZone>() == null)
                {
                    if (mouseHit.point.y <= _limitObject.transform.position.y)
                        mouseHit.point = new Vector3(mouseHit.point.x, _limitObject.transform.position.y, mouseHit.point.z);

                    _shootDirectionV3 = (mouseHit.point - _cannon.transform.position).normalized;
                    _shootDirectionV3.z = 0;
                    _isShoot = true;
                    _rotater.enabled = false;
                    _drawer.enabled = false;
                }
            }
        }
    }

    private void EnableShoot()
    {
        if (!_isShoot && _bullets.Count == _bulletCount)
        {
            _rotater.enabled = true;
            _drawer.enabled = true;

            _cannonMover.MoveToNewPosition();
            _healthMaximator.ResetMinMaxHealth();
            _levelGenerator.ChangeHeight();
            _levelCompleteMenu.CheckBricks();

            _isShootAccepted = true;

            _shootCount = 0;
        }
    }

    private void Shoot()
    {
        _timer += Time.deltaTime;

        if (_timer > _shootSpeed)
        {
            if (_shootCount >= _bulletCount)
                EndShoot();
            else
            {
                var bullet = _bullets.Pop();
                bullet.transform.position = _bulletPosition.position;
                bullet.transform.LookAt(bullet.transform.position + _shootDirectionV3);
                bullet.GetComponent<Rigidbody>().isKinematic = false;
                bullet.SetActive(true);
                _timer = 0;
                _shootCount++;
            }
        }
    }

    private void InitializeBullet()
    {
        string matName = "BulletMaterials/" + PlayerPrefs.GetString("CurrentBullet");
        Material material = Resources.Load<Material>(matName);

        var clone = Instantiate(_bullet, _bulletPool.transform);
        clone.GetComponent<BulletMover>().InitBullet(_bulletSpeed, _bulletPosition, this, material);
        clone.SetActive(false);
        _bullets.Push(clone);
        _spawnedBullets.Add(clone);
    }

    private void SetCountText()
    {
        _countText.text = _bullets.Count > 0 ? _bullets.Count.ToString() : "";
    }
}
