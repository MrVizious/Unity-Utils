using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public class FastQueue<T> : ICollection<T>, IReadOnlyCollection<T>, ICollection,
    IEnumerable<T>, IEnumerable, ISerializable, IDeserializationCallback
{
    private readonly LinkedList<T> list = new();
    private readonly Dictionary<T, LinkedListNode<T>> lookup = new();
    private readonly IEqualityComparer<T> comparer;

    public FastQueue() : this(null) { }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public FastQueue(IEqualityComparer<T>? comparer)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        this.comparer = comparer ?? EqualityComparer<T>.Default;
    }

    #region Core Operations

    public void Enqueue(T item)
    {
        if (lookup.ContainsKey(item))
            throw new InvalidOperationException("Item already exists in queue.");

        var node = list.AddLast(item);
        lookup[item] = node;
    }

    public T Dequeue()
    {
        if (list.Count == 0) throw new InvalidOperationException("Queue is empty");

        var node = list.First!;
        list.RemoveFirst();
        lookup.Remove(node.Value);
        return node.Value;
    }

    public bool Remove(T item)
    {
        if (!lookup.TryGetValue(item, out var node)) return false;

        list.Remove(node);
        lookup.Remove(item);
        return true;
    }

    public T Peek()
    {
        if (list.First == null)
            throw new InvalidOperationException("Queue is empty");
        return list.First.Value;
    }


    public bool TryDequeue(out T item)
    {
        if (list.Count == 0)
        {
            item = default!;
            return false;
        }

        item = list.First!.Value;
        list.RemoveFirst();
        lookup.Remove(item);
        return true;
    }

    public bool TryPeek(out T item)
    {
        if (list.Count == 0)
        {
            item = default!;
            return false;
        }

        item = list.First!.Value;
        return true;
    }

    public void Clear()
    {
        list.Clear();
        lookup.Clear();
    }

    public bool Contains(T item) => lookup.ContainsKey(item);

    public T[] ToArray()
    {
        var array = new T[list.Count];
        list.CopyTo(array, 0);
        return array;
    }


    public IEnumerable<T> AsEnumerable() => list;

    public void EnqueueRange(IEnumerable<T> items)
    {
        foreach (var item in items)
            Enqueue(item);
    }

    public int RemoveWhere(Func<T, bool> predicate)
    {
        int count = 0;
        var node = list.First;

        while (node != null)
        {
            var next = node.Next;
            if (predicate(node.Value))
            {
                list.Remove(node);
                lookup.Remove(node.Value);
                count++;
            }
            node = next;
        }

        return count;
    }

    #endregion

    #region ICollection<T>

    public int Count => list.Count;

    public bool IsReadOnly => false;

    public void Add(T item) => Enqueue(item);


    public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

    #endregion

    #region IEnumerable<T> & IEnumerable

    public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    #region IReadOnlyCollection<T>

    int IReadOnlyCollection<T>.Count => Count;

    #endregion

    #region ICollection (non-generic)

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => this;

    void ICollection.CopyTo(Array array, int index)
    {
        if (array is T[] typedArray)
        {
            CopyTo(typedArray, index);
        }
        else
        {
            throw new ArgumentException("Invalid array type", nameof(array));
        }
    }

    #endregion

    #region ISerializable & IDeserializationCallback

    protected FastQueue(SerializationInfo info, StreamingContext context)
    {
        var items = (T[])info.GetValue("Items", typeof(T[]))!;
        comparer = (IEqualityComparer<T>)info.GetValue("Comparer", typeof(IEqualityComparer<T>))!;
        foreach (var item in items)
        {
            Enqueue(item);
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Items", ToArray());
        info.AddValue("Comparer", comparer);
    }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public void OnDeserialization(object? sender)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        // No-op required for IDeserializationCallback
    }

    #endregion
}
