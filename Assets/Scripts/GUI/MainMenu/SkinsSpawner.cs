using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class SkinsSpawner : MonoBehaviour
{
    public UnityEvent SelectSkinHandler;

    [SerializeField] private GameObject _skinPrefab;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject _selectorImage;

    [SerializeField] private float _height;
    private void OnEnable()
    {
        InitFolder();

       if ( PlayerPrefs.GetString("CurrentBullet") == "")
       { 
            PlayerPrefs.SetString("CurrentBullet", "Mat_1");
       }

       for (int i = 0; i < 12; i++)
        {
            float xPos = (i % 3) * _spawnPos.localPosition.x;
            SpawnSkin(-xPos, -_height * (i / 3), i);
        }
    }

    private void InitFolder()
    {
        if (PlayerPrefs.GetInt("GameInitialized") == 0)
        {
            string path = Application.persistentDataPath + "/Skins";
            string fileName = "OpenedSkins.txt";

            DirectoryInfo dir = Directory.CreateDirectory(path);

            var file = File.Open(path + "/" + fileName, FileMode.OpenOrCreate);
            file.Close();

            var streamWriter = new StreamWriter(path + "/" + fileName, true);
            streamWriter.WriteLine("Mat_1");
            streamWriter.WriteLine("Mat_2");
            streamWriter.Close();

            PlayerPrefs.SetInt("GameInitialized", 1);
        }
    }

    private Material GetMaterial(int id)
    {
        string matName = "BulletMaterials/Mat_";
        return Resources.Load<Material>(matName + (id + 1));
    }

    private void SpawnSkin(float x, float y, int id)
    {
        var clone = Instantiate(_skinPrefab, transform);
        clone.transform.localPosition = _spawnPos.localPosition;
        clone.transform.localPosition += new Vector3(x , y, 0);

        clone.GetComponent<SkinObject>().Initialize(GetMaterial(id), id, this, _selectorImage);
    }
}
