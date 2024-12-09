using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractScriptableItem: ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected int _xTileCount;
    [SerializeField] protected int _yTileCount;
    [SerializeField] protected Sprite _inventorySprite;
    [SerializeField] protected int _weight;

    public string Name => _name;
    public string Description => _description;
    public int XTileCount => _xTileCount;
    public int YTileCount => _yTileCount;
    public Sprite InventorySprite => _inventorySprite;
    public int Weight => _weight;
}
