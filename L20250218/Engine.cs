using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Engine
    {
        private Engine() { }            
        static protected Engine instance;
        static public Engine Instance     
        {
            get
            {
                if (instance == null)

                {
                    instance = new Engine();
                }
                return instance;
            }
        }
        protected bool isRunning = true;
        public World world;
        public string[] scene;        

        public void Load()
        {
            scene = new string[]
            {
                "**********",
                "*P       *",
                "*        *",
                "*        *",
                "*        *",
                "*     M  *",
                "*        *",
                "*        *",
                "*       G*",
                "**********",
            };

            world = new World();

            for (int y = 0; y < scene.Length; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        Wall wall = new Wall(x, y, scene[y][x]);
                        world.Instanciate(wall);
                    }
                    else if (scene[y][x] == ' ')
                    {
                        Floor floor = new Floor(x, y, scene[y][x]);
                        world.Instanciate(floor);
                    }
                    else if (scene[y][x] == 'P')
                    {
                        Player player = new Player(x, y, scene[y][x]);
                        world.Instanciate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        Monster monster = new Monster(x, y, scene[y][x]);
                        world.Instanciate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        Goal goal = new Goal(x, y, scene[y][x]);
                        world.Instanciate(goal);
                    }
                }
            }
        }
               

        public void ProcessInput()
        {
            Input.Process();
        }

        public void Update()
        {
            world.Update();
        }

        protected void Render()
        {
            Console.Clear();
            world.Render();
        }

        //Player player;
        //Goal goal;
        //public void GoalIn()
        //{
        //    while (!isRunning)
        //    {
        //        if (player.X == goal.X && player.Y == goal.Y)
        //        {
        //            Console.Clear();
        //            Console.WriteLine("스테이지 클리어!");

        //        }

        //    }         

        //}


        public void Run()
        {
            while (isRunning)
            {
                ProcessInput();
                Update();
                Render(); 
            }            

        }                
    }
}
