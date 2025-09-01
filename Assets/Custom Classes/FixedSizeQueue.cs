using System.Collections.Generic;

public class FixedSizedQueue<T> : Queue<T>
{
    public int Size { get; private set; }

    public FixedSizedQueue(int size)
    {
        Size = size;
    }

    public new void Enqueue(T obj)
    {
        base.Enqueue(obj);
        while (Count > Size)
        {
            T outObj;
            TryDequeue(out outObj);
        }
    }
}