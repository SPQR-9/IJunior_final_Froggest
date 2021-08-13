using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _activateAfterWin;
    [SerializeField] private UnityEvent _activateOnDisable;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _coinText;

    public UnityAction IsDie;
    public UnityAction IsWin;
    public UnityAction<int> NumberOfCoinsCollected;

    private int _coin;
    private Vector2 _disablePoint;

    private void Start()
    {
        _healthText.text = _health.ToString();
        _disablePoint = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0));
    }

    private void Update()
    {
        if (_health <= 0)
            Die();
        if (transform.position.y < _disablePoint.y)
            Disable();
    }

    public void GetCoin(int value)
    {
        _coin+=value;
        _coinText.text = _coin.ToString();
        NumberOfCoinsCollected.Invoke(_coin);
    }

    public void TakeDamage(int value)
    {
        _health -= value;
        _healthText.text = _health.ToString();
    }

    public void Die()
    {
        IsDie.Invoke();
    }

    public void Disable()
    {
        _healthText.text = "0";
        _activateOnDisable.Invoke();
        gameObject.SetActive(false);
    }

    public void Win()
    {
        IsWin.Invoke();
        _activateAfterWin.Invoke();
    }
}
