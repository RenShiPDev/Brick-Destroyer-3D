using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private CannonShooter _shooter;
    [SerializeField] private TrackDrawer _drawer;

    [SerializeField] private Sprite _pauseImage;
    [SerializeField] private Sprite _resumeImage;
    [SerializeField] private Image _image;

    private bool _isPaused;

    public void OnClick()
    {
        _isPaused = !_isPaused;
        var bullets = FindObjectsOfType<BulletMover>();

        foreach (var bullet in bullets)
        {
            if (bullet.gameObject.activeSelf)
            {
                bullet.enabled = !_isPaused;

                if (_isPaused)
                {
                    _image.sprite = _resumeImage;
                }
                else
                {
                    _image.sprite = _pauseImage;
                }
            }
        }

        _shooter.enabled = !_isPaused;
        _drawer.enabled = !_isPaused;

        if (_isPaused)
        {
            _image.sprite = _resumeImage;
        }
        else
        {
            _image.sprite = _pauseImage;
        }
    }

    private void Update()
    {
    }
}
