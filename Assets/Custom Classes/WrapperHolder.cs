using System;
using UnityEngine;

public class WrapperHolder<T>
{
    [SerializeField]
    public WrapperSO<T> dataWrapperSO;
    private T _content = Activator.CreateInstance<T>();

    public T content
    {
        get
        {
            if (dataWrapperSO != null) return dataWrapperSO.content;
            return _content;
        }
        set
        {
            if (dataWrapperSO != null) dataWrapperSO.content = value;
            _content = value;
        }
    }

    public WrapperHolder() { }
}
