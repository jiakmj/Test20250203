using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Wall : GameObject
    {
        public Wall(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }


        //왜 Wall의 Render을 지워야 벽이 생기는 걸까?
        //Floor를 지우고 Wall의 Render을 냅둬도 벽이 사라진다.
        //젤 상단에 있는 for문의 조건이 y를 가지고 있기 때문인가?

        //public override void Render()
        //{

        //}

    }
}
