using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static Action StartAnimation;
    public static Action EndAnimation;
    public static Action<AbstractAnimationController> SendOpenedElem;
    public static Action CloseOpenedElem; 

    private AbstractAnimationController _isOpened = null;
    private bool _isAnimation = false;

    public bool IsAnimation => _isAnimation;
    public AbstractAnimationController IsOpened => _isOpened;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Debug.Log($"{GetType()}: is initialized");
            Initialize();

            return;
        }
        Destroy(this.gameObject);
    }

    private void Initialize()
    {
        StartAnimation += () =>
        {
            _isAnimation = true;
        };

        EndAnimation += () =>
        {
            _isAnimation = false;
        };

        SendOpenedElem += (elem) =>
        {
            if (_isOpened == null)
            {
                _isOpened = elem;
            }
        };

        CloseOpenedElem += () =>
        {
            _isOpened = null;
        };
    }
}

