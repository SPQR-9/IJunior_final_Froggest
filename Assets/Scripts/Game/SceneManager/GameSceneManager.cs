using UnityEngine;
using IJunior.TypedScenes;

public class GameSceneManager : MonoBehaviour,ISceneLoadHandler<DataBase>
{
    [SerializeField] private int _numberLevel;

    private DataBase _data;

    public void OnSceneLoaded(DataBase data)
    {
        _data = data;
    }

    private void Start()
    {
        _data.CheckingLevelOnRegisteredEvaluation(_numberLevel);
    }

    public void EnterEvaluation(int evaluation)
    {
        _data.ChangeEvaluation(_numberLevel, evaluation);
    }

    public void ReloadScene()
    {
        switch (_numberLevel)
        {
            case 1:
                Game1.Load(_data);
                break;
            case 2:
                Game2.Load(_data);
                break;
            case 3:
                Game3.Load(_data);
                break;
            default:
                Debug.LogError("Error loader data");
                break;
        }
    }

    public void SaveAndExitGame()
    {
        _data.SaveOptions();
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        Menu.Load(_data);
    }
}
