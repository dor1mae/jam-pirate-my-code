using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;

    [SerializeField] private AttackController _attackController;

    [SerializeField] private HealthController _healthController;

    [SerializeField] private StaminaController _staminaController;

    public static Func<StaminaController> GetStaminaController;
    public static Func<HealthController> GetHealthController;

    private void Init()
    {
        _attackController.SetParams(_staminaController);

        GetStaminaController += () =>
            {
                return _staminaController;
            };

        GetHealthController += () =>
        {
            return _healthController;
        };
    }

    private void Start()
    {
        Init();
        EventBus.Death += DieMaybe;
    }

    public void HitPlayer()
    {
        _healthController.RemoveHealth(25);
    }

    private void DieMaybe(HealthController sender)
    {
        Hero senderType = sender.GetComponentInParent<Hero>();
        if (senderType != null) 
        {
            Debug.Log("Would quit if this wasn't in the editor!");
            Application.Quit();
        }
    }
}
