﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Global",menuName = "Create new Global file")]
public class Global : ScriptableObject
{
    private List<SceneOption> _sceneOptions = new List<SceneOption>();

    public bool IsGetEvaluation(int levelNumber,out int evaluation)
    {
        evaluation = 0;
        foreach (var sceneOption in _sceneOptions)
        {
            if(sceneOption.NumberLevel==levelNumber)
            {
                evaluation = sceneOption.Evaluation;
                return true;
            }
        }
        return false;
    }

    public void GetAllOptions()
    {
        SaveOptions();
        _sceneOptions = null;
        LoadOptions();
        foreach (var sceneOption in _sceneOptions)
        {
            Debug.Log(sceneOption.NumberLevel + " " + sceneOption.Evaluation);
        }
    }

    public void RemoveDataOptions()
    {
        PlayerPrefs.DeleteKey("DataFrog");
    }

    public void SaveOptions()
    {
        SaveOptions saveData = new SaveOptions(_sceneOptions);
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("DataFrog", json);
    }

    public void LoadOptions()
    {
        _sceneOptions = JsonUtility.FromJson<SaveOptions>(PlayerPrefs.GetString("DataFrog")).getList();
    }

    public void CheckingLevelOnRegisteredEvaluation(int levelNumber)
    {
        if(!IsGetEvaluation(levelNumber,out _))
            _sceneOptions.Add(new SceneOption {NumberLevel = levelNumber, Evaluation = 0 });
    }

    public void ChangeEvaluation(int levelNumber, int evaluation)
    {
        foreach (var sceneOption in _sceneOptions)
        {
            if (sceneOption.NumberLevel == levelNumber)
            {
                sceneOption.Evaluation = evaluation;
                return;
            }
        }
    }
}


public class SaveOptions
{
    [SerializeField]
    private List<SceneOption> _list = new List<SceneOption>();

    public SaveOptions(List<SceneOption> list)
    {
        _list = list;
    }

    public List<SceneOption> getList()
    {
        return _list;
    }
}

[Serializable]
public class SceneOption
{
    public int NumberLevel;
    public int Evaluation;
}
