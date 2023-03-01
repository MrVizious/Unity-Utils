using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;


namespace RuntimeSet
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();
        public UltEvent<T> onItemAdded, onItemRemoved;

        public void Add(T thing)
        {
            if (!Items.Contains(thing))
            {
                Items.Add(thing);
                onItemAdded.Invoke(thing);
            }
        }

        public void Add(IEnumerable<T> things)
        {
            foreach (T thing in things)
            {
                Add(thing);
            }
        }

        public void Remove(T thing)
        {
            if (Items.Contains(thing))
            {
                Items.Remove(thing);
                onItemRemoved.Invoke(thing);
            }
        }

        public void Remove(IEnumerable<T> things)
        {
            foreach (T thing in things)
            {
                Remove(thing);
            }
        }

        public T RemoveAt(int index)
        {
            T thing = GetAt(index);
            Remove(thing);
            return thing;
        }
        public T RemoveRandom()
        {
            T thing = GetRandom();
            Remove(thing);
            return thing;
        }

        public T GetAt(int index)
        {
            return Items[index];
        }

        public T GetRandom()
        {
            return GetAt(Random.Range(0, Items.Count));
        }


    }
}