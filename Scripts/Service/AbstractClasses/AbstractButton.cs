using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractButton : MonoBehaviour, IButton
{
    public abstract void OnClick();
}
