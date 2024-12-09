using System.Collections;
using UnityEngine;

public class InventoryAnimationController : AbstractAnimationController
{
    protected override IEnumerator CloseAnimation()
    {
        LockCursor();
        PauseManager.ResumeGame.Invoke();

        UIManager.StartAnimation.Invoke();
        
        animator.SetBool("isOpen", false);
        animator.Play("Close");

        yield return new WaitForSeconds(1);

        UIManager.EndAnimation.Invoke();
    }

    protected override IEnumerator OpenAnimation()
    {
        UnlockCursor();
        UIManager.StartAnimation.Invoke();

        animator.SetBool("isOpen", true);
        animator.Play("Open");

        yield return new WaitForSeconds(1);

        UIManager.EndAnimation.Invoke();

        PauseManager.PauseGame.Invoke();
    }

    protected void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    protected void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
