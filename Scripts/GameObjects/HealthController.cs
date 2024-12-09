using System;
using UnityEngine;

public class HealthController : AbstractBarController
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;
    private new void Start()
    {
        if (_bar) base.Start();

        _health = _maxHealth;
        SetValue?.Invoke(_health);
    }

    public void AddHealth(int value)
    {
        if (_health + value <= _maxHealth)
        {
            _health += value;
            SetValue?.Invoke(_health);
        }
        else
        {
            _health = _maxHealth;
            SetValue?.Invoke(_health);
        }
    }

    public void RemoveHealth(int value)
    {
        if (_health - value > 0)
        {
            _health -= value;
            SetValue?.Invoke(_health);
        }
        else OnDeath();
    }

    private void OnDeath()
    {
        SetValue?.Invoke(0);
        EventBus.Death?.Invoke(this);
        Debug.Log($"{gameObject.name} был убит");
    }
}