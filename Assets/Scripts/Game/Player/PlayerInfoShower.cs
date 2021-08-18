using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoShower : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _coinText;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _healthText.text = _player.GetHealthCount().ToString();
        _coinText.text = _player.GetCoinsCount().ToString();
    }

    private void OnEnable()
    {
        _player.HealthChanged += ShowHealth;
        _player.CoinsCollected += ShowCoinsCount;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= ShowHealth;
        _player.CoinsCollected -= ShowCoinsCount;
    }

    private void ShowHealth()
    {
        _healthText.text = _player.GetHealthCount().ToString();
    }

    private void ShowCoinsCount()
    {
        _coinText.text = _player.GetCoinsCount().ToString();
    }
}
