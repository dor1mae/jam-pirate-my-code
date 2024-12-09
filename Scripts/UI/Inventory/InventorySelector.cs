using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelector : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    private AbstractItem _selectedItem;

    public static Action<AbstractItem> ChooseItem;

    private void Start()
    {
        ChooseItem += OnChooseItem;
    }

    private void OnChooseItem(AbstractItem item)
    {
        _selectedItem = item;

        if(item is UsableItem)
        {
            _button.SetActive(true);

            var button = _button.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(
                () =>
                {
                    item.Use();
                }
                );
        }
        else
        {
            _button.SetActive(false);
            _selectedItem = null;
        }
    }
}
