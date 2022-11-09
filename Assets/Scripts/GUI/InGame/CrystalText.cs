using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalText : MonoBehaviour
{
    private Text _text;

    private int _crystalCount;

    private void OnEnable()
    {
        _text = GetComponent<Text>();
        _crystalCount = PlayerPrefs.GetInt("Crystals");
        ChangeText();
    }

    public void UpdateText()
    {
        _crystalCount++;
        PlayerPrefs.SetInt("Crystals", _crystalCount);
        ChangeText();
    }
    public void UpdateCount()
    {
        _crystalCount = PlayerPrefs.GetInt("Crystals");
        _text.text = _crystalCount.ToString();
    }

    private void ChangeText()
    {
        _text.text = _crystalCount.ToString();
    }
}
