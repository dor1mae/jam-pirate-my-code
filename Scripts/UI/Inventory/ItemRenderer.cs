using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemRenderer
{
    private int _tileYCount;
    private int _tileXCount;
    private Tile[,] _tileList;
    private RectTransform _itemFieldTransform;
    private float _tileWidth;
    private float _tileHeight;

    public ItemRenderer(int tileYCount, int tileXCount, Tile[,] tiles, RectTransform itemFieldTransform, float width, float height)
    {
        _itemFieldTransform = itemFieldTransform;
        _tileWidth = width;
        _tileHeight = height;
        _tileXCount = tileXCount;
        _tileYCount = tileYCount;
        _tileList = tiles;
    }

    public ItemUIObject AddItem(AbstractItem item, GameObject itemSlot)
    {
        var tileChecker = new TileChecker(_tileList, _tileYCount, _tileXCount);

        var tempList = tileChecker.CheckStartTile(item);

        if (tempList == null)
        {
            Debug.Log("Не хватает места в инвентаре");

            return null;
        }

        Debug.Log($"Найдено {tempList.Count} ячеек");

        item.LinkTiles(tempList);
        foreach (var tile in item.BusyTiles)
        {
            tile.LinkItem(item);
        }

        return DrawItem(item, itemSlot);
    }

    public bool SetItem(ItemUIObject itemUI, Tile tile)
    {
        var item = itemUI.Item;
        var tileChecker = new TileChecker(_tileList, _tileYCount, _tileXCount);

        var tempList = tileChecker.CheckStartTile(item, tile);

        if (tempList == null)
        {
            Debug.Log("Не хватает места в инвентаре");

            ReplaceItem(itemUI, itemUI.Item.BusyTiles);

            return false;
        }

        Debug.Log($"Найдено {tempList.Count} ячеек");

        ReplaceItem(itemUI, tempList); //последовательность привязки тайла к предмету и предмета к группе тайлов такая, потому что важен возврат из функции, который может привезти к полной очистке листа

        item.UnlinkTiles();

        item.LinkTiles(tempList);
        foreach (var t in tempList)
        {
            t.LinkItem(item);
        }

        return true;
    }

    public ItemUIObject DrawItem(AbstractItem item, GameObject _itemSlot)
    {
        float startX = -1; float startY = -1;
        Tile tile = null;

        for (int i = 0; i < _tileXCount; i++)
        {
            for (int j = 0; j < _tileYCount; j++)
            {
                if (_tileList[i, j].Item == item)
                {
                    tile = _tileList[i, j];

                    if (startX == -1 && startY == -1)
                    {
                        startX = tile.TransformY;
                        startY = tile.TransformX;
                        break;
                    }
                }
            }
        }

        Debug.Log($"Итоговый предмет лежит в координатах {startX};{startY}");

        var itemObject = MonoBehaviour.Instantiate(_itemSlot);
        var itemUIObject = itemObject.GetComponentInChildren<ItemUIObject>();
        itemUIObject.SetImage(item);
        (itemObject.transform as RectTransform).SetParent(_itemFieldTransform, false);
        (itemObject.transform as RectTransform).localPosition = new Vector2(startX, -startY);
        (itemObject.transform as RectTransform).sizeDelta = new Vector2(item.ScriptableItem.XTileCount * _tileWidth, item.ScriptableItem.YTileCount * _tileHeight);

        return itemUIObject;
    }

    public void ReplaceItem(ItemUIObject item, List<Tile> newPlace)
    {
        float startX = -1; float startY = -1;
        Tile tile = null;

        for (int i = 0; i < _tileXCount; i++)
        {
            for (int j = 0; j < _tileYCount; j++)
            {
                if (newPlace.Find(tile => (tile.X == i && tile.Y == j)))
                {
                    tile = _tileList[i, j];

                    if (startX == -1 && startY == -1)
                    {
                        startX = tile.TransformY;
                        startY = tile.TransformX;
                        break;
                    }
                }
            }
        }

        Debug.Log($"Итоговый предмет лежит в координатах {startX};{startY}");

        if (item.IsVertical)
        {
            var itemObject = item.GetGameObject();
            (itemObject.transform as RectTransform).localPosition = new Vector2(startX, -startY);
            (itemObject.transform as RectTransform).sizeDelta = new Vector2(item.Item.ScriptableItem.XTileCount * _tileWidth, item.Item.ScriptableItem.YTileCount * _tileHeight);
        }
        else
        {
            var itemObject = item.GetGameObject();
            (itemObject.transform as RectTransform).localPosition = new Vector2(startX, -startY - item.Item.ScriptableItem.XTileCount * _tileHeight);
            (itemObject.transform as RectTransform).sizeDelta = new Vector2(item.Item.ScriptableItem.XTileCount * _tileHeight, item.Item.ScriptableItem.YTileCount * _tileWidth);
        }

        //CheckChanged(item);
    }

    private void CheckChanged(ItemUIObject item)
    {
        if (item._isChanged)
        {
            item.RotateImageOutObject();
            item._isChanged = false;
        }
    }
}
