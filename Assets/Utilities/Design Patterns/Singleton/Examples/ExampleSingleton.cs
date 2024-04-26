using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DesignPatterns
{
    public sealed class ExampleSingleton : Singleton<ExampleSingleton>
    {
        public void SayHello()
        {
            print("Hello");
        }
    }
}