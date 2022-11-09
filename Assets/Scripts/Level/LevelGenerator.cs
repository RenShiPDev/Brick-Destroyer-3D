using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public UnityEvent ChangeHeightEvent;

    [SerializeField] private Transform _limitTransform;
    [SerializeField] private LevelCompleteMenu _levelCompleteMenu;
    [SerializeField] private InfinityLevelGenerator _infinityLevelGenerator;

    [SerializeField] private float _speed;
    [SerializeField] private int _level;
    [SerializeField] private bool _isInfinityLevel;

    private int _levelID;

    private void OnEnable()
    {
        _levelID = PlayerPrefs.GetInt("CurrentLevel");
        if(_level != 0)
            _levelID = _level;

        if (_levelID == 0)
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.SetInt("LevelRecord", 1);
            _levelID = 1;
        }

        if (Resources.LoadAll<GameObject>("Levels").Length < _levelID)
            _isInfinityLevel = true;

        if (!_isInfinityLevel)
            SpawnRoom();
        else
        {
            _levelCompleteMenu.gameObject.SetActive(false);
            _infinityLevelGenerator.SpawnLine();
            _levelCompleteMenu.gameObject.SetActive(false);
            _infinityLevelGenerator.enabled = true;
        }
    }

    public void ChangeHeight()
    {
        ChangeHeightEvent?.Invoke();
    }

    private void SpawnRoom()
    {
        if(Resources.LoadAll<GameObject>("Levels").Length >= _levelID)
        {
            var room = Resources.Load<GameObject>("Levels/Room" + _levelID);
            var clone = Instantiate(room, gameObject.transform);
            clone.transform.position = Vector3.zero;
            clone.transform.localScale = Vector3.one;
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
            SceneManager.LoadScene("GameScene");
        }
    }
}
