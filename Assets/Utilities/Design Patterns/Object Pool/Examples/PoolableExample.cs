using System.Collections;
using DesignPatterns;
using UtilityMethods;

public class PoolableExample : Poolable<PoolableExample>
{
    public override void OnPoolGet()
    {
        gameObject.SetActive(true);
        UniTaskMethods.DelayedFunction(() => { Release(); }, 2);
    }

    public override void OnPoolRelease()
    {
        gameObject.SetActive(false);
    }
}