using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Player : MonoBehaviour
{
    public event UnityAction Died;
    public event UnityAction Won;
    public event UnityAction CoinsCollected;
    public event UnityAction HealthChanged;

    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _activateAfterWin;
    [SerializeField] private UnityEvent _activateOnDisable;

    private int _coins;
    private Vector2 _disablePoint;

    private void Start()
    {
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
        _coins+=value;
        CoinsCollected.Invoke();
    }

    public int GetCoinsCount()
    {
        return _coins;
    }

    public int GetHealthCount()
    {
        return _health;
    }

    public void TakeDamage(int value)
    {
        _health -= value;
        HealthChanged?.Invoke();
    }

    public void Die()
    {
        Died.Invoke();
    }

    public void Disable()
    {
        _health = 0;
        HealthChanged?.Invoke();
        _activateOnDisable.Invoke();
        gameObject.SetActive(false);
    }

    public void Win()
    {
        Won.Invoke();
        _activateAfterWin.Invoke();
    }
}
