using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class SaveFileChecker : MonoBehaviour
{
    [SerializeField] private DataBase _data; 

    private void Awake()
    {
        CheckSaveFile();
        Menu.Load(_data);
    }

    private void CheckSaveFile()
    {
        if (!PlayerPrefs.HasKey("DataFrog"))
        {
            Debug.Log("Create new save file");
            _data.SaveOptions();
        }
        else
        {
            Debug.Log("Load old file");
            _data.LoadOptions();
        }
    }
}
