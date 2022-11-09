using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBrick : MonoBehaviour
{
    [SerializeField] private GameObject _crystalPrefab;
    [SerializeField] private BrickHealth _brickHealth;
    [SerializeField] private int _crystalsCount;

    private void OnEnable()
    {
        _brickHealth.DieEvent.AddListener(AddCrystals);
    }

    private void OnDisable()
    {
        _brickHealth.DieEvent.RemoveListener(AddCrystals);
    }

    private void AddCrystals()
    {
        for(int i = 0; i < _crystalsCount; i++)
        {
            var clone = Instantiate(_crystalPrefab);
            Vector3 randVector = new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f))/3f;
            clone.transform.position = transform.position+ randVector;
        }
    }
}
