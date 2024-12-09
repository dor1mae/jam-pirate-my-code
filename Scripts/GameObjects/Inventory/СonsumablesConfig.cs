using UnityEngine;

[CreateAssetMenu(fileName = "СonsumablesConfig", menuName = "ScriptableObjects/Сonsumables", order = 1)]
public class СonsumablesConfig : AbstractScriptableItem
{
    public enum ConsumablesType
    {
        Stamina,
        Health
    }

    [SerializeField] protected int _restoreValue;

    public int RestoreValue => _restoreValue;

    [SerializeField] protected ConsumablesType _type;

    public ConsumablesType Type => _type;
}
