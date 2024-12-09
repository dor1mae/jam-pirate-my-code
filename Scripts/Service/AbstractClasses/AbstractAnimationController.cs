using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class AbstractAnimationController : MonoBehaviour, ISimpleUIAnimation
{
    [SerializeField] protected Animator animator;

    public void OnOpenAnimation(InputAction.CallbackContext context)
    {
        Debug.Log($"{gameObject.name}: OnOpenAnimation");
        if(context.started && UIManager.Instance.IsOpened == null && !UIManager.Instance.IsAnimation)
        {
            UIManager.SendOpenedElem.Invoke(this);
            StartCoroutine(OpenAnimation());
        }
    }

    public void OnCloseAnimation(InputAction.CallbackContext context)
    {
        Debug.Log($"{gameObject.name}: OnCloseAnimation");
        if (context.started && UIManager.Instance.IsOpened == this && !UIManager.Instance.IsAnimation)
        {
            StartCoroutine(CloseAnimation());
            UIManager.CloseOpenedElem.Invoke();
        }
    }

    protected abstract IEnumerator OpenAnimation();
    protected abstract IEnumerator CloseAnimation();
}
