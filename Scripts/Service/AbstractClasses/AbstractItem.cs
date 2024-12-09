using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : IItem
{
    protected AbstractScriptableItem _scriptedItem;

    public AbstractScriptableItem ScriptableItem => _scriptedItem;

    protected GridInventory _inventory;
    protected ItemUIObject _itemUIObject;

    public ItemUIObject ItemUIObject => _itemUIObject;

    public abstract void Use();

    protected List<Tile> _busyTiles = new List<Tile>();

    public List<Tile> BusyTiles => _busyTiles;

    public int XTileCount;
    public int YTileCount;


    public AbstractItem(AbstractScriptableItem item)
    {
        _scriptedItem = item;

        XTileCount = ScriptableItem.XTileCount;
        YTileCount = ScriptableItem.YTileCount;
    }

    public void LinkTiles(List<Tile> tiles)
    {
        _busyTiles = tiles;
    }

    public void UnlinkTiles()
    {
        foreach (Tile tile in _busyTiles)
        {
            tile.UnlinkItem();
        }
        _busyTiles.Clear();
    }

    public void RotateVertical()
    {
        XTileCount = ScriptableItem.XTileCount;
        YTileCount = ScriptableItem.YTileCount;
    }

    public void RotateHorizontal()
    {
        YTileCount = ScriptableItem.XTileCount;
        XTileCount = ScriptableItem.YTileCount;
    }

    public void LinkInventoryAndUI(GridInventory grid, ItemUIObject obj)
    {
        _inventory = grid;
        _itemUIObject = obj;
    }
}
