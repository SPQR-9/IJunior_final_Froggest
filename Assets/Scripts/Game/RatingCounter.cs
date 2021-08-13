using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _coinsForStar = 6;
    [SerializeField] private GameSceneManager _sceneManager;

    private int _maxScoreNumber = 5;
    private int _evaluation;
    private int _coins;

    private void OnEnable()
    {
        _player.NumberOfCoinsCollected += ChangeNumberCoins;
        _player.IsWin += SetFinalEvaluation; 
    }

    private void OnDisable()
    {
        _player.NumberOfCoinsCollected -= ChangeNumberCoins;
        _player.IsWin -= SetFinalEvaluation;
    }

    private void ChangeNumberCoins(int value)
    {
        _coins = value;
        SettingRating();
    }

    private void SettingRating()
    {
        _evaluation = _coins / _coinsForStar;
        Debug.Log(_coins + " / " + _coinsForStar + " = " + _evaluation);
    }

    private void SetFinalEvaluation()
    {
        _sceneManager.EnterEvaluation(_evaluation);
    }

    public int GetEvaluation()
    {
        if (_evaluation > _maxScoreNumber)
            return _maxScoreNumber;
        return _evaluation;
    }
}
