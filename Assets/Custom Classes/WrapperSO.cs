using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;



//[CreateAssetMenu(fileName = "WrapperSO", menuName = "Unity-Utils/WrapperClassSO", order = 0)]
public class WrapperClassSO<T> : SerializedScriptableObject where T : new()
{
    public T content = new T();
    public static implicit operator T(WrapperClassSO<T> wrapperSO) => wrapperSO.content;
}
/*
Example usage:

[CreateAssetMenu(fileName = "MyClassWrapperSO", menuName = "Unity-Utils/Wrappers/WrapperClassSO", order = 0)]
public class MyClassWrapperSO : WrapperClassSO<int> {}
*/



//[CreateAssetMenu(fileName = "WrapperSO", menuName = "Unity-Utils/WrapperStructSO", order = 0)]
public class WrapperStructSO<T> : SerializedScriptableObject where T : struct
{
    public T content;
    public static implicit operator T(WrapperStructSO<T> wrapperSO) => wrapperSO.content;
}

/*
Example usage:

[CreateAssetMenu(fileName = "MyStructWrapperSO", menuName = "Unity-Utils/Wrappers/WrapperStructSO", order = 0)]
public class MyStructWrapperSO : WrapperStructSO<int> {}
*/