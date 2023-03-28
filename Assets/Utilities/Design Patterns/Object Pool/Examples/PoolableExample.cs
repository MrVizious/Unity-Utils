using System.Collections;
using DesignPatterns;
using UtilityMethods;
using Cysharp.Threading.Tasks;

public class PoolableExample : Poolable<PoolableExample>
{
    public override void OnPoolGet()
    {
        gameObject.SetActive(true);
        // This is just so the Unity Console doesn't produce a warning
        UniTaskMethods.DelayedFunction(() => { Release(); }, 2).Forget();
    }

    public override void OnPoolRelease()
    {
        gameObject.SetActive(false);
    }
}