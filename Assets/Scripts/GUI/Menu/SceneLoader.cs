using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Text _loadingText;
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private GameObject _camera;

    [SerializeField] private List<string> _textStages = new List<string>();
    [SerializeField] private float _loadingTime;
    [SerializeField] private float _showSpeed;

    private Color _startColor;

    private string _nextSceneName;
    private string _currentSceneName;
    private float _timer;
    private int _stageId;

    private bool _isLoading;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);

        _camera.SetActive(_isLoading);
        _startColor = _backgroundImage.color;
        _backgroundImage.color = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        if (_isLoading)
        {
            if(_startColor.a > _backgroundImage.color.a)
                _backgroundImage.color += _startColor * _showSpeed;
            else
            {
                if(_timer <= 0)
                {
                    _backgroundImage.color = _startColor;
                    _currentSceneName = _nextSceneName;

                    StartCoroutine(AsyncLoader(_nextSceneName));
                    SceneManager.UnloadSceneAsync(_currentSceneName);

                    _timer += Time.deltaTime;
                }
                else
                {
                    _loadingText.text = _textStages[_stageId];
                    _stageId = (int)_timer % _textStages.Count;
                    _timer += Time.deltaTime;

                    if(_timer >= _loadingTime)
                        Hide();
                }
            }
        }
        else
        {
            _backgroundImage.color -= _backgroundImage.color.a < 0 ? _startColor * _showSpeed : _backgroundImage.color;
            _loadingText.text = "";
        }
    }

    public void Load(string sceneName, string currentScene)
    {
        _isLoading = true;
        _nextSceneName = sceneName;
        _currentSceneName = currentScene;
        SetLoader();
    }

    public void Hide()
    {
        _isLoading = false;
        SetLoader();

        PlayerPrefs.SetInt("AdsCounter", PlayerPrefs.GetInt("AdsCounter") + 1);
        ShowAds();
    }

    private IEnumerator AsyncLoader(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
            yield return null;
    }

    private void SetLoader()
    {
        _camera.SetActive(_isLoading);
        _timer = 0;
        _stageId = 0;
    }

    private void ShowAds()
    {
        int adsCounter = PlayerPrefs.GetInt("AdsCounter");
        Debug.Log(adsCounter);

        //if (adsCounter >= (6 + Random.Range(-2,2)))
        //    FindObjectOfType<InterstitionalAD>().ShowOnDisplay();
    }
}
