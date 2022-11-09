using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private SceneLoader _loader;
    public void OnClick()
    {
        _loader.Load("GameScene", SceneManager.GetActiveScene().name);
    }
}
