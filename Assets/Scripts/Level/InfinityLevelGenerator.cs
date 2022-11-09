using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityLevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _standartBricks = new List<GameObject>();
    [SerializeField] private List<GameObject> _bonuces = new List<GameObject>();
    [SerializeField] private Transform _bricksSpawnPos;
    [SerializeField] private LevelGenerator _levelGenerator;

    private List<GameObject> _standartBricksPool = new List<GameObject>();
    private List<GameObject> _bonucesPool = new List<GameObject>();

    private void OnEnable()
    {
        _standartBricksPool = SpawnObjects(_standartBricks, 15);
        _bonucesPool = SpawnObjects(_bonuces, 3);

        _levelGenerator.ChangeHeightEvent.AddListener(SpawnLine);
    }

    private void OnDisable()
    {
        _levelGenerator.ChangeHeightEvent.RemoveListener(SpawnLine);
    }

    public void SpawnLine()
    {
        for (int i = 0; i < 11; i++)
        {
            if (Random.Range(0, 20) == 1)
                continue;

            int xPos = i - 5;
            float ypos = _bricksSpawnPos.localPosition.y;
            Vector3 spawnPos = new Vector3(xPos, ypos, 0);

            bool isBonuce = Random.Range(0, 20) == 1 ? true : false;

            if (isBonuce)
                SpawnBricks(_bonucesPool, spawnPos);
            else
                SpawnBricks(_standartBricksPool, spawnPos);
        }
    }

    private List<GameObject> SpawnObjects(List<GameObject> objects, int count)
    {
        var pool = new List<GameObject>();

        for(int i = 0; i < objects.Count; i++)
            for(int j = 0; j < count; j++)
            {
                var clone = Instantiate(objects[i], transform);
                clone.gameObject.SetActive(false);
                clone.transform.localPosition = _bricksSpawnPos.localPosition;

                pool.Add(clone);
            }

        return pool;
    }

    private void SpawnBricks(List<GameObject> pool, Vector3 position)
    {
        System.Random random = new System.Random();

        for(int i = 0; i < pool.Count; i++)
        {
            var clone = pool[random.Next(0, pool.Count)];
            if (!clone.activeSelf)
            {
                clone.SetActive(true);
                clone.transform.localPosition = position;
                clone.transform.localScale = Vector3.zero;

                if (clone.TryGetComponent(out BrickHealth brickHealth))
                    brickHealth.SetHealth(1, Random.Range(1, 80));

                break;
            }
        }
    }
}
