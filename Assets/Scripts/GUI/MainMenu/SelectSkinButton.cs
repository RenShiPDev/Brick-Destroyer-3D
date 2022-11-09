using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectSkinButton : MonoBehaviour
{
    [SerializeField] private GameObject _priceObject;
    [SerializeField] private Transform _selectorTransform;
    [SerializeField] private Text _priceText;

    private SkinsSpawner _spawner;
    private CrystalText _crystalText;
    private GameObject _selectImage;
    private string _materialName;
    private int _price;

    public void Initialize(int id, SkinsSpawner spawner, GameObject selector)
    {
        _price = (int)(id * 21.4189268f);
        _priceText.text = _price.ToString();
        _materialName = "Mat_" + (id + 1);
        _spawner = spawner;
        _selectImage = selector;

        _crystalText = GameObject.FindObjectOfType<CrystalText>();

        OnMaterialChanged();
    }

    public void OnClick()
    {
        if (FindCurrentMaterial(_materialName))
        {
            PlayerPrefs.SetString("CurrentBullet", _materialName);
        }
        else
        {
            int crystals = PlayerPrefs.GetInt("Crystals");
            if (crystals >= _price)
            {
                SaveMaterial(_materialName);
            }
        }
        OnMaterialChanged();
    }

    private void SaveMaterial(string materialName)
    {

        string path = Application.persistentDataPath + "/Skins";
        string fileName = "OpenedSkins.txt";

        var streamWriter = new StreamWriter(path + "/" + fileName, true);

        streamWriter.WriteLine(materialName);
        streamWriter.Close();

        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") - _price);
        _priceObject.SetActive(false);
        PlayerPrefs.SetString("CurrentBullet", _materialName);
        _crystalText.UpdateCount();
    }

    private bool FindCurrentMaterial(string materialName)
    {
        string path = Application.persistentDataPath + "/Skins";
        string fileName = "OpenedSkins.txt";

        var streamReader = new StreamReader(path + "/" + fileName);

        string nextLine = "";
        while ((nextLine = streamReader.ReadLine()) != null)
        {
            if (materialName == nextLine)
            {
                streamReader.Close();
                return true;
            }
        }

        streamReader.Close();
        return false;
    }

    private void OnMaterialChanged()
    {
        if(_materialName == PlayerPrefs.GetString("CurrentBullet"))
        {
            _selectImage.transform.position = _selectorTransform.position;
        }
    }
}
