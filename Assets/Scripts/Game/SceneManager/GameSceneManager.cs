using UnityEngine;
using IJunior.TypedScenes;

public class GameSceneManager : MonoBehaviour,ISceneLoadHandler<Global>
{
    private Global _global;

    [SerializeField] private int _numberLevel;

    public void OnSceneLoaded(Global global)
    {
        _global = global;
    }

    private void Start()
    {
        _global.CheckingLevelOnRegisteredEvaluation(_numberLevel);
    }

    public void EnterEvaluation(int evaluation)
    {
        _global.ChangeEvaluation(_numberLevel, evaluation);
    }

    public void ReloadScene()
    {
        switch (_numberLevel)
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

    public void SaveAndExitGame()
    {
        _global.SaveOptions();
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Menu.Load(_global);
    }
}
