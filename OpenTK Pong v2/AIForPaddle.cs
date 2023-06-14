using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Mathematics;

namespace OpenTK_Pong_v2
{
    internal class AIForPaddle 
    {
        public static Vector3 PredictPos(Ball ball)
        {
            Ball CopyOfBall = (Ball)ball.Clone();
           

            Console.WriteLine(CopyOfBall.myPos);

            int Iterations = 0;

            while (CopyOfBall.myPos.X <= 0.92)
            {
                Iterations++;
                CopyOfBall.NoRenderRender();
            }

            if (CopyOfBall.myPos.Y > 0.88f)
            {
                CopyOfBall.myPos.Y = 0.88f;
            }
            if (CopyOfBall.myPos.Y < -0.88f)
            {
                CopyOfBall.myPos.Y = -0.88f;
            }

            Console.WriteLine(Iterations);
            Console.WriteLine(CopyOfBall.myPos);
            return CopyOfBall.myPos;
        }


    }
}
