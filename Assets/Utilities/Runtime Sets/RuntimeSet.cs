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

        public virtual void Add(T thing)
        {
            if (!Items.Contains(thing))
            {
                Items.Add(thing);
                onItemAdded.Invoke(thing);
            }
        }

        public virtual void Add(IEnumerable<T> things)
        {
            foreach (T thing in things)
            {
                Add(thing);
            }
        }

        public virtual void Remove(T thing)
        {
            if (Items.Contains(thing))
            {
                Items.Remove(thing);
                onItemRemoved.Invoke(thing);
            }
        }

        public virtual void Remove(IEnumerable<T> things)
        {
            foreach (T thing in things)
            {
                Remove(thing);
            }
        }

        public virtual T RemoveAt(int index)
        {
            T thing = GetAt(index);
            Remove(thing);
            return thing;
        }
        public virtual T RemoveRandom()
        {
            T thing = GetRandom();
            Remove(thing);
            return thing;
        }

        public virtual T GetAt(int index)
        {
            return Items[index];
        }

        public virtual T GetRandom()
        {
            return GetRandom(out int dummy);
        }
        public virtual T GetRandom(out int index)
        {
            index = Random.Range(0, Items.Count);
            return GetAt(index);
        }


    }
}