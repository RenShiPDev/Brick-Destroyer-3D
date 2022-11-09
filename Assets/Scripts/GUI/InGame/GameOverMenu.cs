using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private CannonShooter _cannonShooter;
    private bool _isSetted;

    public void GameOver()
    {
        if(!_isSetted)
            GetComponent<ObjectHidder>().ChangeVisibility();
        _cannonShooter.gameObject.SetActive(false);

        _isSetted = true;
    }
}
