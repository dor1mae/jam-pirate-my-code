using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : AbstractButton
{
    public override void OnClick()
    {
        Debug.Log("Exit");

        Application.Quit();
    }
}
