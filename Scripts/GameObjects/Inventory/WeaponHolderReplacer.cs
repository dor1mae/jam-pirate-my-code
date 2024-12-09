using System;
using UnityEngine;

public class WeaponHolderReplacer: MonoBehaviour
{
    public static Action<Weapon> SendWeapon;

    private GameObject _weapon;

    public GameObject Weapon => _weapon;

    private void Start()
    { 
        SendWeapon += OnSendWeapon;
    }

    private void OnSendWeapon(Weapon weapon)
    {
        foreach(Transform item in transform)
        {
            Destroy(item.gameObject);
        }

        _weapon = Instantiate((weapon.ScriptableItem as WeaponConfig).Weapon);
        _weapon.transform.SetParent(transform, false);
    }
}
