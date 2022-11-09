using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Text _levelText;
    [SerializeField] private Transform _selectorPos;
    [SerializeField] private GameObject _lockedImage;

    private GameObject _selector;
    private int _level;
    private int _currentLevel;
    private int _levelRecord;

    public void SetLevel(int level, GameObject selector)
    {
        _level = level + 1;
        _selector = selector;

        _levelText.text = _level.ToString();

        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        _levelRecord = PlayerPrefs.GetInt("LevelRecord");
        if (_level <= _levelRecord)
        {
            _lockedImage.SetActive(false);
            _levelText.gameObject.SetActive(true);
        }
        else
        {
            _lockedImage.SetActive(true);
            _levelText.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        MoveSelector();
    }

    public void OnClick()
    {
        if (_level <= _levelRecord)
            SetLevel();
    }

    private void SetLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", _level);
        _currentLevel = _level;
        MoveSelector();
    }

    private void MoveSelector()
    {
        if (_currentLevel == _level && _selector != null) 
        { 
            _currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            _selector.transform.position = _selectorPos.position;
        }
    }
}
