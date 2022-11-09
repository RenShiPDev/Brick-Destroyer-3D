using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDie : MonoBehaviour
{
    [SerializeField] private BrickHealth _brickHealth;
    [SerializeField] private GameObject _fractureObject;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _explosionForce;

    private List<GameObject> _fractures = new List<GameObject>();

    private void OnEnable()
    {
        if (_fractureObject != null)
        {
            _brickHealth.DieEvent.AddListener(OnDie);

            _fractureObject.SetActive(false);
            _fractureObject.transform.parent = gameObject.transform;
        }
        else
            this.enabled = false;
    }
    private void OnDisable()
    {
        _brickHealth.DieEvent.RemoveListener(OnDie);
    }

    private void Start()
    {
        for (int i = 0; i < _fractureObject.transform.childCount; i++)
        {
            _fractures.Add(_fractureObject.transform.GetChild(i).gameObject);
        }
    }

    private void OnDie()
    {
        foreach(var frac in _fractures)
        {
            frac.GetComponent<MeshRenderer>().material = _meshRenderer.material;
            frac.GetComponent<Rigidbody>().velocity = frac.transform.localPosition.normalized * _explosionForce;
        }
        _fractureObject.transform.parent = gameObject.transform.parent;
        _fractureObject.SetActive(true);
    }
}
