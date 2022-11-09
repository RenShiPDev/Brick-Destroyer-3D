using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SkinObject : MonoBehaviour
{
    [SerializeField] private GameObject _priceObject;
    [SerializeField] private SelectSkinButton _skinButton;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void Initialize(Material material, int id, SkinsSpawner spawner, GameObject selectorImage)
    {
        _meshRenderer.material = material;

        if ( FindCurrentMaterial(material) )
        {
            _priceObject.SetActive(false);
        }
        _skinButton.Initialize(id, spawner, selectorImage);
    }

    private bool FindCurrentMaterial(Material material)
    {
        string path = Application.persistentDataPath + "/Skins";
        string fileName = "OpenedSkins.txt";

        var streamReader = new StreamReader(path + "/" + fileName);

        string nextLine = "";
        while ((nextLine = streamReader.ReadLine()) != null)
        {
            if(material.name == nextLine)
            {
                streamReader.Close();
                return true;
            }
        }

        streamReader.Close();
        return false;
    }
}
