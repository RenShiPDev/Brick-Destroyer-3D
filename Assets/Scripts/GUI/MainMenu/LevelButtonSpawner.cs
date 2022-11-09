using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosTransform;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _levelButton;
    [SerializeField] private GameObject _selectorImage;
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private ObjectHidder _hidder;

    private float _buttonHeight;
    private int _maxLevels;
    private int _levelRecord;

    private void OnEnable()
    {
        _hidder.VisibleHandler.AddListener(SetScrollBar);
        _levelRecord = PlayerPrefs.GetInt("LevelRecord");
        //PlayerPrefs.DeleteAll();

        if (_levelRecord == 0)
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.SetInt("LevelRecord", 1);
        }
    }
    private void OnDisable()
    {
        _hidder.VisibleHandler.RemoveListener(SetScrollBar);
    }

    private void Start()
    {
        _maxLevels = Resources.LoadAll<GameObject>("Levels").Length;
        _maxLevels = 50;
        SpawnButtons();
    }

    private void SpawnButtons()
    {
        _buttonHeight = _levelButton.GetComponent<RectTransform>().sizeDelta.y;
        _content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, _buttonHeight * _levelRecord * 2 + _buttonHeight);
        _content.transform.localPosition = Vector3.zero;

        int nextLevel = 0;
        for (int i = 1; i <= _maxLevels; i++)
        {
            for(int j = 0; j < 3; j++, nextLevel++)
            {

                var clone = Instantiate(_levelButton, _content.transform);

                var spawnPos = _spawnPosTransform.localPosition;
                spawnPos.x += Mathf.Abs(spawnPos.x) * j;
                spawnPos.y -= _buttonHeight * (i * 2 - _levelRecord);

                clone.transform.localPosition = spawnPos;

                clone.GetComponent<LevelButton>().SetLevel(nextLevel, _selectorImage);
            }

            if (nextLevel > _levelRecord)
                break;
        }
    }

    private void SetScrollBar()
    {
        _scrollBar.value = 1;
    }
}
