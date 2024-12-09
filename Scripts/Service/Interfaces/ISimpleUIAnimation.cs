using System.Collections;
using UnityEngine.InputSystem;
public interface ISimpleUIAnimation
{
    public void OnOpenAnimation(InputAction.CallbackContext context);
    public void OnCloseAnimation(InputAction.CallbackContext context);
}
