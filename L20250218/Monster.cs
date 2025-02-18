using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250218
{
    public class Monster : GameObject
    {
        //랜덤으로 움직이게 하기 위해 Random Next 사용
        private Random random = new Random();
        public Monster(int inX, int inY, char inShape)
        {
            X = inX;
            Y = inY;
            Shape = inShape;
        }

        public override void Update()
        {
            int Direction = random.Next(0, 4);
            int temp = 0;

            if (Direction == 0)
            {
                temp = Y - 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    Y--;
                }
            }
            if (Direction == 1)
            {
                temp = Y + 1;
                if (Engine.Instance.scene[X][temp] != '*')
                {
                    Y++;
                }
            }
            if (Direction == 2)
            {
                temp = X - 1;
                if (Engine.Instance.scene[Y][temp] != '*')
                {
                    X--;
                }
            }
            if (Direction == 3)
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
