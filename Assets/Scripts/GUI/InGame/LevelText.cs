using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    private void OnEnable()
    {
        var thisText = GetComponent<Text>();
        int level = PlayerPrefs.GetInt("CurrentLevel");

        if (Resources.LoadAll<GameObject>("Levels").Length < level)
            thisText.text = "Level: Infinity";
        else
            thisText.text = "Level: " + level;
    }
}
