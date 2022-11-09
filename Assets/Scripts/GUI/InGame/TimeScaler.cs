using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private CannonShooter _shooter;
    [SerializeField] private Text _text;
    [SerializeField] private float _scaleTimeOut;

    private float _timer;
    private float _textTimer;
    private int _scaleStage;

    private void Update()
    {
        if (_shooter.CheckShooting())
        {
            _timer += Time.deltaTime;
            if(_timer >= _scaleTimeOut*(1 + _scaleStage* _scaleTimeOut))
            {
                _scaleStage++;
                Time.timeScale = _scaleStage + 1;
                _timer = 0;
                _text.text = (_scaleStage+1).ToString() + "x";
            }

            if(_text.text != "")
            {
                _textTimer += Time.deltaTime;
                if(_textTimer >= (1+_scaleStage))
                {
                    _text.text = "";
                    _textTimer = 0;
                }
            }
        }
        else
        {
            _text.text = "";
            _timer = 0;
            _scaleStage = 0;
            Time.timeScale = 1;
        }
    }
}
