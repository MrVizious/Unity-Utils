using System.Collections;
using DesignPatterns;
using UtilityMethods;

public class PoolableExample : Poolable<PoolableExample>
{
    public override void OnPoolGet()
    {
        gameObject.SetActive(true);
        // This is just so the Unity Console doesn't produce a warning
#pragma warning disable 4014
        UniTaskMethods.DelayedFunction(() => { Release(); }, 2);
#pragma warning disable 4014
    }

    public override void OnPoolRelease()
    {
        gameObject.SetActive(false);
    }
}