using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



//[CreateAssetMenu(fileName = "WrapperSO", menuName = "Unity-Utils/WrapperSO", order = 0)]
public class WrapperSO<T> : SerializedScriptableObject where T : new()
{
    public T content = new T();
    public static implicit operator T(WrapperSO<T> wrapperSO) => wrapperSO.content;
}

/*
Example usage:

[CreateAssetMenu(fileName = "IntWrapperSO", menuName = "Unity-Utils/Wrappers/WrapperSO", order = 0)]
public class IntWrapperSO : WrapperSO<int> {}
*/