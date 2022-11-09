using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingBrick : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private BrickHealth _brickHealth;

    private void OnEnable()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _levelGenerator.ChangeHeightEvent.AddListener(ChangeBlock);
    }
    private void OnDisable()
    {
        _levelGenerator.ChangeHeightEvent.RemoveListener(ChangeBlock);
    }

    private void ChangeBlock()
    {
        _brickHealth.enabled = !_brickHealth.enabled;
    }
}
