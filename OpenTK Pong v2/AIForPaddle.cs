
using OpenTK.Mathematics;
using System.Diagnostics;

namespace OpenTK_Pong_v2
{
    internal class AIForPaddle
    {
        public Vector3 PredictedPosOfBall;
        public int Iterations;

        public Vector3 PreviousPredict;

        public int AILatency;

        public float MyStep = 0;

        public float StepValue = 0.000004f;

        Stopwatch Stopwatch;

       public bool AwaitingImpact = false;

       public bool CanIdle = true;

        bool MovingUp;

        public AIForPaddle()
        {
            Stopwatch = new Stopwatch();
            Random random = new Random();
            if (random.NextDouble() > .5)
            {
                MovingUp = true;
            } else
                MovingUp = false;
        }


        public void PredictPos(Ball ball)
        {
            CanIdle = false;
            Ball CopyOfBall = (Ball)ball.Clone();

            

            int iterations = 0;

            while (CopyOfBall.myPos.X <= 0.925)
            {
                iterations++;
                CopyOfBall.NoRenderRender();
            }

            if (CopyOfBall.myPos.Y > 0.88f) //this is for better visuals and because sometimes in prediction the ball doesnt bounce and it will get detected at Y>gameFieldSize
            {
                CopyOfBall.myPos.Y = 0.88f;
            }
            if (CopyOfBall.myPos.Y < -0.88f)
            {
                CopyOfBall.myPos.Y = -0.88f;
            }
            

            Iterations = iterations;
            PreviousPredict = PredictedPosOfBall;
            PredictedPosOfBall = CopyOfBall.myPos;
            TravelToPrediction();
            
            // MyStep = PredictedPosOfBall.Y;
        }

        public void Initialize()
        {
            switch (Settings.AIDif) //scraped
            {
                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    break;

            }

             while(true)
             while (CanIdle)
             {   
                     Idle();
             }

        }
        bool up = true;
        public void Idle()
        {
            Random rnd = new Random();
            Stopwatch.StartNew();

            if (rnd.Next(900000) == 1&&Stopwatch.ElapsedMilliseconds>50)
                {
                    up = !up;
                Stopwatch.StartNew();
                }
            


            if (up && MyStep < 0.88f)
            {
                MyStep += 0.00000004f;
            }
            else if (MyStep > -.88f)
            {
                MyStep -= 0.00000004f;
            }
        }

        public void TravelToPrediction()
        {
            Stopwatch.Start();
            if (PredictedPosOfBall.Y > MyStep)
            {
                while (MyStep < PredictedPosOfBall.Y)
                {
                    if (Stopwatch.ElapsedMilliseconds > 5)
                    {
                        MyStep += StepValue / 200;
                        Stopwatch.StartNew();
                    }



                }
                AwaitingImpact = true;
            }
            else
            {
                while (MyStep > PredictedPosOfBall.Y)
                {
                    if (Stopwatch.ElapsedMilliseconds > 5)
                    {
                        MyStep -= StepValue / 200;
                        Stopwatch.StartNew();
                    }
                }
                AwaitingImpact = true;
            }
            Stopwatch.Stop();
            //ImpactAwaiting();
          
        }

        public void ImpactAwaiting() //not implemented
        {
            Random rnd = new Random();
            
            int taunt = rnd.Next(1);
            Console.WriteLine("im");
            while (AwaitingImpact)
            {
                if (!AwaitingImpact)
                    break;

               

                

                switch (taunt)
                {
                    case 0:
                        if(MovingUp)
                        {
                            MyStep += StepValue;
                            
                        }else
                            MyStep -= StepValue;
                        if (Stopwatch.ElapsedMilliseconds > 15)
                        {
                            MovingUp = !MovingUp;
                            Stopwatch.StartNew();
                        }

                        break;
                }
            }
        }
    }
}
