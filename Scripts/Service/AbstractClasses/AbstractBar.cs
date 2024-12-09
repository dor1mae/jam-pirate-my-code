using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractBar : MonoBehaviour, IBar
{
    [SerializeField] protected Image _imageFilled;
    public abstract void ChangeValue(float value);
    public abstract float GetValue();
    public abstract void SetValue(float value);
}

