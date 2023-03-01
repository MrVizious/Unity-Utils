using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

[CreateAssetMenu(fileName = "Singleton Reference", menuName = "Design Patterns/Singleton/Example Singleton Reference SO", order = 1)]
public class ExampleSingletonReferenceSO : SingletonReferenceSO<ExampleSingleton>
{
    public override ExampleSingleton Instance
    {
        get
        {
            return ExampleSingleton.Instance;
        }
    }
}
