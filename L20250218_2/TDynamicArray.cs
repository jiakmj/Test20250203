using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace L20250218_2
{
    class TDynamicArray<T>
    {
        public TDynamicArray() { }
        ~TDynamicArray() { }

        protected T[] objects = new T[0];

        protected int count = 0;
        public int Count
        {
            get
            {
                return count;
            }
        }

        public void Add(T inObject)
        {
            if(count >= objects.Length)
            {
                ExtendSpace();
            }
            objects[count] = inObject;
            count++;
        }

        protected void ExtendSpace()
        {
            T[] newObject = new T[objects.Length * 2];

            for(int i = 0; i < objects.Length; ++i)
            {
                newObject[i] = objects[i];
            }
            objects = null;
            objects = newObject;
        }

        public bool Remove(T removeObj)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (removeObj.Equals(objects[i]))
                {
                    return RemoveAt(i);
                }
            }
            return false;
        }

        public bool RemoveAt(int index)
        {
            if(index >= 0 && index < Count)
            {
                for (int i = index; i < Count - 1; ++i)
                {
                    objects[i] = objects[i + 1];
                }
                count--;
                return true;
            }
            return false;
        }

        public void Insert(int insertIndex, T value)
        {
            if(objects.Length == count)
            {
                ExtendSpace();
            }
            for(int i = count; i > insertIndex; --i)
            {
                objects[i] = objects[i - 1];
            }
            objects[insertIndex + 1] = value;
            count++;
        }

        public T this[int index]
        {
            get
            {
                return objects[index];
            }
            set
            {
                if (index < objects.Length)
                {
                    objects[index] = value;
                }
            }
        }
    }
}
