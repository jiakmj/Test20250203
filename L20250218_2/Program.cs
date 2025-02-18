using System.Security.AccessControl;

namespace L20250218_2
{
    class DynamicArray
    {
        public DynamicArray()
        {

        }

        ~DynamicArray()
        {

        }

        //objects
        //[1][2][3]
        // ^  ^  ^  ^
        //newObjects
        //[1][2][3][ ][ ][ ]
        //          ^
        //objects <- newObjects
        //[1][2][3][4][ ][ ]
        //          ^
        public void Add(Object inObject)
        {
            if (count >= objects.Length)
            {
                ExtendSpace();
            }
            objects[count] = inObject;
            count++;
        }

        protected void ExtendSpace()
        {
            //배열 늘리기
            //이전 정보 옮기기
            Object[] newObject = new Object[objects.Length * 2];
            //이전값 이동
            for (int i = 0; i < objects.Length; ++i)
            {
                newObject[i] = objects[i];
            }
            objects = null;
            objects = newObject;
        }

        //[][][][][]
        public bool Remove(Object removeObj)
        {
            for (int i = 0; i < Count; ++i)
            {
                if (removeObj == objects[i])
                {
                    return RemoveAt(i);
                }
            }

            return false;
        }

        //[][][][][][]
        public bool RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i > Count - 1; ++i)
                {
                    objects[i] = objects[i + 1];
                }

                count--;
                return true;
            }
            return false;
        }


        //[1][2][3]
        //Insert(2, 5)
        //[1][2][3][4]
        public void Insert(int insertIndex, Object value)
        {
            //3 == 3 + 1 -> [1][2][3] -> [1][2][3][][][]
            //1. object.length == count
            //확장
            //추가
            //3 == 2 + 1    [1][2][3][][][]
            //2. object.length < count
            //확장X
            //추가
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

        protected Object[] objects = new Object[10];

        protected int count = 0;

        public int Count
        {
            get 
            {
                return count;
            }
        }

        public Object this[int index]
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
    //Generic
    //형태가 동일해져서 버그 발생이 적어짐

    class Program
    {
        static void Main(string[] args)
        {
            char A = 'A';
            char B = 'B';
            string line = A + B + "*"; 


            DynamicArray a = new DynamicArray();
            for (int i = 0; i < 10; ++i)
            {
                a.Add(i);
            }
            a[1] = 11;
            a[9] = 29;

            a.RemoveAt(9);
            a.RemoveAt(1);
            a.RemoveAt(3);
            a.Insert(2, 11);
            a.Insert(4, "배고파");
            a.Add(new GameObject());


            for (int i = 0; i < a.Count; ++i)
            {
                Console.Write(a[i] + ", ");
            }
        }
    }
}
