using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    //좌표값을 가지는 모든 것들에게 게임 오브젝트를 상속받게 한다.
    //플레이어, 몬스터, 목표, 벽, 바닥 등
    public class GameObject
    {
        public int X;
        public int Y;
        public char Shape;        

        public virtual void Update()
        {

        }

        public virtual void Render()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Shape);          
        }

    }
}
