using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class Time
    {
        public static float deltaTime
        {
            get
            {
                return (float)deltaTimeSpan.TotalMilliseconds;
            }
        }

        public static TimeSpan deltaTimeSpan;

        public static DateTime currentTime;
        public static DateTime lastTime;

        public static void Update()
        {
            currentTime = DateTime.Now;
            deltaTimeSpan = currentTime - lastTime;
            lastTime = currentTime;
        }


    }
}
