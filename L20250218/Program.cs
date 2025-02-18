namespace L20250218
{
    internal class Program
    {
        class Singleton
        {
            private Singleton()
            {

            }

            static Singleton instance;
            static public Singleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
        }

        static void Main(string[] args)
        {
            Engine.Instance.Load(); //World가 아닌 Engine에서 Load를 만들었던 이유
            Engine.Instance.Run();           
        }
    }
}
