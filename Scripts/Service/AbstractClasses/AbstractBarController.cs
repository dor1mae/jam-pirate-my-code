using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractBarController: MonoBehaviour
{
    public Action<float> ChangeValue;
    public Action<float> SetValue;
    public Func<float> GetValue;

    [SerializeField] protected AbstractBar _bar;

    protected void Start()
    {
        if (_bar)
        {
            ChangeValue += _bar.ChangeValue;
            SetValue += _bar.SetValue;
            GetValue += _bar.GetValue;
        }
    }
}
    
