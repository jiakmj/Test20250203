using System.Runtime.CompilerServices;

namespace L20250217_2
{
    class Monster
    {
        public virtual void Move()
        {
            Console.WriteLine("이동한다");
        }
    
    }

    class Slime : Monster
    {
        public override void Move()
        {
            Console.WriteLine("미끄러진다.");
        }

        public void Sticky()
        {
            Console.WriteLine("끈적인다.");
        }
    }

    class Goblin : Monster
    {
        public override void Move()
        {
            Console.WriteLine("뛰어다닌다.");
        }

        public void Throw()
        {
            Console.WriteLine("던진다");
        }

    }

    internal class Program
    {    
        static void Main(string[] args)
        {
            
            Monster[] monsters = new Monster[3];
            monsters[0] = new Slime();
            monsters[1] = new Goblin();
            monsters[2] = new Slime();

            //Down casting, 동적변환
            for (int i = 0; i < monsters.Length; i++)
            {
                Slime? s = monsters[i] as Slime;                
                if (s != null)
                {
                    s.Sticky();
                }

                Goblin? g = monsters[i] as Goblin;
                if (g != null)
                {
                    g.Throw();
                }
            }

            
        }
    }
}
