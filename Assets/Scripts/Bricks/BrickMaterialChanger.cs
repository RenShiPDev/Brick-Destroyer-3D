using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMaterialChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _healthMaterials = new List<Material>();
    [SerializeField] private BrickHealth _brickHealth;
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private float _flashTime;

    private BrickHealthMaximator _healthMaximator;
    private Vector3 _startColor;

    private int _health;

    private void OnEnable()
    {
        _brickHealth.HealthChangedEvent += OnHealthChanged;
    }
    private void OnDisable()
    {
        _brickHealth.HealthChangedEvent -= OnHealthChanged;
    }

    private void Start()
    {
        var meshColor = _meshRenderer.material.color;
        Vector3 meshColorV3 = new Vector3(meshColor.r, meshColor.g, meshColor.b);
        _startColor = meshColorV3;
    }

    private void Update()
    {
        if (_health != 0)
            SetColor();
    }

    private void SetColor()
    {
        var meshColor = _meshRenderer.material.color;
        Vector3 meshColorV3 = new Vector3(meshColor.r, meshColor.g, meshColor.b);

        if (meshColorV3 != _startColor)
        {
            Vector3 meshMaterialColor = Vector3.Slerp(meshColorV3, _startColor, _flashTime * Time.deltaTime);
            _meshRenderer.material.color = new Color(meshMaterialColor.x, meshMaterialColor.y, meshMaterialColor.z);
        }
    }

    private void OnHealthChanged(int health, BrickHealthMaximator maximator)
    {
        if (_health != 0)
            _meshRenderer.material.color = new Color(1, 1, 1);

        _health = health;
        _healthMaximator = maximator;

        SetHealthMaterial();
    }

    private void SetHealthMaterial()
    {
        var minMaxHealth = _healthMaximator.GetMinMaxHealth();

        int step = (minMaxHealth[1] - minMaxHealth[0]) / _healthMaterials.Count;
        for (int i = 0; i < _healthMaterials.Count; i++)
            if ((_health + step * i) >= minMaxHealth[1])
            {
                var material = _healthMaterials[_healthMaterials.Count - i - 1];
                var newColor = material.color;

                _meshRenderer.material = material;
                _startColor = new Vector3(material.color.r, material.color.g, material.color.b);

                _meshRenderer.material.color = new Color(newColor.r, newColor.g, newColor.b);
                break;
            }
    }
}
