using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToStart = new List<GameObject>();

    private void Start()
    {
        foreach (var obj in _objectsToStart)
            obj.SetActive(true);
    }
}
