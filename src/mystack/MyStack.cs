using System;
using System.Collections.Generic;

namespace Practical_8_1
{

    public class MyStack<T>
    {
        private T[] array;
        public int Count { get; private set; }

        public MyStack(int capacity)
        {
            array = new T[capacity];
            this.Count = 0;
        }

        public void Push(T val)
        {
            if (Count < array.Length) array[Count++] = val;
            else throw new InvalidOperationException("The stack is out of capacity.");
        }

        public T Pop()
        {
            if (Count > 0) return array[--Count];
            else throw new InvalidOperationException("The stack is empty.");
        }

        // The search starts at the top of the stack.
        public T Find(Func<T, bool> criteria)
        {
            if (criteria == null) throw new ArgumentNullException(nameof(criteria));
            for (int i = Count - 1; i >= 0; i--)
            {
                if (criteria(array[i])) return array[i];
            }
            return default(T);
        }

        // Returns elements in an array, or null if no elements match.
        public T[] FindAll(Func<T, bool> criteria)
        {
            if (criteria == null) throw new ArgumentNullException(nameof(criteria));
            List<T> resultList = new List<T>();
            for (int i = Count - 1; i >= 0; i--)
            {
                if (criteria(array[i]))
                {
                    resultList.Add(array[i]);
                }
            }
            return resultList.Count > 0 ? resultList.ToArray() : null;
        }

        // Returns the number of removed elements.
        public int RemoveAll(Func<T, bool> criteria)
        {
            if (criteria == null) throw new ArgumentNullException(nameof(criteria));
            
            int originalCount = Count;
            int writeIndex = 0;
            
            for (int i = 0; i < Count; i++)
            {
                if (!criteria(array[i]))
                {
                    array[writeIndex++] = array[i];
                }
            }
            
            int removed = Count - writeIndex;
            Count = writeIndex;
            
            for(int i = Count; i < originalCount; i++)
            {
                array[i] = default(T);
            }

            return removed;
        }

        // Returns the element with the maximum value stored in the stack.
        public T Max()
        {
            if (Count == 0) return default(T);
            Comparer<T> comparer = Comparer<T>.Default;
            T max = array[0];
            for (int i = 1; i < Count; i++)
            {
                if (comparer.Compare(array[i], max) > 0)
                {
                    max = array[i];
                }
            }
            return max;
        }

        // Returns the element with the minimum value stored in the stack.
        public T Min()
        {
            if (Count == 0) return default(T);
            Comparer<T> comparer = Comparer<T>.Default;
            T min = array[0];
            for (int i = 1; i < Count; i++)
            {
                if (comparer.Compare(array[i], min) < 0)
                {
                    min = array[i];
                }
            }
            return min;
        }
    }

}