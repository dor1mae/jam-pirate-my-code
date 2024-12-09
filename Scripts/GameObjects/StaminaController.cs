using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StaminaController : AbstractBarController
{
    [SerializeField] private int _maxStamina = 100;
    private int _stamina;

    private new void Start()
    {
        base.Start();

        _stamina = _maxStamina;
        SetValue?.Invoke(_stamina);

        StartCoroutine(PassiveStamina());
    }

    private IEnumerator PassiveStamina()
    {
        while (true)
        {
            AddStamina(1);
            yield return new WaitForSeconds(1);
        }
    }

    public bool TryAction(int cost)
    {
        if (_stamina - cost < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RemoveStamina(int cost)
    {
        if (_stamina - cost < 0)
        {
            _stamina = 0;

            SetValue?.Invoke(_stamina);
        }
        else
        {
            _stamina -= cost;
            SetValue?.Invoke(_stamina);
        }
    }

    public void AddStamina(int value)
    {
        if (_stamina + value > _maxStamina)
        {
            _stamina = _maxStamina;
            SetValue?.Invoke(_stamina);
        }
        else
        {
            _stamina += value;
            SetValue?.Invoke(_stamina);
        }
    }
}