using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponConfig : AbstractScriptableItem
{
    [SerializeField] protected int _damage;
    [SerializeField] protected int _speed;
    [SerializeField] protected int _staminaCost;
    [SerializeField] protected GameObject _weapon;


    //[SerializeField] private Model _model;

    public int Damage => _damage;
    public int Speed => _speed;
    public int StaminaCost => _staminaCost;

    public GameObject Weapon => _weapon;
}
