using System.Reflection;
using System.Text;

namespace L20250217
{      
    public class Program
    {    
        public static int Compare(GameObject first, GameObject second)
        {
            SpriteRenderer spriteRenderer = first.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = second.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null || spriteRenderer2 == null)
            {
                return 0;
            }
                return spriteRenderer.orderLayer - spriteRenderer2.orderLayer;
        }

        public static void Main(string[] args)
        {
            Engine.Instance.Init();
            Engine.Instance.SetSortCompare(Compare);

            Engine.Instance.Load("level01.map");
            Engine.Instance.Run();

            Engine.Instance.Quit();

            //engine.Stop();            
        }
    }
}