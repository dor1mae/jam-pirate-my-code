using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private Image _itemSlotImage;
    [SerializeField] private Image _itemBackground;

    [SerializeField] private Sprite _simpleBackground;
    [SerializeField] private Sprite _lightBackground;

    private Vector3 offset;
    private Canvas Canvas;

    private AbstractItem _item;

    private Tile _placeForItem;

    public AbstractItem Item => _item;

    private bool _isDragged = false;

    private bool _isVertical = true;

    public bool _isChanged = false;

    private const int vertical = 0;
    private const int horizontal = 90;

    public bool IsVertical => _isVertical;

    private void Start()
    {
        Canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (_isDragged)
        {
            if(Input.GetKeyUp(KeyCode.R))
            {
                RotateImage();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragged = true;
        EventBus.SendTile += OnSendTile;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var Object = _itemBackground.gameObject;

        Vector2 localPosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        Object.transform as RectTransform,
        Input.mousePosition,
        null, //this is the thing for your camera
        out localPosition); //считаем значение позиции относительно локальной координатной плоскости

        (Object.transform as RectTransform).position = (Object.transform as RectTransform).TransformPoint(localPosition); // назначаем новую позицию
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragged = false;
        EventBus.SendTile -= OnSendTile;

        EventBus.SetItemInNewPlace?.Invoke(this, _placeForItem);

        Debug.Log($"{transform.position.x}, {transform.position.y}");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemBackground.sprite = _lightBackground;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemBackground.sprite = _simpleBackground;
    }

    private void RotateImage()
    {
        var Object = _itemBackground.gameObject;

        if (_isChanged)
        {
            _isChanged = false;
        }
        else _isChanged = true;

        if (_isVertical)
        {
            Object.transform.rotation = Quaternion.Euler(new Vector3(Object.transform.rotation.x, Object.transform.rotation.x, horizontal));
            Item.RotateHorizontal();
            _isVertical = false;
        }
        else
        {
            Object.transform.rotation = Quaternion.Euler(new Vector3(Object.transform.rotation.x, Object.transform.rotation.x, vertical));
            Item.RotateVertical();
            _isVertical = true;
        }
    }

    public void RotateImageOutObject()
    {
        var Object = _itemBackground.gameObject;

        if (_isVertical)
        {
            Object.transform.rotation = Quaternion.Euler(new Vector3(Object.transform.rotation.x, Object.transform.rotation.y, horizontal));
            Item.RotateHorizontal();
            _isVertical = false;
        }
        else
        {
            Object.transform.rotation = Quaternion.Euler(new Vector3(Object.transform.rotation.x, Object.transform.rotation.y, vertical));
            Item.RotateVertical();
            _isVertical = true;
        }
    }

    public void SetImage(AbstractItem item)
    {
        _item = item;

        _itemSlotImage.sprite = _item.ScriptableItem.InventorySprite;
    }

    private void OnSendTile(Tile tile)
    {
        _placeForItem = tile;
    }

    public GameObject GetGameObject()
    {
        return _itemBackground.gameObject;
    }

    private void OnDestroy()
    {
        _item.UnlinkTiles();
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventorySelector.ChooseItem?.Invoke(_item);
    }
}