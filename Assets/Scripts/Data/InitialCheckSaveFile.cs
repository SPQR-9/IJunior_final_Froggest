using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class InitialCheckSaveFile : MonoBehaviour
{
    [SerializeField] private Global _global; 
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("DataFrog"))
        {
            Debug.Log("Create new save file");
            _global.SaveOptions();
        }
        else
        {
            Debug.Log("Load old file");
            _global.LoadOptions();
        }
        Menu.Load(_global);
    }
}
