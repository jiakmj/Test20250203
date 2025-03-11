using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace L20250225
{
    /* 동적 배열 클래스를 만들어 보세요.
     * Add로 배열에 자료를 추가하고
     * RemoveAt으로 배열 자료를 삭제 합니다.
     * 그리고 자료 갯수는 Count로 보여줍니다.
     * 자료의 접근과 입력은 [] 연산자로 되게 만들어 주세요 
     * ex) list<a> Objects = new list<a>;
     * d[1] = 1; */
    class DynamicArray<T> : IEnumerable, IEnumerable<T>
    {

        protected T[] data;
        protected int count;        


        public DynamicArray() //생성자 생성하기 강사님의 추천 방법
        {
            data = new T[10];
            count = 0;
        }

        ~DynamicArray() { }

        public void Add(T newData)
        {
            if (count >= data.Length)
            {
                T[] newArray = new T[data.Length * 2];
                Array.Copy(data, newArray, data.Length);
                data = newArray;
            }           
            data[count] = newData;  
            count++;
        }               
         
        public void RemoveAt(int index)
        {
            for (int i = index + 1; i < data.Length; ++i)
                {
                    data[i - 1] = data[i];
                }
                count--;            
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < count; ++i)
            {
                yield return data[i];
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < count; ++i)
            {
                yield return data[i];
            }
        }

        public T this[int index]
        {
            get
            {
                return data[index];
            }
            set
            {
                data[index] = value;              
            }
        }
        
        public int Count
        {
            get 
            { 
                return count;
            }
        }
    }

    //public abstract class Animal
    //{
    //    //pure virtual function
    //    public abstract void Eat(); //추상이라 구현을 안 하고 넘길 수 있음
    //    public int legs;
    
    //} // -> 다중 상속, C# 다중 상속 안돼

    //public interface 네발달린짐승
    //{
    //    void Run();
    //}

    //public interface 새
    //{
    //    void Fly();
    //}

    //class Lion : Animal, 네발달린짐승
    //{
    //    public override void Eat()
    //    {

    //    }

    //    public void Run()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
   
    //class Tiger : Animal
    //{
    //    public override void Eat()
    //    {

    //    }
    //}

    //class Chicken : Animal, 새
    //{
    //    public override void Eat()
    //    {
            
    //    }

    //    public void Fly()
    //    {
    //        string message = "조금 난다.";
    //    }
    //}


    ////다중 상속, C++만 있음, Diamond, interface X
    //class Liger : Lion, Tiger 
    //{

    //}

    //C#, java

    //혼자 만들면 안 씀 -> 다 같이 만든다. -> 다른 놈을 믿을 수 없다.

    public interface IItem
    {
        void Use();
    }

    public interface IEatable
    {
        void Use();
    }

    public class Posion : IItem, IEatable
    {
        public void Use()
        {
            Console.WriteLine("포션마시기");
        }
    }

    public class Sword : IItem
    {
        public void Use()
        {
            Console.WriteLine("검 착용하기");
        }
    }


    internal class Program
    {
        static int[] data = { 1, 2, 3, 4, 5 };
        static IEnumerable GetNumbers()
        {
            for (int i = 0; i < data.Length; ++i)
            {
                yield return data[i];
            }
        }
        static void Main(string[] args)
        {
            foreach (var i in GetNumbers())
            {
                Console.WriteLine(i);
            }
            return;

            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.RemoveAt(5);
        }

    }

            //for (int i = 0; i < list.Count; ++i)
            //{
            //    Console.WriteLine(list[i]);
            //}

            //ranege for
            //foreach (int value in list)
            //{
            //    Console.WriteLine(value);
            //}

            //DynamicArray<int> d = new DynamicArray<int>();
            //d.Add(1);
            //d.Add(2);
            //d.Add(3);
            //d.Add(4);
            //d.Add(5);
            //d.Add(6);
            //d.Add(7);
            //d.Add(8);
            //d.Add(9);
            //d.Add(10);

            //d.RemoveAt(10);

            //foreach (int value in d)
            //{
            //    Console.WriteLine(value);
            //}

}

    //    class Component

    //    {
    //        public virtual void OnTriggerEnter() { }
    //        public virtual void OnTriggerExit() { }
    //    }


    //    //함수 강제 구현, 다중 상속
    //    static void Main2(string[] args)
    //    {            
    //        Object posion = new Posion();
    //        Type type = posion.GetType();
    //        if (typeof(Posion) == type.GetInterface("IITEM"))
    //        {
    //            (posion as Posion).Use();
    //        }

    //        //Animal a = new Animal(); //추상적이라 만들 수 없음, 상속을 위해 만든 것임
    //        List<IItem> items = new List<IItem>();
    //        items.Add(new Posion());
    //        items.Add(new Sword());
    //        foreach(var item in items)
    //        {
    //            item.Use();
    //        }    
    //    }
    //

