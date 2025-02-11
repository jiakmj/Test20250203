using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;


namespace L20250211
{
    public class Player
    {
        public int hp;
        public int gold;

        public Player()
        {
            hp = 100;
            gold = 10;
            Console.WriteLine("플레이어 생성자"); //초기화

        }
        public Player(int hp, int gold)
        {
            this.hp = hp;
            this.gold = gold;
        }

        ~Player()
        {
            //Network, DB 종료
            Console.WriteLine("플레이어 소멸자"); //삭제
            //생성자, 소멸자를 쓰는 이유는 시점을 명확하게 하기 위해
        }
        public void Attack()
        {
            Console.WriteLine("플레이어가 공격합니다.");
        }

        public void Move()
        {
            Console.WriteLine("플레이어가 이동합니다.");
        }

        public void PickUp()
        {

        }

        public void Dead()
        {

        }

    }

    public class Monster
    {
        public Monster()
        {
            Console.WriteLine("몬스터 생성자");
        }
        ~Monster()
        {
            Console.WriteLine("몬스터 소멸자");
        }

        protected int hp;
        //public int Hp;
        //{
        //    get
        //    {
        //        return hp;
        //    }
        //    set
        //    {
        //        hp = value;
        //    }
        //}
        public int Hp //=> { return hp }
        {
            get;
            set;
        }
        protected int gold;              


        public void Attack()
        {

        }

        //virtual function table 재정의 할거만 쓰기 남발하면 효율 떨어짐
        public virtual void Move()
        {
            Console.WriteLine("몬스터가 이동한다.");
        }
        public void Dead()
        {

        }
    }

    public class Goblin : Monster
    {
        //고블린 공격 가능 걸어서 이동
        public Goblin()
        {
            Console.WriteLine("고블린 생성자");
        }
        ~Goblin()
        {
            Console.WriteLine("고블린 소멸자");
        }

        public override void Move()
        {
            Console.WriteLine("고블린이 걷는다.");
        }
    }


    public class Slime : Monster
    {
        //슬라임 공격 가능 미끄러져 이동
        public Slime()
        {

        }
        ~Slime()
        {

        }

        public override void Move()
        {
            Console.WriteLine("슬라임이 미끄러진다.");
        }
    }

    class Boar : Monster
    {
        //멧돼지 공격 가능 뛰어서 이동
        public Boar()
        {

        }
        ~Boar()
        {

        }
        public override void Move()
        {
            Console.WriteLine("멧돼지가 뛴다.");
        }

    }


    public class Program
    {
        //static void Sample()
        //{
        //    Monster goblin = new Goblin(); //생성할 때 생성자 한 번 호출
            
        //}

        static void Main(string[] args)
        {

            Monster monster = new Goblin();
            int currentHP = monster.Hp;
            monster.Hp = currentHP;
            

            //Monster[] monsters = new Monster[2];
            //monsters[0] = new Goblin();
            //monsters[1] = new Slime();

            //Console.WriteLine(typeof(Goblin).Name);
         
            ////다형성, virtual, override
            //monsters[0].Move();
            //monsters[1].Move();

            //GameObject[] gameObjects = new GameObject[2];
            //gameObjects[0] = new Cube();
            //gameObjects[1] = new Box();

            //Render All
            //for (int i = 0; i < gameObjects.Length; i++)
            //{
            //    gameObjects[0].Render();
            //    gameObjects[1].Render();
            //}
            //Sample();
            //GC.Collect(); //garbage collecting 보여줄려고 강제로 넣은거 프레임 떨어짐
            //Player player = new Player();

            //Random rand = new Random();

            //int GoblinCount = rand.Next(1, 4);           
            //Goblin[] goblins = new Goblin[goblinCount];
            //for (int i = 0; i < goblins.Length; i++)
            //{
            //    goblins[i] = new Goblin();
            //}          

            //int slimeCount = rand.Next(1, 5);
            //Slime[] slimes = new Slime[slimeCount];
            //for (int i = 0; i < slimes.Length; i++)
            //{
            //    slimes[i] = new Slime();
            //}

            //int boarCount = rand.Next(1, 3);
            //Boar[] boars = new Boar[boarCount];
            //for (int i = 0; i < boars.Length; i++)
            //{
            //    boars[i] = new Boar();
            //}


            //while(true)
            //{
            //    //Input();
            //    Console.ReadKey();
            //    Console.Clear();

            //    //Update();
            //player.Move();
            //for (int i = 0; i < goblins.Length; i++)
            //{
            //    goblins[i].Move();
            //}
            //for (int i = 0; i < slimes.Length; i++)
            //{
            //    slimes[i].Move();
            //}
            //for (int i = 0; i < boars.Length; i++)
            //{
            //    boars[i].Move();
            //}

            //    //Render();              


            //}

        }


    }

}

