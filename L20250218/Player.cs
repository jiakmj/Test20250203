using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Player : GameObject
    {
        public Player(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }
                

        public override void Update()
        {
            //키보드 wasd와 방향키 둘다 작동되게 하기 위해 or 연산자 || 사용
            int temp = 0;
            if (Input.GetKeyDown(ConsoleKey.W) || Input.GetKeyDown(ConsoleKey.UpArrow))
            {
                temp = Y - 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    Y--;
                }                

            }
            if (Input.GetKeyDown(ConsoleKey.S) || Input.GetKeyDown(ConsoleKey.DownArrow))
            {
                temp = Y + 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    Y++;
                }
                
            }
            if (Input.GetKeyDown(ConsoleKey.A) || Input.GetKeyDown(ConsoleKey.LeftArrow))
            {
                temp = X - 1;
                if (Engine.Instance.scene[Y][temp] != '*')
                {
                    X--;
                }
                
            }
            if (Input.GetKeyDown(ConsoleKey.D) || Input.GetKeyDown(ConsoleKey.RightArrow))
            {
                temp = X + 1;
                if (Engine.Instance.scene[Y][temp] != '*')
                {
                    X++;
                }
                
            }           

        }




    }
}
