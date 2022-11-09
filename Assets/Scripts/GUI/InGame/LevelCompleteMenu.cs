using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteMenu : MonoBehaviour
{
    [SerializeField] private GameObject _crystalPrefab;
    [SerializeField] private Transform _crystalSpawnPos;

    private float _timer = 0;
    private bool _crystalSpawned;
    private bool _visibilityChanged;

    private void Update()
    {
        if (_visibilityChanged && !_crystalSpawned)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.3f)
                SpawnCrystals();
        }
    }

    public void CheckBricks()
    {
        int bricksCount = 0;
        foreach (var brick in FindObjectsOfType<BrickHealth>())
            if (brick.gameObject.activeSelf && brick.gameObject.GetComponent<Bonuse>() == null)
                bricksCount++;

        if (bricksCount == 0)
            GameOver();
    }

    private void GameOver()
    {
        GetComponent<ObjectHidder>().ChangeVisibility();

        FindObjectOfType<CannonShooter>().gameObject.SetActive(false);

        _visibilityChanged = true;
    }
     
    private void SpawnCrystals(int count = 10)
    {
        _crystalSpawned = true;
        for (int i = 0; i < count; i++)
        {
            var clone = Instantiate(_crystalPrefab);
            clone.transform.position = _crystalSpawnPos.position;
            clone.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }

        int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("CurrentLevel", nextLevel);

        int levelRecord = PlayerPrefs.GetInt("LevelRecord");

        if (nextLevel > levelRecord)
            PlayerPrefs.SetInt("LevelRecord", levelRecord + 1);
    }
}
