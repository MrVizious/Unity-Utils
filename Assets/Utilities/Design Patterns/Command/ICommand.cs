using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace DesignPatterns
{
    public interface ICommand
    {
        UniTask Execute();
        UniTask Undo();
    }
}
