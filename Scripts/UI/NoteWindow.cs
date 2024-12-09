using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class NoteWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private InputActionAsset _actionAsset;
    private InputAction _action;

    [SerializeField] private Button _closeButton;

    public static Action OnOpenWindow;
    public static Action OnCloseWindow;

    private void Awake()
    {
        _action = _actionAsset.FindAction("Inventory");
        _action.performed += (context) =>
        {
            OnCloseWindow.Invoke();
        };

        OnCloseWindow += () =>
        {
            gameObject.SetActive(false);
        };

        OnOpenWindow += () =>
        {
            gameObject.SetActive(true);
        };

        _closeButton.onClick.AddListener( () =>
        {
            OnCloseWindow?.Invoke();
        }
        );

        OnCloseWindow.Invoke();
    }

    public void OpenNote(Note note)
    {
        _title.text = note.Title;
        _text.text = note.Text;
    }
}
