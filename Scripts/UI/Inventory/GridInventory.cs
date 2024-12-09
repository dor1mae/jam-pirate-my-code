using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GridInventory : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private int _tileXCount;
    [SerializeField] private int _tileYCount;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _itemSlot;
    [SerializeField] private RectTransform _itemFieldTransform;

    private ItemRenderer _itemDrawer;
    private Tile[,] _tileList;
    private List<AbstractItem> _items = new List<AbstractItem>();

    private float _tileWidth;
    private float _tileHeight;

    public static Action<AbstractItem> SendItem;

    public List<AbstractItem> Items => _items;

    private void SetGrid()
    {
        _tileList = new Tile[_tileXCount, _tileYCount];

        var _width = _image.rectTransform.rect.width;
        var _height = _image.rectTransform.rect.height;

        _tileWidth = _width / _tileXCount;
        _tileHeight = _height / _tileYCount;

        _grid.cellSize = new Vector2(_tileWidth, _tileHeight);

        int count = 1;
        for (var i = 0; i < _tileXCount; i++)
        {
            for (var j = 0; j < _tileYCount; j++)
            {
                var newTileObject = Instantiate(_tilePrefab);
                var tileClass = newTileObject.GetComponent<Tile>();

                tileClass.SetParams(i, j, _tileWidth, _tileHeight, this);
                
                _tileList[i, j] = tileClass;

                //Debug.Log($"Создан тайл в координате {tileClass.GetXY()}");

                newTileObject.transform.SetParent(_grid.transform, false);

                count++;
            }
        }

        _itemDrawer = new(_tileYCount, _tileXCount, _tileList, _itemFieldTransform, _tileWidth, _tileHeight);
    }

    public void AddItem(AbstractItem item)
    {
        _items.Add(item);
        var itemUI = _itemDrawer.AddItem(item, _itemSlot);
        if(itemUI) item.LinkInventoryAndUI(this, itemUI);
    }

    public bool SetItem(ItemUIObject itemUI, Tile tile)
    {
        return _itemDrawer.SetItem(itemUI, tile);
    }

    public void RemoveItem(AbstractItem item)
    {
        Destroy(item.ItemUIObject.gameObject);
        Items.Remove(item);
    }

    private void Start()
    {
        SetGrid();

        EventBus.SetItemInNewPlace += SetItem;
        SendItem += AddItem;
    }
}
