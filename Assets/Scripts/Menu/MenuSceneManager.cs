using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class MenuSceneManager : MonoBehaviour,ISceneLoadHandler<Global>
{
    private Global _global;
    
    [SerializeField] private List<Sprite> _starSprites;

    public void OnSceneLoaded(Global global)
    {
        _global = global;
    }

    public Sprite ShowEvoluations(int levelNumber)
    {
        _global.IsGetEvaluation(levelNumber, out int evaluation);
        return _starSprites[evaluation];
    }

    public void SaveAndExitGame()
    {
        _global.SaveOptions();
        Application.Quit();
    }

    public void LoadNewGame(int level)
    {
        switch (level)
        {
            case 1:
                Game1.Load(_global);
                break;
            case 2:
                Game2.Load(_global);
                break;
            case 3:
                Game3.Load(_global);
                break;
            default:
                Debug.LogError("Error loader");
                break;
        }
    }
}
