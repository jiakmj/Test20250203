using System;
using System.Numerics;
using System.Threading;
using Newtonsoft.Json;
using SDL2;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace L20250217
{
    public class Engine
    {
        private Engine() 
        {

        } 

        static protected Engine instance;

        //더블 버퍼링
        static public char[,] backBuffer = new char[20, 40];
        static public char[,] frontBuffer = new char[20, 40];

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

        public IntPtr myWindow;
        public IntPtr myRenderer;
        public SDL.SDL_Event myEvent;

        public World world;

        public IntPtr Font;

        public bool Init()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
            {
                Console.WriteLine("Fail Init");
                return false;
            }
           
            myWindow = SDL.SDL_CreateWindow(
                "Game",
                 100, 100,
                 640, 480,
                 SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            
            myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
              SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);

            SDL_ttf.TTF_Init();
            //Font = SDL_ttf.TTF_OpenFont(projectFolder + " / data / ", 30);
            Font = SDL_ttf.TTF_OpenFont("c:/Windows/Fonts/gulim.ttc", 30);

            world = new World();

            return true;
        }

        public bool Quit()
        {
            isRunning = false;

            SDL_ttf.TTF_Quit();

            SDL.SDL_DestroyRenderer(myRenderer);
            SDL.SDL_DestroyWindow(myWindow);

            SDL.SDL_Quit();

            return true;
        }


        public void Load(string filename)
        {    
            List<string> scene = new List<string>();

            StreamReader sr = new StreamReader(filename);

            while (!sr.EndOfStream)
            {
                scene.Add(sr.ReadLine());
            }
            sr.Close();            

            for (int y = 0; y < scene.Count; y++)
            {
                for (int x = 0; x < scene[y].Length; x++)
                {
                    if (scene[y][x] == '*')
                    {
                        GameObject wall = new GameObject();
                        wall.Name = "Wall";
                        wall.transform.X = x;
                        wall.transform.Y = y;

                        SpriteRenderer spriteRenderer = wall.AddComponent<SpriteRenderer>(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("wall.bmp", false);
                        spriteRenderer.orderLayer = 2;

                        spriteRenderer.Shape = '*';

                        wall.AddComponent<BoxCollider2D>();

                        world.Instanciate(wall);
                    }
                    else if (scene[y][x] == ' ')
                    {
                       
                    }
                    else if (scene[y][x] == 'P')
                    {
                        GameObject player = new GameObject();
                        player.Name = "Player";
                        player.transform.X = x;
                        player.transform.Y = y;

                        player.AddComponent(new PlayerController());
                        SpriteRenderer spriteRenderer =player.AddComponent(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 0;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("player.bmp", true);
                        spriteRenderer.orderLayer = 3;
                        spriteRenderer.processTime = 150.0f;
                        spriteRenderer.maxCellCountX = 5;

                        spriteRenderer.Shape = 'P';

                        player.AddComponent<CharacterController2D>();                        

                        world.Instanciate(player);
                    }
                    else if (scene[y][x] == 'M')
                    {
                        GameObject monster = new GameObject();
                        monster.Name = "Monster";
                        monster.transform.X = x;
                        monster.transform.Y = y;                                              
                        
                        SpriteRenderer spriteRenderer = monster.AddComponent(new SpriteRenderer());
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("monster.bmp", false);
                        spriteRenderer.orderLayer = 4;

                        spriteRenderer.Shape = 'M';

                        monster.AddComponent<AIController>(new AIController());                         
                        CharacterController2D characterController2D = monster.AddComponent<CharacterController2D>();
                        characterController2D.isTrigger = true;

                        world.Instanciate(monster);
                    }
                    else if (scene[y][x] == 'G')
                    {
                        GameObject goal = new GameObject();
                        goal.Name = "Goal";
                        goal.transform.X = x;
                        goal.transform.Y = y;

                        SpriteRenderer spriteRenderer = goal.AddComponent<SpriteRenderer>(new SpriteRenderer());                        
                        spriteRenderer.colorKey.r = 255;
                        spriteRenderer.colorKey.g = 255;
                        spriteRenderer.colorKey.b = 255;
                        spriteRenderer.colorKey.a = 255;
                        spriteRenderer.LoadBmp("goal.bmp", false);

                        spriteRenderer.orderLayer = 2;

                        spriteRenderer.Shape = 'G';

                        goal.AddComponent<CharacterController2D>().isTrigger = true;                   

                        world.Instanciate(goal);
                    }
                    GameObject floor = new GameObject();
                    floor.Name = "Floor";
                    floor.transform.X = x;
                    floor.transform.Y = y;

                    SpriteRenderer spriteRenderer2 = floor.AddComponent<SpriteRenderer>(new SpriteRenderer());
                    spriteRenderer2.colorKey.r = 255;
                    spriteRenderer2.colorKey.g = 255;
                    spriteRenderer2.colorKey.b = 255;
                    spriteRenderer2.colorKey.a = 255;
                    spriteRenderer2.LoadBmp("floor.bmp", false);
                    spriteRenderer2.orderLayer = 1;

                    spriteRenderer2.Shape = ' ';
                    world.Instanciate(floor);
                }
                //심판 생성
                GameObject gameManager = new GameObject();
                gameManager.Name = "GameManager";

                gameManager.AddComponent<GameManager>();
                world.Instanciate(gameManager);
            }

            //loading complete
            //sort
            world.Sort();

            Awake();

            //json
            //string SceneFile = JsonConvert.SerializeObject(world.GetAllGameObjects, new JsonSerializerSettings 
            //{
            //    TypeNameHandling = TypeNameHandling.All,
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,                
            //});
            //Console.WriteLine(SceneFile);

            //StreamWriter sw = new StreamWriter("sample.uasset");
            //sw.WriteLine(SceneFile);
            //sw.Close();

            //StreamReader sr2 = new StreamReader("sample.uasset");
            //string SceneFile = sr2.ReadToEnd();
            //sr2.Close();

            //world.GetAllGameObjects = JsonConvert.DeserializeObject<List<GameObject>>(SceneFile, new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All,
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //});
        }       

        public void ProcessInput()
        {
            Input.Process();
        }


        protected void Update()
        {
            world.Update();
        }

        protected void Render()
        {
            SDL.SDL_SetRenderDrawColor(myRenderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(myRenderer);

            world.Render();          
                   
            for (int Y = 0; Y < 20; ++Y)
            {
                for (int X = 0; X < 40; ++X)
                {
                    if (frontBuffer[Y, X] != backBuffer[Y, X])
                    {
                        frontBuffer[Y, X] = backBuffer[Y, X];
                        Console.SetCursorPosition(X, Y);
                        Console.Write(frontBuffer[Y, X]);
                    }
                }
            }
            SDL.SDL_RenderPresent(myRenderer);
        }

        public void Run()
        {
            Console.CursorVisible = false; //커서 안보이게 하기
                      

            while (isRunning)
            {
                SDL.SDL_PollEvent(out myEvent);

                Time.Update();

                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;

                }
                Update();
                Render();
            }
        }

        public void Awake()
        {
            world.Awake();
        }

        public void SetSortCompare(World.SortCompare inSortCompare)
        {
            world.sortCompare = inSortCompare;
        }
    }
}
