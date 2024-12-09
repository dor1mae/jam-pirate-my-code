using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IRect, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GridInventory _inventory;

    private int _x; //положение тайла как элемента в сетке
    private int _y;
    private float _transformX; //положение тайла на пикселях, выделенных для сетки как элемента UI
    private float _transformY;
    private float _width;
    private float _height;

    private bool _isBusy = false;

    private AbstractItem _item;

    [SerializeField] private Image _image;
    [SerializeField] private Sprite _simpleSprite;
    [SerializeField] private Sprite _lightSprite;
    [SerializeField] private Sprite _testSprite;

    public Image Image => _image;
    public bool IsBusy => _isBusy;
    public AbstractItem Item => _item;
    public int X => _x;
    public int Y => _y;

    public float TransformX => _transformX;
    public float TransformY => _transformY;

    public float Width => _width;
    public float Height => _height;

    public void SetParams(int x, int y, float width, float height, GridInventory inventory)
    {
        _x = x;
        _y = y;
        _width = width;
        _height = height;
        _inventory = inventory;

        SetXYTransform();
    }

    private void SetXYTransform()
    {
        
        if(X == 0)
        {
            _transformX = 0;
        }
        else _transformX = X * _height;

        if (Y == 0)
        {
            _transformY = 0;
        }
        else _transformY = Y * _width;
    }

    public void LinkItem(AbstractItem item)
    {
        _item = item;
        _image.sprite = _testSprite;
        _isBusy = true;
    }

    public void UnlinkItem()
    {
        _item = null;
        _image.sprite = _simpleSprite;
        _isBusy = false;
    }

    public System.Numerics.Vector2 GetWidthAndHeight()
    {
        return new System.Numerics.Vector2(_width, _height);
    }

    public System.Numerics.Vector2 GetXY()
    {
        return new System.Numerics.Vector2(_transformX, _transformY);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(_id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_isBusy)
        {
            _image.sprite = _lightSprite;

            EventBus.SendTile?.Invoke(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isBusy)
        {
            _image.sprite = _simpleSprite;
        }
    }
}
