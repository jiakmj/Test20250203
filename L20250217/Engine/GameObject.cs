using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace L20250217
{
    public class GameObject 
    {
        public List<Component> components = new List<Component>();
             
        public bool isTrigger = false;
        public bool isCollide = false;

        public string Name;

        protected static int gameObjectCount = 0;

        public Transform transform;

        public GameObject()
        {      
            Init();
            gameObjectCount++;
            Name = $"GameObject({gameObjectCount})";
        }
        ~GameObject()
        {
            gameObjectCount--;
        }
        public void Init()
        {
            transform = new Transform();
            AddComponent<Transform>();
        }
        public T AddComponent<T>(T inComponent) where T : Component
        {
            components.Add(inComponent);
            inComponent.gameObject = this;   
            inComponent.transform = transform;

            return inComponent;
        }
        public T AddComponent<T>() where T : Component, new()
        {
            T inComponent = new T();
            AddComponent<T>(inComponent);

            return inComponent;
        }

        public virtual void Update()
        {
            //모든 컴포넌트의 update 함수 실행해줘.
        }
        
        public bool PredictCollision(int newX, int newY)
        {            
            return false;
        }
        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component is T)
                {
                    return component as T;
                }
            }
            return null;
        }

        public void ExecuteMethod(string methodName, Object[] parameters)
        {
            foreach (var component in components)
            {
                Type type = component.GetType();
                MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var methodInfo in methodInfos)
                {
                    if (methodInfo.Name.CompareTo(methodName) == 0)
                    {
                        methodInfo.Invoke(component, parameters);
                    }
                }
            }
        }
        public static GameObject Find(string gameObjectName)
        {
            foreach(var choiceObject in Engine.Instance.world.GetAllGameObjects)
            {
                if (choiceObject.Name.CompareTo(gameObjectName) == 0)
                {
                    return choiceObject;
                }
            }

            return null;
        }
    }
}

